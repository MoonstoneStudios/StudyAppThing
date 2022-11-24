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

        /// <summary>
        /// A Queue of questions to answer.
        /// </summary>
        private Queue<Question> QuestionsToAnswer = new Queue<Question>();

        /// <summary>
        /// If the current answer was previously incorrect.
        /// </summary>
        [ObservableProperty]
        private bool previouslyIncorrect;

        /// <summary>
        /// The amount of questions left.
        /// </summary>
        public int AmountOfQuestionsLeft => QuestionsToAnswer.Count;

        /// <summary>
        /// The questions answered incorrectly.
        /// </summary>
        private List<Question> questionsWrong = new List<Question>();

        public QuestionsViewModel(int lesson)
        {
            // lessons are indexed starting at one, subtract to make it start at 0.
            var l = User.CurrentUnit.Lessons[lesson - 1];
            Lesson = l;
            GenerateRandQuestions();

            PropertyChanged += ProperyHasChanged;

            CurrentQuestion = QuestionsToAnswer.Dequeue();
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
            // move the progress only if correct.
            if (CorrectAnswer) Progress++;
            // add back into list if icorrect
            else
            {
                // if incorrect.
                CurrentQuestion.PreviouslyIncorrect = true;
                questionsWrong.Add(CurrentQuestion);   
                QuestionsToAnswer.Enqueue(CurrentQuestion);
            }

            if (CurrentQuestion.HasBecause)
            {
                IQuestionView view = QuestionViewModel as IQuestionView;
                Because = view.GetBecause();
            }
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
            QuestionViewModel.PropertyChanged -= QuestionVMChanged;

            if (AmountOfQuestionsLeft == 0)
            {
                // TODO: show congrats and stats view
                // TODO: cache views instead of making new ones.
                Lesson.Progress++;
                var stats = new LessonStats()
                {
                    // TODO: change placeholder when needed
                    NumberOfQuestions = 10,
                    QuestionsWrong = questionsWrong
                };
                var vm = new LessonCompleteViewModel(stats);
                ViewSwitcher.SwitchView(vm);
                //vm.Stats = stats;
            }
            else
            {
                CurrentQuestion = QuestionsToAnswer.Dequeue();
                // reset values
                ShowAnswer = false;
                Because = "";
                // if the question was perviously incorrect,
                // show the "Incorrect" text.
                PreviouslyIncorrect = CurrentQuestion.PreviouslyIncorrect;
            }
        }

        private void GenerateRandQuestions()
        {
            // 10 questions at a time for now
            QuestionsToAnswer.Clear();
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var randQ = random.Next(Lesson.Questions.Count);
                // prevent from selecting the same quetsion.
                while (QuestionsToAnswer.Contains(Lesson.Questions[randQ]))
                    randQ = random.Next(Lesson.Questions.Count);

                QuestionsToAnswer.Enqueue(Lesson.Questions[randQ]);
            }
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
