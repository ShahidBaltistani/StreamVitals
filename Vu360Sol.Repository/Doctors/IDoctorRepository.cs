using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Logs;

namespace Vu360Sol.Repository.Doctors
{
   public interface IDoctorRepository
    {
        Task<Doctor> Get(int Id);
        Task<IEnumerable<Doctor>> GetAll();
        Task<IEnumerable<Doctor>> GetAll(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<Doctor> Add(Doctor model);
        Task<Doctor> Update(Doctor model);
        Task<Doctor> Delete(int id);
        Task<Doctor> UpdateDoctorStatus(int DoctorID, int Status);
        Task<IEnumerable<Doctor>> GetAllInActive(int PageSize, int PageNumber, string Search);
        Task<int> GetAllInActivePageCount(string Search);
        Task<Doctor> DoctorApproval(int Id);

        Task<IEnumerable<FailToImportDoctorsLog>> ImportDoctors(IEnumerable<Doctor> model);
    }
}
