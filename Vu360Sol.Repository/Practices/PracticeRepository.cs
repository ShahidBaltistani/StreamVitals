using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Doctors;

namespace Vu360Sol.Repository.Practices
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly VU360SolContext _context;
        public PracticeRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<Practice> Add(Practice model)
        {
            model.CreatedOn = DateTime.Now;
            await _context.Practices.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Practice> Delete(int id)
        {
            var data = await _context.Practices.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<Practice> Get(int Id)
        {
            return (await _context.Practices.Where(x=>x.IsActive == true && x.IsDeleted == false)
               .FirstOrDefaultAsync(d => d.Id == Id));
        }

        public async Task<IEnumerable<Practice>> GetAllPractice()
        {
            return (await _context.Practices
                 .Where(x => x.IsDeleted == false && x.IsActive == true)
                  .ToListAsync());
        }

        public async Task<Practice> Update(Practice model)
        {
            var doc = _context.Practices.Attach(model);
            doc.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Practice>> GetAll(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Practices.Where(w => w.IsActive == true && w.IsDeleted == false &&
                         (w.Name.ToLower().Contains(Search.ToLower())
                        )
                         )
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Practices.Where(w =>w.IsActive == true && w.IsDeleted == false
                        )
                        .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                        .ToListAsync();
            }

        }
        public async Task<int> GetAllPageCount(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Practices.Where(w => w.IsActive == true && w.IsDeleted == false &&
                         (w.Name.ToLower().Contains(Search.ToLower())
                         )
                         )
                         .CountAsync();
            }
            else
            {
                return await _context.Practices.Where(w => w.IsActive == true && w.IsDeleted == false
                        )
                        .CountAsync();
            }

        }
    }
}
