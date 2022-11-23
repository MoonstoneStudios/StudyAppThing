using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels.Questions
{
    /// <summary>
    /// A question view.
    /// </summary>
    public interface IQuestionView
    {
        public bool CanSubmit { get; }

        /// <summary>
        /// Evaluate the question.
        /// </summary>
        /// <returns>
        /// True if the question is answered correctly.
        /// False if question answered incorretly.
        /// </returns>
        public bool Evaluate();

        /// <summary>
        /// Get the question's because.
        /// </summary>
        /// <returns>The because.</returns>
        public string GetBecause();
    }
}
