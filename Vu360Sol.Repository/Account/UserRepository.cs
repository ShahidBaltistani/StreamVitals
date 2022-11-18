using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VU360Sol.Database;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Repository.Account
{
    public class UserRepository : IUserRepository
    {
        private readonly VU360SolContext _context;
        public UserRepository(VU360SolContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == username && x.Password == password && x.IsActive == true);
            if (user == null)
                return null;

            return user;
        }

        public async Task<User> Register(User model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UsernameExist(User user)
        {
            if (await _context.Users.AnyAsync(x => x.Email == user.Email && (user.Id >0 ? x.Id != user.Id : true)))
                return true;

            return false;
        }
        public async Task<Visitor> AddVisitor(Visitor model)
        {
            try
            {
                await _context.Visitors.AddAsync(model);
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {

            }
            return model;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return (await _context.Users
                 .Where(x => x.IsDeleted == false && x.IsActive == false && x.RoleId!=1).Include(x=>x.Role).Include(x => x.Gender).ToListAsync());
        }

        public async Task<User> Approve(int Id)
        {
            var data = await _context.Users.FindAsync(Id);
            if (data != null)
            {
                data.IsActive = true;
                await _context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<bool> ChangePassword(User model, string newPassword)
        {
            var data = await _context.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return false;
            }
            if (data.Password == model.Password)
            {
                data.Password = newPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<User>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false &&
                        (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.Gender)
                    .Include(x => x.Role)
                         .OrderByDescending(x => x.Id)
                         .Distinct()
                         .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                         .ToListAsync();
            }
            else
            {
                return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false)
               .Include(x => x.Gender)
               .Include(x => x.Role)
                    .OrderByDescending(x => x.Id)
                    .Distinct()
                    .Skip((PageNumber - 1) * PageSize).Take(PageSize)
                    .ToListAsync();
            }
        }

        public async Task<int> GetAllPageCount(string Search)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false &&
                        (x.FirstName.ToLower().Contains(Search.ToLower()) || x.LastName.ToLower().Contains(Search.ToLower()) || x.Email.ToLower().Contains(Search.ToLower()))
                        )
                    .Include(x => x.Gender)
                    .Include(x => x.Role)
                    .CountAsync();
            }
            else
            {
                return await _context.Users.Where(x => x.IsDeleted == false && x.IsActive == false)
                    .Include(x => x.Gender)
                    .Include(x => x.Role)
                  .CountAsync();

            }
        }

        public async Task<User> ForgotPassword(User model)
        {
            var data = await _context.Users.Where(x => x.Email == model.Email && x.IsDeleted == false).FirstOrDefaultAsync();
            return data;

        }

        public async Task<Doctor> GetDoctorByUserId(int UserId)
        {
            var dr = new Doctor();
            var doctor = await _context.Doctors.Where(x => x.UserId == UserId && x.IsDeleted == false).FirstOrDefaultAsync();
            if (doctor != null)
            {
                if (doctor.IsActive == true)
                {
                    return doctor;

                }
            }
            return dr;
        }
    }
}
