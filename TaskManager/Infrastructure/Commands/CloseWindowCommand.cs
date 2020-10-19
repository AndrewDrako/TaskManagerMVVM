using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Infrastructure.Commands.Base;

namespace TaskManager.Infrastructure.Commands
{
    internal class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public override void Execute(object p)
        {
            var window = p as Window;

            if (window is null)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);
            }

            if (window is null)
            {
                window = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);
            }

            window?.Close();
        }
    }
}
