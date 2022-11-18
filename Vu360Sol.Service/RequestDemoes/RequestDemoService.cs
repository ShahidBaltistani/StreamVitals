using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.RequestDemoes;
using Vu360Sol.ViewModel.RequestDemoes;
using VU360Sol.Entities.RequestDemoes;

namespace Vu360Sol.Service.RequestDemoes
{
   public class RequestDemoService
    {
        private readonly IRequestDemoRepository _repo;

        private readonly IMapper _mapper;
        public RequestDemoService(IRequestDemoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RequestDemoViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<RequestDemoViewModel>>(data);
            return result;
        }

        public async Task<IEnumerable<RequestDemoViewModel>> GetAll(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAll(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<RequestDemoViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }

        public async Task<RequestDemoViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<RequestDemoViewModel>(data);
            return result;
        }

        public async Task<RequestDemo> Add(RequestDemoViewModel model)
        {
            var data = _mapper.Map<RequestDemo>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<RequestDemo> Update(RequestDemoViewModel model)
        {
            var data = _mapper.Map<RequestDemo>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<RequestDemo> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

        public async Task<IEnumerable<RequestDemoViewModel>> GetAllByDoctorId(int UserId)
        {
            var data = await _repo.GetAllByDoctorId(UserId);
            var result = _mapper.Map<IEnumerable<RequestDemoViewModel>>(data);
            return result;
        }
   }
}
