using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.RequestDemoes;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Dashboards
{
   public class DashboardRepository : IDashboardRepository
    {
        DateTime date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

        private readonly VU360SolContext _context;
        public DashboardRepository(VU360SolContext context)
        {
            _context = context;
        }

        /// User Approval Section

        public async Task<User> Approve(int Id)
        {
            var data = await _context.Users.FindAsync(Id);
            if (data != null)
            {
                data.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<IEnumerable<User>> GetAllInActive(int PageSize, int PageNumber, string Search, int days)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false
                           && x.IsActive == false && x.CreatedOn >= DateTime.Now.Date.AddDays(-7)
                           && (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn >= DateTime.Now.Date.AddDays(-7))
                   .Include(x => x.Gender)
                   .Include(x => x.Role)
                        .OrderByDescending(x => x.Id)
                        .Distinct()
                        .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .ToListAsync();
                }

            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false &&  x.CreatedOn >= DateTime.Now.Date.AddDays(-30)
                          &&  (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn >= DateTime.Now.Date.AddDays(-30))
                   .Include(x => x.Gender)
                   .Include(x => x.Role)
                        .OrderByDescending(x => x.Id)
                        .Distinct()
                        .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .ToListAsync();
                }

            }
            else
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn.Value.Date == date.Date &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date == date.Date && x.IsActive == false)
                   .Include(x => x.Gender)
                   .Include(x => x.Role)
                        .OrderByDescending(x => x.Id)
                        .Distinct()
                        .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .ToListAsync();
                }
            }

        }

        public async Task<int> GetAllPageCountInActiveUser(string Search, int days)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-7) &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                        .CountAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-7) && x.IsActive == false)
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                      .CountAsync();

                }

            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-30) &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                        .CountAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-30) && x.IsActive == false)
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                      .CountAsync();

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false && x.CreatedOn.Value.Date == date.Date &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                        .CountAsync();
                }
                else
                {
                    return await _context.Users.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date == date.Date && x.IsActive == false)
                        .Include(x => x.Gender)
                        .Include(x => x.Role)
                      .CountAsync();

                }
            }

        }

        /// RequestDemo Section

        public async Task<IEnumerable<RequestDemo>> GetRequestDemo(int PageSize, int PageNumber, string Search, int days)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes
                           .Where(x => x.IsDeleted == false
                           && x.IsActive == true
                           && x.CreatedOn >= DateTime.Now.Date.AddDays(-7)
                           && (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.RequestDemoes
                             .Where(x => x.IsDeleted == false && x.IsActive == true && x.CreatedOn >= DateTime.Now.Date.AddDays(-7))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }

            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes
                           .Where(x => x.IsDeleted == false
                           && x.IsActive == true
                           && x.CreatedOn >= DateTime.Now.Date.AddDays(-30)
                           && (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.RequestDemoes
                             .Where(x => x.IsDeleted == false && x.IsActive == true && x.CreatedOn >= DateTime.Now.Date.AddDays(-30))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }

            }
            else
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes
                           .Where(x => x.IsDeleted == false
                           && x.IsActive == true
                           && x.CreatedOn.Value.Date == date.Date
                           && (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.RequestDemoes
                             .Where(x => x.IsDeleted == false && x.IsActive == true
                              && x.CreatedOn.Value.Date == date.Date)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }

        }

        public async Task<int> GetAllPageCountRequestDemo(string Search, int days)
        {
            if (days == 7)
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-7) &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-7) && x.IsActive == true).CountAsync();

                }
            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-30) &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= DateTime.Now.AddDays(-30) && x.IsActive == true).CountAsync();

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true && x.CreatedOn.Value.Date == date.Date &&
                            (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true
                    && x.CreatedOn.Value.Date == date.Date
                    ).CountAsync();



                }
            }
       
        }

        /// Visitor Section
        public async Task<IEnumerable<Visitor>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search, int Days)
        {
            if (Days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }
            else if (Days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date == date.Date &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date == date.Date)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }

            }

        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search, int Days)
        {
            if (Days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }
            else if (Days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30))
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date == date.Date &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date == date.Date)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }

            }
        }
        public async Task<int> GetAllPageCountForLearning(string Search, int Days)
        {
            if (Days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7))
                             .CountAsync();
                }

            }
            else if (Days ==30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30))
                             .CountAsync();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date == date.Date &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && x.CreatedOn.Date == date.Date)
                             .CountAsync();
                }
            }
        }
        public async Task<int> GetAllPageCountForStarting(string Search, int Days)
        {
            if (Days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-7))
                             .CountAsync();
                }

            }
            else if (Days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30) &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date >= DateTime.Now.AddDays(-30))
                             .CountAsync();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date == date.Date &&
                    (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                    )
                        .CountAsync();
                }
                else
                {
                    return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 && x.CreatedOn.Date == date.Date)
                             .CountAsync();
                }
            }
        }
    }
}
