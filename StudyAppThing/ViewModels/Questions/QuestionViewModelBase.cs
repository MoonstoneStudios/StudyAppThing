using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels.Questions
{
    public partial class QuestionViewModelBase : ViewModelBase
    {
        /// <summary>
        /// The question.
        /// </summary>
        public Question Question;

        /// <summary>
        /// Evaluate the question for correctness.
        /// </summary>
        /// <returns>If the question is correct.</returns>
        public virtual bool Evaluate()
        {
            throw new Exception();
        }
    }
}
