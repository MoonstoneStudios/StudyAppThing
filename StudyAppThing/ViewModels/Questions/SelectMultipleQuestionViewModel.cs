using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Models.Questions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels.Questions
{
    public partial class SelectMultipleQuestionViewModel : QuestionViewModelBase, IQuestionView
    {
        new public SelectMultipleQuestion Question { get; set; }

        public bool CanSubmit => SelectedButtons.Count != 0;

        [ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(CanSubmit))]
        //[NotifyPropertyChangedFor(nameof(IsZeroSelected))]
        //[NotifyPropertyChangedFor(nameof(IsOneSelected))]
        //[NotifyPropertyChangedFor(nameof(IsTwoSelected))]
        //[NotifyPropertyChangedFor(nameof(IsThreeSelected))]
        private ObservableCollection<int> selectedButtons = new ObservableCollection<int>();

        public bool IsZeroSelected
        {
            get
            {
                return SelectedButtons.Contains(0);
            }
        }

        public bool IsOneSelected
        {
            get
            {
                return SelectedButtons.Contains(1);
            }
        }

        public bool IsTwoSelected
        {
            get
            {
                return SelectedButtons.Contains(2);
            }
        }

        public bool IsThreeSelected
        {
            get
            {
                return SelectedButtons.Contains(3);
            }
        }

        public SelectMultipleQuestionViewModel()
        {
            SelectedButtons.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(CanSubmit));
                OnPropertyChanged(nameof(IsZeroSelected));
                OnPropertyChanged(nameof(IsOneSelected));
                OnPropertyChanged(nameof(IsTwoSelected));
                OnPropertyChanged(nameof(IsThreeSelected));
            };
        }

        public override bool Evaluate()
        {
            bool correct = true;
            //bool partial = false;
            for (int i = 0; i < Question.AnswerIndicies.Length; i++)
            {
                // if the correct button is selected but answer already incorrect.
                //if (SelectedButtons.Contains(Question.AnswerIndicies[i]) && !correct)
                //{
                //    partial = true;
                //}
                // if the correct answer is not selected.
                if (!SelectedButtons.Contains(Question.AnswerIndicies[i])) 
                {
                    correct = false;
                }
            }

            // if there are more selected than correct.
            if (SelectedButtons.Count > Question.AnswerIndicies.Length)
            {
                // if other answers are correct then set partial.
                //if (correct) partial = true;
                correct = false;
            }

            //if (Question.DontAllowPartialCredit) partial = false;

            // TODO: partial credit
            // TODO: refine logic
            return correct;
        }

        [RelayCommand]
        public void Select(string num)
        {
            int i = int.Parse(num);
            // if button already selected unselect it.
            if (SelectedButtons.Contains(i))
                SelectedButtons.Remove(i);
            else SelectedButtons.Add(i);
        }

        public string GetBecause()
        {
            return Question.GetBecause(Evaluate());
        }
    }
}
