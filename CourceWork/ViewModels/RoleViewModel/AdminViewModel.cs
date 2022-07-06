using Courcework.ViewModels;
using CourceWork.Model;
using CourceWork.Views;
using System.Data;
using System.Windows;

namespace CourceWork.ViewModel.RoleViewModel
{
    class AdminViewModel : PersonViewModel
    {
        public DataTable Logins { get; set; }
        public DataTable Roles { get; set; }
        public DataTable Appointments { get; set; }
        public AdminViewModel(string login) : base(login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            //Logins = db.SelectAdapter("*", "logindata").Tables[0];
            Roles = db.SelectAdapter("*", "roles").Tables[0];
            Appointments = db.SelectAdapter("*", "appointments").Tables[0];
        }

        public class LoginData
        {
            public int Id { get; set; }
            public string Login { get; set; }
            public string Role { get; set; }

            public LoginData(int id, string login, string role)
            {
                Id = id;
                Login = login;
                Role = role;
            }
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
