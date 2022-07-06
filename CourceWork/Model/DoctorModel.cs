using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.Model
{
    public class DoctorModel : PersonModel
    {
        internal int Id { get; set; }
        internal string Specialization { get; set; }
        internal string Cabinet { get; set; }
        internal int PersonDataId { get; set; }

        public DoctorModel()
        {

        }

        public int SavePatientInDataBase(string login)
        {
            PersonDataId = SavePersonInDataBase(login);

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Id = (int)db.InsertRet("doctors", "specialisation, cabinet,personDataId", $"'{Specialization}', '{Cabinet}', {PersonDataId}", "id");
            return Id;
        }
    }
}
