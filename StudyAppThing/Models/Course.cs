using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// A course is a collection of <see cref="Unit"/>s and levels.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The name of the course.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// The <see cref="Unit"/>s in the course.
        /// </summary>
        public List<Unit> Units { get; set; } = new List<Unit>();

        /// <summary>
        /// The link to the image file.
        /// </summary>
        public string Image { get; set; } = "";

        /// <summary>
        /// Who the course is by.
        /// </summary>
        public string By { get; set; } = "";

        /// <summary>
        /// The <see cref="Bitmap"/> loaded from <see cref="Image"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap ImageBitmap { get; set; }

    }
}
