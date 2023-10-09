using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Entities.ViewModels
{
    public class RolePermissionVM
    {
        public SiteRole Role { get; set; }

        public List<Permission> permissions { get; set; }
    }
}
