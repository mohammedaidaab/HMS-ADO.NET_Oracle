////////////////////////////////////////////////////
    //Author : Mohammed Gaffer Aidaab 
    //For : King Faisual University 
    //Under : ISB integrated sulution business Company 
    //Halls ManageMent System 
/////////////////////////////////////////////////////
///

using HMS.Business.Repositories;
using HMS.Data;
using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using HMS.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.UI.Controllers
{
    //[Authorize]
    public class BuildingController : Controller
    {
        private readonly IBuildingRepository _IbulddingRepository;
        private readonly ICollageRepository _ICollageRepository;
        private readonly IPermissionRepository _IPermissionRepository;
		private readonly CancellationToken cancellationToken;


		public BuildingController(IBuildingRepository ibulddingRepository,
                                  ICollageRepository collageRepository,
                                  IPermissionRepository permissionRepository)
        {
            _IbulddingRepository = ibulddingRepository;
            _ICollageRepository = collageRepository;
            _IPermissionRepository = permissionRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (await _IPermissionRepository.hasPermission(User.GetUserId(), "buildings-Read", cancellationToken))
            {
                var bulddings = await _IbulddingRepository.GetAll();

                return View(bulddings);
            }
            return RedirectToAction("AccessDenied", "account");

        }

		public async Task<IActionResult> Create()
        {
            if (await _IPermissionRepository.hasPermission(User.GetUserId(), "buildings-Create", cancellationToken))
            {

                ViewBag.collageselct = new SelectList(await _ICollageRepository.GetAll(), nameof(collage.ID), nameof(collage.Name));

                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "account");
            }
        }

        public async Task<IActionResult> store(BuildingCollegeVM buildingCollegeVM)
        {

            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (await _IPermissionRepository.hasPermission(User.GetUserId(), "buildings-Create", cancellationToken))
            {
                if (ModelState.IsValid)
                {
                    BaseResponse res = await _IbulddingRepository.create(buildingCollegeVM);
                    if (res.IsSuccess == true)
                    {
                        TempData["message"] = res.Message.ToString();
                        TempData["type"] = res.Type;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["message"] = res.Message;
                        TempData["type"] = res.Type;
                        return RedirectToAction("Create", buildingCollegeVM);

                    }

                }
                else
                {
                    TempData["message"] = "الرجاء التحقق من البيانات الني تم ادخالها";
                    TempData["type"] = "warning";
                    return RedirectToAction("Create", buildingCollegeVM);
                }
            }
            else
            {
				return RedirectToAction("AccessDenied", "account");
			}

		}

        public async Task<IActionResult> Edit(int id)
        {
			if (await _IPermissionRepository.hasPermission(User.GetUserId(), "buildings-Update", cancellationToken))
			{
				var building = await _IbulddingRepository.GetById(id);

                ViewBag.collegelist = new SelectList(await _ICollageRepository.GetAll(), nameof(collage.ID), nameof(collage.Name), nameof(building.BuldingCollageNumber));

                return View(building);
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}
		}

        public async Task<IActionResult> update(BuildingCollegeVM buildingCollegeVM)
        {

            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                Building building = new Building
                {
                    ID = buildingCollegeVM.ID,
                    Name = buildingCollegeVM.buldingName,
                    Number = buildingCollegeVM.buldingnumber,
                    Collage_ID = buildingCollegeVM.BuldingCollageNumber
                };
                BaseResponse res = await _IbulddingRepository.update(building);

                if (res.IsSuccess == true)
                {
                    TempData["message"] = res.Message;
                    TempData["type"] = res.Type;
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["message"] = res.Message;
                    TempData["type"] = res.Type;

                    var buildinglist = await _IbulddingRepository.GetById(building.ID);
                    ViewBag.collageselct = new SelectList(await _ICollageRepository.GetAll(), nameof(collage.ID), nameof(collage.Name), nameof(building.Name));

                    return View("Edit", buildingCollegeVM);

                }
               
            }

            TempData["message"] = "حدث خط ما الرجاء التحقق من استكمال جميع الحقول او التواصل مع مدير النظام في حال استمرار المشكلة";

            //return View("Edit", buildingCollegeVM.ID);
            return Content("sdfasdfsad");
        }


        public async Task<IActionResult> delete(int id)
        {
            if (await _IPermissionRepository.hasPermission(User.GetUserId(), "buildings-Update", cancellationToken))
            {
                BaseResponse res = await _IbulddingRepository.delete(id);
                if (res.IsSuccess == true)
                {
                    TempData["message"] = res.Message;
                    TempData["type"] = res.Type;

                    return RedirectToAction("index");
                }
                else
                {
                    TempData["message"] = res.Message;
                    TempData["type"] = res.Type;

                    return RedirectToAction("index");

                }
			}
			else
			{
				return RedirectToAction("AccessDenied", "account");
			}

		}

    }
}
