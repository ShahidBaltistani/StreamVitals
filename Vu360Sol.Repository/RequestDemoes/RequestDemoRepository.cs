using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.RequestDemoes;

namespace Vu360Sol.Repository.RequestDemoes
{
   public class RequestDemoRepository : IRequestDemoRepository
    {
        private readonly VU360SolContext _context;
        public RequestDemoRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<RequestDemo>> GetAll()
        {
            return (await _context.RequestDemoes
                 .Where(x => x.IsDeleted == false && x.IsActive == true)
                  .ToListAsync());
        }
        public async Task<RequestDemo> Get(int Id)
        {
            return (await _context.RequestDemoes.Include(x=>x.Doctor)
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<RequestDemo> Add(RequestDemo model)
        {
            await _context.RequestDemoes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<RequestDemo> Delete(int id)
        {
            var data = await _context.RequestDemoes.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<RequestDemo> Update(RequestDemo model)
        {
            var doc = _context.RequestDemoes.Attach(model);
            doc.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<RequestDemo>> GetAllByDoctorId(int UserId)
        {
            var doctorAgaintsUserId = await _context.Doctors.Where(x => x.UserId == UserId).FirstOrDefaultAsync();
            return (await _context.RequestDemoes
                 .Where(x => x.IsDeleted == false && x.IsActive == true && x.DoctorId==doctorAgaintsUserId.Id)
                  .ToListAsync());
        }

        public async Task<IEnumerable<RequestDemo>> GetAll(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true && 
                        (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                        )
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true)
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
                return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true &&
                        (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                        ).CountAsync();
            }
            else
            {
                return await _context.RequestDemoes.Where(x => x.IsDeleted == false && x.IsActive == true).CountAsync();

            }
        }
    }
}
