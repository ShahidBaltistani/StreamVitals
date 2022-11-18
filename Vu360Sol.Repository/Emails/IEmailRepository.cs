using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Emails;

namespace Vu360Sol.Repository.Emails
{
    public interface IEmailRepository
    {
        Task<SentEmail> Get(int Id);
        Task<IEnumerable<SentEmail>> GetAll();
        Task<SentEmail> Add(SentEmail model);
        Task<SentEmail> Update(SentEmail model);
        Task<SentEmail> Delete(int id);
    }
}
