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
    public partial class TrueOrFalseQuestionViewModel : QuestionViewModelBase, IQuestionView
    {
        new public TrueOrFalseQuestion Question { get; set; }

        public bool CanSubmit => selectedButton != -1;

        /// <summary>
        /// The selected button.
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsTrueSelected))]
        [NotifyPropertyChangedFor(nameof(IsFalseSelected))]
        [NotifyPropertyChangedFor(nameof(CanSubmit))]
        private int selectedButton = -1;

        /// <summary>
        /// If the true button is selected.
        /// </summary>
        public bool IsTrueSelected => SelectedButton == 0;

        /// <summary>
        /// If the false button is selected.
        /// </summary>
        public bool IsFalseSelected => SelectedButton == 1;

        public override bool Evaluate()
        {
            return (Question.Answer && IsTrueSelected) || (!Question.Answer && IsFalseSelected);
        }

        public string GetBecause()
        {
            return Question.GetBecause(Evaluate());
        }

        [RelayCommand]
        public void Select(string num)
        {
            int i = int.Parse(num);
            // if button already selected unselect it.
            if (i == SelectedButton)
                SelectedButton = -1;
            else SelectedButton = i;
        }
    }
}
