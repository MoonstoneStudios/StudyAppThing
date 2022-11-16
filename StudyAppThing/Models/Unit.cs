using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// A unit is a collection of levels.
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// The unit's name.
        /// </summary>
        /// <remarks>
        /// Will be displayed as: Unit # - Name
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// The units number.
        /// </summary>
        /// <remarks>
        /// Will be displayed as: Unit # - Name
        /// </remarks>
        public int UnitNumber { get; set; }

        /// <summary>
        /// The full name of the unit.
        /// </summary>
        public string FullName => $"Unit {UnitNumber} - {Name}";

        /// <summary>
        /// The lessons in the unit.
        /// </summary>
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
