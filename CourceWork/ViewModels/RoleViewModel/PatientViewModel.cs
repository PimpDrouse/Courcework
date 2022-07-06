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
    class PatientViewModel : PersonViewModel
    {
        public PatientModel PatientData { get; set; }
        public PatientViewModel(string login, Window window) : base(login)
        {
            PatientData = new PatientModel();

            WorkWithDataBase db = new WorkWithDataBase("admin", "admin");
            if ((bool)db.Select("FirstEnter", "logins", $"login = '{login}'")[0][0])
            {
                FirstEnterPatientWindow patientWindow = new FirstEnterPatientWindow();
                patientWindow.DataContext = new FirstEnterPatientViewModel(login);
                patientWindow.ShowDialog();

                db.Update("Logins", "firstenter", "false", $"login = '{login}'");
            }
        }

        public void Button()
        {
            return;
        }
    }
}
