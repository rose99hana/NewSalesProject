using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Interfaces
{
    public interface IViewModel
    {
        string Name { get; set; }
        bool ChangeViewCommandIsEnable { get; set; }
    }
}
