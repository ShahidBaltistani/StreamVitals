using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.SalePersons;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.SalePersonDashboards
{
   public interface ISalePersonDashboardRepository
    {
        Task<IEnumerable<Doctor>> ContactedDoctors(int PageSize, int PageNumber, string Search, int days, int salePersonId);
        Task<int> CountContactedDoctors(string Search, int days, int salePersonId);
        Task<IEnumerable<DoctorAssigned>> DoctorAssignedList(int PageSize, int PageNumber, string Search, int days, int salePersonId);
        Task<int> CountDoctorAssignedList(string Search, int days, int salePersonId);
        Task<IEnumerable<Visitor>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search, int Days);
        Task<int> GetAllPageCountForLearning(string Search, int Days);
        Task<IEnumerable<Visitor>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search, int Days);
        Task<int> GetAllPageCountForStarting(string Search, int Days);
        Task<DoctorAssigned> GetById(int Id);
    }
}
