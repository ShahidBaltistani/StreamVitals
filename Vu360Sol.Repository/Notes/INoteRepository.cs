using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Notes;

namespace Vu360Sol.Repository.Notes
{
   public interface INoteRepository
    {
        Task<Note> Get(int Id);
        Task<IEnumerable<Note>> GetAll();
        Task<Note> Add(Note model);
        Task<Note> Update(Note model);
        Task<Note> Delete(int id);
        Task<IEnumerable<Note>> GetAllNotesByRequestDemoId(int RequestDemoId);
    }
}
