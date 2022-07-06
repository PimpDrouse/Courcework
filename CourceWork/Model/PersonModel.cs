using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.Model
{
    public abstract class PersonModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public int LoginId { get; set; }
        public int HospitalId { get; set; }


        public PersonModel()
        {

        }

        public PersonModel(int id, string firstName, string lastName, string surname, int loginId, int hospitalId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Surname = surname;
            LoginId = loginId;
            HospitalId = hospitalId;
        }


        public int SavePersonInDataBase(string login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            LoginId = (int)db.Select("Id", "Logins", $"Login='{login}'")[0][0];
            Id = (int)db.InsertRet("persondata", $"lastname,firstname,surname,loginid,hospitalid", $"'{FirstName}', '{LastName}', '{Surname}', {LoginId}, {HospitalId}", "id");
            return Id;
        }

        public void GetHospitalId(string item)
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            HospitalId = (int)db.Select("Id", "hospitals", $"Name = '{item}'")[0][0];
        }
    }
}
