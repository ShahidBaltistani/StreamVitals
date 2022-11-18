using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Repository.SalePersons
{
   public class SalePersonRepository : ISalePersonRepository
    {
        private readonly VU360SolContext _context;
        public SalePersonRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<SalePerson>> GetAll()
        {
            return (await _context.SalePersons
                 .Where(x => x.IsDeleted == false && x.IsActive == true)
                 .Include(x=>x.User.Gender)
                  .ToListAsync());
        }
        public async Task<SalePerson> Get(int Id)
        {
            return (await _context.SalePersons
                 .Include(x => x.User.Gender)
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<SalePerson> Add(SalePerson model)
        {

            await _context.SalePersons.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<SalePerson> Delete(int id)
        {
            var data = await _context.SalePersons.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<SalePerson> Update(SalePerson model)
        {
            model.IsActive = true;
            model.User.IsActive = true;
            _context.Set<SalePerson>().Update(model);
           // var doc = _context.SalePersons.Attach(model);
           // doc.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<DoctorAssigned> AddDoctorAssigned(DoctorAssigned model)
        {
        //   model.AssignedDate = DateTime.Now;//DateTime.Now
            await _context.AddAsync(model);
           await _context.SaveChangesAsync();
            return model;
        }

      

        public async Task<IEnumerable<DoctorAssigned>> GetDoctorAssignedList()
        {
            var data = await _context.DoctorAssigned.Include(x => x.Doctor.User.Gender).Include(x => x.SalePerson.User.Gender).Include(x=>x.Note).OrderByDescending(x=>x.AssignedDate).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Gender>> GetAllGender()
        {
            return (await _context.Genders.ToListAsync());
        }

        public async Task<IEnumerable<SalePerson>> GetAllInActive()
        {
            return (await _context.SalePersons.Include(x=>x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
                 .Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false).ToListAsync());
        }
        public async Task<SalePerson> SalePersonApproval(int Id)
        {
            var data = await _context.SalePersons.Include(x => x.User).Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                data.IsActive = true;
                data.User.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<IEnumerable<SalePerson>> GetAll(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == true &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                        .Include(x => x.User.Gender)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == true)
                    .Include(x => x.User.Gender)
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
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == true &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User.Gender)
                    .CountAsync();
            }
            else
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == true).Include(x => x.User.Gender).CountAsync();

            }
        }

        public async Task<IEnumerable<SalePerson>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            //return (await _context.Doctors.Include(x => x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
            //.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false).ToListAsync());

            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false)
                    .Include(x => x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
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
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false &&
                        (x.User.FirstName.ToLower().Contains(Search.ToLower()) || x.User.LastName.ToLower().Contains(Search.ToLower()) || x.User.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
                    .CountAsync();
            }
            else
            {
                return await _context.SalePersons.Where(x => x.IsDeleted == false && x.IsActive == false && x.User.IsActive == false)
                    .Include(x => x.User).Include(x => x.User.Gender).Include(x => x.User.Role)
                    .CountAsync();

            }
        }
    }
}
