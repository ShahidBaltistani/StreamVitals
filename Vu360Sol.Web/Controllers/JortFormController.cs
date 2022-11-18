using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vu360Sol.ViewModel.JortForm;
using Vu360Sol.Web.SVRPDJortForm;

namespace Vu360Sol.Web.Controllers
{
    public class JortFormController : Controller
    {
        // GET: JortForm
        public ActionResult PaientAppointmentJortFormList()
        {
            var list = new PatientAppointmentFormData().getPaitents();

            //PatientAppointmentJortFormViewModel model = new PatientAppointmentJortFormViewModel();

            //model.FirstName = "zabair";
            //model.LastName = "zaid";
            //model.MobilePhone = "123456";
            //model.ResidentialCity = "Fsd";
            //list.Add(model);
            if (list == null)
                list = new List<PatientAppointmentJortFormViewModel>();

            return View(list);
        }
    }
}