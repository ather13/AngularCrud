
using CoreCrudApi.Db.Extensions;
using CoreCrudApi.Db.Models;
using CoreCrudApi.Db.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreCrudApi.Db.EntityFramework
{
    public class EmployeeDBContext : DbContext
    {
        private static bool _created = false; // make this false to setup database schema and seed data
        private readonly ICurrentTenantService? _currentTenantService;
        public int _currentTenantId { get; set; }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public EmployeeDBContext(ICurrentTenantService currentTenantService, DbContextOptions<EmployeeDBContext> options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();

            }
            _currentTenantService = currentTenantService;
            _currentTenantId = int.Parse( _currentTenantService?.TenantId ?? "0");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        //{
        //    optionbuilder.UseSqlite(@"Data Source=..\CoreCrudApi.Db\Database\Employee.db");
        //}

        // On Save Changes - write tenant Id to table
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = _currentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // On Model Creating - multitenancy query filters 
            modelBuilder.Entity<Employee>().HasQueryFilter(a => a.TenantId == _currentTenantId);

            modelBuilder.Entity<Employee>(e => {
                e.HasKey(nameof(Employee.EmployeeId));
                e.Property(x => x.EmployeeId).ValueGeneratedOnAdd();
                e.Property(x => x.Name).IsRequired(true);
                e.Property(x => x.Phone).IsRequired(false);
                e.Property(x => x.Email).IsRequired(false);
                e.Property(x => x.Address).IsRequired(false);
                e.Property(x => x.City).IsRequired(false);
                e.Property(x => x.Region).IsRequired(false);
                e.Property(x => x.Country).IsRequired(false);
                e.Property(x => x.PostalCode).IsRequired(false);
            });

            modelBuilder.Entity<User>(e => {
                e.HasKey(nameof(User.UserId));
                e.Property(x => x.Name).IsRequired(true);
                e.Property(x => x.Email).IsRequired(true);
                e.Property(x => x.UserName).IsRequired(true);
                e.Property(x => x.Password).IsRequired(true);
            });

            modelBuilder.Entity<Tenant>(e => {
                e.HasKey(nameof(Tenant.TenantId));
                e.Property(x => x.TenantName).IsRequired(true);
            });

            modelBuilder.Entity<Role>(e => {
                e.HasKey(nameof(Role.RoleId));
                e.Property(x => x.RoleName).IsRequired(true);
            });

            //// one to many relationship
            //modelBuilder.Entity<Role>()
            //    .HasOne(b => b.User)
            //    .WithMany(a => a.Roles)
            //    .HasForeignKey(b => b.UserId);

            //// many to many relationship
            modelBuilder.Entity<UserRoleMap>()
                .HasKey(sc => new { sc.UserId, sc.RoleId });

            modelBuilder.Entity<UserRoleMap>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserRoleMaps)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserRoleMap>()
                .HasOne(sc => sc.Role)
                .WithMany(c => c.UserRoleMaps)
                .HasForeignKey(sc => sc.RoleId);

            modelBuilder.Seed();
        }
    }

}
