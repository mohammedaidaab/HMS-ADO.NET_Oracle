////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
/////////////////////////////////////////////////////

using HMS.Data.Entities;
using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using HMS.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog.Core;
using System;
using System.Collections.Immutable;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.UI.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private IHallrepository _IHallRepository;
        private IReservationRepository _IReservationRepository;
        private ISiteUserRepository _ISiteUserRepository;
        private UserManager<SiteUser> _UserManager;
        private readonly IPermissionRepository _IPermissionRepository;
		private readonly CancellationToken cancellationToken;

		public ReservationController(IHallrepository iHallRepository, 
                                     IReservationRepository ireservationrepository,
                                     ISiteUserRepository siteUserRepository,
                                     UserManager<SiteUser> userManager,
                                     IPermissionRepository permissionRepository
                                     )
        {
            _IHallRepository = iHallRepository;
            _IReservationRepository = ireservationrepository;
            _ISiteUserRepository = siteUserRepository;
            _UserManager = userManager;
			_IPermissionRepository = permissionRepository;

		}


        public async Task<IActionResult> Index()
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "reservations-Read", cancellationToken))
			{
				    if (User.IsInRole("super_admin"))
                {
                    var reservations = await _IReservationRepository.GetAll();
                    return View(reservations);

                }
                else
                {
                    var userid = _UserManager.GetUserId(User);
                   
                    var reservations = await _IReservationRepository.GetByUserId(userid);
                    return View(reservations);

                }
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}


        public async Task<IActionResult> Create()
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "reservations-Create", cancellationToken))
			{
				ViewBag.HallList = new SelectList(await _IHallRepository.GetAll(), nameof(Hall.ID), nameof(Hall.Name));
                return View();
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}


        public async Task<IActionResult> store(Reservation reservation)
        {

            reservation.User_id = Convert.ToInt32(User.GetUserId());

            if (ModelState.IsValid)
            {
               BaseResponse res = await _IReservationRepository.create(reservation);
               
                if (res.IsSuccess == true) 
                {
                    TempData["message"]=res.Message;
                    TempData["type"] = res.Type;
                    return RedirectToAction("Index");
                }
                else
                {
					TempData["message"] = res.Message;
					TempData["type"] = res.Type;
					return RedirectToAction("create", reservation);

				}

			}
            return NotFound();   

		}


        public async Task<IActionResult> Edit(int id)
        {

			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "reservations-Update", cancellationToken))
			{
				ReservationHallVM reservation = await _IReservationRepository.GetById(id);
				ViewBag.HallList = new SelectList(await _IHallRepository.GetAll(), nameof(Hall.ID), nameof(Hall.Name),nameof(reservation.Hall_Id));
				return View(reservation);
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}



        public async Task<IActionResult> Update(Reservation reservation)
        {
            reservation.User_id = Convert.ToInt32(User.GetUserId());

			//var errors = ModelState.Values.SelectMany(v => v.Errors);


			if (ModelState.IsValid)
            {
				BaseResponse res = await _IReservationRepository.update(reservation);
				if (res.IsSuccess == true)
				{
					TempData["message"] = res.Message;
					TempData["type"] = res.Type;
					return RedirectToAction("Index");
				}
				else
				{
					TempData["message"] = res.Message;
					TempData["type"] = res.Type;
					return RedirectToAction("Edit", reservation);
				}
			}


			TempData["message"] = "حدثت مشكلة غير متوقعة الرجاء التواصل مع مشرف النظام  في جحال استمرار الاشكالية";
			return RedirectToAction("Edit", reservation);
		}



	}
}
