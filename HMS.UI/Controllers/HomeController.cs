////////////////////////////////////////////////////
//Author : Mohammed Gaffer Aidaab 
//For    : King Faisual University 
//Under  : ISB integrated sulution business Company 
//Halls ManageMent System 
/////////////////////////////////////////////////////  

using HMS.Domain.Entities.ViewModels;
using HMS.Domain.Interfaces.Repositories;
using HMS.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace HMS.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDashBordRepository _dashBordRepository;

        public HomeController(IDashBordRepository dashBordRepository)
        {
            _dashBordRepository = dashBordRepository;
        }

        public IActionResult Index()
        {
            DashBordVM dashboard = new DashBordVM
            {
                Number_Of_reservations = _dashBordRepository.gettodayreservations(),

                Number_of_halls = _dashBordRepository.getnumberofalls(),

                Number_of_active_halls = _dashBordRepository.GetActiveHalls(),

                reservation = _dashBordRepository.reservations(),

                Lastreservation = _dashBordRepository.GetLastReservation(),

            };

            float res = (float.Parse(dashboard.Number_Of_reservations.ToString())/float.Parse(dashboard.Number_of_halls.ToString())) * 100;

            TempData["res"] = res; 
            //float x = float.Parse(Convert.ToDecimal(dashboard.Number_Of_reservations) / dashboard.Number_of_halls);
            
            return View(dashboard);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
