using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Visitors
{
    public interface IVisitorRepository
    {
        Task<Visitor> AddVisitor(Visitor model);
        Task<IEnumerable<Visitor>> GetAllVisitor();
        Task<IEnumerable<Visitor>> GetAllVisitorForLearning();
        Task<IEnumerable<Visitor>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCountForLearning(string Search);
        Task<IEnumerable<Visitor>> GetAllVisitorForStarting();
        Task<IEnumerable<Visitor>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCountForStarting(string Search);
    }
}
