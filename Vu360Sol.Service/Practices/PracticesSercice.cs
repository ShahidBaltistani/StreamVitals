using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Practices;
using Vu360Sol.ViewModel.Practices;
using VU360Sol.Entities.Doctors;

namespace Vu360Sol.Service.Practices
{
   public class PracticesSercice
    {
        private readonly IPracticeRepository _repo;

        private readonly IMapper _mapper;
        public PracticesSercice(IPracticeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PracticeViewModel>> GetAllPractice()
        {
            var data = await _repo.GetAllPractice();
            var result = _mapper.Map<IEnumerable<PracticeViewModel>>(data);
            return result;
        }

        public async Task<IEnumerable<PracticeViewModel>> GetAll(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAll(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<PracticeViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }

        public async Task<PracticeViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<PracticeViewModel>(data);
            return result;
        }

        public async Task<Practice> Add(PracticeViewModel model)
        {
            var data = _mapper.Map<Practice>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<Practice> Update(PracticeViewModel model)
        {
            var data = _mapper.Map<Practice>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<Practice> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

    }
}
