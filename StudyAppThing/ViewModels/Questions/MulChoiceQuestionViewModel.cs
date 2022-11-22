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
    public partial class MulChoiceQuestionViewModel : QuestionViewModelBase, IQuestionView
    {
        new public MultipleChoiceQuestion Question { get; set; }

        public bool CanSubmit => SelectedButton != -1;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubmit))]
        [NotifyPropertyChangedFor(nameof(IsZeroSelected))]
        [NotifyPropertyChangedFor(nameof(IsOneSelected))]
        [NotifyPropertyChangedFor(nameof(IsTwoSelected))]
        [NotifyPropertyChangedFor(nameof(IsThreeSelected))]
        private int selectedButton = -1;

        public bool IsZeroSelected
        {
            get
            {
                return SelectedButton == 0;
            }
        }

        public bool IsOneSelected
        {
            get
            {
                return SelectedButton == 1;
            }
        }

        public bool IsTwoSelected
        {
            get
            {
                return SelectedButton == 2;
            }
        }

        public bool IsThreeSelected
        {
            get
            {
                return SelectedButton == 3;
            }
        }

        public override bool Evaluate()
        {
            Console.WriteLine(SelectedButton == Question.AnswerIndex);
            return SelectedButton == Question.AnswerIndex;
        }

        [RelayCommand]
        public void Select(string num)
        {
            int i = int.Parse(num);
            if (i == SelectedButton)
                SelectedButton = -1;
            else SelectedButton = i;
        }

    }
}
