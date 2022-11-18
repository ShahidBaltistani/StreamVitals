using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Visitors;
using Vu360Sol.ViewModel.Visitors;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Service.Visitors
{
   public class VisitorService
    {
        private readonly IVisitorRepository _repo;
        private readonly IMapper _mapper;
        public VisitorService(IVisitorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Visitor> AddVisitor(VisitorViewModel model)
        {
            var userM = _mapper.Map<Visitor>(model);
            var user = await _repo.AddVisitor(userM);
            return user;
        }
        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForLearning()
        {
            var data = await _repo.GetAllVisitorForLearning();
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForStarting()
        {
            var data = await _repo.GetAllVisitorForStarting();
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForLearning(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllVisitorForLearning(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<IEnumerable<VisitorViewModel>> GetAllVisitorForStarting(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllVisitorForStarting(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<VisitorViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCountForLearning(string search)
        {
            return await _repo.GetAllPageCountForLearning(search);
        }
        public async Task<int> GetAllPageCountForStarting(string search)
        {
            return await _repo.GetAllPageCountForStarting(search);
        }
    }
}
