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
                        break;
                    default:
                        break;
                }
            }
        }

        [RelayCommand]
        public void Sumbit()
        {
            bool correct = QuestionViewModel.Evaluate();
            if (correct)
            {
                var random = new Random();
                var randQ = random.Next(Lesson.Questions.Count);
                // prevent from selecting the same quetsion.
                while (Lesson.Questions[randQ] == CurrentQuestion)
                    randQ = random.Next(Lesson.Questions.Count);
                CurrentQuestion = Lesson.Questions[randQ];
            }
        }

    }
}
