using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Repository.SalePersons
{
   public interface ISalePersonRepository
    {
        Task<SalePerson> Get(int Id);
        Task<IEnumerable<SalePerson>> GetAll();
        Task<IEnumerable<SalePerson>> GetAll(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<SalePerson> Add(SalePerson model);
        Task<SalePerson> Update(SalePerson model);
        Task<SalePerson> Delete(int id);
        Task<DoctorAssigned> AddDoctorAssigned(DoctorAssigned model);
     
        Task<IEnumerable<DoctorAssigned>> GetDoctorAssignedList();
        Task<IEnumerable<SalePerson>> GetAllInActive(int PageSize, int PageNumber, string Search);
        Task<int> GetAllInActivePageCount(string Search);
        Task<SalePerson> SalePersonApproval(int Id);

        // For Gender
        Task<IEnumerable<Gender>> GetAllGender();

    }
}
