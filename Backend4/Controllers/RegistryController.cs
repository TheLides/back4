using System;
using System.Globalization;
using Backend4.Models;
using System.Linq;
using Backend4.Models.Controls;
using Backend4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers
{
    public class RegistryController : Controller
    {
        private readonly IRegistryService _registryService;

        public RegistryController(IRegistryService registryService)
        {
            _registryService = registryService;
        }

        public ActionResult SignUp()
        {
            var model = new RegistryModel();
            this.ViewBag.AllMonths = this.GetAllMonths();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegistryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            this.ViewBag.AllMonths = this.GetAllMonths();
            string birthdayDate = model.Date.ToString() + model.Month + model.Year;
            if (this._registryService.CheckTheUser(model.FirstName, model.LastName, birthdayDate, model.Gender))
            {
                return this.View("SignUpAlreadyExists", new RegistryModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Date = model.Date,
                    Month = model.Month,
                    Year = model.Year,
                    Gender = model.Gender,
                    HasClone = true
                });
            }

            return this.View("SignUpCredentials", new FinalRegistryModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Date = model.Date,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                HasClone = false
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpAlreadyExists(RegistryModel model)
        {
            this.ViewBag.AllMonths = this.GetAllMonths();
            return this.View("SignUpCredentials", new FinalRegistryModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Date = model.Date,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                HasClone = model.HasClone
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUpCredentials(FinalRegistryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            this.ViewBag.AllMonths = this.GetAllMonths();
            string birthdayDate = model.Date.ToString() + model.Month + model.Year;
            this._registryService.AddTheUser(model.FirstName, model.LastName, birthdayDate, model.Email, model.Password,
                model.HasClone, model.IsRemembered, model.Gender);

            return this.View("SignUpResult", model);
        }

        private Month[] GetAllMonths()
        {
            return CultureInfo.InvariantCulture.DateTimeFormat.MonthNames
                .Select((x, i) => new Month {Id = i + 1, Name = x})
                .Where(x => !String.IsNullOrEmpty(x.Name))
                .ToArray();
        }

    }
}