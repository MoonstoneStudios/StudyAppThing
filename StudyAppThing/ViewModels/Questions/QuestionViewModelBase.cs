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

        public Question Question;

        public virtual bool Evaluate()
        {
            throw new Exception();
        }

    }
}
