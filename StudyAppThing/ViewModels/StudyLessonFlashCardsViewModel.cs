using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using StudyAppThing.Misc;
using StudyAppThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.ViewModels
{
    public partial class StudyLessonFlashCardsViewModel : ViewModelBase, IWindowTitleChanger
    {
        public string WindowTitle => "Study Flash Cards";

        /// <summary>
        /// The lesson.
        /// </summary>
        [ObservableProperty]
        private Lesson lesson;

        /// <summary>
        /// The lesson's flash cards.
        /// </summary>
        public List<FlashCard> FlashCards => Lesson.FlashCards;

        /// <summary>
        /// The current flach card.
        /// </summary>
        [ObservableProperty]
        private FlashCard currentCard;

        /// <summary>
        /// If the word side should be shown.
        /// </summary>
        [ObservableProperty]
        private bool showWord;

        /// <summary>
        /// The string to show on the card. It is the flashcard's Word or Definition.
        /// </summary>
        [ObservableProperty]
        private string stringToShow;

        /// <summary>
        /// The image to show.
        /// </summary>
        [ObservableProperty]
        private Bitmap imageToShow;

        public StudyLessonFlashCardsViewModel(int lessonNum)
        {
            Lesson = User.CurrentUnit.Lessons.Where(l => l.Number == lessonNum).First();
            // TODO
            CurrentCard = FlashCards[0];
            UpdateFlashCard();
        }

        public void UpdateFlashCard()
        {
            // flip
            ShowWord = !ShowWord;
            ImageToShow = null;
            if (ShowWord)
            {
                StringToShow = CurrentCard.Word;
                if (CurrentCard.WordHasImage) ImageToShow = CurrentCard.WordImageBitmap;
            }
            else
            {
                StringToShow = CurrentCard.Definition;
                if (CurrentCard.DefinitionHasImage) ImageToShow = CurrentCard.DefinitionImageBitmap;
            }
        }

    }
}
