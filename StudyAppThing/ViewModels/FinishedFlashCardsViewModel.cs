using StudyAppThing.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class FinishedFlashCardsViewModel : ViewModelBase, IWindowTitleChanger
    {
        public string WindowTitle => "Flash Cards Complete!";
    }
}
