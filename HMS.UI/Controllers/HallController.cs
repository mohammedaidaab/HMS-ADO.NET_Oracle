////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
/////////////////////////////////////////////////////
///

using HMS.Domain.Interfaces.Repositories;
using HMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities;
using System;
using HMS.Business.Repositories;
using HMS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using HMS.Domain.Entities.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using HMS.Infrastructure.Extensions;
using System.Threading;

namespace HMS.MVC.Controllers
{
    [Authorize]
    public class HallController : Controller
    {
        private readonly IHallrepository _hallRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IPermissionRepository _IPermissionRepository;
        private readonly CancellationToken cancellationToken;

        public HallController(IHallrepository hallRepository,
            IBuildingRepository buildingRepository,
            IPermissionRepository permissionRepository)
        {
            _hallRepository = hallRepository;
            _buildingRepository = buildingRepository;
			_IPermissionRepository = permissionRepository;
           
        }

        public async Task<IActionResult> Index()
        {
            if (await _IPermissionRepository.hasPermission(User.GetUserId(), "halls-Read", cancellationToken))
            {
                var HallBuildingVM = await _hallRepository.GetAll();
                return View(HallBuildingVM);
            }
            else
            {
                return RedirectToAction("AccessDenied", "account");
            }
        }
		public async Task<IActionResult> Index2()
		{
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "halls-Read", cancellationToken))
			{
				var HallBuildingVM = await _hallRepository.GetAll();
				return View("index2",HallBuildingVM);
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}
        [HttpPost]
        public JsonResult GetDetails()
        {
            object data = new object();

            var start = (Convert.ToInt32(Request.Form["start"]));
            var Length = (Convert.ToInt32(Request.Form["length"])) == 0 ? 10 : (Convert.ToInt32(Request.Form["length"]));
            var searchvalue = Request.Form["search[value]"].ToString() ?? "";
            var sortcoloumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
            var SortColumn = "";
            var SortOrder = "";
            var sortDirection = Request.Form["order[0][dir]"].ToString() ?? "asc";
            var recordsTotal = 0;
            try
            {
                switch (sortcoloumnIndex)
                {
                    case 0:
                        SortColumn = "name";
                        break;
                    case 1:
                        SortColumn = "building_Name";
                        break;
                    default:
                        SortColumn = "number";
                        break;
                }
                if (sortDirection == "asc")
                    SortOrder = "asc";
                else
                    SortOrder = "desc";

                var data2 = _hallRepository.GetAllpaging(start, searchvalue, Length, SortColumn, sortDirection);//.ToList();

                data = data2.HallBuildings;
                recordsTotal = data2.totalPages;

            }
            catch (Exception ex)
            {

            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal });
        }
        public async Task<IActionResult> CreateAsync()
		{

			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "halls-Create", cancellationToken))
            {
				ViewBag.Buildings = new SelectList(await _buildingRepository.GetAll(), nameof(BuildingCollegeVM.ID), nameof(BuildingCollegeVM.buldingName));

				return View();
            }
            else
            {
				return RedirectToAction("AccessDenied", "account");
			}

		}
        [HttpPost]
        public async Task<IActionResult> store(Hall hall)
        {

            if (ModelState.IsValid)
            {
                BaseResponse res = await _hallRepository.Add(hall);
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
                    
                    return RedirectToAction("create", hall);
                }
            }

            TempData["message"] = "يوجد خطا ما الرجاء التحقق من البانات المدخلة  وفي حال تواصل المشكلة الرجاء التواصل مع مسؤول لنظام";  
            return RedirectToAction("create", hall);

        }
        public async Task<IActionResult> Details(int Id)
        {
            var hall = await _hallRepository.GetById(Id);
            return View(hall);
        }
        public async Task<IActionResult> Edit(int id)
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "halls-Update", cancellationToken))
			{

				var hall = await _hallRepository.GetById(id);

                 ViewBag.Buildings = new SelectList(await _buildingRepository.GetAll(), nameof(BuildingCollegeVM.ID), nameof(BuildingCollegeVM.buldingName),nameof(hall.Building_ID));

                return View(hall);
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}
        [HttpPost]
        public async Task<IActionResult> Edit(Hall hall)
        {

            if (ModelState.IsValid)
            {
                BaseResponse res = await _hallRepository.Update(hall);
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

                    ViewBag.Buildings = new SelectList(await _buildingRepository.GetAll(), nameof(BuildingCollegeVM.ID), nameof(BuildingCollegeVM.buldingName), nameof(hall.Building_ID));

                    return RedirectToAction("edit", hall.ID);
                }
            }

            return RedirectToAction("Edit", hall);
        }
        public async Task<IActionResult> Delete(int id)
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "halls-Delete", cancellationToken))
			{
				BaseResponse res = await _hallRepository.Delete(id);
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
                    return RedirectToAction("Index");
                }
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}
    }
}
