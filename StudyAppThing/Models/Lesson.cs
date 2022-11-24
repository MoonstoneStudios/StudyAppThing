using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// A lesson is the final step of a <see cref="Course"/>.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// The max progress needed to complete a full circle.
        /// </summary>
        public const int MAX_PROGRESS = 6;

        /// <summary>
        /// The name of the lesson.
        /// </summary>
        public string Name { get; set; } = "";

        private int progress;

        /// <summary>
        /// The progress in the lesson. This will be desomstrated by a circular progress bar.
        /// </summary>
        public int Progress
        {
            get => progress;
            set
            {
                // if the full circles should be incremented.
                // think of this like experience and levels.
                // Progress is like expierience points and FullCircles
                // is like a level.
                if (value >= MAX_PROGRESS + 1)
                {
                    // reset.
                    progress = 0;
                    FullCircles++;
                }
                else
                {
                    progress = value;
                }
            }
        }

        /// <summary>
        /// The amount of times the lesson has been completed fully.
        /// </summary>
        public int FullCircles { get; set; }

        /// <summary>
        /// The full circle text.
        /// </summary>
        public string FullCirclesText => "x" + FullCircles;

        /// <summary>
        /// The lesson number.
        /// </summary>
        /// <remarks>
        /// This is on a per-unit basis. i.e. there can be more than one Lesson 1 as long as it is in a different unit.
        /// </remarks>
        public int Number { get; set; }

        /// <summary>
        /// The questions in the lesson.
        /// </summary>
        public List<Question> Questions { get; set; } = new List<Question>();

        /// <summary>
        /// The link to the image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The <see cref="Bitmap"/> version of <see cref="Image"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap ImageBitmap { get; set; }
    }
}
