using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.AssignDoctors;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.SharedViewModels;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Service.AssignDoctors
{
   public class AssignDoctorService
    {
        private readonly IAssignDoctorRepository _repo;

        private readonly IMapper _mapper;
        public AssignDoctorService(IAssignDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorAssignedViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<DoctorAssignedViewModel>> GetAllDoctorAssigned(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllDoctorAssigned(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }

        public async Task<DoctorAssignedViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<DoctorAssignedViewModel>(data);
            return result;
        }

        public async Task<DoctorAssigned> Add(DoctorAssignedViewModel model)
        {
            var data = _mapper.Map<DoctorAssigned>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<DoctorAssigned> Update(DoctorAssignedViewModel model)
        {
            var data = _mapper.Map<DoctorAssigned>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<DoctorAssigned> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

        public async Task<IEnumerable<DoctorAssignedViewModel>> GetByDoctorId(int id)
        {
            var data = await _repo.GetByDoctorId(id);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        
        
        public async Task<IEnumerable<DoctorAssignedViewModel>> GetBySalePersonIdPage(int PageSize,int PageNumber,string Search ,int SalePersonId)
        {
            
            var data = await _repo.GetBySalePersonIdPage(PageSize, PageNumber, Search,SalePersonId);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        public async Task<int> GetCountBySalePersonId( string search, int salePersonId)
        {

            return await _repo.GetCountBySalePersonId( search, salePersonId);
            
        }
        public async Task<bool> DoctorIdExist(int doctorId)
        {
           return await _repo.DoctorIdExist(doctorId);
        }

        public async Task<IEnumerable<DoctorViewModel>> ContectedDoctorsPage(int PageSize, int PageNumber, string Search, int UserId)
        {
            var data = await _repo.ContectedDoctorsPage(PageSize, PageNumber, Search, UserId);
            var result = _mapper.Map<IEnumerable<DoctorViewModel>>(data);
            return result;
        }
        public async Task<int> ContectedDoctorsCount(string search, int UserId)
        {

            return await _repo.ContectedDoctorsCount(search, UserId);

        }
        public async Task<IEnumerable<DoctorAssignedViewModel>> ContectedDoctorsReport(bool NotesRequired)
        {
            var data = await _repo.ContectedDoctorsReport(NotesRequired);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<DoctorAssignedViewModel>> AllDoctorsReport(bool NotesRequired)
        {
            var data = await _repo.AllDoctorsReport(NotesRequired);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
    }
}
