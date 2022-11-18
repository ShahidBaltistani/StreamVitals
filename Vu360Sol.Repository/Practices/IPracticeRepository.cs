using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Doctors;

namespace Vu360Sol.Repository.Practices
{
   public interface IPracticeRepository
    {
        Task<Practice> Get(int Id);
        Task<IEnumerable<Practice>> GetAllPractice();
        Task<IEnumerable<Practice>> GetAll(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<Practice> Add(Practice model);
        Task<Practice> Update(Practice model);
        Task<Practice> Delete(int id);
    }
}
