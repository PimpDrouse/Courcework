using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourceWork.ViewModel.RoleViewModel
{
    public abstract class PersonViewModel
    {
        private string Login { get; set; }

        public PersonViewModel(string login)
        {
            Login = login;
        }
    }
}
