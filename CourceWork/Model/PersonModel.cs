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
        public string Lname { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public int LoginId { get; set; }
        public int HospitalId { get; set; }


        public PersonModel()
        {

        }

        public PersonModel(int id, string lastName, string firstName, string surname, int loginId, int hospitalId)
        {
            Id = id;
            Lname = lastName;
            Fname = firstName;
            Sname = surname;
            LoginId = loginId;
            HospitalId = hospitalId;
        }

        public int SavePersonInDataBase(string login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            LoginId = (int)db.Select("Id", "Logins", $"Login='{login}'")[0][0];
            Id = (int)db.ScalarFunction("insertPersonData", $"'{Lname}', '{Fname}', '{Sname}', {LoginId}, {HospitalId}");
            return Id;
        }

        public void UpdatePerson()
        {
            WorkWithDataBase db = new WorkWithDataBase("doctor", "doctor");
            db.Proc("updatePerson", $"{Id}, '{Lname}', '{Fname}', '{Sname}', {HospitalId}");
        }

        public void GetHospitalId(string item)
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            HospitalId = (int)db.Select("Id", "hospitals", $"Name = '{item}'")[0][0];
        }
    }
}
