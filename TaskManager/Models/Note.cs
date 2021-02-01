﻿using TaskManager.ViewModels.Base;

namespace TaskManager.Models
{
    public class Note : ViewModel
    { 
        private string content = "";

        /// <summary>
        /// Note content
        /// </summary>
        public string Content
        {
            get
            { 
                return content ;
            }
            set => Set(ref content, value);
        }

        private string target;

        /// <summary>
        /// Note goal
        /// </summary>
        public string Target
        {
            get => target;
            set => Set(ref target, value);
        }
    }
}
