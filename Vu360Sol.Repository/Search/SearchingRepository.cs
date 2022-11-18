using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Searching;

namespace Vu360Sol.Repository.Search
{
    public class SearchingRepository : ISearchingRepository
    {
        private readonly VU360SolContext _context;
        public SearchingRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Searching>> Search(int PageSize, int PageNumber, string Search)
        {
            return await _context.Searching.Where
                (x => x.Description.ToLower().Contains(Search.ToLower()) || x.Keyword.ToLower().Contains(Search.ToLower()) || x.Paragraph.ToLower().Contains(Search.ToLower())
                )
                .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                .ToListAsync();
        }
        public async Task<int> GetAllPageCount(string Search)
        {
            return await _context.Searching.Where
                (x => x.Description.ToLower().Contains(Search.ToLower()) || x.Keyword.ToLower().Contains(Search.ToLower()) || x.Paragraph.ToLower().Contains(Search.ToLower())
                ).CountAsync();
        }
        public Task<Searching> Add(Searching model)
        {
            throw new NotImplementedException();
        }

        public Task<Searching> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Searching> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Searching>> GetAll()
        {
            throw new NotImplementedException();
        }

        
        public Task<Searching> Update(Searching model)
        {
            throw new NotImplementedException();
        }
    }
}
