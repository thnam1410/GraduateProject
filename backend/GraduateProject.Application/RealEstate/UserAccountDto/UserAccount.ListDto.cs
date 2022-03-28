using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using GraduateProject.Domain.Ums.Entities;
namespace GraduateProject.Application.RealEstate.UserAccountDto;

    public class UserAccountListDto
    {  
         public string? FullName { get; set; }
         public string? Email { get; set; }
         public bool? Active { get; set; } = true;
         public string? UserName { get; set; }
         public string? PhoneNumber { get; set; }
         // public string? RoleId { get; set; }
         // public string? Code { get; set; }
         public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
         //public ICollection<Post> Posts { get; set; } = new List<Post>();
         //public IEnumerable<Role> GetRoles() => UserRoles.Select(x => x.Role).ToList();

    }
