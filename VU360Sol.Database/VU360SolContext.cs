using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.JortForm;
using VU360Sol.Entities.Logs;
using VU360Sol.Entities.Notes;
using VU360Sol.Entities.RequestDemoes;
using VU360Sol.Entities.SalePersons;
using VU360Sol.Entities.Searching;
using VU360Sol.Entities.Visitors;

namespace VU360Sol.Database
{
    public class VU360SolContext : DbContext
    {

        public VU360SolContext(DbContextOptions<VU360SolContext> options)
           : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<SalePerson> SalePersons { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<RequestDemo> RequestDemoes { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<DoctorAssigned> DoctorAssigned { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<RefrenceTable> RefrenceTables { get; set; }
        public DbSet<VisitorPurpose> VisitorPurposes { get; set; }
        public DbSet<FailToImportDoctorsLog> FailToImportDoctorsLog { get; set; }
        public DbSet<Searching> Searching { get; set; }
        public DbSet<EmailConfigurations> EmailConfigurations { get; set; }

        public DbSet<Practice> Practices { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

        }
    }
}
