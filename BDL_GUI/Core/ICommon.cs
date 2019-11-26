using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDL_GUI.Core
{
    public interface ICommon
    {
        CommonWindowProperties GetProperties();

        object Content { get; set; }
    }
}
