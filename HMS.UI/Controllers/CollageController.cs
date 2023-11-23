
////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
////////////////////////////////////////////////////
///

using HMS.Data;
using HMS.Data.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Interfaces.Repositories;
using HMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Business.Repositories;
using HMS.Infrastructure.Extensions;
using System.Threading;
using System;

namespace HMS.UI.Controllers
{
    [Authorize]
    
    public class CollageController : Controller
    {
        private readonly ICollageRepository _collageRepository;
        private readonly IPermissionRepository _IPermissionRepository;
        private readonly CancellationToken cancellationToken;

		public CollageController(ICollageRepository collageRepository,IPermissionRepository permissionRepository)
        {
            _collageRepository = collageRepository;
            _IPermissionRepository = permissionRepository;
		}
        public async Task<IActionResult> Index()
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "colleges-Read", cancellationToken))
			{
				var collages = await _collageRepository.GetAll();
            return View(collages);
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
            //var data = "" ; 

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
                        SortColumn = "time_Start";
                        break;
                    case 2:
                        SortColumn = "time_End";
                        break;
                    case 3:
                        SortColumn = "date";
                        break;
                    case 4:
                        SortColumn = "hall_name";
                        break;
                    default:
                        SortColumn = "user_Name";
                        break;
                }
                if (sortDirection == "asc")
                    SortOrder = "asc";
                else
                    SortOrder = "desc";

                var data2 = _collageRepository.GetAllpaging(start, searchvalue, Length, SortColumn, sortDirection);//.ToList();

                data = data2.colleges;
                recordsTotal = data2.totalPages;

                //recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
            }
            catch (Exception ex)
            {

            }
            return Json(new { data = data, recordsTotal = recordsTotal, recordsFiltered = recordsTotal });
        }
        public async Task<IActionResult> Create()
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "colleges-Read", cancellationToken))
			{
				return View();
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}
        public async Task<IActionResult> store(collage collage)
        {
            if (ModelState.IsValid)
            {
                BaseResponse res = await _collageRepository.Add(collage);
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
					return RedirectToAction("create", collage);

				}
			}
            else
            {
                return View("Create");
            }

        }
        public async Task<IActionResult> Details(int id)
        {
            var collage = await _collageRepository.GetById(id);
            return View(collage);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var collage = await _collageRepository.GetById(id);
            return View(collage);
        }
        [HttpPost]
        public async Task<IActionResult> update(collage collage)
        {
            if (ModelState.IsValid)
            {
                BaseResponse res = await _collageRepository.Update(collage);

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
					return Redirect("/collage/edit/" + collage.ID);

				}
            }

            TempData["message"] = "يوجد خطا ما غير متوقع الرجاء التواصل مع مسؤول النظام";
            return Redirect("/collage/edit/"+collage.ID);

		}
		public async Task<IActionResult> Delete(int id)
        {
            BaseResponse res = await _collageRepository.Delete(id);
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
    }
}
