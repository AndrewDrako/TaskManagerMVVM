using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    internal class Note : ViewModel
    {
        private string _Content;
        public string Content
        {
            get => _Content;
            set => Set(ref _Content, value);
        }
    }
}
