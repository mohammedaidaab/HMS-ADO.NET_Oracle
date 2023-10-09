﻿using HMS.Domain.Entities;
using System.Collections.Generic;

namespace HMS.MVC.ViewModels.ManageViewModels
{
    public class UserListViewModel
    {
        public int Count { get; set; }

        public List<SiteUser> List { get; set; }
    }
}
