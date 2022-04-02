using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraduateProject.Application.RealEstate.UserAccountDto;
using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using GraduateProject.Extensions;
using GraduateProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Controllers
{
    [Authorize]
    [Route("/api/user-account")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountController(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
    
        [AllowAnonymous]
        [HttpGet("user-account-list")]
        public async Task<ApiResponse<List<UserAccountListDto>>> UserAccountAction()
        {
            var userAccountList = await _userAccountRepository.Queryable()
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .Select(x=>new UserAccountListDto
                {
                    FullName = x.FullName,
                    Email = x.Email,
                    Active = x.Active,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber,
                    UserRoles =  x.UserRoles
                })
                .ToListAsync();
            return ApiResponse<List<UserAccountListDto>>.Ok(userAccountList);
        }

    }
}
    

