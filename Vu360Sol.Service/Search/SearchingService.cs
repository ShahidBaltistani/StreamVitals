using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Search;
using Vu360Sol.ViewModel.Search;

namespace Vu360Sol.Service.Search
{
    public class SearchingService
    {
        private readonly ISearchingRepository _repo;

        private readonly IMapper _mapper;
        public SearchingService(ISearchingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SearchingViewModel>> Search(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.Search(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<SearchingViewModel>>(data);
            return result;
        }
        public async Task<int> GetAllPageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }
    }
}
