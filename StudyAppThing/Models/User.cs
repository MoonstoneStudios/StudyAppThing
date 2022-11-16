using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// The user.
    /// </summary>
    public partial class User : ObservableObject
    {
        /// <summary>
        /// When the user is online, they have an account that is registered. 
        /// When teh user is offline, they chose not to make an account.
        /// </summary>
        public bool Online { get; set; }

        /// <summary>
        /// The user name.
        /// </summary>
        // set to default value.
        private string userName = "**No Account**";

        /// <summary>
        /// The username of the user.
        /// </summary>
        /// <remarks>
        /// When the user is not <see cref="Online"/> this name has a default value of **No Account**.
        /// </remarks>
        public string UserName { get => userName; set => userName = value; }

        /// <summary>
        /// The user's courses.
        /// </summary>
        [ObservableProperty]
        private List<Course> courses = new List<Course>();
    }
}
