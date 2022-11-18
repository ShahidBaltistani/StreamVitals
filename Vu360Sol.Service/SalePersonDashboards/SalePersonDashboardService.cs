using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Dashboards;
using Vu360Sol.Repository.SalePersonDashboards;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.Service.SalePersonDashboards
{
   public class SalePersonDashboardService
    {
        private readonly ISalePersonDashboardRepository _repo;
        private readonly IMapper _mapper;

        public SalePersonDashboardService(ISalePersonDashboardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DoctorViewModel>> ContactedDoctors(int PageSize, int PageNumber, string Search, int days, int salePersonId)
        {
            var data = await _repo.ContactedDoctors(PageSize, PageNumber, Search, days, salePersonId);
            var result = _mapper.Map<IEnumerable<DoctorViewModel>>(data);
            return result;
        }

        public async Task<int> CountContactedDoctors(string search, int days, int salePersonId)
        {
            return await _repo.CountContactedDoctors(search, days, salePersonId);
        }

        public async Task<IEnumerable<DoctorAssignedViewModel>> DoctorAssignedList(int PageSize, int PageNumber, string Search, int days, int salePersonId)
        {
            var data = await _repo.DoctorAssignedList(PageSize, PageNumber, Search, days, salePersonId);
            var result = _mapper.Map<IEnumerable<DoctorAssignedViewModel>>(data);
            return result;
        }
        public async Task<int> CountDoctorAssignedList(string search, int days,int salePerson)
        {
            return await _repo.CountDoctorAssignedList(search, days, salePerson);
        }

        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search, int Days)
        {
            var data = await _repo.GetAllVisitorForLearning(PageSize, PageNumber, Search, Days);
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search, int Days)
        {
            var data = await _repo.GetAllVisitorForStarting(PageSize, PageNumber, Search, Days);
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCountForLearning(string search, int Days)
        {
            return await _repo.GetAllPageCountForLearning(search, Days);
        }
        public async Task<int> GetAllPageCountForStarting(string search, int Days)
        {
            return await _repo.GetAllPageCountForStarting(search, Days);
        }

        public async Task<DoctorAssignedViewModel> GetById(int Id)
        {
            var data = await _repo.GetById(Id);
            var result = _mapper.Map<DoctorAssignedViewModel> (data);
            return result;
        }
    }
}
