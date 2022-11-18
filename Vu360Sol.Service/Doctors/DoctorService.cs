using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Doctors;
using Vu360Sol.ViewModel.Doctors;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Logs;

namespace Vu360Sol.Service.Doctors
{
   public class DoctorService
    {
        private readonly IDoctorRepository _repo;

        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DoctorViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<DoctorViewModel>>(data);
            return result;
        }

        public async Task<IEnumerable<DoctorViewModel>> GetAll(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAll(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<DoctorViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }

        public async Task<IEnumerable<DoctorViewModel>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllInActive(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<DoctorViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllInActivePageCount(string search)
        {
            return await _repo.GetAllInActivePageCount(search);
        }

        public async Task<DoctorViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<DoctorViewModel>(data);
            return result;
        }

        public async Task<Doctor> Add(DoctorViewModel model)
        {
            var data = _mapper.Map<Doctor>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<Doctor> Update(DoctorViewModel model)
        {
            var data = _mapper.Map<Doctor>(model);
            var result = await _repo.Update(data);
            return result;
        }
        public async Task<Doctor> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

        public async Task<Doctor> UpdateDoctorStatus(int DoctorID, int StatusId)
        {
            var result = await _repo.UpdateDoctorStatus(DoctorID, StatusId);
            return result;
        }

        public async Task<Doctor> DoctorApproval(int Id)
        {
            var data = await _repo.DoctorApproval(Id);
            var result = _mapper.Map<Doctor>(data);
            return result;
        }
        public async Task<IEnumerable<FailToImportDoctorsLog>> ImportDoctors(IEnumerable<DoctorViewModel> model)
        {
            var data = _mapper.Map<IEnumerable<Doctor>>(model);
            var result = await _repo.ImportDoctors(data);
            var viewModel =_mapper.Map<IEnumerable<FailToImportDoctorsLog>>(result);
            return viewModel;
        }
    }
}
