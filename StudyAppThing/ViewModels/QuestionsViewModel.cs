using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using StudyAppThing.Models.Questions;
using StudyAppThing.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class QuestionsViewModel : ViewModelBase, IWindowTitleChanger
    {
        public string WindowTitle => "Questions";

        /// <summary>
        /// The lesson.
        /// </summary>
        [ObservableProperty]
        private Lesson lesson;

        /// <summary>
        /// The View Model for the current question.
        /// </summary>
        [ObservableProperty]
        private QuestionViewModelBase questionViewModel;

        /// <summary>
        /// The current question.
        /// </summary>
        [ObservableProperty]
        private Question currentQuestion;

        /// <summary>
        /// The amount of questions answered.
        /// </summary>
        [ObservableProperty]
        private int progress;

        /// <summary>
        /// If the answer box should be shown.
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SumbitCommand))]
        private bool showAnswer;

        /// <summary>
        /// If the answer is correct.
        /// </summary>
        [ObservableProperty]
        private bool correctAnswer;

        /// <summary>
        /// The because to display.
        /// </summary>
        [ObservableProperty]
        private string because;

        /// <summary>
        /// If the "Are you sure?" dialog should be shown.
        /// </summary>
        [ObservableProperty]
        private bool showDialog;

        public QuestionsViewModel(int lesson)
        {
            // lessons are indexed starting at one, subtract to make it start at 0.
            var l = User.CurrentUnit.Lessons[lesson - 1];
            Lesson = l;
            var random = new Random();
            var randQ = random.Next(Lesson.Questions.Count);
            PropertyChanged += ProperyHasChanged;

            CurrentQuestion = Lesson.Questions[randQ];
        }

        private void ProperyHasChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentQuestion))
            {
                switch (CurrentQuestion.QuestionType)
                {
                    case QuestionType.MulChoice:
                        var q = CurrentQuestion as MultipleChoiceQuestion;
                        q.Shuffle();
                        QuestionViewModel = new MulChoiceQuestionViewModel()
                        {
                            Question = q
                        };
                        QuestionViewModel.PropertyChanged += QuestionVMChanged;
                        break;
                    default:
                        break;
                }
            }
        }

        private void QuestionVMChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IQuestionView.CanSubmit))
            {
                SumbitCommand.NotifyCanExecuteChanged();
            }
        }

        [RelayCommand(CanExecute = nameof(CanSubmit))]
        public void Sumbit()
        {
            CorrectAnswer = QuestionViewModel.Evaluate();
            if (CurrentQuestion.HasBecause)
            {
                IQuestionView view = QuestionViewModel as IQuestionView;
                Because = view.GetBecause();
            }
            Progress++;
            ShowAnswer = true;
        }

        private bool CanSubmit()
        {
            if (ShowAnswer) return false;

            // all questions should be IQuestionView
            var view = QuestionViewModel as IQuestionView;
            return view.CanSubmit;
        }

        [RelayCommand]
        private void MoveOn()
        {
            var random = new Random();
            var randQ = random.Next(Lesson.Questions.Count);
            QuestionViewModel.PropertyChanged -= QuestionVMChanged;
            // prevent from selecting the same quetsion.
            while (Lesson.Questions[randQ] == CurrentQuestion)
                randQ = random.Next(Lesson.Questions.Count);
            CurrentQuestion = Lesson.Questions[randQ];
            // reset values
            ShowAnswer = false;
            Because = "";
        }

        [RelayCommand]
        private void QuitButton()
        {
            ShowDialog = true;
        }

        public void QuitLesson()
        {
            ViewSwitcher.SwitchView(new HomeViewModel());
        }

    }
}
