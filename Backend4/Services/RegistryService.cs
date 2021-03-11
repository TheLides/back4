using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Backend4.Services
{
    public sealed class RegistryService : IRegistryService
    {
        private readonly ILogger _log;
        private readonly List<SavedInfo> _users = new List<SavedInfo>();


        public RegistryService(ILogger<IRegistryService> log)
        {
            this._log = log;
        }

        public void AddTheUser(String firstname, String lastName, String birthdayDate, String email, String password,
            Boolean userExisted, Boolean userRemembered, Gender gender)
        {
            lock (this._users)
            {
                this._users.Add(new SavedInfo(firstname, lastName, birthdayDate, gender, password, userExisted,
                    userRemembered));
                this._log.LogInformation($"Adding user {firstname} {lastName} with {email} to the system");
            }
        }

        public bool CheckTheUser(String firstName, String lastName, String birthdayDate, Gender gender)
        {
            this._log.LogInformation($"Checking {firstName} {lastName} for existence");
            foreach (var i in _users)
            {
                if (i.FirstName == firstName && i.LastName == lastName && i.BirthdayDate == birthdayDate &&
                    i.Gender == gender)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class SavedInfo
    {
        public SavedInfo(String firstname, String lastName, String birthdayDate, Gender gender, String password,
            Boolean userExisted, Boolean userRemembered)
        {
            FirstName = firstname;
            LastName = lastName;
            BirthdayDate = birthdayDate;
            Gender = gender;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String BirthdayDate { get; set; }
        public Gender Gender { get; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Boolean UserExisted { get; set; }
        public Boolean UserRemembered { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}