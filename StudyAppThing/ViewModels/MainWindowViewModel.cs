using CommunityToolkit.Mvvm.ComponentModel;
using StudyAppThing.Loader;
using StudyAppThing.Models;

namespace StudyAppThing.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            // TODO: online user.

            // user is offline here.
            var loader = new CourseLoader();
            // todo: load all courses
        }
    }
}
