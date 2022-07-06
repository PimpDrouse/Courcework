using CourceWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourceWork.ViewModel.RoleViewModel.FirstEnterViewModels
{
    public class FirstEnterPatientViewModel
    {
        private PatientModel PatientData { get; set; }
        private List<string> Hospitals { get; set; }
        private string Login { get; set; }
        private string Item { get; set; }

        public FirstEnterPatientViewModel(string login)
        {
            Login = login;
            PatientData = new PatientModel();
            HospitalInit();
        }

        public void SaveButton(Window sender)
        {
            if (!CheckFields()) return;

            PatientData.GetHospitalId(Item);
            PatientData.SavePatientInDataBase(Login);

            sender.Close();
        }

        private void HospitalInit()
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }

        private bool CheckFields()
        {
            if (PatientData.LastName == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели фамилию!!");
                return false;
            }

            if (PatientData.FirstName == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели имя!!");
                return false;
            }

            if (PatientData.Surname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не отчество!!");
                return false;
            }

            if (Item == null)
            {
                MessageBox.Show("Предупреждение", "Вы не выбрали поликлинику!!");
                return false;
            }

            if (PatientData.MedicalPolicy == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели медицинский полис!!");
                return false;
            }

            return true;
        }
    }
}
