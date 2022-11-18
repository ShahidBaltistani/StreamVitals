using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.RequestDemoes;

namespace Vu360Sol.Repository.RequestDemoes
{
   public interface IRequestDemoRepository
    {
        Task<RequestDemo> Get(int Id);
        Task<IEnumerable<RequestDemo>> GetAll();
        Task<IEnumerable<RequestDemo>> GetAll(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<IEnumerable<RequestDemo>> GetAllByDoctorId(int UserId);
        Task<RequestDemo> Add(RequestDemo model);
        Task<RequestDemo> Update(RequestDemo model);
        Task<RequestDemo> Delete(int id);
    }
}
