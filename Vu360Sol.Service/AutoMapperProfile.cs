using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Common;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.Logs;
using Vu360Sol.ViewModel.Notes;
using Vu360Sol.ViewModel.Practices;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.Search;
using Vu360Sol.ViewModel.Visitors;
using VU360Sol.Entities;
using VU360Sol.Entities.Account;
using VU360Sol.Entities.Common;
using VU360Sol.Entities.Doctors;
using VU360Sol.Entities.Logs;
using VU360Sol.Entities.Notes;
using VU360Sol.Entities.RequestDemoes;
using VU360Sol.Entities.SalePersons;
using VU360Sol.Entities.Searching;
using VU360Sol.Entities.Visitors;

namespace Vu360Sol.Service
{
   public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();

            CreateMap<Doctor, DoctorViewModel>();
            CreateMap<DoctorViewModel, Doctor>(); 
            

            CreateMap<SalePerson, SalePersonViewModel>();
            CreateMap<SalePersonViewModel, SalePerson>();
            
            CreateMap<RequestDemo, RequestDemoViewModel>();
            CreateMap<RequestDemoViewModel, RequestDemo>();

            CreateMap<Visitor, VisitorViewModel>();
            CreateMap<VisitorViewModel, Visitor>();

            CreateMap<Status, StatusViewModel>();
            CreateMap<StatusViewModel, Status>();

            CreateMap<DoctorAssigned, DoctorAssignedViewModel>();
                //.ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate));
            CreateMap<DoctorAssignedViewModel, DoctorAssigned>();

            CreateMap<Note, NoteViewModel>();
            CreateMap<NoteViewModel, Note>();

            CreateMap<Log, LogViewModel>();
            CreateMap<LogViewModel, Log>();


            CreateMap<Gender, GenderViewModel>();
            CreateMap<GenderViewModel, Gender>();

            CreateMap<VisitorPurpose, VisitorPurposeViewModel>();
            CreateMap<VisitorPurposeViewModel, VisitorPurpose>();

            CreateMap<RefrenceTable, RefrenceTableViewModel>();
            CreateMap<RefrenceTableViewModel, RefrenceTable>();
            
            CreateMap<User, ChangePasswordViewModel>();
            CreateMap<ChangePasswordViewModel, User>();
            
            CreateMap<User, ForgotPasswordViewModel>();
            CreateMap<ForgotPasswordViewModel, User>();

            CreateMap<FailToImportDoctorsLogViewModel, FailToImportDoctorsLog>();
            CreateMap<FailToImportDoctorsLog, FailToImportDoctorsLogViewModel>();

            CreateMap<EmailConfigurationViewModel, EmailConfigurations>();
            CreateMap<EmailConfigurations, EmailConfigurationViewModel>();

            CreateMap<SearchingViewModel, Searching>();
            CreateMap<Searching, SearchingViewModel>();

            CreateMap<Practice, PracticeViewModel>();
            CreateMap<PracticeViewModel, Practice>();
        }
    }
}
