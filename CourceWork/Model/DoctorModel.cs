using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.Model
{
    public class DoctorModel : PersonModel
    {
        public int Id { get; set; }
        public string Spec { get; set; }
        public string Pos { get; set; }
        public string Cab { get; set; }
        public int PersonDataId { get; set; }

        public DoctorModel()
        {

        }

        public DoctorModel(int id, string spec, string pos, string cab, int personDataId, string lname, string fname, string sname, int loginid, int hospitalid) : base (personDataId, lname, fname, sname, loginid, hospitalid)
        {
            Id = id;
            Spec = spec;
            Pos = pos;
            Cab = cab;
            PersonDataId = personDataId;
        }

        public void UpdateDoctor()
        {
            WorkWithDataBase db = new WorkWithDataBase("doctor", "doctor");
            db.Proc("updateDoctor", $"{Id}, '{Spec}', '{Pos}', '{Cab}'");
        }

        public int SavePatientInDataBase(string login)
        {
            PersonDataId = SavePersonInDataBase(login);

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Id = (int)db.ScalarFunction("insertDoctor", $"'{Spec}', '{Pos}', '{Cab}', {PersonDataId}");
            db.Proc("generateappointment", "");
            return Id;
        }
    }
}
