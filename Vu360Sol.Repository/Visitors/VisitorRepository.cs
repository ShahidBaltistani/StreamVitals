using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Visitors
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VU360SolContext _context;
        public VisitorRepository(VU360SolContext context)
        {
            _context = context;
        }
        public async Task<Visitor> AddVisitor(Visitor model)
        {
            await _context.Visitors.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        

        public async Task<IEnumerable<Visitor>> GetAllVisitor()
        {
            return (await _context.Visitors.ToListAsync());
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorForLearning()
        {
            return (await _context.Visitors.Where(x=>x.VisitorPurposeId==3).ToListAsync());
        }
        public async Task<IEnumerable<Visitor>> GetAllVisitorForStarting()
        {
            return (await _context.Visitors.Where(x => x.VisitorPurposeId == 4).ToListAsync());
        }
        public async Task<IEnumerable<Visitor>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 && 
                (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                )
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 3)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 &&
                (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                )
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 4)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
        }
        public async Task<int> GetAllPageCountForLearning(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 3 &&
                (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                )
                    .CountAsync();
            }
            else
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 3)
                         .CountAsync();
            }
        }

        public async Task<int> GetAllPageCountForStarting(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 4 &&
                (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                )
                    .CountAsync();
            }
            else
            {
                return await _context.Visitors.Where(x => x.VisitorPurposeId == 4)
                         .CountAsync();
            }
        }
    }
}
