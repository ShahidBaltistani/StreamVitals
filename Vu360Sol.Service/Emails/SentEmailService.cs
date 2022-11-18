using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Emails;
using Vu360Sol.ViewModel.Emails;
using VU360Sol.Entities.Emails;

namespace Vu360Sol.Service.Emails
{
   public class SentEmailService
    {
        private readonly IEmailRepository _repo;

        private readonly IMapper _mapper;
        public SentEmailService(IEmailRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SentEmailViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<SentEmailViewModel>>(data);
            return result;
        }

        public async Task<SentEmailViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<SentEmailViewModel>(data);
            return result;
        }

        public async Task<SentEmail> Add(SentEmailViewModel model)
        {
            var data = _mapper.Map<SentEmail>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<SentEmail> Update(SentEmailViewModel model)
        {
            var data = _mapper.Map<SentEmail>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<SentEmail> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }
    }
}
