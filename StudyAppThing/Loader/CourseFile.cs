using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAppThing.Models;

namespace StudyAppThing.Loader
{
    /// <summary>
    /// Represents the Course.yaml file.
    /// </summary>
    public sealed class CourseFile
    {
        /// <summary>
        /// The name of the course.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The image of the course.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Who the course is by.
        /// </summary>
        public string By { get; set; }

        /// <summary>
        /// The units in the course.
        /// </summary>
        public List<UnitFile> Units { get; set; }
    }

    /// <summary>
    /// Represents a <see cref="Unit"/> in the Course.yaml file.
    /// </summary>
    public sealed class UnitFile
    {
        /// <summary>
        /// The unit number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The name of the unit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lessons in the unit.
        /// </summary>
        public List<LessonFile> Lessons { get; set; }
    }

    /// <summary>
    /// Represents a <see cref="Lesson"/> in the Course.yaml file.
    /// </summary>
    public sealed class LessonFile
    {
        /// <summary>
        /// The lesson number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The name of the lesson.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The image of the lesson.
        /// </summary>
        public string Image { get; set; }
    }

}
