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

        /// <summary>
        /// If the question has a because.
        /// </summary>
        public abstract bool HasBecause { get; }

        /// <summary>
        /// Get the question's because based on the answer.
        /// </summary>
        /// <remarks>
        /// This is handled in the implementation.
        /// </remarks>
        /// <param name="data">Data related to the question. Added by the View Model.</param>
        /// <returns>A string the of because.</returns>
        public virtual string GetBecause(object data)
        {
            return "";
        }
    }
}
