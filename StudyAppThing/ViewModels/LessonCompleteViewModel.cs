using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class LessonCompleteViewModel : ViewModelBase, IWindowTitleChanger
    {
        public string WindowTitle => "Lesson Complete!";

        /// <summary>
        /// The stats.
        /// </summary>
        [ObservableProperty]
        private LessonStats stats;

        public LessonCompleteViewModel(LessonStats stats)
        {
            Stats = stats;
        }

        [RelayCommand]
        public void Home()
        {
            ViewSwitcher.SwitchView(new HomeViewModel());
        }

    }
}
