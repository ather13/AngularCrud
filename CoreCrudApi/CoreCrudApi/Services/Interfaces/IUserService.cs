using CoreCrudApi.Db.Models;
using CoreCrudApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreCrudApi.Services.Interfaces
{
    public interface IUserService
    {
        Task Delete(int id);
        Task<IEnumerable<User>> Get();
        Task<User> Get(int id);
        Task<User> Get(string userName);
        Task Post([FromBody] User value);
        Task Put(int id, [FromBody] User value);
        bool VerifyPassword(User user, AuthRequestDto authRequestDto);
    }
}