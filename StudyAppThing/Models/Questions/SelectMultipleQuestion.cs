using Avalonia.Media.Imaging;
using DynamicData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    public class SelectMultipleQuestion : Question
    {
        public override QuestionType QuestionType => QuestionType.SelectMultiple;

        public override string HeaderText => "Select the Correct Responses (There is More Than One!)";

        public override bool HasBecause => true;

        /// <summary>
        /// The choices that can be selected.
        /// </summary>
        /// <remarks>
        /// If the count is greater than 4 then a random 3 choices plus the answer 
        /// will be displayed to the user.
        /// </remarks>
        public string[] Choices { get; set; }

        /// <summary>
        /// An unshuffled verion of <see cref="Choices"/>.
        /// </summary>
        [JsonIgnore]
        public string[] ChoicesUnshuffled { get; set; }

        /// <summary>
        /// Why the answer is correct.
        /// </summary>
        public string CorrectBecause { get; set; }

        /// <summary>
        /// Why the answer is incorrect.
        /// </summary>
        public string IncorrectBecause { get; set; }

        /// <summary>
        /// The indicies of the answer. The answer is stored in <see cref="Choices"/>.
        /// </summary>
        public int[] AnswerIndicies { get; set; }

        /// <summary>
        /// The question to be displayed.
        /// </summary>
        public string Question { get; set; }

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
        /// If set to true, partial credit will not be givin. 
        /// i.e. all correct choices must be selected. 
        /// </summary>
        public bool DontAllowPartialCredit { get; set; }

        /// <summary>
        /// Shuffle the choices.
        /// </summary>
        public void Shuffle()
        {
            // credit: https://stackoverflow.com/a/108836
            var r = new Random();
            string[] answers = new string[AnswerIndicies.Length];
            for (int i = 0; i < AnswerIndicies.Length; i++)
            {
                answers[i] = Choices[AnswerIndicies[i]];
            }
            Choices = Choices.OrderBy(x => r.Next()).ToArray();
            // end credit
            for (int i = 0; i < Choices.Length; i++)
            {
                for (int j = 0; j < answers.Length; j++)
                {
                    if (Choices[i] == answers[j])
                    {
                        // set the indecies to the shuffled values.
                        AnswerIndicies[j] = i;
                        break;
                    }
                }
            }

            // create a temp
            var temp = new string[] { Choices[0], Choices[1], Choices[2], Choices[3] };
            for (int i = 0; i < answers.Length; i++)
            {
                if (!temp.Contains(answers[i]))
                {
                    // if the answer is not part of the first 4 choices
                    // put it there to garuntee that it will be a selectable option.
                    var ind = r.Next(4);
                    Choices[ind] = answers[i];
                    // TODO: update answer indexes lol
                }
            }
        }

        /// <summary>
        /// Get the because.
        /// </summary>
        /// <param name="data">Data is if the question was answered correctly.</param>
        /// <returns></returns>
        public override string GetBecause(object data)
        {
            bool correct = (bool)data;
            return correct ? CorrectBecause : IncorrectBecause;
        }
    }
}
