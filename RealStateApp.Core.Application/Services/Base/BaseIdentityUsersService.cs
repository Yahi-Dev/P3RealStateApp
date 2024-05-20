using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;
using System.Collections.Generic;

namespace RealStateApp.Core.Application.Services.Base
{
    public class BaseIdentityUsersService : IBaseIdentityUsersService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public BaseIdentityUsersService(IAccountService accountService, IMapper mapper)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        public virtual async Task<Response<int>> CreateAsync(SaveUserViewModel viewModel, string id, string origin, string role)
        {
            Response<int> result = new();
            try
            {
                await _accountService.RegisterUserAsync(_mapper.Map<RegisterRequest>(viewModel), origin, role);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public virtual async Task<Response<int>> UpdateAsync(SaveUserViewModel viewModel, string id)
        {
            Response<int> result = new();
            try
            {
                var entity = await _accountService.GetByIdAsync(id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Message = "Id is not registered.";
                    return result;
                }
                var request = _mapper.Map<RegisterRequest>(viewModel);
                await _accountService.UpdateUserAsync(request);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public virtual async Task<Response<int>> DeleteByIdAsync(string id)
        {
            Response<int> result = new();
            try
            {
                var entity = await _accountService.GetByIdAsync(id);
                if (entity == null)
                {
                    result.Succeeded = false;
                    result.Message = "Id is not registered.";
                    return result;
                }
                await _accountService.Remove(entity);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }


        public virtual async Task<Response<BaseUserViewModel>> GetByIdAsync(string id)
        {
            Response<BaseUserViewModel> result = new();
            try
            {
                var user = await _accountService.GetByIdAsync(id);
                result.Data = _mapper.Map<BaseUserViewModel>(user);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public virtual async Task<Response<List<BaseUserViewModel>>> GetAllAsync()
        {
            Response<List<BaseUserViewModel>> result = new();
            result.Succeeded = true;
            try
            {
                var list = await _accountService.GetAllUsers();
                result.Data = _mapper.Map<List<BaseUserViewModel>>(list);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<Response<int>> ChangeStatusAsync(SaveChangeStatusViewModel vm)
        {
            Response<int> result = new();

            if (vm.Role == RolesEnum.Agent.ToString())
            {
                var agent = await _accountService.GetByIdAsync(vm.Id);
                if (agent.IsActive == true)
                {
                    agent.IsActive = false;
                }
                else
                {
                    agent.IsActive = true;
                }
                var userVm = _mapper.Map<RegisterRequest>(agent);
                await _accountService.ChangeUserStatus(userVm);
            }

            if (vm.Role == RolesEnum.Admin.ToString())
            {
                var admin = await _accountService.GetByIdAsync(vm.Id);
                if (admin.IsActive == true)
                {
                    admin.IsActive = false;
                }
                else
                {
                    admin.IsActive = true;
                }
                var userVm = _mapper.Map<RegisterRequest>(admin);
                await _accountService.ChangeUserStatus(userVm);
            }

            if (vm.Role == RolesEnum.Developer.ToString())
            {
                var developer = await _accountService.GetByIdAsync(vm.Id);
                if (developer.IsActive == true)
                {
                    developer.IsActive = false;
                }
                else
                {
                    developer.IsActive = true;
                }
                var userVm = _mapper.Map<RegisterRequest>(developer);
                await _accountService.ChangeUserStatus(userVm);
            }

            result.Message = "Se ha cambiado el estado";
            return result;
        }
    }
}
