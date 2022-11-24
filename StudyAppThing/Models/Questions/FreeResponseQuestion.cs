using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// A question where the user has to type their response.
    /// </summary>
    public class FreeResponseQuestion : Question
    {
        /// <summary>
        /// The least amount of differance between the answer and the response.
        /// </summary>
        public const double ANSWER_TOLERANCE = 2;

        public override QuestionType QuestionType => QuestionType.FreeResponse;

        public override string HeaderText => "Type the Correct Response";

        public override bool HasBecause => true;

        /// <summary>
        /// The question being asked.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// The answer.
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Why the answer is correct.
        /// </summary>
        public string CorrectBecause { get; set; }

        /// <summary>
        /// Why the answer is not correct.
        /// </summary>
        public string IncorrectBecause { get; set; }

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
        /// Responses that will always be marked incorrect.
        /// </summary>
        public string[] ExplicitlyIncorrect { get; set; }

        /// <summary>
        /// If there should be spellcheck.
        /// </summary>
        public bool SpellCheck { get; set; } = true;

        /// <summary>
        /// Get the because.
        /// </summary>
        /// <param name="data">The data is if the question was answered correctly.</param>
        /// <returns></returns>
        public override string GetBecause(object data)
        {
            var correct = (bool)data;
            return correct ? CorrectBecause : IncorrectBecause;
        }

    }
}
