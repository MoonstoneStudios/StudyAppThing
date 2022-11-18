using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAppThing.Views.UserControls;

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
        public ObservableCollection<Course> Courses { get; } = new ObservableCollection<Course>();

        /// <summary>
        /// The user's current course.
        /// </summary>
        [ObservableProperty]
        // change everything that uses CurrentCourseIndex
        [NotifyPropertyChangedFor(nameof(CurrentCourseIndex))]
        // change everything that uses CurrentUnit
        [NotifyPropertyChangedFor(nameof(CurrentUnit))]
        private Course currentCourse;

        /// <summary>
        /// The index of the current course in <see cref="Courses"/>.
        /// </summary>
        public int CurrentCourseIndex
        {
            get
            {
                return Courses.IndexOf(CurrentCourse);
            }
        }

        /// <summary>
        /// The current unit that is being viewed. This is changed by the <see cref="LevelsView"/> code-behind.
        /// </summary>
        [ObservableProperty]
        private Unit currentUnit;

        public User()
        {
            Courses.CollectionChanged += (object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => 
            {
                OnPropertyChanged(nameof(Courses));
            };
        }
    }
}
