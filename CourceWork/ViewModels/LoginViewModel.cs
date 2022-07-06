using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CourceWork;
using CourceWork.Model;
using CourceWork.View.UserWindow;
using CourceWork.ViewModel.RoleViewModel;
using CourceWork.Views;
using CourceWork.ViewModels;

namespace Courcework.ViewModels
{
    public class LoginViewModel
    {
        private ICommand _singInCommand;
        private ICommand _singUpCommand;
        public string Login { get; set; }
        public string Password { get; set; }

        public LoginViewModel()
        {

        }

        public ICommand SignUpCommand 
        {
            get
            {
                return _singUpCommand ?? (_singUpCommand = new RelayCommand(
                    obj =>
                    {
                        var signUpWindow = new SignUpWindow();
                        signUpWindow.DataContext = new SignUpViewModel();
                        signUpWindow.ShowDialog();
                    }
                    ));
            }
        }

        public ICommand SignInCommand
        {
            get
            {
                return _singUpCommand ?? (_singInCommand = new RelayCommand(obj => {
                    Window loginWindow = obj as Window;
                    switch (LoginCheck())
                    {
                        case -1:
                            return;
                        case 1:
                            var adminWindow = new AdminWindow();
                            adminWindow.Show();
                            adminWindow.DataContext = new AdminViewModel(Login);
                            break;
                        case 2:
                            var doctorWindow = new DoctorWindow();
                            doctorWindow.Show();
                            doctorWindow.DataContext = new DoctorViewModel(Login, doctorWindow);
                            break;
                        case 3:
                            var patientWindow = new PatientWindow();
                            patientWindow.Show();
                            patientWindow.DataContext = new PatientViewModel(Login, patientWindow);
                            break;
                    }
                    loginWindow.Close();
                }));
            }
        }

        private object LoginCheck()
        {
            if (Login == null || Login == "")
            {
                MessageBox.Show("Предупреждение", "Вы не ввели логин!!!");
                return -1;
            }

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            DataTable dataTable = db.SelectAdapter("RoleId", "Logins", $"Login = '{Login}' AND Password = '{Password}'").Tables[0];

            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Предупреждение", "Неверный логин или пароль!!!");
                return -1;
            }

            return dataTable.Rows[0].ItemArray[0];

        }
    }
}