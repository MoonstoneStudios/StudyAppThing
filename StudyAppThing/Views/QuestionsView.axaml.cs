using Avalonia.Controls;
using Avalonia.Interactivity;
using StudyAppThing.ViewModels;

namespace StudyAppThing.Views
{
    public partial class QuestionsView : UserControl
    {
        public QuestionsView()
        {
            InitializeComponent();
        }

        public void OkClicked(object sender, RoutedEventArgs args)
        {
            // nofify view model.
            QuestionsViewModel vm = (QuestionsViewModel)DataContext;
            vm.QuitLesson();
        }

        public void CancelClicked(object sender, RoutedEventArgs args)
        {
            // nofify view model.
            QuestionsViewModel vm = (QuestionsViewModel)DataContext;
            vm.ShowDialog = false;
        }

    }
}
