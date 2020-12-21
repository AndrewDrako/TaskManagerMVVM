﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Note : ViewModel
    {
        #region Note content

        private string _Content = "";
        public string Content
        {
            get
            { 
                return _Content ;
            }
            set => Set(ref _Content, value);
        }

        #endregion

        #region Note goal

        private string _Target;
        public string Target
        {
            get => _Target;
            set => Set(ref _Target, value);
        }

        #endregion
    }
}
