using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.Model
{
    class PatientModel : PersonModel
    {
        public int Id { get; set; }
        public string MedicalPolicy { get; set; }
        public int PersonDataId { get; set; }

        public PatientModel()
        {

        }

        public PatientModel(int personDataId, string firstName, string lastName, string surname, int loginId, int hospitalId, int id, string medicalPolicy) : base(personDataId, firstName, lastName, surname, loginId, hospitalId)
        {
            Id = id;
            MedicalPolicy = medicalPolicy;
        }

        public int SavePatientInDataBase(string login)
        {
            PersonDataId = SavePersonInDataBase(login);

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Id = (int)db.InsertRet("patients", "medicalPolicy,personDataId", $"'{MedicalPolicy}', {PersonDataId}", "id");
            return Id;
        }
    }
}
