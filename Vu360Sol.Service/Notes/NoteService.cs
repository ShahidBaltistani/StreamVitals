using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Notes;
using Vu360Sol.ViewModel.Notes;
using VU360Sol.Entities.Notes;

namespace Vu360Sol.Service.Notes
{
   public class NoteService
    {
        private readonly INoteRepository _repo;

        private readonly IMapper _mapper;
        public NoteService(INoteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteViewModel>> GetAll()
        {
            var data = await _repo.GetAll();
            var result = _mapper.Map<IEnumerable<NoteViewModel>>(data);
            return result;
        }

        public async Task<NoteViewModel> Get(int id)
        {
            var data = await _repo.Get(id);
            var result = _mapper.Map<NoteViewModel>(data);
            return result;
        }

        public async Task<Note> Add(NoteViewModel model)
        {
            var data = _mapper.Map<Note>(model);
            var result = await _repo.Add(data);
            return result;
        }

        public async Task<Note> Update(NoteViewModel model)
        {
            var data = _mapper.Map<Note>(model);
            var result = await _repo.Update(data);
            return result;
        }

        public async Task<Note> Delete(int id)
        {
            var result = await _repo.Delete(id);
            return result;
        }

        public async Task<IEnumerable<NoteViewModel>> GetAllNotesByRequestDemoId(int RequestDemoId)
        {
            var data = await _repo.GetAllNotesByRequestDemoId(RequestDemoId);
            var result = _mapper.Map<IEnumerable<NoteViewModel>>(data);
            return result;
        }
    }
}
