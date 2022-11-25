using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    public class TrueOrFalseQuestion : Question
    {
        public override QuestionType QuestionType => QuestionType.TrueOrFalse;

        public override string HeaderText => "Select the Correct Answer";

        public override bool HasBecause => true;

        /// <summary>
        /// The question being asked.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// The answer.
        /// </summary>
        public bool Answer { get; set; }

        /// <summary>
        /// An image to be displayed.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The <see cref="Bitmap"/> loaded from <see cref="Image"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap ImageBitmap { get; set; }

        /// <summary>
        /// If there is an image.
        /// </summary>
        public bool HasImage => !string.IsNullOrEmpty(Image);

        /// <summary>
        /// Why the answer is correct.
        /// </summary>
        public string CorrectBecause { get; set; }

        /// <summary>
        /// Why the answer is not correct.
        /// </summary>
        public string IncorrectBecause { get; set; }

        /// <summary>
        /// Get the because.
        /// </summary>
        /// <param name="data">If the question was correct.</param>
        /// <returns></returns>
        public override string GetBecause(object data)
        {
            bool b = (bool)data;
            return b ? CorrectBecause : IncorrectBecause;
        }
    }
}
