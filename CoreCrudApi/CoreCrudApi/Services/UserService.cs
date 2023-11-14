using CoreCrudApi.Db.EntityFramework;
using CoreCrudApi.Db.Models;
using CoreCrudApi.Exceptions;
using CoreCrudApi.Helpers;
using CoreCrudApi.Models;
using CoreCrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;

namespace CoreCrudApi.Services
{
    public class UserService : IUserService
    {
        private readonly EmployeeDBContext _EmployeeDBContext;
        private readonly JwtHandler _jwtHandler;

        public UserService(EmployeeDBContext EmployeeDBContext
            , JwtHandler jwtHandler)
        {
            _EmployeeDBContext = EmployeeDBContext;
            _jwtHandler = jwtHandler;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _EmployeeDBContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _EmployeeDBContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> Get(string userName)
        {
            return await _EmployeeDBContext.Users
                    .Include(x => x.UserRoleMaps)
                    .ThenInclude(r => r.Role)                    
                    .FirstOrDefaultAsync(x => x.UserName.ToLower() ==userName.ToLower());
        }

        // need to add encrypt decrypt logic
        // case sensitive macth for password
        public bool VerifyPassword(User user, AuthRequestDto authRequestDto)
        {
            return user.Password == authRequestDto.Password;
        }

        public async Task Post([FromBody] User value)
        {
            _EmployeeDBContext.Add<User>(value);

            await _EmployeeDBContext.SaveChangesAsync();
        }


        public async Task Put(int id, [FromBody] User value)
        {
            if (value is not null)
            {
                var User = await _EmployeeDBContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);

                if (User is null)
                {
                    throw new NotFoundException();
                }
                value.UserId = id;

                _EmployeeDBContext.Update<User>(value);
                await _EmployeeDBContext.SaveChangesAsync();

            }
        }

        public async Task Delete(int id)
        {
            var User = await _EmployeeDBContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);

            if (User is null)
            {
                throw new NotFoundException();
            }

            _EmployeeDBContext.Remove<User>(User);
            await _EmployeeDBContext.SaveChangesAsync();

        }

    }
}
