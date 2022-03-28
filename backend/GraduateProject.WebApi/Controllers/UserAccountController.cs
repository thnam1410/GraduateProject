using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GraduateProject.Application.Extensions;
using GraduateProject.Application.Ums.Dto;
using GraduateProject.Application.RealEstate.UserAccountDto;
using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using GraduateProject.Extensions;
using GraduateProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace GraduateProject.Controllers;

[Authorize]
[Route("/api/user-account")]
public class UserAccountController : ControllerBase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IObjectMapper _mapper;
    private readonly ICurrentUser<Guid> _currentUser;
    private readonly IValidator _validator;

    public UserAccountController(IUserAccountRepository userAccountRepository, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager,
        IRoleRepository roleRepository, IUnitOfWork unitOfWork, IObjectMapper mapper, ICurrentUser<Guid> currentUser, IValidator validator)
    {
        _userAccountRepository = userAccountRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
        _validator = validator;
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


