using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAppThing.Views;

namespace StudyAppThing.Misc
{
    /// <summary>
    /// A View Model that implements <see cref="IWindowTitleChanger"/> will change
    /// the title of the <see cref="MainWindow"/>.
    /// </summary>
    public interface IWindowTitleChanger
    {
        /// <summary>
        /// The title of the window when this View Model's View is displayed.
        /// </summary>
        /// <remarks>
        /// The text, "APP NAME - ", will be prefixed automatically.
        /// </remarks>
        public string WindowTitle { get; }
    }
}
