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
    public class FirstEnterDoctorViewModel
    {
        public DoctorModel DoctorData { get; set; }
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

                            DoctorData.GetHospitalId(Item);
                            DoctorData.SavePatientInDataBase(Login);
                            db.Update("Logins", "firstenter", "false", $"login = '{Login}'");

                            sender.Close();
                        }
                    ));
            }
        }

        public FirstEnterDoctorViewModel()
        {

        }

        public FirstEnterDoctorViewModel(string login)
        {
            Login = login;
            DoctorData = new DoctorModel();
            HospitalInit();
        }



        private void HospitalInit()
        {
            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }

        private bool CheckFields()
        {
            if (DoctorData.Lname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели фамилию!!");
                return false;
            }

            if (DoctorData.Fname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели имя!!");
                return false;
            }

            if (DoctorData.Sname == null)
            {
                MessageBox.Show("Предупреждение", "Вы не отчество!!");
                return false;
            }

            if (Item == null)
            {
                MessageBox.Show("Предупреждение", "Вы не выбрали поликлинику!!");
                return false;
            }

            if (DoctorData.Spec == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели специализацию!!");
                return false;
            }

            if (DoctorData.Pos == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели должность!!");
                return false;
            }

            if (DoctorData.Cab == null)
            {
                MessageBox.Show("Предупреждение", "Вы не ввели кабинет!!");
                return false;
            }

            return true;
        }
    }
}
