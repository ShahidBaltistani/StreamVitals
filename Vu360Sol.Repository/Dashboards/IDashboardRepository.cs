using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.RequestDemoes;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Dashboards
{
   public interface IDashboardRepository
    {
        Task<User> Approve(int Id);
        Task<IEnumerable<User>> GetAllInActive(int PageSize, int PageNumber, string Search, int days);
        Task<int> GetAllPageCountInActiveUser(string Search, int days);
        Task<IEnumerable<RequestDemo>> GetRequestDemo(int PageSize, int PageNumber, string Search, int days);
        Task<int> GetAllPageCountRequestDemo(string Search, int days);
        Task<IEnumerable<Visitor>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search, int Days);
        Task<int> GetAllPageCountForLearning(string Search, int Days);
        Task<IEnumerable<Visitor>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search, int Days);
        Task<int> GetAllPageCountForStarting(string Search, int Days);
    }
}
