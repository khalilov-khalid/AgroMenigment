using API_AGROMG.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<LanguageContext> LanguageContexts { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Profession> Professions { get; set; }

        public DbSet<UserProfessions> UserProfessions { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Packet> Packets { get; set; }

        public DbSet<Modules> Modules { get; set; }
    }
}
