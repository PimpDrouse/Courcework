using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.Model
{
    public class PatientModel : PersonModel
    {
        public int Id { get; set; }
        public string MedicalPolicy { get; set; }
        public int PersonDataId { get; set; }

        public PatientModel()
        {

        }

        public PatientModel(int id, string medicalPolicy, int personDataId, string lastName, string firstName, string surname, int loginId, int hospitalId) : base(personDataId, lastName, firstName, surname, loginId, hospitalId)
        {
            Id = id;
            MedicalPolicy = medicalPolicy;
            PersonDataId = personDataId;
        }

        public void UpdatePatient()
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            db.Proc("updatePatient", $"{Id}, '{MedicalPolicy}'");
        }

        public int SavePatientInDataBase(string login)
        {
            PersonDataId = SavePersonInDataBase(login);

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Id = (int)db.ScalarFunction("insertPatient", $"'{MedicalPolicy}', {PersonDataId}");
            return Id;
        }
    }
}
