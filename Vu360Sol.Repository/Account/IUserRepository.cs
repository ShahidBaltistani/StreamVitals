using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Account
{
   public interface IUserRepository
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User model);
        Task<bool> UsernameExist(User model);
        Task<Visitor> AddVisitor(Visitor model);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> Approve(int Id);
        Task<bool> ChangePassword(User model, string newPassword);
        Task<IEnumerable<User>> GetAllInActive(int PageSize, int PageNumber, string Search);
        Task<int> GetAllPageCount(string Search);
        Task<User> ForgotPassword(User model);
        Task<Doctor> GetDoctorByUserId(int UserId);
    }
}
