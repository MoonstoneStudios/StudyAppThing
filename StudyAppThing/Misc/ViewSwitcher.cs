using StudyAppThing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Misc
{
    /// <summary>
    /// A class used to switch the views.
    /// </summary>
    public static class ViewSwitcher
    {

        /// <summary>
        /// Invoked when the views are being switched.
        /// </summary>
        public static event EventHandler ViewsSwitching = delegate { };

        /// <summary>
        /// Invoked for the <see cref="MainWindowViewModel"/> to hook on to.
        /// </summary>
        public static event EventHandler<MainWindowSwitchViewEventArgs> MainWindowSwitchViews = delegate { };

        /// <summary>
        /// Switch the current view.
        /// </summary>
        /// <param name="viewModel">The view model of the view to switch to.</param>
        public static void SwitchView(ViewModelBase viewModel)
        {
            MainWindowSwitchViews.Invoke(null, new MainWindowSwitchViewEventArgs()
            {
                NewView = viewModel
            });
        }

        /// <summary>
        /// Invoke <see cref="ViewsSwitching"/> from the <see cref="MainWindowViewModel"/>.
        /// </summary>
        public static void InvokeSwitching() => ViewsSwitching.Invoke(null, new EventArgs());

    }
}
