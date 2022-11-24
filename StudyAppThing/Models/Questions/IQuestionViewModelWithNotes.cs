using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// If the question view has specific notes for the user.
    /// </summary>
    public interface IQuestionViewModelWithNotes
    {
        /// <summary>
        /// If the question has specific notes for the user.
        /// </summary>
        public string Notes { get;}
    }
}
