using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels.Questions
{
    public partial class MulChoiceQuestionViewModel : QuestionViewModelBase
    {
        new public MultipleChoiceQuestion Question { get; set; }

        [ObservableProperty]
        private int selectedButton;

        public override bool Evaluate()
        {
            Console.WriteLine(SelectedButton == Question.AnswerIndex);
            return SelectedButton == Question.AnswerIndex;
        }

        [RelayCommand]
        public void Select(string num)
        {
            int i = int.Parse(num);
            SelectedButton = i;
        }

    }
}
