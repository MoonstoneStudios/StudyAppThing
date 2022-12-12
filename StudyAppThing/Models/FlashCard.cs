using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// A flash card will contain a definition and a word. There will be an optional image.
    /// </summary>
    public class FlashCard
    {

        /// <summary>
        /// The word of the flash card. (front side)
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// The definition on the flash card. (back side).
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// The image to be shown on the word side.
        /// </summary>
        public string WordImage { get; set; }

        /// <summary>
        /// The image to be shown on the definition side.
        /// </summary>
        public string DefinitionImage { get; set; }

        /// <summary>
        /// If the word has an image.
        /// </summary>
        public bool WordHasImage => !string.IsNullOrEmpty(WordImage);

        /// <summary>
        /// If the definition has an image.
        /// </summary>
        public bool DefinitionHasImage => !string.IsNullOrEmpty(DefinitionImage);

        /// <summary>
        /// The <see cref="Bitmap"/> version of <see cref="WordImage"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap WordImageBitmap { get; set; }

        /// <summary>
        /// The <see cref="Bitmap"/> version of <see cref="DefinitionImage"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap DefinitionImageBitmap { get; set; }

    }
}
