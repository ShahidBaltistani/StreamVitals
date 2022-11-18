using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.Visitors;

namespace VU360Sol.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitorPurpose>().HasData(
                    new VisitorPurpose
                    {
                        Id = 1,
                        Name = "Contact",
                    },
                    new VisitorPurpose
                    {
                        Id = 2,
                        Name = "Subscribe",
                    }
                );
            modelBuilder.Entity<Gender>().HasData(
                    new Gender
                    {
                        Id = 1,
                        Name = "Male",
                    },
                    new Gender
                    {
                        Id = 2,
                        Name = "Female",
                    },
                    new Gender
                    {
                        Id = 3,
                        Name = "None",
                    }
                );
            modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Admin",
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Doctor",
                    },
                    new Role
                    {
                        Id = 3,
                        Name = "SalePerson",
                    }
                );
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Email = "admin@gmail.com",
                        Password = "111111",
                        RoleId = 1,
                        GenderId = 1,
                        IsActive= true

                    }
                );
            modelBuilder.Entity<Status>().HasData(
                    new Status
                    {
                        Id = 1,
                        Name = "Un Registered"
                    },
                     new Status
                     {
                         Id = 2,
                         Name = "Registration Request Sent"
                     },
                      new Status
                      {
                          Id = 3,
                          Name = "Registered"
                      }
                ); 
            modelBuilder.Entity<RefrenceTable>().HasData(
                    new RefrenceTable
                    {
                        Id = 1,
                        Name = "Doctors",
                        IsActive = true
                    },
                     new RefrenceTable
                     {
                         Id = 2,
                         Name = "SalePersons",
                         IsActive = true
                     },
                     new RefrenceTable
                     {
                         Id = 3,
                         Name = "Users",
                         IsActive = true
                     },
                     new RefrenceTable
                     {
                         Id = 4,
                         Name = "RequestDemo",
                         IsActive = true
                     }

                );
            //modelBuilder.Entity<EmailConfigurations>().HasData(
            //        new EmailConfigurations
            //        {
            //            Id = 1,
            //            Email = "streamvitalsllc@gmail.com",
            //            Password = "VuSolution360",
            //            Host= "smtp.gmail.com",
            //            Port=587

            //        }
            //    );
        }
    }
}
