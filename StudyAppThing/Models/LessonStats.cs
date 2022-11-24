using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models
{
    /// <summary>
    /// Stats on how a user did in a lesson.
    /// </summary>
    public class LessonStats
    {
        /// <summary>
        /// The number of incorrect questions.
        /// </summary>
        public int NumberIncorrect => QuestionsWrong.Count;

        /// <summary>
        /// The list of incorrect questions.
        /// </summary>
        public List<Question> QuestionsWrong { get; set; } = new List<Question>();

        /// <summary>
        /// The number of questions answered.
        /// </summary>
        public int NumberOfQuestions { get; set; }

        /// <summary>
        /// The accuracy as a percentage.
        /// </summary>
        public int Accuracy => CalculateAccuracy();

        /// <summary>
        /// Calculate the accuracy percentage of the user in this specific lesson.
        /// </summary>
        /// <returns>A number from 0-100.</returns>
        private int CalculateAccuracy()
        {
            // Precent proportion.
            // This finds the percent
            // of # correct to total questions.
            //
            // # correct         x
            //----------  =  ----------
            // total            100

            if (NumberIncorrect == 0) return 100;
            int correct = NumberOfQuestions - NumberIncorrect;
            // cross-multiply
            correct *= 100;
            // divide by total (this finds x, cross-multiplying not done here)
            correct /= NumberOfQuestions;

            return correct;
        }
    }
}
