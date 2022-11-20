using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using StudyAppThing.Views.UserControls;

namespace StudyAppThing.ViewModels
{
    /// <summary>
    /// The view model for <see cref="HeaderView"/>
    /// </summary>
    public partial class HeaderViewModel : ViewModelBase
    {
        /// <summary>
        /// The text to be displayed in the header.
        /// </summary>
        [ObservableProperty]
        private string headerText;

        public HeaderViewModel()
        {
            User.PropertyChanged += UserChanged;
            // Header is created after the courses are loaded.
            HeaderText = User.CurrentUnit.FullName;
        }

        private void UserChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // if the unit has changed.
            if (e.PropertyName == nameof(User.CurrentUnit))
            {
                // the currentUnit will be null when the app starts up.
                // once the UI is loaded the unit will be set.
                if (User.CurrentUnit != null)
                {
                    HeaderText = User.CurrentUnit.FullName;
                }
            }
        }
    }
}
