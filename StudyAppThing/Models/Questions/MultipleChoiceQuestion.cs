﻿using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// A question that is multiple choice.
    /// </summary>
    public class MultipleChoiceQuestion : Question
    {
        public override QuestionType QuestionType { get => QuestionType.MulChoice; }

        public override string HeaderText => "Select the Correct Answer";

        /// <summary>
        /// The choices that can be selected.
        /// </summary>
        /// <remarks>
        /// If the count is greater than 4 then a random 3 choices plus the answer 
        /// will be displayed to the user.
        /// </remarks>
        public string[] Choices { get; set; }

        /// <summary>
        /// The index of the answer. The answer is stored in <see cref="Choices"/>.
        /// </summary>
        public int AnswerIndex { get; set; }

        /// <summary>
        /// The question to be displayed.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// An image to be displayed.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// The <see cref="Bitmap"/> loaded from <see cref="Image"/>.
        /// </summary>
        [JsonIgnore]
        public Bitmap ImageBitmap { get; set; }
    }
}