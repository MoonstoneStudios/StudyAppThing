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
    public interface IQuestionView<T> where T : Question
    {

        /// <summary>
        /// The question this view will use.
        /// </summary>
        public T Question { get; set; }

        /// <summary>
        /// Evaluate the question.
        /// </summary>
        /// <returns>
        /// True if the question is answered correctly.
        /// False if question answered incorretly.
        /// </returns>
        public bool Evaluate();

    }
}
