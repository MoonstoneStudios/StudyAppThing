using System;
using CommunityToolkit.Mvvm.ComponentModel;
using StudyAppThing.Loader;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using StudyAppThing.Views;

namespace StudyAppThing.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        [ObservableProperty]
        private ViewModelBase currentView;

        public MainWindowViewModel()
        {
            // TODO: online user.
            CurrentView = new HomeViewModel();

            // user is offline here.
            var loader = new CourseLoader();
            // todo: load all courses
        }
    }
}
