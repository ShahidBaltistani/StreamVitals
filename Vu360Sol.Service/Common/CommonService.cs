using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.EmailConfiguration;
using Vu360Sol.ViewModel.Common;

namespace Vu360Sol.Service.EmailConfiguration
{
   public class CommonService
    {
        private readonly ICommonRepository _repo;

        private readonly IMapper _mapper;
        public CommonService(ICommonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<EmailConfigurationViewModel> Get()
        {
            var data = await _repo.Get();
            var result = _mapper.Map<EmailConfigurationViewModel>(data);
            return result;
        }
    }
}
