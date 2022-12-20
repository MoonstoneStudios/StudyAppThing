using Avalonia.Controls;
using Avalonia.Input;
using StudyAppThing.ViewModels;

namespace StudyAppThing.Views
{
    public partial class StudyLessonFlashCardsView : UserControl
    {
        public StudyLessonFlashCardsView()
        {
            InitializeComponent();
        }

        public void FlashCardClicked(object sender, PointerPressedEventArgs e)
        {
            StudyLessonFlashCardsViewModel vm = (StudyLessonFlashCardsViewModel)DataContext;
            vm.FlipFlashCard();
        }

    }
}
