using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// This is a question that is exactly like <see cref="FreeResponseQuestion"/>,
    /// but the header is different and the <see cref="TranslateFreeResponseQuestion.Question"/> property
    /// will have a different format.
    /// </summary>
    public class TranslateFreeResponseQuestion : FreeResponseQuestion
    {

        public override string HeaderText => "Type the Correct Translation";

        public override QuestionType QuestionType => QuestionType.TranslateFreeResponse;

    }
}
