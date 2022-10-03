using CourceWork.ViewModels;
using CourceWork.Model;
using CourceWork.Views;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace CourceWork.ViewModel.RoleViewModel
{
    class AdminViewModel : PersonViewModel
    {
        private ICommand _loginsUpdateButton;
        private ICommand _loginsDeleteButton;

        private ICommand _roleAddButton;
        private ICommand _roleUpdateButton;
        private ICommand _roleDeleteButton;

        private ICommand _hospitalAddButton;
        private ICommand _hospitalUpdateButton;
        private ICommand _hospitalDeleteButton;

        public DataRowView SelectedNameHospital { get; set; }
        public DataRowView SelectedLogins { get; set; }
        public DataRowView SelectedNameRole { get; set; }



        private ICommand _logOutButton;

        public DataTable Logins { get; set; }
        public DataTable Roles { get; set; }
        public DataTable Hospitals { get; set; }
        public DataTable Appointments { get; set; }
        public DataTable CountAppointment { get; set; }

        public ICommand LoginsUpdateButton 
        {
            get
            {
                return _loginsUpdateButton ?? (_loginsUpdateButton = new RelayCommand
                    (
                        obj => 
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            foreach (DataRow row in Logins.GetChanges(DataRowState.Modified).Rows)
                            {
                                if (row.ItemArray[0] is System.DBNull) break;
                                string arg = $"{row.ItemArray[0]}, '{row.ItemArray[1]}', '{row.ItemArray[2]}', '{row.ItemArray[3]}'";
                                db.Proc("updateLoginData", arg);
                            }
                        }
                    ));
            }
        }
        public ICommand LoginsDeleteButton
        {
            get
            {
                return _loginsDeleteButton ?? (_loginsDeleteButton = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            string arg = $"'{SelectedLogins.Row.ItemArray[2]}'";
                            db.Proc("deleteLoginData", arg);
                            Logins.Rows.Remove(SelectedLogins.Row);
                        }
                    ));
            }
        }

        public ICommand RoleAddButton
        {
            get
            {
                return _roleAddButton ?? (_roleAddButton = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            foreach (DataRow row in Roles.GetChanges(DataRowState.Added).Rows)
                            {
                                if (row.ItemArray[0] is not System.DBNull) break;
                                string arg = $"'{row.ItemArray[1]}'";
                                db.Proc("insertRole", arg);
                            }
                        }
                    ));
            }
        }
        public ICommand RoleUpdateButton
        {
            get
            {
                return _roleUpdateButton ?? (_roleUpdateButton = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            foreach (DataRow row in Roles.GetChanges(DataRowState.Modified).Rows)
                            {
                                if (row.ItemArray[0] is System.DBNull) break;
                                string arg = $"{row.ItemArray[0]}, '{row.ItemArray[1]}'";
                                db.Proc("updateRole", arg);
                            }
                        }
                    ));
            }
        }
        public ICommand RoleDeleteButton
        {
            get
            {
                return _roleDeleteButton ?? (_roleDeleteButton = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            string arg = $"'{SelectedNameRole.Row.ItemArray[1]}'";
                            db.Proc("deleteRole", arg);
                            Roles.Rows.Remove(SelectedNameRole.Row);
                        }
                    ));
            }
        }

        public ICommand HospitalAddButton 
        {
            get 
            {
                return _hospitalAddButton ?? (_hospitalAddButton = new RelayCommand
                    (
                        obj => 
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            foreach(DataRow row in Hospitals.GetChanges(DataRowState.Added).Rows) 
                            {
                                if (row.ItemArray[0] is not System.DBNull) break;
                                string arg = $"'{row.ItemArray[1]}', '{row.ItemArray[2]}'";
                                db.Proc("insertHospital", arg);
                            }
                        }
                    ));
            }
        }
        public ICommand HospitalUpdateButton
        {
            get 
            {
                return _hospitalUpdateButton ?? (_hospitalUpdateButton = new RelayCommand
                    (
                        obj => 
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            foreach(DataRow row in Hospitals.GetChanges(DataRowState.Modified).Rows) 
                            {
                                if (row.ItemArray[0] is System.DBNull) break;
                                string arg = $"{row.ItemArray[0]}, '{row.ItemArray[1]}', '{row.ItemArray[2]}'";
                                db.Proc("updateHospital", arg);
                            }
                        }
                    ));
            }
        }
        public ICommand HospitalDeleteButton
        {
            get 
            {
                return _hospitalDeleteButton ?? (_hospitalDeleteButton = new RelayCommand
                    (
                        obj =>
                        {
                            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
                            string arg = $"'{SelectedNameHospital.Row.ItemArray[1]}', '{SelectedNameHospital.Row.ItemArray[2]}'";
                            db.Proc("deleteHospital", arg);
                            Hospitals.Rows.Remove(SelectedNameHospital.Row);
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

        public AdminViewModel() : this("aaa") { }
        public AdminViewModel(string login) : base(login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Logins = db.SelectAdapter("*", "logindata").Tables[0];
            Roles = db.SelectAdapter("*", "roles").Tables[0];
            Hospitals = db.SelectAdapter("*", "hospitals").Tables[0];
            Appointments = db.SelectAdapter("*", "adminAppointments").Tables[0];
            CountAppointment = db.SelectAdapter("*", "countFreeAppointment()").Tables[0];

        }

        public void LogOut(Window adminWindow)
        {
            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel();
            loginWindow.Show();
            adminWindow.Close();
        }
    }
}
