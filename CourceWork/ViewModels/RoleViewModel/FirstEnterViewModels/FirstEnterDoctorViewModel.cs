using CourceWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourceWork.ViewModel.RoleViewModel.FirstEnterViewModels
{
    public class FirstEnterDoctorViewModel
    {
        private DoctorModel DoctorData { get; set; }
        private List<string> Hospitals { get; set; }
        private string Login { get; set; }
        private string Item { get; set; }

        public FirstEnterDoctorViewModel(string login)
        {
            Login = login;
            DoctorData = new DoctorModel();
            HospitalInit();
        }

        public void SaveButton(Window sender)
        {
            if (!CheckFields()) return;

            DoctorData.GetHospitalId(Item);
            DoctorData.SavePatientInDataBase(Login);

            sender.Close();
        }

        private void HospitalInit()
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }

        private bool CheckFields()
        {
            if (DoctorData.LastName == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели фамилию!!");
                return false;
            }

            if (DoctorData.FirstName == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели имя!!");
                return false;
            }

            if (DoctorData.Surname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не отчество!!");
                return false;
            }

            if (Item == null)
            {
                MessageBox.Show("Предупреждение", "Вы не выбрали поликлинику!!");
                return false;
            }

            if (DoctorData.Specialization == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели специализацию!!");
                return false;
            }

            if (DoctorData.Cabinet == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели кабинет!!");
                return false;
            }

            return true;
        }
    }
}
