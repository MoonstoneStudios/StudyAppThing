using CommunityToolkit.Mvvm.ComponentModel;
using F23.StringSimilarity;
using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels.Questions
{
    public partial class FreeResponseQuestionViewModel : QuestionViewModelBase, IQuestionView, IQuestionViewModelWithNotes
    {
        new public FreeResponseQuestion Question { get; set; }

        public bool CanSubmit => !string.IsNullOrWhiteSpace(response);

        public string Notes
        {
            get
            {
                if (!ExactAnswerProvided && correct)
                {
                    return $"There seems to be a slight typo! You probably meant: \"{Question.Answer}\"";
                }
                return "";
            }
        }

        /// <summary>
        /// The user's response.
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubmit))]
        private string response;

        /// <summary>
        /// If the exact answer was provided.
        /// </summary>
        public bool ExactAnswerProvided { get; set; }

        /// <summary>
        /// If the question was answered correctly.
        /// </summary>
        private bool correct;

        public override bool Evaluate()
        {
            // credit: https://stackoverflow.com/a/5871826
            // remove all punctuation
            var r = Regex.Replace(Response, @"[^\w\s]", "").ToLower();
            var a = Regex.Replace(Question.Answer, @"[^\w\s]", "").ToLower();
            // end credit

            // if the response is the same.
            if (r == a)
            {
                ExactAnswerProvided = true;
                correct = true;
                return true;
            }

            // if the response is explicitly incorrect
            if (Question.ExplicitlyIncorrect.Contains(r))
            {
                correct = false;
                return false;
            }

            if (Question.SpellCheck)
            {
                var dam = new Damerau();
                var similarity = dam.Distance(a, r);
                correct = false;

                // similar enough to the string.
                if (similarity <= FreeResponseQuestion.ANSWER_TOLERANCE)
                {
                    ExactAnswerProvided = false;
                    correct = true;
                    return true;
                }
            }

            return false;
        }

        public string GetBecause()
        {
            return Question.GetBecause(correct);
        }
    }
}
