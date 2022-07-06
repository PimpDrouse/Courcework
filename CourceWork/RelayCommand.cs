using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourceWork
{
    class RelayCommand : ICommand
    {
        private Action<object> _action;
        private Func<bool> _canExecute;

        public RelayCommand(Action<object> action) : this(action, () => true) { }
        public RelayCommand(Action<object> action, Func<bool> canExecute) 
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object paremeter) => _canExecute();
        public void Execute(object parameter) => _action(parameter);
    }
}
