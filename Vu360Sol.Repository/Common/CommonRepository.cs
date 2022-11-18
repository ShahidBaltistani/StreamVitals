using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities.Common;

namespace Vu360Sol.Repository.EmailConfiguration
{
    public class CommonRepository : ICommonRepository
    {
        private readonly VU360SolContext _context;
        public CommonRepository(VU360SolContext context)
        {
            this._context = context;
        }
        public async Task<EmailConfigurations> Get()
        {
            return (await _context.EmailConfigurations.FirstOrDefaultAsync());
        }
    }
}
