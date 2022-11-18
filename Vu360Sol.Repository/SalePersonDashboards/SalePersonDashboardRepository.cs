using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.SalePersons;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.SalePersonDashboards
{
   public class SalePersonDashboardRepository : ISalePersonDashboardRepository
    {
        DateTime date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

        private readonly VU360SolContext _context;
        public SalePersonDashboardRepository(VU360SolContext context)
        {
            _context = context;
        }

        /// ContactedDoctors Section

        public async Task<IEnumerable<Doctor>> ContactedDoctors(int PageSize, int PageNumber, string Search, int days, int salePersonId)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate.Date >= DateTime.Now.AddDays(-7) && w.Doctor.IsDeleted == false && (salePersonId == 0 ? true : w.SalePerson.User.Id == salePersonId) && w.IsDeleted == false &&
                         (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                         )
                        .Include(x => x.Doctor.User.Gender)
                        .Include(x => x.Doctor.User.Role)
                             .OrderByDescending(x => x.AssignedDate)
                             .Select(x => x.Doctor)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate.Date >= DateTime.Now.AddDays(-7) && w.Doctor.IsDeleted == false && (salePersonId == 0 ? true : w.SalePerson.User.Id == salePersonId) && w.IsDeleted == false
                              )
                              .OrderByDescending(x => x.AssignedDate)
                              .Include(x => x.Doctor.User.Gender)
                              .Select(x => x.Doctor)
                              .Distinct()
                              .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                              .ToListAsync();
                }

            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate.Date >= DateTime.Now.AddDays(-30) && w.Doctor.IsDeleted == false && (salePersonId == 0 ? true : w.SalePerson.User.Id == salePersonId) && w.IsDeleted == false &&
                         (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                         )
                        .Include(x => x.Doctor.User.Gender)
                        .Include(x => x.Doctor.User.Role)
                             .OrderByDescending(x => x.AssignedDate)
                             .Select(x => x.Doctor)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate.Date >= DateTime.Now.AddDays(-30) && w.Doctor.IsDeleted == false && (salePersonId == 0 ? true : w.SalePerson.User.Id == salePersonId) && w.IsDeleted == false
                              )
                              .OrderByDescending(x => x.AssignedDate)
                              .Include(x => x.Doctor.User.Gender)
                              .Select(x => x.Doctor)
                              .Distinct()
                              .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                              .ToListAsync();
                }

            }
            else
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate.Date == date.Date && w.Doctor.IsDeleted == false 
                    && (salePersonId == 0 ? true : w.SalePerson.User.Id == salePersonId) && w.IsDeleted == false &&
                         (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                         )
                        .Include(x => x.Doctor.User.Gender)
                        .Include(x => x.Doctor.User.Role)
                        .Select (x=>x.Doctor)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    var data = await _context.DoctorAssigned.Where(
                                     w => w.NoteId != null 
                                     && w.AssignedDate.Date == date.Date
                                     && w.Doctor.IsDeleted == false 
                                     && w.SalePerson.UserId == salePersonId 
                                     && w.IsDeleted == false
                              )
                              
                              .Include(x => x.Doctor.User.Gender)
                              .Select(x => x.Doctor)
                              .Distinct()
                              .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                              .ToListAsync();
                    return data;
                }
            }

        }

        public async Task<int> CountContactedDoctors(string Search, int days, int salePersonId)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate >= DateTime.Now.AddDays(-7) && w.Doctor.IsDeleted == false &&  w.SalePerson.UserId == salePersonId && w.IsDeleted == false &&
                             (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                             )
                             .OrderByDescending(x => x.AssignedDate)
                             .Include(x => x.Doctor.User.Gender)
                             .Select(x => x.Doctor)
                             .Distinct()
                             .CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate >= DateTime.Now.AddDays(-7) && w.Doctor.IsDeleted == false &&  w.SalePerson.UserId == salePersonId && w.IsDeleted == false
                            )
                            .OrderByDescending(x => x.AssignedDate)
                            .Include(x => x.Doctor.User.Gender)
                            .Select(x => x.Doctor)
                            .Distinct()
                            .CountAsync();
                }

            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate >= DateTime.Now.AddDays(-30) && w.Doctor.IsDeleted == false &&  w.SalePerson.UserId == salePersonId && w.IsDeleted == false &&
                             (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                             )
                             .OrderByDescending(x => x.AssignedDate)
                             .Include(x => x.Doctor.User.Gender)
                             .Select(x => x.Doctor)
                             .Distinct()
                             .CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate >= DateTime.Now.AddDays(-30) && w.Doctor.IsDeleted == false && w.SalePerson.UserId == salePersonId && w.IsDeleted == false
                            )
                            .OrderByDescending(x => x.AssignedDate)
                            .Include(x => x.Doctor.User.Gender)
                            .Select(x => x.Doctor)
                            .Distinct()
                            .CountAsync();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate == date.Date && w.Doctor.IsDeleted == false && w.SalePerson.UserId == salePersonId && w.IsDeleted == false &&
                             (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                             || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                             )
                             .OrderByDescending(x => x.AssignedDate)
                             .Include(x => x.Doctor.User.Gender)
                             .Select(x => x.Doctor)
                             .Distinct()
                             .CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.AssignedDate == date.Date && w.AssignedDate >= DateTime.Now.AddDays(-7) && w.Doctor.IsDeleted == false &&  w.SalePerson.UserId == salePersonId && w.IsDeleted == false
                            )
                            .OrderByDescending(x => x.AssignedDate)
                            .Include(x => x.Doctor.User.Gender)
                            .Select(x => x.Doctor)
                            .Distinct()
                            .CountAsync();
                }
            }

        }

        /// DoctorAssigned Section

        public async Task<IEnumerable<DoctorAssigned>> DoctorAssignedList(int PageSize, int PageNumber, string Search, int days, int salePersonId)
        {
            if (days == 7)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned
                           .Where(x => x.IsDeleted == false
                           && x.AssignedDate >= DateTime.Now.Date.AddDays(-7)
                           && x.SalePerson.UserId == salePersonId
                           && (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            )
                           .Include(x=>x.Doctor.User.Gender)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.DoctorAssigned
                             .Where(x => x.IsDeleted == false
                             && x.SalePerson.UserId == salePersonId
                             && x.AssignedDate >= DateTime.Now.Date.AddDays(-7))
                             .Include(x => x.Doctor.User.Gender)
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
                    return await _context.DoctorAssigned
                           .Where(x => x.IsDeleted == false
                          && x.SalePerson.UserId == salePersonId
                           && x.AssignedDate >= DateTime.Now.Date.AddDays(-30)
                           && (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            )
                           .Include(x => x.Doctor.User.Gender)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.DoctorAssigned
                             .Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate >= DateTime.Now.Date.AddDays(-30))
                             .Include(x => x.Doctor.User.Gender)
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
                    return await _context.DoctorAssigned
                           .Where(x => x.IsDeleted == false
                           && x.AssignedDate.Date == date.Date
                           && x.SalePerson.UserId == salePersonId
                           && (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            )
                           .Include(x=>x.Doctor.User.Gender)
                           .Include(x=>x.SalePerson.User.Gender)
                           .Include(x=>x.Note)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
                else
                {
                    return await _context.DoctorAssigned
                             .Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date == date.Date)
                             .Include(x => x.Doctor.User.Gender)
                             .Include(x => x.SalePerson.User.Gender)
                             .Include(x => x.Note)
                             .OrderByDescending(x => x.Id)
                             .Distinct()
                             .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                             .ToListAsync();
                }
            }

        }

        public async Task<int> CountDoctorAssignedList(string Search, int days, int salePersonId)
        {
            if (days == 7)
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId &&  x.AssignedDate.Date >= DateTime.Now.AddDays(-7) &&
                            (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date >= DateTime.Now.AddDays(-7)).CountAsync();

                }
            }
            else if (days == 30)
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date >= DateTime.Now.AddDays(-30) &&
                            (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date >= DateTime.Now.AddDays(-30)).CountAsync();

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date == date.Date &&
                            (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                            ).CountAsync();
                }
                else
                {
                    return await _context.DoctorAssigned.Where(x => x.IsDeleted == false && x.SalePerson.UserId == salePersonId && x.AssignedDate.Date == date.Date).CountAsync();

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
            else if (Days == 30)
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

        public async Task<DoctorAssigned> GetById(int Id)
        {
            var data = await _context.DoctorAssigned
                         .Where(x => x.IsDeleted == false && x.DoctorId == Id).Include(x=>x.Doctor.User.Gender).Include(x=>x.Doctor.Status).Include(x => x.Doctor.Practice).FirstOrDefaultAsync();
                     return data;
        }
    }
}
