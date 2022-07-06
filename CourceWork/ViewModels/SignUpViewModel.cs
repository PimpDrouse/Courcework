using CourceWork.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CourceWork.ViewModels
{
    public class SignUpViewModel
    {
        private ICommand _signUpCommand;
        public string Login { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string SelectedRole { get; set; }

        public SignUpViewModel()
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            Roles = db.SelectAdapter("role", "roles", "role != 'Admin'").Tables[0].Rows.OfType<DataRow>().Select(x => x.ItemArray[0].ToString()).ToList();
        }

        public ICommand SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new RelayCommand(
                    obj =>
                    {
                        Window sender = obj as Window;
                        CheckSingUp();
                        sender.Close();
                    }
                    ));
            }
        }

        private void CheckSingUp()
        {
            if (Login == null || Password == null || Login == "" || Password == "")
            {
                MessageBox.Show("Предупреждение", "Вы не ввели логин или пароль!!");
                return;
            }

            if (SelectedRole == null)
            {
                MessageBox.Show("Предупреждение", "Роль не выбрана!!");
                return;
            }

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin"); 
            DataTable list = db.SelectAdapter("*", "logins", $"Login = '{Login}'").Tables[0];

            if (list.Rows.Count != 0)
            {
                MessageBox.Show("Предупреждение", "Пользователь с таким логином уже существует!!");
                return;
            }

            db.Insert("Logins", "Login, Password, RoleId", $"'{Login}', '{Password}', {(int)db.SelectAdapter("Id", "Roles", $"Role = '{SelectedRole}'").Tables[0].Rows[0].ItemArray[0]}");
        }
    }
}
