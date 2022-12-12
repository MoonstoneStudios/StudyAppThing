using Avalonia.Controls;
using Avalonia.Input;
using StudyAppThing.ViewModels;

// namespace is not the same as the folder
// because the ViewLocator uses the namespace and the name, not JUST the name.
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
            vm.UpdateFlashCard();
        }

    }
}
