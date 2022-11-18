using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Notes;

namespace Vu360Sol.Repository.Notes
{
   public class NoteRepository : INoteRepository
    {
        private readonly VU360SolContext _context;
        public NoteRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Note>> GetAll()
        {
            return (await _context.Notes
                .Where(x => x.IsDeleted == false && x.IsActive == true)
                  .ToListAsync());
        }
        public async Task<Note> Get(int Id)
        {
            return (await _context.Notes
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<Note> Add(Note model)
        {
            await _context.Notes.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<Note> Delete(int id)
        {
            var data = await _context.Notes.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }
        public async Task<Note> Update(Note model)
        {
            var doc = _context.Notes.Attach(model);
            doc.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<Note>> GetAllNotesByRequestDemoId(int RequestDemoId)
        {
            return (await _context.Notes
                .Where(x => x.IsDeleted == false && x.IsActive == true && x.ReferenceId==RequestDemoId && x.RefrenceTableId==4)
                  .ToListAsync());
        }
    }
}
