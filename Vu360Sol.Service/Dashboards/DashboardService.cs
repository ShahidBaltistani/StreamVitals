using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Dashboards;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.Service.Dashboards
{
   public class DashboardService
    {
        private readonly IDashboardRepository _repo;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Approve(int Id)
        {
            var data = await _repo.Approve(Id);
            var result = _mapper.Map<UserViewModel>(data);
            return result;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllInActive(int PageSize, int PageNumber, string Search, int days)
        {
            var data = await _repo.GetAllInActive(PageSize, PageNumber, Search, days);
            var result = _mapper.Map<IEnumerable<UserViewModel>>(data);
            return result;
        }

        public async Task<int> GetAllInActivePageCount(string search, int days)
        {
            return await _repo.GetAllPageCountInActiveUser(search, days);
        }

        public async Task<IEnumerable<RequestDemoViewModel>> GetRquestDemo(int PageSize, int PageNumber, string Search, int days)
        {
            var data = await _repo.GetRequestDemo(PageSize, PageNumber, Search, days);
            var result = _mapper.Map<IEnumerable<RequestDemoViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCountRequestDemo(string search, int days)
        {
            return await _repo.GetAllPageCountRequestDemo(search, days);
        }

        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search, int Days)
        {
            var data = await _repo.GetAllVisitorForLearning(PageSize, PageNumber, Search,Days);
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
    }
}
