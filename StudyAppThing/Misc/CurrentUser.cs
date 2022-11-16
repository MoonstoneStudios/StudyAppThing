using StudyAppThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Misc
{
    /// <summary>
    /// The current user.
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// The current user.
        /// </summary>
        public static User User { get; set; }

        static CurrentUser()
        {
            User = new User();
        }

    }
}
