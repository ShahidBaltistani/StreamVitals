using System;
using System.Collections.Generic;
using System.Text;
using Vu360Sol.ViewModel.Account;
using Vu360Sol.ViewModel.Doctors;
using Vu360Sol.ViewModel.Logs;
using Vu360Sol.ViewModel.Practices;
using Vu360Sol.ViewModel.RequestDemoes;
using Vu360Sol.ViewModel.SalePersons;
using Vu360Sol.ViewModel.Search;
using Vu360Sol.ViewModel.Visitors;

namespace Vu360Sol.ViewModel.SharedViewModels
{
    public class BaseListingViewModel
    {
    }

    public class Pager
    {
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            if (pageSize == 0) pageSize = 10;

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page;
            //var currentPage = page == 0 ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        //public Pager( int? page, int? pageSize = 10)
        //{
        //    if (pageSize == 0) pageSize = 10;

        //    CurrentPage = page.GetValueOrDefault();
        //    PageSize = pageSize.GetValueOrDefault();
            
        //}
        public  int TotalItems { get; private set; }
        public  int CurrentPage { get; private set; }
        public  int PageSize { get; private set; }
        public  int TotalPages { get; private set; }
        public  int StartPage { get; private set; }
        public  int EndPage { get; private set; }

    }
    public class DoctorAssignedPaginationModel 
    {
        public IEnumerable<DoctorAssignedViewModel> doctorsAssigned { get; set; }

        public Pager page { get; set; }

        public string SearchTerm { get; set; }

        public int Count { get; set; }
        public int Days { get; set; }

        
    }
    public class DoctorPaginationModel
    {
        public IEnumerable<DoctorViewModel> doctors { get; set; }

        public Pager page { get; set; }

        public string SearchTerm { get; set; }

        public int Count { get; set; }




    }
    public class FailToImportDoctorsLogPaginationModel
    {
        public IEnumerable<FailToImportDoctorsLogViewModel> FailToImportDoctorsLogViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
    }
    public class DoctorAssignedViewModelPaginationModel
    {
        public IEnumerable<DoctorAssignedViewModel> doctorAssignedViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }

    }
    public class RequestDemoViewModelPaginationModel
    {
        public IEnumerable<RequestDemoViewModel> requestDemoViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
        public int? Days { get; set; }
    }
    public class DoctorViewModelPaginationModel
    {
        public IEnumerable<DoctorViewModel> doctorViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
    }
    public class SalePersonViewModelPaginationModel
    {
        public IEnumerable<SalePersonViewModel> salePersonViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
    }
    public class VisitorViewModelPaginationModel
    {
        public IEnumerable<VisitorViewModel> visitorViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
        public int? Days { get; set; }

    }
    public class UserViewModelPaginationModel
    {
        public IEnumerable<UserViewModel> userViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
        public int? Days { get; set; }
    }
    public class SearchingPaginationModel
    {
        public IEnumerable<SearchingViewModel> SearchingViewModels { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
    } 
    
    public class PracticeViewModelPaginationModel
    {
        public IEnumerable<PracticeViewModel> practice { get; set; }
        public Pager page { get; set; }
        public string SearchTerm { get; set; }
        public int Count { get; set; }
    }
}
