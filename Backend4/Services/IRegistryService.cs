using System;

namespace Backend4.Services
{
    public interface IRegistryService
    {
        void AddTheUser(String firstname, String lastName, String birthdayDate, String email, String password,
            Boolean userExisted, Boolean userRemembered, Gender gender);
        Boolean CheckTheUser(String firstName, String lastName, String birthdayDate, Gender gender);
    }
}