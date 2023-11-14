using CoreCrudApi.Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreCrudApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task Delete(int id);
        Task<IEnumerable<Employee>> Get();
        Task<Employee> Get(int id);
        Task Post([FromBody] Employee value);
        Task Put(int id, [FromBody] Employee value);
    }
}