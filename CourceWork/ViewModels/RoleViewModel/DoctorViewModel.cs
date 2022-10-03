using CourceWork.Model;
using CourceWork.View.UserWindow.FirstEnterWindow;
using CourceWork.ViewModel.RoleViewModel.FirstEnterViewModels;
using CourceWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourceWork.ViewModel.RoleViewModel
{
    public class DoctorViewModel : PersonViewModel
    {
        public DoctorModel MyData { get; set; }
        public string Item { get; set; }
        public List<string> Hospitals { get; set; }
        public DataTable Appointments { get; set; }
        public DataRowView SelectedApppointment { get; set; }

        private ICommand _cancelAppointment;
        private ICommand _saveData;
        private ICommand _logOutButton;

        public ICommand CancelAppointment
        {
            get
            {
                return _cancelAppointment ?? (_cancelAppointment = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("doctor", "doctor");
                            string arg = $"'{SelectedApppointment.Row.ItemArray[0]}'";
                            db.Update("appointments","patientid", "NULL", $"id = {arg}");
                            Appointments.Rows.Remove(SelectedApppointment.Row);
                        }
                    ));
            }
        }
        public ICommand SaveData
        {
            get
            {
                return _saveData ?? (_saveData = new RelayCommand
                    (
                        obj =>
                        {
                            MyData.GetHospitalId(Item);
                            MyData.UpdatePerson();
                            MyData.UpdateDoctor();
                        }
                    ));
            }
        }
        public ICommand LogOutButton
        {
            get
            {
                return _logOutButton ?? (_logOutButton = new RelayCommand
                    (
                        obj =>
                        {
                            Window adminWindow = obj as Window;
                            var loginWindow = new LoginWindow();
                            loginWindow.DataContext = new LoginViewModel();
                            loginWindow.Show();
                            adminWindow.Close();
                        }
                    ));
            }
        }
        public DoctorViewModel()
        {

        }

        public DoctorViewModel(string login) : base(login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            if ((bool)db.Select("FirstEnter", "logins", $"login = '{login}'")[0][0])
            {
                FirstEnterDoctorWindow doctorWindow = new FirstEnterDoctorWindow();
                doctorWindow.DataContext = new FirstEnterDoctorViewModel(login);
                doctorWindow.ShowDialog();
            }

            DataRow data = db.SelectAdapter("*", $"getDoctorData('{login}')").Tables[0].Rows[0];
            MyData = new DoctorModel((int)data[0], (string)data[1], (string)data[2], (string)data[3],
                (int)data[4], (string)data[5], (string)data[6], (string)data[7], (int)data[8], (int)data[9]);

            Appointments = db.SelectAdapter("*", $"getAppointmentDoc('{MyData.Id}')").Tables[0];
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }
    }
}
