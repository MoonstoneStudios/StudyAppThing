using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private bool showWord = true;

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

        /// <summary>
        /// A list of flashcards marked as "know this".
        /// </summary>
        private List<FlashCard> knowThis = new List<FlashCard>();

        /// <summary>
        /// A list of flashcards marked as "kninda know".
        /// </summary>
        private List<FlashCard> kindaKnow = new List<FlashCard>();

        /// <summary>
        /// A list of flashcards marked as "dont know this".
        /// </summary>
        private List<FlashCard> dontKnowThis = new List<FlashCard>();

        /// <summary>
        /// The queue of flashcards.
        /// </summary>
        private Queue<FlashCard> flashCardQueue = new Queue<FlashCard>();

        public StudyLessonFlashCardsViewModel(int lessonNum)
        {
            Lesson = User.CurrentUnit.Lessons.Where(l => l.Number == lessonNum).First();

            // shuffle
            Random r = new Random();
            var shuffled = FlashCards.OrderBy(f => r.Next()).ToList();
            shuffled.ForEach(f => flashCardQueue.Enqueue(f));
            CurrentCard = flashCardQueue.Dequeue();

            UpdateFlashCard();
        }

        public void FlipFlashCard()
        {
            // flip
            ShowWord = !ShowWord;
            UpdateFlashCard();
        }

        public void UpdateFlashCard()
        {
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

        [RelayCommand]
        public void KnowThis()
        {
            knowThis.Add(CurrentCard);
            NextCard();
        }

        [RelayCommand]
        public void KindaKnowThis()
        {
            kindaKnow.Add(CurrentCard);
            NextCard();
        }

        [RelayCommand]
        public void DontKnowThis()
        {
            dontKnowThis.Add(CurrentCard);
            NextCard();
        }

        private void NextCard()
        {
            // out of cards
            if (flashCardQueue.Count == 0)
            {
                // no more cards to add to pile.
                if (kindaKnow.Count == 0 && dontKnowThis.Count == 0)
                {
                    ViewSwitcher.SwitchView(new FinishedFlashCardsViewModel());
                    return;
                }

                // add back to pile.
                var newList = new List<FlashCard>();
                newList.AddRange(kindaKnow);
                newList.AddRange(dontKnowThis);

                kindaKnow.Clear();
                dontKnowThis.Clear();

                Random r = new Random();
                newList = newList.OrderBy(f => r.Next()).ToList();
                newList.ForEach(f => flashCardQueue.Enqueue(f));
            }

            CurrentCard = flashCardQueue.Dequeue();
            ShowWord = true;
            UpdateFlashCard();
        }
    }
}
