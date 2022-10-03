using CourceWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourceWork.ViewModel.RoleViewModel.FirstEnterViewModels
{
    public class FirstEnterPatientViewModel
    {
        public PatientModel PatientData { get; set; }
        public List<string> Hospitals { get; set; }
        public string Login { get; set; }
        public string Item { get; set; }

        public ICommand _saveButton;

        public ICommand SaveButton
        {
            get
            {
                return _saveButton ?? (_saveButton = new RelayCommand
                    (
                        obj =>
                        {
                            Window sender = obj as Window;
                            if (!CheckFields()) return;
                            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");

                            PatientData.GetHospitalId(Item);
                            PatientData.SavePatientInDataBase(Login);
                            db.Update("Logins", "firstenter", "false", $"login = '{Login}'");

                            sender.Close();
                        }
                    ));
            }
        }

        public FirstEnterPatientViewModel()
        {

        }

        public FirstEnterPatientViewModel(string login)
        {
            Login = login;
            PatientData = new PatientModel();
            HospitalInit();
        }

        private void HospitalInit()
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }

        private bool CheckFields()
        {
            if (PatientData.Lname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели фамилию!!");
                return false;
            }

            if (PatientData.Fname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели имя!!");
                return false;
            }

            if (PatientData.Sname == null)
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
