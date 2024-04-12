using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.StudentCRUD
{
    public class AppUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
