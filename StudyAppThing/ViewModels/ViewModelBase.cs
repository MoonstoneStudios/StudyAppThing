using CommunityToolkit.Mvvm.ComponentModel;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using StudyAppThing.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    /// <summary>
    /// The base class for all view models.
    /// </summary>
    public class ViewModelBase : ObservableObject
    {
        /// <summary>
        /// The user.
        /// </summary>
        public User User => CurrentUser.User;

    }
}
