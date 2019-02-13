using NewSalesProject.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Interfaces
{
    public interface ICRUDViewModel
    {
        void DeleteMany(IList items);
        void UpdateItemDetails();
        void SaveProperties(Dictionary<string, string> columnDictionary);
        string DtGridProperties { get; set; }
    }
}
