using Avalonia.Controls;
using StudyAppThing.Misc;

namespace StudyAppThing.Views.UserControls
{
    public partial class SideBarView : UserControl
    {
        public SideBarView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the sidebar's selection has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // set the course to the new value.
            var dropDown = (DropDown)sender;
            var index = dropDown.SelectedIndex;
            CurrentUser.User.CurrentCourse = CurrentUser.User.Courses[index];
            // set the new unit.
            CurrentUser.User.CurrentUnit = CurrentUser.User.CurrentCourse.Units[0];
        }

    }
}
