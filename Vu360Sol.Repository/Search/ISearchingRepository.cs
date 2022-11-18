using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Searching;

namespace Vu360Sol.Repository.Search
{
    public interface ISearchingRepository
    {
        Task<IEnumerable<Searching>> Search(int PageSize, int PageNumber, string Search);
        Task<Searching> Get(int Id);
        Task<IEnumerable<Searching>> GetAll();
        Task<int> GetAllPageCount(string Search);
        Task<Searching> Add(Searching model);
        Task<Searching> Update(Searching model);
        Task<Searching> Delete(int id);
    }
}
