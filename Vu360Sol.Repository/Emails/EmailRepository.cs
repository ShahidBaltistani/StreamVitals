using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Emails;

namespace Vu360Sol.Repository.Emails
{
   public class EmailRepository : IEmailRepository
    {
        private readonly VU360SolContext _context;
        public EmailRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<SentEmail>> GetAll()
        {
           var data = await _context.SentEmails
                  .ToListAsync();
            return data;
        }
        public async Task<SentEmail> Get(int Id)
        {
            return (await _context.SentEmails
                .FirstOrDefaultAsync(d => d.Id == Id));
        }
        public async Task<SentEmail> Add(SentEmail model)
        {
            await _context.SentEmails.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<SentEmail> Delete(int id)
        {
            var data = await _context.SentEmails.FindAsync(id);
            _context.SentEmails.Remove(data);
           await _context.SaveChangesAsync();
            return data;
        }
        public async Task<SentEmail> Update(SentEmail model)
        {
            var doc = _context.SentEmails.Attach(model);
            doc.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
