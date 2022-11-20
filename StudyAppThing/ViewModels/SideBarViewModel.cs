using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class SideBarViewModel : ViewModelBase
    {
        public SideBarViewModel()
        {
        }

        [RelayCommand]
        public void SwitchToLoaderboard()
        {
            ViewSwitcher.SwitchView(new LeaderboardViewModel());
        }

        [RelayCommand]
        public void SwitchToHome()
        {
            ViewSwitcher.SwitchView(new HomeViewModel());
        }

    }
}
