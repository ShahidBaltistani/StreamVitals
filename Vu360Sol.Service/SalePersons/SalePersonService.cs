using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.SalePersons;
using Vu360Sol.ViewModel.Common;
using Vu360Sol.ViewModel.SalePersons;
using VU360Sol.Entities.SalePersons;

namespace Vu360Sol.Service.SalePersons
{
   public class SalePersonService
    {
        private readonly ISalePersonRepository _repo;

        private readonly IMapper _mapper;
        public SalePersonService(ISalePersonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SalePersonViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<SalePersonViewModel>>(data);
            return result;
        }

        public async Task<IEnumerable<SalePersonViewModel>> GetAll(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAll(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<SalePersonViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }
        public async Task<IEnumerable<SalePersonViewModel>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllInActive(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<SalePersonViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllInActivePageCount(string search)
        {
            return await _repo.GetAllInActivePageCount(search);
        }

        public async Task<SalePersonViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<SalePersonViewModel>(data);
            return result;
        }

        public async Task<SalePerson> Add(SalePersonViewModel model)
        {
            var data = _mapper.Map<SalePerson>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<SalePerson> Update(SalePersonViewModel model)
        {
            var data = _mapper.Map<SalePerson>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<SalePerson> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

        public async Task<DoctorAssigned> AddDoctorAssigned(DoctorAssignedViewModel model)
        {
            var data = _mapper.Map<DoctorAssigned>(model);
            var result = await _repo.AddDoctorAssigned(data);
            return result;
        }

    

        public async Task<IEnumerable<DoctorAssignedViewModel>> GetDoctorAssignedList()
        {
            var data = await _repo.GetDoctorAssignedList();
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }

        public async Task<IEnumerable<GenderViewModel>> GetAllGender()
        {
            var data = await _repo.GetAllGender();
            var result = _mapper.Map<IEnumerable<GenderViewModel>>(data);
            return result;
        }

        public async Task<SalePerson> SalePersonApproval(int Id)
        {
            var data = await _repo.SalePersonApproval(Id);
            var result = _mapper.Map<SalePerson>(data);
            return result;
        }
    }
}
