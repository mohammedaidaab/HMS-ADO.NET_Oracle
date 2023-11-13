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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

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


        // GET: reaservations
        public async Task<ViewResult> Index2(string sortOrder, string currentFilter, string searchString, int? page)
        
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User_Name_desc" ? "User_Name_asc" : "User_Name_desc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var res = await _IReservationRepository.GetAll();

            List<ReservationHallVM> re = res.ToList();

            var Reservations = from s in re
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {

                Reservations = Reservations.Where(s => s.Name.Contains(searchString));
                                       //|| s.FirstMidName.Contains(searchString)) ;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Reservations = Reservations.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    Reservations = Reservations.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    Reservations = Reservations.OrderByDescending(s => s.Date);
                    break;
                case "User_Name_desc":
                    Reservations = Reservations.OrderByDescending(s => s.User_Name);
                    break;
                case "User_Name_asc":
                    Reservations = Reservations.OrderBy(s => s.User_Name);
                    break;
                default:  // Name ascending 
                    Reservations = Reservations.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Reservations.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> test(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User_Name_desc" ? "User_Name_asc" : "User_Name_desc";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //var res = await _IReservationRepository.GetAll();
            var res = await _IReservationRepository.GetAllpaging();

            List<ReservationHallVM> re = res.ToList();

            var Reservations = from s in re
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {

                Reservations = Reservations.Where(s => s.Name.Contains(searchString));
                //|| s.FirstMidName.Contains(searchString)) ;
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Reservations = Reservations.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    Reservations = Reservations.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    Reservations = Reservations.OrderByDescending(s => s.Date);
                    break;
                case "User_Name_desc":
                    Reservations = Reservations.OrderByDescending(s => s.User_Name);
                    break;
                case "User_Name_asc":
                    Reservations = Reservations.OrderBy(s => s.User_Name);
                    break;
                default:  // Name ascending 
                    Reservations = Reservations.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Reservations.ToPagedList(pageNumber, pageSize));
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

			var errors = ModelState.Values.SelectMany(v => v.Errors);


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

		public async Task<IActionResult> Delete(int id)
		{
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "reservations-Delete", cancellationToken))
			{
				BaseResponse res = await _IReservationRepository.Delete(id);
				if (res.IsSuccess)
				{
					TempData["type"] = res.Type;
					TempData["message"] = res.Message;
					return RedirectToAction("Index");
				}
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
			return RedirectToAction("Index");
		}

	}
}
