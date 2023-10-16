////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For : King Faisual University 
//Under : ISB integrated sulution business Company 
//Halls ManageMent System 
///////////////////////////////////////////////////// 
///

using HMS.Business.Repositories;
using HMS.Domain.Entities;
using HMS.Domain.Entities.Shared;
using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.UI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly ISiteRoleRepository _SiteRoleRepository;
        private readonly IPermissionRepository _PermissionRepository;

        public RoleController(ISiteRoleRepository siteRoleRepository, IPermissionRepository permissionRepository)
        {
            _SiteRoleRepository = siteRoleRepository;
            _PermissionRepository = permissionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var  roles  = await  _SiteRoleRepository.GetAllRoles();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Store(SiteRole role, CancellationToken cancellationToken) {

			//var errors = ModelState.Values.SelectMany(v => v.Errors);

			role.NormalizedName = role.Name.ToUpper();

            if (ModelState.IsValid)
            {
                var res = await _SiteRoleRepository.CreateAsync(role,cancellationToken);
                if(res.Succeeded)
                {
                    TempData["message"] = "تم انشاء الصلاحية بنجاح";
                    TempData["type"] = "success";
                    return RedirectToAction("index");
                }
                else
                {
					TempData["message"] = "لم يتم انشاء الصلاحية الرجاء التحقق من اليانات المدخلة";
					TempData["type"] = "warning";
					return View("create", role);
                }
            }
			TempData["message"] = "يوجد خطا ف يالبانات المدخلة الرجء مراجعة البانات";
			TempData["type"] = "danger";
            return View("create", role);

        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken) 
        {
            string ID = id.ToString();

            SiteRole role = await _SiteRoleRepository.FindByIdAsync(ID, cancellationToken);

            RolePermissionVM rolePermissionVM = new RolePermissionVM
            {
                Role = await _SiteRoleRepository.FindByIdAsync(ID, cancellationToken),

                permissions = await _PermissionRepository.GetAllByRole(role),
            };


           // var permissions = await _PermissionRepository.GetAllByRole(role);

            return View(rolePermissionVM);
        }

		public async Task<IActionResult> Update(SiteRole role, string[] Perms, CancellationToken cancellationToken)
		{

			var errors = ModelState.Values.SelectMany(v => v.Errors);

			if (ModelState.IsValid)
			{
				role.NormalizedName = role.Name.ToUpper();

				if (ModelState.IsValid)
				{
					var roleresult = await _SiteRoleRepository.UpdateAsync(role, cancellationToken);
					if (roleresult.Succeeded)
					{
						List<Permission> permissions = await _PermissionRepository.GetByName(Perms);

						var res = _PermissionRepository.update(permissions, role.Id);

						TempData["message"] = "تم تعديل بيانات الصلاحية بنجاح";
						TempData["type"] = "success";
						return RedirectToAction("index");
					}
					else
					{
						TempData["message"] = "لم يتم تعديل البيانات الرجاء التحقق من اليانات المدخلة";
						TempData["type"] = "warning";
						return View("Edit", role.Id);
					}
				}

				TempData["message"] = "يوجد خطا ف يالبانات المدخلة الرجء مراجعة البانات";
				TempData["type"] = "danger";
				return View("edit", role.Id);
			}

			return View("edit", role.Id);
		}


	}
}
