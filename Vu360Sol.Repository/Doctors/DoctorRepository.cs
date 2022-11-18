using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Logs;

namespace Vu360Sol.Repository.Doctors
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly VU360SolContext _context;
        public DoctorRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Doctor>> GetAll()
        {
            return (await _context.Doctors
                 .Where(x => x.IsDeleted == false && x.IsActive == true)
                   .Include(x => x.User.Gender)
                  .Include(x=> x.Status)
                  .Include(x=> x.Practice)
                  .ToListAsync());
        }
        public async Task<Doctor> Get(int Id)
        {
            return (await _context.Doctors
                 .Include(x => x.User.Gender)
                .Include(x => x.Status)
                .Include(x => x.Practice)
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<Doctor> Add(Doctor model)
        {
            await _context.Doctors.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<Doctor> Delete(int id)
        {
            var data = await _context.Doctors.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<Doctor> Update(Doctor model)
        {
           _context.Set<Doctor>().Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<Doctor> UpdateDoctorStatus(int DoctorID, int StatusId)
        {
            
            var doc =await _context.Doctors.Where(x => x.Id == DoctorID).FirstOrDefaultAsync();
            if(doc != null)
            {
                doc.StatusId = StatusId;
                _context.Entry(doc).Property("StatusId").IsModified = true;
              
            }
            return doc;
        }
        public async Task<IEnumerable<Doctor>> GetAllInActive()
        {
            return (await _context.Doctors.Include(x => x.User).Include(x => x.User.Gender).Include(x => x.Practice).Include(x => x.User.Role)
                 .Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false).ToListAsync());
        }

        public async Task<Doctor> DoctorApproval(int Id)
        {
            var data = await _context.Doctors.Include(x=>x.User).Include(x => x.Practice).Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.IsActive = true;
                data.User.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<IEnumerable<FailToImportDoctorsLog>> ImportDoctors(IEnumerable<Doctor> model)
        {
            var emailList = model.Select(x => x.User.Email).ToList();
            var userExistList = new List<string>();
            var DocNotExsist = new List<Doctor>();
            var DocExsist = new List<Doctor>();
            var log = new List<FailToImportDoctorsLog>();
            if (emailList != null && emailList.Count > 0)
             userExistList = await _context.Users.Where(x => emailList.Contains(x.Email) && x.IsDeleted == false).Select(x=> x.Email).ToListAsync();
            if (userExistList != null && userExistList.Count > 0)
            {
                DocNotExsist = model.Where(s => !userExistList.Any(p => p == s.User.Email)).ToList();
                DocExsist = model.Where(s => userExistList.Any(p => p == s.User.Email)).ToList();
            }
            if (DocNotExsist != null && DocNotExsist.Count > 0)
            {
                await _context.Doctors.AddRangeAsync(DocNotExsist);
                await _context.SaveChangesAsync();
            }
            if (DocExsist != null && DocExsist.Count > 0)
            {
                log = DocExsist.Select(x => new FailToImportDoctorsLog
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    AppointmentCount = x.AppointmentCount,
                    Credentials = x.Credentials,
                    DoctorStatus = x.DoctorStatus,
                    GenderId = x.User.GenderId,
                    LocationAddress = x.LocationAddress,
                    LocationCity = x.LocationCity,
                    LocationCode = x.LocationCode,
                    LocationName = x.LocationName,
                    LocationState = x.LocationState,
                    LocationZip = x.LocationZip,
                    Networks = x.Networks,
                    NPI = x.NPI,
                    PracticeName = x.PracticeName,
                    ProviderType = x.ProviderType,
                    Type = x.Type,
                    WebsiteURL = x.WebsiteURL,
                    CreatedBy = x.CreatedBy.Value,
                    CreatedOn= x.CreatedOn.Value
                }).ToList() ;

                await _context.FailToImportDoctorsLog.AddRangeAsync(log);
                await _context.SaveChangesAsync();
            }
                return log;
        }

        public async Task<IEnumerable<Doctor>> GetAll(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == true &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User.Gender)
                  .Include(x => x.Status)
                  .Include(x => x.Practice)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == true)
                    .Include(x => x.User.Gender)
                  .Include(x => x.Status)
                  .Include(x => x.Practice)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
        }

        public async Task<int> GetAllPageCount(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == true &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User.Gender)
                  .Include(x => x.Status)
                    .CountAsync();
            }
            else
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == true).Include(x => x.User.Gender)
                  .Include(x => x.Status).CountAsync();

            }
        }

        public async Task<IEnumerable<Doctor>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User).Include(x => x.Practice).Include(x => x.User.Gender).Include(x => x.User.Role)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false)
                    .Include(x => x.User).Include(x => x.Practice).Include(x => x.User.Gender).Include(x => x.User.Role)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
        }

        public async Task<int> GetAllInActivePageCount(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User).Include(x => x.Practice).Include(x => x.User.Gender).Include(x => x.User.Role)
                    .CountAsync();
            }
            else
            {
                return await _context.Doctors.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false)
                    .Include(x => x.User).Include(x => x.Practice).Include(x => x.User.Gender).Include(x => x.User.Role)
                    .CountAsync();

            }
        }
    }
}
