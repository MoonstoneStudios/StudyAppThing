﻿using StudyAppThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.RepresentationModel;
using StudyAppThing.Models.Questions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Avalonia.Media.Imaging;

using FilePath = System.IO.Path;

namespace StudyAppThing.Loader
{
    /// <summary>
    /// Loads and parses course information
    /// </summary>
    public class CourseLoader
    {
        public const string APP_SAVE_FOLDER = "StudyAppThing";

        // TODO: error handling

        /// <summary>
        /// Load all the courses
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Course"/>s.</returns>
        public List<Course> LoadAllCourses()
        {
            var courses = new List<Course>();
            // get the special directory.
            var appSaves = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appSaves = FilePath.Combine(appSaves, APP_SAVE_FOLDER);

            if (!Directory.Exists(appSaves)) Directory.CreateDirectory(appSaves);

            foreach (var save in Directory.GetFiles(appSaves, "*.zip"))
            {
                courses.Add(LoadCourse(save));
            }

            return courses;
        }

        /// <summary>
        /// Load the course data from a zip file.
        /// </summary>
        /// <param name="directory">The zip file's directory.</param>
        /// <returns>A new <see cref="Course"/>.</returns>
        public Course LoadCourse(string directory)
        {
            Course course = new Course();
            var tempDir = FilePath.GetTempPath() + "Study";
            var zipName = FilePath.GetFileNameWithoutExtension(directory);

            // extracxt files to temp directory.
            using (ZipArchive zipFile = ZipFile.OpenRead(directory))
            {
                if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);

                // extract and overrite files.
                zipFile.ExtractToDirectory(tempDir, true);
            }

            // get the folder created by the zip folder.
            // this will get the first folder because it
            // should be the only folder
            var folder = Directory.GetDirectories(tempDir)[0];
            // get the name of the folder
            var folderName = FilePath.GetFileNameWithoutExtension(folder);
            var zipDir = tempDir + "\\" + folderName;
            var files = Directory.GetFiles(zipDir, "*.json", SearchOption.AllDirectories);

            // load the course file first
            foreach (string path in files)
            {
                var name = FilePath.GetFileNameWithoutExtension(path);
                // Load the course data file.
                if (name == "Course")
                {
                    CourseFile cFile = LoadFile<CourseFile>(path);
                    // set the course data.
                    AssignCourseValues(ref course, cFile, zipDir);
                    break;
                }
            }

            // load the files.
            foreach (var file in files)
            {
                var name = FilePath.GetFileNameWithoutExtension(file);
                // already loaded file.
                if (name == "Course") { continue; }
                else if (name == "Flash Cards") 
                { 
                    // load the flah card
                    LoadFlashCards(course, file, zipDir); 
                }
                else
                {
                    // load the question
                    LoadQuestionFile(file, course, zipDir);
                }
            }

            // remove the temp folder, everything has been loaded.
            Directory.Delete(tempDir, true);
            return course;
        }

        /// <summary>
        /// Load a YAML file to a C# object.
        /// </summary>
        /// <typeparam name="T">The C# object's <see cref="Type"/>.</typeparam>
        /// <param name="file">The path to the file.</param>
        /// <returns>The C# object.</returns>
        private T LoadFile<T>(string file)
        {
            var text = File.ReadAllText(file);
            T obj = JsonConvert.DeserializeObject<T>(text);
            return obj;
        }

        /// <summary>
        /// Assign the course values from the Course.yaml file.
        /// </summary>
        /// <param name="course">The <see cref="Course"/>.</param>
        /// <param name="courseFile">The <see cref="CourseFile"/></param>
        private void AssignCourseValues(ref Course course, CourseFile courseFile, string zipDir)
        {
            // set course values
            course.Name = courseFile.Name;
            course.Image = courseFile.Image;
            course.By = courseFile.By;

            using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", course.Image)))
            {
                // 500 as placeholder for now
                course.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
            }

            // loop through units.
            List<Unit> units = new List<Unit>();

            foreach (UnitFile uFile in courseFile.Units)
            {
                // make the unit.
                Unit u = new Unit()
                {
                    UnitNumber = uFile.Number,
                    Name = uFile.Name
                };

                // loop through lessons.
                List<Lesson> lessons = new List<Lesson>();
                foreach (LessonFile lFile in uFile.Lessons)
                {
                    // set lesson data
                    Lesson lesson = new Lesson();
                    lesson.Name = lFile.Name;
                    lesson.Number = lFile.Number;
                    lesson.Image = lFile.Image;
                    lesson.Progress = lFile.Progress;
                    lesson.FullCircles = lFile.FullCircles;

                    // load icon
                    using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", lesson.Image)))
                    {
                        // 500 as placeholder for now
                        lesson.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                    }

                    lessons.Add(lesson);
                }

                // assign the lessons.
                u.Lessons = lessons;
                // add units to course
                units.Add(u);
            }

            course.Units = units;
        }

