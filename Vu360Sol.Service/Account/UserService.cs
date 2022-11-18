using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vu360Sol.Repository.Account;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.Visitors;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Service.Account
{
   public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<UserViewModel> Login(string username, string password)
        {
            var user = await _repo.Login(username, password);
            var userVM = _mapper.Map<UserViewModel>(user);
            return userVM;
        }
        public async Task<User> Register(UserViewModel model)
        {
            var userM = _mapper.Map<User>(model);
            var user = await _repo.Register(userM);
            return user;
        }
        public async Task<bool> UsernameExist(UserViewModel user)
        {
            var userM = _mapper.Map<User>(user);
            var data = await _repo.UsernameExist(userM);
            if (data)
                return true;

            return false;
        }
        public async Task<Visitor> AddVisitor(VisitorViewModel model)
        {
            var userM = _mapper.Map<Visitor>(model);
            var user = await _repo.AddVisitor(userM);
            return user;
        }
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var data = await _repo.GetAllUsers();
            var result = _mapper.Map<IEnumerable<UserViewModel>>(data);
            return result;
        }
        public async Task<UserViewModel> Approve(int Id)
        {
            var data = await _repo.Approve(Id);
            var result = _mapper.Map<UserViewModel>(data);
            return result;
        }


        public async Task<bool> ChangePassword(ChangePasswordViewModel model, string newPassword)
        {
            var user = _mapper.Map<User>(model);
            var password = await _repo.ChangePassword(user, newPassword);
            if (password)
                return true;

            return false;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllInActive(int PageSize, int PageNumber, string Search)
        {
            var data = await _repo.GetAllInActive(PageSize, PageNumber, Search);
            var result = _mapper.Map<IEnumerable<UserViewModel>>(data);
            return result;
        }

        public async Task<int> GetAllInActivePageCount(string search)
        {
            return await _repo.GetAllPageCount(search);
        }

        public async Task<User> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = _mapper.Map<User>(model);
            var data = await _repo.ForgotPassword(user);
            return data;
        }

        public async Task<DoctorViewModel> GetDoctorByUserId(int Id)
        {
            var data = await _repo.GetDoctorByUserId(Id);
            var result = _mapper.Map<DoctorViewModel>(data);
            return result;
        }
    }
}
