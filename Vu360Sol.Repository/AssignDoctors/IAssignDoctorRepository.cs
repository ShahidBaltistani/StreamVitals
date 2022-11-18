using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Doctors;
using Vu360Sol.ViewModel.SharedViewModels;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Repository.AssignDoctors
{
   public interface IAssignDoctorRepository
    {
        Task<DoctorAssigned> Get(int Id);
        Task<IEnumerable<DoctorAssigned>> GetAll();
        Task<IEnumerable<DoctorAssigned>> GetAllDoctorAssigned(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<DoctorAssigned> Add(DoctorAssigned model);
        Task<DoctorAssigned> Update(DoctorAssigned model);
        Task<DoctorAssigned> Delete(int id);

        Task<IEnumerable<DoctorAssigned>> GetByDoctorId(int Id);
        Task<IEnumerable<DoctorAssigned>> GetBySalePersonId(int Id);
        Task<bool> DoctorIdExist(int doctorId);
        Task<IEnumerable<Doctor>> ContectedDoctorsPage(int PageSize, int PageNumber, string Search, int UserId);
        Task<int> ContectedDoctorsCount(string Search, int UserId);
        Task<IEnumerable<DoctorAssigned>> GetBySalePersonIdPage(int PageSize, int PageNumber, string Search, int SalePersonId);
        Task<int> GetCountBySalePersonId(string search, int salePersonId);

        Task<IEnumerable<DoctorAssigned>> ContectedDoctorsReport(bool NotesRequired);

        Task<IEnumerable<DoctorAssigned>> AllDoctorsReport(bool NotesRequired);

    }
}
