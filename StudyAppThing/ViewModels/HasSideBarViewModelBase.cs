using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    /// <summary>
    /// The base view model for a view with a <see cref="SideBarViewModel"/>.
    /// </summary>
    public class HasSideBarViewModelBase : ViewModelBase
    {
        /// <summary>
        /// The sidebar.
        /// </summary>
        public SideBarViewModel SideBar { get; protected set; }
    }
}
