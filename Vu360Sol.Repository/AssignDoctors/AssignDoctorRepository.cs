using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.ViewModel.SharedViewModels;
using VU360Sol.Database;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Repository.AssignDoctors
{
   public class AssignDoctorRepository : IAssignDoctorRepository
    {
        private readonly VU360SolContext _context;
        public AssignDoctorRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<DoctorAssigned>> GetAll()
        {
            return (await _context.DoctorAssigned
                   .Include(x => x.Doctor.User.Gender)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
                  .Where(x=>x.IsDeleted == false)
                  .ToListAsync());
        }
        public async Task<IEnumerable<DoctorAssigned>> GetAllDoctorAssigned()
        {
            return (await _context.DoctorAssigned
                   .Include(x => x.Doctor.User.Gender)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
                  .Where(x => x.SalePersonId != null && x.IsDeleted == false)
                  .ToListAsync());
        }
        public async Task<DoctorAssigned> Get(int Id)
        {
            return (await _context.DoctorAssigned
                 .Include(x => x.Doctor.User.Gender)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
                  .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<DoctorAssigned> Add(DoctorAssigned model)
        {
            await _context.DoctorAssigned.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<DoctorAssigned> Delete(int id)
        {
            var data = await _context.DoctorAssigned.FindAsync(id);
            if (data != null)
            {
                var notes = await _context.Notes.FindAsync(data.NoteId);

                notes.IsDeleted = true;
                _context.Entry(notes).Property("IsDeleted").IsModified = true;
                data.IsDeleted = true;
                _context.Entry(data).Property("IsDeleted").IsModified = true;
                //_context.Remove(data);
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<DoctorAssigned> Update(DoctorAssigned model)
        {
            var doc = _context.DoctorAssigned.Attach(model);
            doc.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<DoctorAssigned>> GetByDoctorId(int Id)
        {
            var data = await _context.DoctorAssigned
                .Include(x => x.Doctor.User)
                .Include(x => x.SalePerson.User)
                .Include(x => x.Note)
                .Where(x => x.DoctorId == Id && x.IsDeleted == false)
                .OrderByDescending(x => x.AssignedDate).ToListAsync();
            return data;
        }  
        public async Task<IEnumerable<DoctorAssigned>> GetBySalePersonId(int Id)
        {
            
            var data = await _context.DoctorAssigned
                .Include(x => x.Doctor.User)
                .Include(x => x.SalePerson.User)
                .Include(x => x.Note)
                .Where(x => x.SalePerson.UserId == Id && x.IsDeleted == false)
                .OrderByDescending(x => x.AssignedDate)
                .ToListAsync();
            return data;
        }
        public async Task<int> GetCountBySalePersonId( string search, int salePersonId)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await _context.DoctorAssigned
                    .Where(x => x.SalePerson.UserId == salePersonId && x.IsDeleted == false &&
                    (x.Doctor.User.FirstName.ToLower().Contains(search.ToLower())
                     || x.Doctor.User.LastName.ToLower().Contains(search.ToLower())
                     || x.Doctor.User.Email.ToLower().Contains(search.ToLower()))
                    )
                    .CountAsync();
            }
            else
            {
                return await _context.DoctorAssigned
                   .Where(x => x.SalePerson.UserId == salePersonId && x.IsDeleted == false)
                   .CountAsync();
            }
        }
        public async Task<IEnumerable<DoctorAssigned>> GetBySalePersonIdPage(int PageSize, int PageNumber, string Search, int SalePersonId)
        {
            var assigned = new List<DoctorAssigned>();
            try
            {

                if (!string.IsNullOrEmpty(Search))
                {
                    assigned = await _context.DoctorAssigned
                     .Include(x => x.Doctor.User)
                     .Include(x => x.SalePerson.User)
                     .Include(x => x.Note)
                     .Where(x => x.SalePerson.UserId == SalePersonId && x.IsDeleted == false &&
                     (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                     ||  x.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                     || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                     )
                     .OrderByDescending(x => x.AssignedDate)
                     .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                     .ToListAsync();

                }
                else
                {
                    assigned = await _context.DoctorAssigned
                    .Include(x => x.Doctor.User)
                    .Include(x => x.Doctor.Practice)
                    .Include(x => x.SalePerson.User)
                    .Include(x => x.Note)
                    .Where(x => x.SalePerson.UserId == SalePersonId && x.IsDeleted == false)
                    .OrderByDescending(x => x.AssignedDate)
                    .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                    .ToListAsync();

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return assigned;
        }


        public async Task<bool> DoctorIdExist(int doctorId)
        {
            var data = await _context.DoctorAssigned
                .Where(x=>x.DoctorId == doctorId)
             .FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Doctor>> ContectedDoctorsPage(int PageSize, int PageNumber, string Search, int UserId)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && (UserId == 0 ? true : w.SalePerson.User.Id == UserId) && w.IsDeleted == false &&
                         (w.Doctor.User.FirstName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                         || w.Doctor.User.Email.ToLower().Contains(Search.ToLower()))
                         )
                         .OrderByDescending(x => x.AssignedDate)
                         .Include(x => x.Doctor.User.Gender)
                         .Include(x => x.Doctor.Practice)

                         .Select(x => x.Doctor)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && (UserId == 0 ? true : w.SalePerson.User.Id == UserId) && w.IsDeleted == false
                        )
                        .OrderByDescending(x => x.AssignedDate)
                        .Include(x => x.Doctor.User.Gender)
                        .Include(x => x.Doctor.Practice)

                        .Select(x => x.Doctor)
                        .Distinct()
                        .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .ToListAsync();
            }

        }
        public async Task<int> ContectedDoctorsCount(string Search, int UserId)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && (UserId == 0 ? true : w.SalePerson.User.Id == UserId) && w.IsDeleted == false &&
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
                return await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && (UserId == 0 ? true : w.SalePerson.User.Id == UserId) && w.IsDeleted == false
                        )
                        .OrderByDescending(x => x.AssignedDate)
                        .Include(x => x.Doctor.User.Gender)
                        .Select(x => x.Doctor)
                        .Distinct()
                        .CountAsync();
            }

        }
   

        public async Task<IEnumerable<DoctorAssigned>> GetAllDoctorAssigned(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.DoctorAssigned.Where(x => x.SalePersonId != null && x.IsDeleted == false && 
                        (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                        || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()) || x.SalePerson.User.FirstName.ToLower().Contains(Search.ToLower())
                        || x.SalePerson.User.LastName.ToLower().Contains(Search.ToLower()) || x.SalePerson.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.Doctor.User.Gender)
                    .Include(x => x.Doctor.Practice)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.DoctorAssigned.Where(x => x.SalePersonId != null && x.IsDeleted == false)
                    .Include(x => x.Doctor.User.Gender)
                    .Include(x => x.Doctor.Practice)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
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
                return await _context.DoctorAssigned.Where(x => x.SalePersonId != null && x.IsDeleted == false &&
                       (x.Doctor.User.FirstName.ToLower().Contains(Search.ToLower()) || x.Doctor.User.LastName.ToLower().Contains(Search.ToLower())
                        || x.Doctor.User.Email.ToLower().Contains(Search.ToLower()) || x.SalePerson.User.FirstName.ToLower().Contains(Search.ToLower())
                        || x.SalePerson.User.LastName.ToLower().Contains(Search.ToLower()) || x.SalePerson.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.Doctor.User.Gender)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note)
                    .CountAsync();
            }
            else
            {
                return await _context.DoctorAssigned.Where(x => x.SalePersonId != null && x.IsDeleted == false).Include(x => x.Doctor.User.Gender)
                   .Include(x => x.SalePerson.User.Gender)
                  .Include(x => x.Note).CountAsync();

            }
        }

        public async Task<IEnumerable<DoctorAssigned>> ContectedDoctorsReport(bool NotesRequired)
        {
            var data = new List<DoctorAssigned>();
            if (NotesRequired)
            {
                data = await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && w.IsDeleted == false)
                             .OrderByDescending(x => x.AssignedDate)
                             .Include(x => x.Doctor.User.Gender)
                             .Include(x => x.Note)
                             .Include(x => x.Doctor.Practice)
                             .ToListAsync();
            }
            else
            {
               var doc= await _context.DoctorAssigned.Where(w => w.NoteId != null && w.Doctor.IsDeleted == false && w.IsDeleted == false)
                             .OrderByDescending(x => x.AssignedDate)
                             .Include(x => x.Doctor.User.Gender)
                             .Include(x => x.Doctor.Practice)
                             .Select(x=> x.Doctor)
                             .Distinct()
                             .ToListAsync();
                if(doc.Count> 0)
                {
                    data = doc.Select(x => new DoctorAssigned
                    {
                        Id = x.Id,
                        DoctorId = x.Id,
                        Doctor = x
                    }).ToList();
                }
                
            }
            return data;

        }
        public async Task<IEnumerable<DoctorAssigned>> AllDoctorsReport(bool NotesRequired)
        {
            var data = new List<DoctorAssigned>();
            if (NotesRequired)
            {
                data = await _context.DoctorAssigned.Where(w => w.Doctor.IsDeleted == false && w.IsDeleted == false)
                         .OrderByDescending(x => x.AssignedDate)
                         .Include(x => x.Doctor.User.Gender)
                         .Include(x => x.Doctor.Practice)
                         .Include(x => x.Note)
                         .ToListAsync();
            }
            else
            {
                var doc = await _context.DoctorAssigned.Where(w => w.Doctor.IsDeleted == false && w.IsDeleted == false)
                                         .OrderByDescending(x => x.AssignedDate)
                                         .Include(x => x.Doctor.User.Gender)
                                         .Include(x => x.Doctor.Practice)
                                         .Select(x => x.Doctor)
                                          .Distinct()
                                         .ToListAsync();
                if (doc.Count > 0)
                {
                    data = doc.Select(x => new DoctorAssigned
                    {
                        Id = x.Id,
                        DoctorId= x.Id,
                        Doctor = x
                    }).ToList();
                }
            }
            return data;

        }

    }
}
