using CoreCrudApi.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreCrudApi.Db.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasData(new Tenant[]
            {
                new Tenant {TenantId = 1,TenantName="Amazon"},
                new Tenant {TenantId = 2,TenantName="Google"},
                new Tenant {TenantId = 3,TenantName="Tesla"}
            });

            modelBuilder.Entity<Employee>().HasData(new Employee[] {
               new Employee { EmployeeId=1,TenantId=1, Name = "Sam", Phone="12345678", Address = "213 SW 11 st", City = "Dalla", Region = "TX", Country = "USA", PostalCode = "38910",Email="sam@gmail.com" },
               new Employee { EmployeeId=2,TenantId=1, Name = "John",Phone="12345679", Address = "213 hello st", City = "Houston", Region = "TX", Country = "USA", PostalCode = "38920",Email="john@gmail.com" },
               new Employee { EmployeeId=3,TenantId=2, Name = "Jeff",Phone="12345679", Address = "213 hello st", City = "Houston", Region = "TX", Country = "USA", PostalCode = "38920",Email="jeff@gmail.com" },
               new Employee { EmployeeId=4,TenantId=2, Name = "Elon",Phone="12345679", Address = "213 hello st", City = "Houston", Region = "TX", Country = "USA", PostalCode = "38920",Email="elon@gmail.com" },
               new Employee { EmployeeId=5,TenantId=2, Name = "Paul",Phone="12345679", Address = "213 hello st", City = "Houston", Region = "TX", Country = "USA", PostalCode = "38920",Email="paul@gmail.com" },
            });

            modelBuilder.Entity<User>().HasData(new User[] {
               new User { UserId=1,TenantId=0,  Name = "Admin", UserName="Admin", Password="Admin@123",Email="admin@gmail.com" },
               new User { UserId=2,TenantId=1, Name = "Sam", UserName="Sam", Password="Sam@123",Email="sam@gmail.com" },
               new User { UserId=3,TenantId=1, Name = "John", UserName="John", Password="Sam@123",Email="john@gmail.com" },
               new User { UserId=4,TenantId=2, Name = "Jeff", UserName="Jeff", Password="Sam@123",Email="jeff@gmail.com" },
               new User { UserId=5,TenantId=2, Name = "Elon", UserName="Elon", Password="Paul@123",Email="elon@gmail.com" },
               new User { UserId=6,TenantId=2, Name = "Paul", UserName="Paul", Password="Paul@123" , Email = "paul@gmail.com"},
            });

            modelBuilder.Entity<Role>().HasData(new Role[] {
               new Role { RoleId=1, RoleName = "Admin" },
               new Role { RoleId=2, RoleName = "Operations" },
               new Role { RoleId=3, RoleName = "GeneralUser" },
            });

            modelBuilder.Entity<UserRoleMap>().HasData(new UserRoleMap[] {
               new UserRoleMap { RoleId=1, UserId=1 },
               
               new UserRoleMap { RoleId=3, UserId=2 },

               new UserRoleMap { RoleId = 2, UserId = 3 },
               new UserRoleMap { RoleId=3, UserId=3 },
            });
        }
    }
}
