using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.Utilities.Exceptions;
using MinhDucEvent.ViewModels.Common;
using MinhDucEvent.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleVm>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }

        public async Task<Guid> Create(RoleCreateRequest request)
        {
            var role = new AppRole()
            {
                Name = request.Name,
                Description = request.Description
            };

            await _roleManager.CreateAsync(role);
            return role.Id;
        }

        public async Task<bool> Delete(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) throw new MinhDucEventException($"Cannot find a Role: {roleId}");
            var res = await _roleManager.DeleteAsync(role);

            if (res.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<PagedResult<RoleVm>> GetAllPaging(GetManageRolePagingRequest request)
        {
            //1. select join
            var query = _roleManager.Roles;
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Name.Contains(request.Keyword));
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RoleVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<RoleVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<RoleVm> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) throw new MinhDucEventException($"Cannot find a Role: {id}");

            var roleVM = new RoleVm()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return roleVM;
        }

        public async Task<bool> Update(RoleUpdateRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) throw new MinhDucEventException($"Cannot find a Role: {request.Id}");

            role.Name = request.Name;
            role.Description = request.Description;

            var res = await _roleManager.UpdateAsync(role);
            if (res.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}