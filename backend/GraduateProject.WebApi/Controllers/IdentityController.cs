using GraduateProject.Application.Ums.Dto;
using GraduateProject.Domain.Common;
using GraduateProject.Domain.Ums.Entities;
using GraduateProject.Domain.Ums.Repositories;
using GraduateProject.Extensions;
using GraduateProject.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers;

[Authorize]
[Route("/api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IdentityController(IUserAccountRepository userAccountRepository, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager,
        IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _userAccountRepository = userAccountRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    // [AllowAnonymous]
    // [HttpPost("login")]
    // public async Task<IActionResult> LoginAction([FromBody] LoginForm form)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         
    //     }
    //
    //     return Ok();
    // }


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ApiResponse<object>> Register([FromBody] RegisterForm form)
    {
        if (ModelState.IsValid)
        {
            var newUser = new UserAccount()
            {
                UserName = form.UserName,
                PhoneNumber = form.PhoneNumber,
                Email = form.Email,
            };
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var identityResult = await _userManager.CreateAsync(newUser, form.Password);
                await _unitOfWork.SaveChangesAsync();
                if (identityResult.Succeeded)
                {
                    var userRole = await _roleRepository.GetUserRole();
                    await _roleRepository.AddUserRole(newUser.Id, userRole.Id);
                    await _unitOfWork.SaveChangesAsync();
                    await _signInManager.SignInAsync(newUser, false);
                    var userProfile = await GetUserProfile(newUser);
                    await _unitOfWork.CommitTransactionAsync();
                    return ApiResponse<object>.Ok(userProfile);
                }
                else
                {
                    return ApiResponse<object>.Fail(string.Join("\n", identityResult.Errors.Select(x => $"{x.Code} - {x.Description}")));
                }
            }
            catch (Exception e)
            {
                await _unitOfWork.RollBackTransactionAsync();
                return ApiResponse<object>.Fail(e.Message);
            }
        }

        return ApiResponse<object>.Fail("Register fail");
    }

    private async Task<ApiResponse<object>> GetUserProfile(UserAccount userAccount)
    {
        var user = await _userAccountRepository.GetUserWithAuthInfo(userAccount.Id);
        List<string> claims;
        if (user.HasRoleAdminSystem())
        {
            var allRoles = await _roleRepository.ToListAsync();
            claims = allRoles.Select(x => x.Code).ToList();
        }
        else
        {
            claims = user.GetRoles().Select(x => x.Code).ToList();
        }

        return ApiResponse<object>.Ok(new
        {
            user,
            claims
        });
    }
}