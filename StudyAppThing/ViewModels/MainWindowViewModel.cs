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
        /// <summary>
        /// The string that will be prefixed on the apps title.
        /// </summary>
        public const string APP_TITLE_PREFIX = "StudyAppThing - ";

        /// <summary>
        /// The current view to be displayed.
        /// </summary>
        [ObservableProperty]
        private ViewModelBase currentView;

        /// <summary>
        /// The window's title.
        /// </summary>
        [ObservableProperty]
        private string title;

        public MainWindowViewModel()
        {
            // TODO: online user.
            var homeView = new HomeViewModel();
            CurrentView = homeView;
            Title = APP_TITLE_PREFIX + homeView.WindowTitle;

            ViewSwitcher.MainWindowSwitchViews += SwitchViews;

            // user is offline here.
            var loader = new CourseLoader();
            var courses = loader.LoadAllCourses();

            foreach (var c in courses)
            {
                User.Courses.Add(c);
            }
            User.CurrentCourse = User.Courses[0];
        }

        /// <summary>
        /// Switch the displayed view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchViews(object? sender, MainWindowSwitchViewEventArgs e)
        {
            if (e.NewView.GetType() == CurrentView.GetType()) return;

            ViewSwitcher.InvokeSwitching();
            CurrentView = e.NewView;
            // change window title.
            if (e.NewView is IWindowTitleChanger titleChanger)
            {
                Title = APP_TITLE_PREFIX + titleChanger.WindowTitle;
            }
        }
    }
}
