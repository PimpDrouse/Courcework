using CourceWork.Model;
using CourceWork.View.UserWindow.FirstEnterWindow;
using CourceWork.ViewModel.RoleViewModel.FirstEnterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourceWork.ViewModel.RoleViewModel
{
    public class DoctorViewModel : PersonViewModel
    {
        private DoctorModel DoctorData { get; set; }
        public DoctorViewModel(string login, Window window) : base(login)
        {
            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            if ((bool)db.Select("FirstEnter", "logins", $"login = '{login}'")[0][0])
            {
                FirstEnterDoctorWindow doctorWindow = new FirstEnterDoctorWindow();
                doctorWindow.DataContext = new FirstEnterDoctorViewModel(login);
                doctorWindow.ShowDialog(); //window

                db.Update("Logins", "firstenter", "false", $"login = '{login}'");
            }
        }
    }
}
