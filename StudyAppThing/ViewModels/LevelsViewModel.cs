using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using StudyAppThing.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class LevelsViewModel : ViewModelBase
    {
        /// <summary>
        /// This is a list of objects that will be shown in the list view.
        /// </summary>
        /// <remarks>
        /// This is an object list because the seperators should be displayed.
        /// </remarks>
        public ObservableCollection<object> LessonsAndSeperators { get; set; } = new ObservableCollection<object>();

        public LevelsViewModel()
        {
            // when the collection is changed update the view.
            LessonsAndSeperators.CollectionChanged
                += (object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
                {
                    OnPropertyChanged(nameof(LessonsAndSeperators));
                };
            User.PropertyChanged += UserChanged;
            // Levels VM is created after the courses are loaded.
            RegenerateList();
        }

        /// <summary>
        /// When the <see cref="CurrentUser.User"/> is changed.
        /// </summary>
        private void UserChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // if current course is changed.
            if (e.PropertyName == nameof(User.CurrentCourse))
            {
                RegenerateList();
            }
        }

        /// <summary>
        /// Regenerate the list.
        /// </summary>
        private void RegenerateList()
        {
            LessonsAndSeperators.Clear();
            foreach (var unit in User.CurrentCourse.Units.OrderBy(u => u.UnitNumber))
            {
                LessonsAndSeperators.Add(new UnitSeperator(unit));
                foreach (var l in unit.Lessons.OrderBy(l => l.Number))
                {
                    LessonsAndSeperators.Add(l);
                }
            }
        }

    }

    /// <summary>
    /// Represents a Unit seperator to be displayed in <see cref="LevelsViewModel"/>.
    /// </summary>
    public class UnitSeperator
    {
        /// <summary>
        /// The unit.
        /// </summary>
        public Unit Unit { get; set; }
        public UnitSeperator(Unit unit)
        {
            Unit = unit;
        }
    }
}
