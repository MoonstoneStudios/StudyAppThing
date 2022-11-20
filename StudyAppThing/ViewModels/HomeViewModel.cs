using StudyAppThing.Loader;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class HomeViewModel : ViewModelBase, IWindowTitleChanger
    {

        public string WindowTitle => "Home";

        public HomeViewModel()
        {
            
        }

    }
}
