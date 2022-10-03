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
    public class PatientViewModel : PersonViewModel
    {
        public PatientModel MyData { get; set; }
        public string Item { get; set; }
        public List<string> Hospitals { get; set; }
        public DataTable MyAppointments { get; set; }
        public DataRowView SelectedMyApppointment { get; set; }
        public DataTable FreeAppointments { get; set; }
        public DataRowView SelectedFreeApppointment { get; set; }

        private ICommand _cancelAppointment;
        private ICommand _ApplyAppointment;
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
                            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
                            string arg = $"'{SelectedMyApppointment.Row.ItemArray[0]}'";
                            db.Update("appointments", "patientid", "NULL", $"id = {arg}");
                            MyAppointments.Rows.Remove(SelectedMyApppointment.Row);
                        }
                    ));
            }
        }
        public ICommand ApplyAppointment
        {
            get
            {
                return _ApplyAppointment ?? (_ApplyAppointment = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("patient", "patient");
                            string arg = $"'{SelectedFreeApppointment.Row.ItemArray[0]}'";
                            db.Update("appointments", "patientid", $"{MyData.Id}", $"id = {arg}");
                            FreeAppointments.Rows.Remove(SelectedFreeApppointment.Row);
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
                            MyData.UpdatePatient();
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

        public PatientViewModel()
        {

        }
        public PatientViewModel(string login, Window window) : base(login)
        {
            MyData = new PatientModel();

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            if ((bool)db.Select("FirstEnter", "logins", $"login = '{login}'")[0][0])
            {
                FirstEnterPatientWindow patientWindow = new FirstEnterPatientWindow();
                patientWindow.DataContext = new FirstEnterPatientViewModel(login);
                patientWindow.ShowDialog();
            }


            DataRow data = db.SelectAdapter("*", $"getPatientData('{login}')").Tables[0].Rows[0];
            MyData = new PatientModel((int)data[0], (string)data[1], (int)data[2], (string)data[3], (string)data[4], (string)data[5], (int)data[6], (int)data[7]);

            MyAppointments = db.SelectAdapter("*", $"getAppointmentPat('{MyData.Id}')").Tables[0];
            FreeAppointments = db.SelectAdapter("*", $"getFreeAppointment()").Tables[0];
            Hospitals = db.Select("Name", "hospitals").Select(x => x[0].ToString().Trim()).ToList();
        }
    }
}
