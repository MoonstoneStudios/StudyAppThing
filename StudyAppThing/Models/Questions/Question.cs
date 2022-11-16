using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using StudyAppThing.Loader;

namespace StudyAppThing.Models.Questions
{
    /// <summary>
    /// The interface for questions.
    /// </summary>
    public abstract class Question
    {
        /// <summary>
        /// The question type.
        /// </summary>
        public abstract QuestionType QuestionType { get; }

        /// <summary>
        /// The header text for the question.
        /// </summary>
        public abstract string HeaderText { get; }
    }
}
