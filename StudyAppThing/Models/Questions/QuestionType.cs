using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// The types of question.
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// The user is presented with options and needs to select
        /// the correct answer.
        /// </summary>
        MulChoice,

        /// <summary>
        /// The user is presented with a text box and needs to
        /// type in the correct answer.
        /// </summary>
        FreeResponse,

        /// <summary>
        /// A true or false question.
        /// </summary>
        TrueOrFalse,

        /// <summary>
        /// A question where a user is provided text to translate
        /// and must type out the response.
        /// </summary>
        TranslateFreeResponse
    }
}
