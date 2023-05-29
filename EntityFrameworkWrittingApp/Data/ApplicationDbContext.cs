using EntityFrameworkWrittingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFrameworkWrittingApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
