using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// Just like the <see cref="MultipleChoiceQuestion"/> but the question
    /// is structured differently.
    /// </summary>
    public class FillInTheBlankQuestion : MultipleChoiceQuestion
    {
        public override string HeaderText => "Fill in the Blank";

        public override QuestionType QuestionType => QuestionType.FillInTheBlank;
    }
}
