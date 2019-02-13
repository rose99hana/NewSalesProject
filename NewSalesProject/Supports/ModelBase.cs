using NewSalesProject.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Supports
{
    public abstract class ModelBase : NotifyUIBase, IDataErrorInfo
    {
        protected Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        protected bool hasErrors = false;
        [Browsable(false)]
        [NotMapped]
        public bool HasErrors
        {
            get
            {
                return hasErrors;
            }
            set
            {
                hasErrors = value;
                OnPropertyChanged("HasErrors");
            }
        }

        [Browsable(false)]
        public string Error => string.Empty;

        public string this[string propertyName]
        {
            get
            {
                CollectErrors(propertyName);
                return Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
            }
        }

        protected virtual void CollectErrors(string propertyName)
        {
        }

        protected void CheckNotNull(string propertyName, string value)
        {
            if (value == "")
            {
                Errors.Add(propertyName, "This field is required");
                HasErrors = true;
            }
            else HasErrors = false;
        }



        private bool isEditable = false;
        [Browsable(false)]
        [NotMapped]
        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
                OnPropertyChanged("IsEditable");
            }
        }


    }
}