        /// <summary>
        /// Load the question file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="course">The course.</param>
        private void LoadQuestionFile(string file, Course course, string zipDir)
        {
            string text = File.ReadAllText(file);

            // get the root object.
            JObject rootObject = JObject.Parse(text);

            // get the lesson and unit number from the file.
            var unitNumber = (int)rootObject["Unit"];
            var lessonNumber = (int)rootObject["Lesson"];

            // get the lesson and unit the questions are part of.
            Unit unit = course.Units.Where(u => u.UnitNumber == unitNumber).First();
            Lesson lesson = unit.Lessons.Where(l => l.Number == lessonNumber).First();

            List<Question> questions = new List<Question>();

            JArray jQuestions = (JArray)rootObject["Questions"];
            // loop through questions and load.
            foreach (JObject question in jQuestions.Cast<JObject>())
            {
                string type = (string)question["Type"];
                // load question based on type.
                switch (type)
                {
                    // load the multiple choice questions
                    case nameof(QuestionType.MulChoice):
                        {
                            List<string> choices = new List<string>();
                            List<string> because = new List<string>();

                            foreach (var item in (JArray)question["Choices"])
                            {
                                // if there is a because.
                                if (item is JArray array)
                                {
                                    // the answer.
                                    choices.Add((string)array[0]);
                                    // the because
                                    because.Add((string)array[1]);
                                }
                                else
                                {
                                    // just an answer.
                                    choices.Add((string)item);
                                }
                            }

                            MultipleChoiceQuestion q = 
                                JsonConvert.DeserializeObject<MultipleChoiceQuestion>(question.ToString());

                            q.Choices = choices.ToArray();
                            q.ChoicesUnshuffled = choices.ToArray();
                            q.Because = because.ToArray();

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }
                            q.Shuffle();
                            questions.Add(q);
                            break;
                        }
                    case nameof(QuestionType.FreeResponse):
                        {
                            FreeResponseQuestion q =
                                JsonConvert.DeserializeObject<FreeResponseQuestion>(question.ToString());

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }

                            for (int i = 0; i < q.ExplicitlyIncorrect.Length; i++)
                            {
                                q.ExplicitlyIncorrect[i] = q.ExplicitlyIncorrect[i].ToLower();
                            }

                            questions.Add(q);
                            break;
                        }
                    case nameof(QuestionType.TrueOrFalse):
                        {
                            TrueOrFalseQuestion q =
                                JsonConvert.DeserializeObject<TrueOrFalseQuestion>(question.ToString());

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }

                            questions.Add(q);
                            break;
                        }
                    case nameof(QuestionType.TranslateFreeResponse):
                        {
                            TranslateFreeResponseQuestion q =
                                JsonConvert.DeserializeObject<TranslateFreeResponseQuestion>(question.ToString());

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }

                            for (int i = 0; i < q.ExplicitlyIncorrect.Length; i++)
                            {
                                q.ExplicitlyIncorrect[i] = q.ExplicitlyIncorrect[i].ToLower();
                            }

                            questions.Add(q);
                            break;
                        }
                    case nameof(QuestionType.FillInTheBlank):
                        {
                            List<string> choices = new List<string>();
                            List<string> because = new List<string>();

                            foreach (var item in (JArray)question["Choices"])
                            {
                                // if there is a because.
                                if (item is JArray array)
                                {
                                    // the answer.
                                    choices.Add((string)array[0]);
                                    // the because
                                    because.Add((string)array[1]);
                                }
                                else
                                {
                                    // just an answer.
                                    choices.Add((string)item);
                                }
                            }

                            FillInTheBlankQuestion q =
                                JsonConvert.DeserializeObject<FillInTheBlankQuestion>(question.ToString());

                            q.Choices = choices.ToArray();
                            q.ChoicesUnshuffled = choices.ToArray();
                            q.Because = because.ToArray();

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }
                            q.Shuffle();
                            questions.Add(q);
                            break;
                        }
                    case nameof(QuestionType.SelectMultiple):
                        {
                            SelectMultipleQuestion q =
                                JsonConvert.DeserializeObject<SelectMultipleQuestion>(question.ToString());

                            //Array.Copy(q.Choices, q.ChoicesUnshuffled, q.Choices.Length);

                            if (q.HasImage)
                            {
                                using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", q.Image)))
                                {
                                    // 500 as placeholder for now
                                    q.ImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                                }
                            }
                            q.Shuffle();
                            questions.Add(q);
                            break;
                        }
                }
            }

            // set the questions
            // add to existing list in case questions are in different files.
            lesson.Questions = lesson.Questions.Concat(questions).ToList();
        }

        /// <summary>
        /// Load the flash cards.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <param name="file">The directory to the flash card file.</param>
        private void LoadFlashCards(Course course, string file, string zipDir)
        {
            var text = File.ReadAllText(file);
            // load in the root file.
            JObject rootObject = JObject.Parse(text);

            // get the lesson and unit number from the file.
            var unitNumber = (int)rootObject["Unit"];
            var lessonNumber = (int)rootObject["Lesson"];

            // get the lesson and unit the questions are part of.
            Unit unit = course.Units.Where(u => u.UnitNumber == unitNumber).First();
            Lesson lesson = unit.Lessons.Where(l => l.Number == lessonNumber).First();

            List<FlashCard> flashCards = new List<FlashCard>();

            // get the array of flashcard object
            JArray array = (JArray)rootObject["FlashCards"];
            foreach (JObject flashCard in array.Cast<JObject>())
            {
                // deserialize the flashcard
                FlashCard card = JsonConvert.DeserializeObject<FlashCard>(flashCard.ToString());

                // if there is a word image.
                if (card.WordHasImage)
                {
                    using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", card.WordImage)))
                    {
                        // 500 as placeholder for now
                        card.WordImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                    }
                }
                // if there is a definition image.
                if (card.DefinitionHasImage)
                {
                    using (FileStream stream = File.OpenRead(FilePath.Combine(zipDir, "Assets", card.DefinitionImage)))
                    {
                        // 500 as placeholder for now
                        card.DefinitionImageBitmap = Bitmap.DecodeToWidth(stream, 500);
                    }
                }

                // add the flashcard to the list.
                flashCards.Add(card);
            }

            lesson.FlashCards = flashCards;
        }
    }
}
