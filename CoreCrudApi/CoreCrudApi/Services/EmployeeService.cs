using CoreCrudApi.Db.EntityFramework;
using CoreCrudApi.Db.Models;
using CoreCrudApi.Exceptions;
using CoreCrudApi.Helpers;
using CoreCrudApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoreCrudApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDBContext _employeeDBContext;
        private readonly IMemoryCache _memoryCache;

        public EmployeeService(EmployeeDBContext employeeDBContext, IMemoryCache memoryCache)
        {
            _employeeDBContext = employeeDBContext;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            if (_memoryCache.TryGetValue(CacheKeys.Employees, out IEnumerable<Employee>? employees))
            {
                return employees ?? Enumerable.Empty<Employee>();
            }

            return await RefreshCompleteCache();
        }

        private async Task<IEnumerable<Employee>> RefreshCompleteCache()
        {
            var result = await _employeeDBContext.Employees.AsNoTracking().ToListAsync();

            _memoryCache.Set(CacheKeys.Employees, result);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        private async Task UpdateCache(Employee employee, bool isDelete = false)
        {
            if (_memoryCache.TryGetValue(CacheKeys.Employees, out List<Employee>? employees))
            {
                if (employees is not null)
                {
                    var emp = employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                    if (emp is not null)
                    {
                        if (isDelete) { 
                            employees.Remove(emp);
                        }
                        else
                        {
                            emp.Name = employee.Name;
                            emp.Address = employee.Address;
                            emp.City = employee.City;
                            emp.Region = employee.Region;
                            emp.PostalCode = employee.PostalCode;
                            emp.Country = employee.Country;
                            emp.Phone = employee.Phone;
                        }
                    }
                    else
                    {
                        employees.Add(employee);
                    }
                }

                _memoryCache.Set(CacheKeys.Employees, employees);
            }
            else
            {
                await RefreshCompleteCache();
            }
        }


        public async Task<Employee> Get(int id)
        {
            if (_memoryCache.TryGetValue(CacheKeys.Employees, out IEnumerable<Employee>? employees))
            {
                if(employees is not null)
                {
                    return employees.FirstOrDefault(x => x.EmployeeId == id);
                }
            }

            return await _employeeDBContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);
        }


        public async Task Post([FromBody] Employee value)
        {
            _employeeDBContext.Add<Employee>(value);

            await _employeeDBContext.SaveChangesAsync();

            Task.Run(async () => await UpdateCache(value));
        }


        public async Task Put(int id, [FromBody] Employee value)
        {
            if (value is not null)
            {
                var employee = await _employeeDBContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);

                if (employee is null)
                {
                    throw new NotFoundException();
                }
                    value.EmployeeId = id;

                    _employeeDBContext.Update<Employee>(value);
                    await _employeeDBContext.SaveChangesAsync();
                
            }

            Task.Run(async () => await UpdateCache(value));
        }

        public async Task Delete(int id)
        {
            var employee = await _employeeDBContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (employee is null)
            {
                throw new NotFoundException();
            }

            _employeeDBContext.Remove<Employee>(employee);
            await _employeeDBContext.SaveChangesAsync();

            Task.Run(async () => await UpdateCache(employee));

        }
    }
}
