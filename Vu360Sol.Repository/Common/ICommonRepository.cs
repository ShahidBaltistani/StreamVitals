using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities.Common;
namespace Vu360Sol.Repository.EmailConfiguration
{
    public interface ICommonRepository
    {
        Task<EmailConfigurations> Get();
    }
}
