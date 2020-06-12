using AppQuiz.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Persistence.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Common;
using AppQuiz.Application.Chapters.Specifications;
using AppQuiz.Application.Quizzes.Specifications;
using AppQuiz.Application.Questions.Specifications;

namespace AppQuiz.Api.Data
{
    public static class SeedData
    {
        //ChapterIds
        public static Guid CommonTestForBegginer = Guid.Parse("4a88f57a-6fbd-4900-bbff-ebb3405b1278");
        public static Guid GrammarTestForBegginer = Guid.Parse("3857d6ff-96ba-4ccb-a019-a4a87f9bcf7e");
        public static Guid LexicalForBegginer = Guid.Parse("0b3c6630-5092-4053-899e-f871c1d8f94b");

        public static Guid CommonTestForElementary = Guid.Parse("f3560a01-0007-41f7-88e1-52c5007fd3fa");
        public static Guid GrammarTestForElementary = Guid.Parse("0345c0da-4492-4457-aac9-979bed7da825");
        public static Guid LexicalForElementary = Guid.Parse("9d88ade1-1a22-4751-bb6d-9df9bfd7e318");

        public static Guid CommonTestForPreIntermediate = Guid.Parse("c257672b-77e3-45cd-b227-86c9fb7ea843");
        public static Guid GrammarTestForPreIntermediate = Guid.Parse("0e6c9246-e9b7-42e6-963c-1bea7932f87a");
        public static Guid LexicalForPreIntermediate = Guid.Parse("c9ca5c94-625c-4071-a0cc-c3c05f6588ec");

        public static Guid CommonTestForIntermediate = Guid.Parse("66a5b3bf-21d6-4b09-a4d7-cc1f574c342e");
        public static Guid GrammarTestForIntermediate = Guid.Parse("a2e42dbe-a872-4a21-b129-9e83edfc04df");
        public static Guid LexicalForIntermediate = Guid.Parse("bb785898-f779-4b09-94ff-77f02f424655");

        public static Guid CommonTestForUpperIntermediate = Guid.Parse("d6d64286-5bf0-4504-8c02-29708c49865d");
        public static Guid GrammarTestForUpperIntermediate = Guid.Parse("b07d8352-47e2-4c2e-a21c-92d3995b3265");
        public static Guid LexicalForUpperIntermediate = Guid.Parse("a474f104-43d9-462c-bcca-68fa8c67d6b2");

        //QuizIds
        public static Guid IdiomsCommonTestForBegginer = Guid.Parse("31c25f92-e51d-4b08-b992-1d6decb06e5d");
        public static Guid SlangCommonTestForBegginer = Guid.Parse("16a4d4d6-0836-45cc-b425-bed772ec9ad5");
        public static Guid PhraseVerbsCommonTestForBegginer = Guid.Parse("fec03f97-5155-4f25-88c5-78d0300c9a50");
        public static Guid BritishOrAmericanCommonTestForBegginer = Guid.Parse("a4e115e7-18e1-40c3-8340-d6ebacb7481f");

        public static Guid ArticlesGrammarTestForBegginer = Guid.Parse("b6c067f5-09df-44a5-8bb0-4feea515e204");
        public static Guid PronounsGrammarTestForBegginer = Guid.Parse("0ab6fc6d-6f44-4b63-a50c-3417b2fcbbac");
        public static Guid ToBeGrammarTestForBegginer = Guid.Parse("eea29bd4-f5df-459a-b743-5dfa4fbd2647");
        public static Guid ToDoGrammarTestForBegginer = Guid.Parse("e81e8def-b4de-495f-8053-a49a23a91661");

        public static Guid AdjectivesLexicalForBegginer = Guid.Parse("3dc70f8c-4e08-4209-9319-b2704ac28f27");
        public static Guid SameWordsLexicalForBegginer = Guid.Parse("4e438425-f2fc-47f8-b5cc-d25ccb28cfff");
        public static Guid UnionsLexicalForBegginer = Guid.Parse("b5e3a5cd-1c51-4e2d-bcad-046f9ccd3903");
        public static Guid WritingLettersLexicalForBegginer = Guid.Parse("d41c0cd9-bfd7-4112-a40f-b648f0637770");


        public static Guid IdiomsCommonTestForElementary = Guid.Parse("1e27ad14-6ee4-4008-90b9-100735f38cd3");
        public static Guid SlangCommonTestForElementary = Guid.Parse("e207903c-e744-40ba-8686-50c8db3bf171");
        public static Guid PhraseVerbsCommonTestForElementary = Guid.Parse("9ca97640-5891-436e-8efb-a1b4e162ed26");
        public static Guid BritishOrAmericanCommonTestForElementary = Guid.Parse("dcbe6157-ab97-4a81-afb4-ac5d6f6491bf");

        public static Guid ArticlesGrammarTestForElementary = Guid.Parse("faf78a3c-38bd-4547-9318-54faab2ba89f");
        public static Guid PronounsGrammarTestForElementary = Guid.Parse("1e70a578-1d62-420d-85cb-2653f839dca2");
        public static Guid ToBeGrammarTestForElementary = Guid.Parse("751b6efc-bfbe-4509-bc5d-f1bba48e66f8");
        public static Guid ToDoGrammarTestForElementary = Guid.Parse("6f2a21eb-d04c-4f01-bf1b-2d3e9ad78a0b");

        public static Guid AdjectivesLexicalForElementary = Guid.Parse("9e1700f6-52dc-4f4e-ac5e-93a56a3beaed");
        public static Guid SameWordsLexicalForElementary = Guid.Parse("7beace94-e849-4a7e-be85-a6dfe22a8d68");
        public static Guid UnionsLexicalForElementary = Guid.Parse("045fffea-0bee-4b0f-96e6-f42af096a496");
        public static Guid WritingLettersLexicalForElementary = Guid.Parse("575e31e8-8fae-4c9e-a06e-1c93760590dd");

        public static Guid IdiomsCommonTestForPreIntermediate = Guid.Parse("a6e70932-3ee8-4ec1-9c6d-624a507c18c0");
        public static Guid SlangCommonTestForPreIntermediate = Guid.Parse("8f63af9b-67b4-4e10-9636-673d2b1e9eca");
        public static Guid PhraseVerbsCommonTestForPreIntermediate = Guid.Parse("a3d534dd-4447-4336-ba70-441365e4e4d3");
        public static Guid BritishOrAmericanCommonTestForPreIntermediate = Guid.Parse("91707f9d-807b-4135-bf02-2c75c188d833");

        public static Guid ArticlesGrammarTestForPreIntermediate = Guid.Parse("ccf43eb5-de92-41d4-bc0d-6611ade2bfee");
        public static Guid PronounsGrammarTestForPreIntermediate = Guid.Parse("e58f97f8-ed6c-45c4-9ba4-0efef3717351");
        public static Guid ToBeGrammarTestForPreIntermediate = Guid.Parse("0ea25022-5c9a-4129-b65c-564b87ba0fdc");
        public static Guid ToDoGrammarTestForPreIntermediate = Guid.Parse("9a09cca3-196a-4649-bd6c-f40140165a7b");

        public static Guid AdjectivesLexicalForPreIntermediate = Guid.Parse("646334a0-5bd1-4343-a0b7-e6a114366d5b");
        public static Guid SameWordsLexicalForPreIntermediate = Guid.Parse("43a40c25-761b-42bf-bbcb-d3b3640f5f7f");
        public static Guid UnionsLexicalForPreIntermediate = Guid.Parse("4ab5988f-2b29-4a87-be8d-7a85c1298afa");
        public static Guid WritingLettersLexicalForPreIntermediate = Guid.Parse("dcc33677-05f6-4b0b-ab5a-310b91c212cc");
        public static async Task InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var chapterRepository = scope.ServiceProvider.GetRequiredService<IRepository<Chapter>>();

                foreach (var chapter in GetChapters())
                {
                    var existing = await chapterRepository.GetAsync(
                        new ChapterByIdSpecification(chapter.Id));
                    if (existing == null)
                        await chapterRepository.SaveAsync(chapter);
                }

                var quizRepository = scope.ServiceProvider.GetRequiredService<IRepository<Quiz>>();

                foreach (var quiz in GetQuizzes())
                {
                    var existing = await quizRepository.GetAsync(
                        new QuizByIdSpecification(quiz.Id));
                    if (existing == null)
                    {
                        await quizRepository.SaveAsync(quiz);
                        
                    }
                }

                var questionRepository = scope.ServiceProvider.GetRequiredService<IRepository<Question>>();
                var existingQuestion = await questionRepository.GetAllAsync();
                if (!existingQuestion.Any())
                {
                    foreach (var question in GetBegginerQuestions())
                    {
                        await questionRepository.SaveAsync(question);
                    }
                    foreach (var question in GetElementaryQuestions())
                    {
                        await questionRepository.SaveAsync(question);
                    }
                    foreach (var question in GetPreImtermidiateQuestions())
                    {
                        await questionRepository.SaveAsync(question);
                    }
                }
            }
        }

        public static List<Chapter> GetChapters()
        {
            return new List<Chapter>()
            {
                new Chapter()
                {
                    Id = CommonTestForBegginer,
                    EnglishLevel = EnglishLevel.Beginner,
                    Name = "Common tests"
                },
                new Chapter()
                {
                    Id = GrammarTestForBegginer,
                    EnglishLevel = EnglishLevel.Beginner,
                    Name = "Grammar tests"
                },
                new Chapter()
                {
                    Id = LexicalForBegginer,
                    EnglishLevel = EnglishLevel.Beginner,
                    Name = "Lexical tests"
                },
                new Chapter()
                {
                    Id = CommonTestForElementary,
                    EnglishLevel = EnglishLevel.Elementary,
                    Name = "Common tests"
                },
                new Chapter()
                {
                    Id = GrammarTestForElementary,
                    EnglishLevel = EnglishLevel.Elementary,
                    Name = "Grammar tests"
                },
                new Chapter()
                {
                    Id = LexicalForElementary,
                    EnglishLevel = EnglishLevel.Elementary,
                    Name = "Lexical tests"
                },
                new Chapter()
                {
                    Id = CommonTestForPreIntermediate,
                    EnglishLevel = EnglishLevel.PreIntermediate,
                    Name = "Common tests"
                },
                new Chapter()
                {
                    Id = GrammarTestForPreIntermediate,
                    EnglishLevel = EnglishLevel.PreIntermediate,
                    Name = "Grammar tests"
                },
                new Chapter()
                {
                    Id = LexicalForPreIntermediate,
                    EnglishLevel = EnglishLevel.PreIntermediate,
                    Name = "Lexical tests"
                },
                new Chapter()
                {
                    Id = CommonTestForIntermediate,
                    EnglishLevel = EnglishLevel.Intermediate,
                    Name = "Common tests"
                },
                new Chapter()
                {
                    Id = GrammarTestForIntermediate,
                    EnglishLevel = EnglishLevel.Intermediate,
                    Name = "Grammar tests"
                },
                new Chapter()
                {
                    Id = LexicalForIntermediate,
                    EnglishLevel = EnglishLevel.Intermediate,
                    Name = "Lexical tests"
                },
                new Chapter()
                {
                    Id = CommonTestForUpperIntermediate,
                    EnglishLevel = EnglishLevel.UpperIntermediate,
                    Name = "Common tests"
                },
                new Chapter()
                {
                    Id = GrammarTestForUpperIntermediate,
                    EnglishLevel = EnglishLevel.UpperIntermediate,
                    Name = "Grammar tests"
                },
                new Chapter()
                {
                    Id = LexicalForUpperIntermediate,
                    EnglishLevel = EnglishLevel.UpperIntermediate,
                    Name = "Lexical tests"
                },
            };
        }

        public static List<Quiz> GetQuizzes()
        {
            return new List<Quiz>() { 
                //Begginer
                new Quiz
                {
                    Id = IdiomsCommonTestForBegginer,
                    ChapterId = CommonTestForBegginer,
                    Title = "Idioms",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SlangCommonTestForBegginer,
                    ChapterId = CommonTestForBegginer,
                    Title = "Slang",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = PhraseVerbsCommonTestForBegginer,
                    ChapterId = CommonTestForBegginer,
                    Title = "Phrase Verbs",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = BritishOrAmericanCommonTestForBegginer,
                    ChapterId = CommonTestForBegginer,
                    Title = "British or American",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = ArticlesGrammarTestForBegginer,
                    ChapterId = GrammarTestForBegginer,
                    Title = "Articles",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = PronounsGrammarTestForBegginer,
                    ChapterId = GrammarTestForBegginer,
                    Title = "Pronouns",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToBeGrammarTestForBegginer,
                    ChapterId = GrammarTestForBegginer,
                    Title = "To Be",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToDoGrammarTestForBegginer,
                    ChapterId = GrammarTestForBegginer,
                    Title = "To Do",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = AdjectivesLexicalForBegginer,
                    ChapterId = GrammarTestForBegginer,
                    Title = "Adjectives",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SameWordsLexicalForBegginer,
                    ChapterId = LexicalForBegginer,
                    Title = "Same Words",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = UnionsLexicalForBegginer,
                    ChapterId = LexicalForBegginer,
                    Title = "Unions",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = WritingLettersLexicalForBegginer,
                    ChapterId = LexicalForBegginer,
                    Title = "Writing Letters",
                    Priority = Priority.High
                },
                //Elementary
                new Quiz
                {
                    Id = IdiomsCommonTestForElementary,
                    ChapterId = CommonTestForElementary,
                    Title = "Idioms",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SlangCommonTestForElementary,
                    ChapterId = CommonTestForElementary,
                    Title = "Slang",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = PhraseVerbsCommonTestForElementary,
                    ChapterId = CommonTestForElementary,
                    Title = "Phrase Verbs",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = BritishOrAmericanCommonTestForElementary,
                    ChapterId = CommonTestForElementary,
                    Title = "British or American",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = ArticlesGrammarTestForElementary,
                    ChapterId = GrammarTestForElementary,
                    Title = "Articles",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = PronounsGrammarTestForElementary,
                    ChapterId = GrammarTestForElementary,
                    Title = "Pronouns",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToBeGrammarTestForElementary,
                    ChapterId = GrammarTestForElementary,
                    Title = "To Be",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToDoGrammarTestForElementary,
                    ChapterId = GrammarTestForElementary,
                    Title = "To Do",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = AdjectivesLexicalForElementary,
                    ChapterId = GrammarTestForElementary,
                    Title = "Adjectives",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SameWordsLexicalForElementary,
                    ChapterId = LexicalForElementary,
                    Title = "Same Words",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = UnionsLexicalForElementary,
                    ChapterId = LexicalForElementary,
                    Title = "Unions",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = WritingLettersLexicalForElementary,
                    ChapterId = LexicalForElementary,
                    Title = "Writing Letters",
                    Priority = Priority.High
                },
                //PreIntermediate
                new Quiz
                {
                    Id = IdiomsCommonTestForPreIntermediate,
                    ChapterId = CommonTestForPreIntermediate,
                    Title = "Idioms",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SlangCommonTestForPreIntermediate,
                    ChapterId = CommonTestForPreIntermediate,
                    Title = "Slang",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = PhraseVerbsCommonTestForPreIntermediate,
                    ChapterId = CommonTestForPreIntermediate,
                    Title = "Phrase Verbs",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = BritishOrAmericanCommonTestForPreIntermediate,
                    ChapterId = CommonTestForPreIntermediate,
                    Title = "British or American",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = ArticlesGrammarTestForPreIntermediate,
                    ChapterId = GrammarTestForPreIntermediate,
                    Title = "Articles",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = PronounsGrammarTestForPreIntermediate,
                    ChapterId = GrammarTestForPreIntermediate,
                    Title = "Pronouns",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToBeGrammarTestForPreIntermediate,
                    ChapterId = GrammarTestForPreIntermediate,
                    Title = "To Be",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = ToDoGrammarTestForPreIntermediate,
                    ChapterId = GrammarTestForPreIntermediate,
                    Title = "To Do",
                    Priority = Priority.High
                },
                new Quiz
                {
                    Id = AdjectivesLexicalForPreIntermediate,
                    ChapterId = GrammarTestForPreIntermediate,
                    Title = "Adjectives",
                    Priority = Priority.Low
                },
                new Quiz
                {
                    Id = SameWordsLexicalForPreIntermediate,
                    ChapterId = LexicalForPreIntermediate,
                    Title = "Same Words",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = UnionsLexicalForPreIntermediate,
                    ChapterId = LexicalForPreIntermediate,
                    Title = "Unions",
                    Priority = Priority.Medium
                },
                new Quiz
                {
                    Id = WritingLettersLexicalForPreIntermediate,
                    ChapterId = LexicalForPreIntermediate,
                    Title = "Writing Letters",
                    Priority = Priority.High
                },
                
            };
        }

        public static List<Question> GetBegginerQuestions()
        {
            return new List<Question>()
            {
                //Idioms
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I prefer to talk to people (face to face) rather than to talk on the phone.",
                    CorrectAnswer = "in person",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "in person"
                        },
                        new Option
                        {
                            Value = "facing them"
                        },
                        new Option
                        {
                            Value = "looking at them"
                        },
                        new Option
                        {
                            Value = "seeing them"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I simply can't stand sugar in my tea or coffee.",
                    CorrectAnswer = "can't tolerate",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "can't eat"
                        },
                        new Option
                        {
                            Value = "can't drink"
                        },
                        new Option
                        {
                            Value = "can't tolerate"
                        },
                        new Option
                        {
                            Value = "can't allow"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Don't worry, we pride ourselves on being helpful to all our customers and getting that booked for you is all in a day's work for us.",
                    CorrectAnswer = "perfectly normal",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "perfectly easy"
                        },
                        new Option
                        {
                            Value = "perfectly simple"
                        },
                        new Option
                        {
                            Value = "perfectly done"
                        },
                        new Option
                        {
                            Value = "perfectly normal"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "My advice to you is to make your mind up before it's too late and simply go for it.",
                    CorrectAnswer = "take the opportunity now",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "take the opportunity soon"
                        },
                        new Option
                        {
                            Value = "take the opportunity fairly"
                        },
                        new Option
                        {
                            Value = "take the opportunity now"
                        },
                        new Option
                        {
                            Value = "take the opportunity slowly"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I'd appreciate that. Go on, I'm all ears.",
                    CorrectAnswer = "I am listening very carefully",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "I can hear what you're saying"
                        },
                        new Option
                        {
                            Value = "I am listening very carefully"
                        },
                        new Option
                        {
                            Value = "I am listening to you"
                        },
                        new Option
                        {
                            Value = "I can't hear a word"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "No, not really except she did get angry sometimes but her bark was worse than her bite.",
                    CorrectAnswer = "not as dangerous as she seemed",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "not as stupid as she sounded"
                        },
                        new Option
                        {
                            Value = "not as dangerous as she seemed"
                        },
                        new Option
                        {
                            Value = "not as tall as she looked"
                        },
                        new Option
                        {
                            Value = "not as clear as she appeared"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "You'll just have to make a decision. You can't have it both ways.",
                    CorrectAnswer = "benefit by agreeing to two opposite views",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "benefit by cancelling two vertical views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two opposite views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two similar views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two average views"
                        }
                    },
                    QuizId = IdiomsCommonTestForBegginer
                },
                //Slang
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Wow, that is a (great) car!",
                    CorrectAnswer = "awesome",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "awesome"
                        },
                        new Option
                        {
                            Value = "able"
                        },
                        new Option
                        {
                            Value = "action"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After (staying awake late at night) studying, I felt tired the next day.",
                    CorrectAnswer = "an all-nighter",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "an angel"
                        },
                        new Option
                        {
                            Value = "an all-nighter"
                        },
                        new Option
                        {
                            Value = "an atmosphere"
                        },
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I don't want to stay in this (dirty, smelly place).",
                    CorrectAnswer = "armpit",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "ark"
                        },
                        new Option
                        {
                            Value = "armpit"
                        },
                        new Option
                        {
                            Value = "apple"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Dave is (the best) player on the team.",
                    CorrectAnswer = "ace",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "action"
                        },
                        new Option
                        {
                            Value = "apple"
                        },
                        new Option
                        {
                            Value = "ace"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Where's (the alcohol) kept around here?",
                    CorrectAnswer = "booze",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "brains"
                        },
                        new Option
                        {
                            Value = "bacon"
                        },
                        new Option
                        {
                            Value = "booze"
                        },
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I wouldn't live in such (a cheap) place if I didn't have to.",
                    CorrectAnswer = "cheesy",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "croak"
                        },
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "cheesy"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Watching T.V. all day is turning you into a (lazy, good-for-nothing).",
                    CorrectAnswer = "couch potato",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "chair"
                        },
                        new Option
                        {
                            Value = "couch potato"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "He (lost control of himself suddenly) when he heard the news.",
                    CorrectAnswer = "flipped out",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "filled up"
                        },
                        new Option
                        {
                            Value = "flopped"
                        },
                        new Option
                        {
                            Value = "flipped out"
                        }
                    },
                    QuizId = SlangCommonTestForBegginer
                },
                //PhraseVerbs
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Everyone could see by the grimace on his face that he didn't ___ the meal in front of him.",
                    CorrectAnswer = "care for",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "care on"
                        },
                        new Option
                        {
                            Value = "care for"
                        },
                        new Option
                        {
                            Value = "care of"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After his illness, he worked hard to ___ on his missed schoolwork.",
                    CorrectAnswer = "catch up",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "catch back"
                        },
                        new Option
                        {
                            Value = "catch up"
                        },
                        new Option
                        {
                            Value = "catch"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Rick ___ a terrible cold this week.",
                    CorrectAnswer = "came down with",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "came up with"
                        },
                        new Option
                        {
                            Value = "came about with"
                        },
                        new Option
                        {
                            Value = "came down with"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Lawrence is trying to ___ on fatty foods.",
                    CorrectAnswer = "cut down",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "cut down"
                        },
                        new Option
                        {
                            Value = "cut up"
                        },
                        new Option
                        {
                            Value = "cut about"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForBegginer
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "The custody battle ___ for many months.",
                    CorrectAnswer = "dragged on",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "dragged down"
                        },
                        new Option
                        {
                            Value = "dragged on"
                        },
                        new Option
                        {
                            Value = "dragged in"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForBegginer
                }
            };
        }

        public static List<Question> GetElementaryQuestions()
        {
            return new List<Question>()
            {
                //Idioms
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I prefer to talk to people (face to face) rather than to talk on the phone.",
                    CorrectAnswer = "in person",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "in person"
                        },
                        new Option
                        {
                            Value = "facing them"
                        },
                        new Option
                        {
                            Value = "looking at them"
                        },
                        new Option
                        {
                            Value = "seeing them"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I simply can't stand sugar in my tea or coffee.",
                    CorrectAnswer = "can't tolerate",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "can't eat"
                        },
                        new Option
                        {
                            Value = "can't drink"
                        },
                        new Option
                        {
                            Value = "can't tolerate"
                        },
                        new Option
                        {
                            Value = "can't allow"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Don't worry, we pride ourselves on being helpful to all our customers and getting that booked for you is all in a day's work for us.",
                    CorrectAnswer = "perfectly normal",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "perfectly easy"
                        },
                        new Option
                        {
                            Value = "perfectly simple"
                        },
                        new Option
                        {
                            Value = "perfectly done"
                        },
                        new Option
                        {
                            Value = "perfectly normal"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "My advice to you is to make your mind up before it's too late and simply go for it.",
                    CorrectAnswer = "take the opportunity now",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "take the opportunity soon"
                        },
                        new Option
                        {
                            Value = "take the opportunity fairly"
                        },
                        new Option
                        {
                            Value = "take the opportunity now"
                        },
                        new Option
                        {
                            Value = "take the opportunity slowly"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I'd appreciate that. Go on, I'm all ears.",
                    CorrectAnswer = "I am listening very carefully",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "I can hear what you're saying"
                        },
                        new Option
                        {
                            Value = "I am listening very carefully"
                        },
                        new Option
                        {
                            Value = "I am listening to you"
                        },
                        new Option
                        {
                            Value = "I can't hear a word"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "No, not really except she did get angry sometimes but her bark was worse than her bite.",
                    CorrectAnswer = "not as dangerous as she seemed",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "not as stupid as she sounded"
                        },
                        new Option
                        {
                            Value = "not as dangerous as she seemed"
                        },
                        new Option
                        {
                            Value = "not as tall as she looked"
                        },
                        new Option
                        {
                            Value = "not as clear as she appeared"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "You'll just have to make a decision. You can't have it both ways.",
                    CorrectAnswer = "benefit by agreeing to two opposite views",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "benefit by cancelling two vertical views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two opposite views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two similar views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two average views"
                        }
                    },
                    QuizId = IdiomsCommonTestForElementary
                },
                //Slang
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Wow, that is a (great) car!",
                    CorrectAnswer = "awesome",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "awesome"
                        },
                        new Option
                        {
                            Value = "able"
                        },
                        new Option
                        {
                            Value = "action"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After (staying awake late at night) studying, I felt tired the next day.",
                    CorrectAnswer = "an all-nighter",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "an angel"
                        },
                        new Option
                        {
                            Value = "an all-nighter"
                        },
                        new Option
                        {
                            Value = "an atmosphere"
                        },
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I don't want to stay in this (dirty, smelly place).",
                    CorrectAnswer = "armpit",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "ark"
                        },
                        new Option
                        {
                            Value = "armpit"
                        },
                        new Option
                        {
                            Value = "apple"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Dave is (the best) player on the team.",
                    CorrectAnswer = "ace",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "action"
                        },
                        new Option
                        {
                            Value = "apple"
                        },
                        new Option
                        {
                            Value = "ace"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Where's (the alcohol) kept around here?",
                    CorrectAnswer = "booze",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "brains"
                        },
                        new Option
                        {
                            Value = "bacon"
                        },
                        new Option
                        {
                            Value = "booze"
                        },
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I wouldn't live in such (a cheap) place if I didn't have to.",
                    CorrectAnswer = "cheesy",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "croak"
                        },
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "cheesy"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Watching T.V. all day is turning you into a (lazy, good-for-nothing).",
                    CorrectAnswer = "couch potato",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "chair"
                        },
                        new Option
                        {
                            Value = "couch potato"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "He (lost control of himself suddenly) when he heard the news.",
                    CorrectAnswer = "flipped out",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "filled up"
                        },
                        new Option
                        {
                            Value = "flopped"
                        },
                        new Option
                        {
                            Value = "flipped out"
                        }
                    },
                    QuizId = SlangCommonTestForElementary
                },
                //PhraseVerbs
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Everyone could see by the grimace on his face that he didn't ___ the meal in front of him.",
                    CorrectAnswer = "care for",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "care on"
                        },
                        new Option
                        {
                            Value = "care for"
                        },
                        new Option
                        {
                            Value = "care of"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After his illness, he worked hard to ___ on his missed schoolwork.",
                    CorrectAnswer = "catch up",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "catch back"
                        },
                        new Option
                        {
                            Value = "catch up"
                        },
                        new Option
                        {
                            Value = "catch"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Rick ___ a terrible cold this week.",
                    CorrectAnswer = "came down with",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "came up with"
                        },
                        new Option
                        {
                            Value = "came about with"
                        },
                        new Option
                        {
                            Value = "came down with"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Lawrence is trying to ___ on fatty foods.",
                    CorrectAnswer = "cut down",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "cut down"
                        },
                        new Option
                        {
                            Value = "cut up"
                        },
                        new Option
                        {
                            Value = "cut about"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "The custody battle ___ for many months.",
                    CorrectAnswer = "dragged on",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "dragged down"
                        },
                        new Option
                        {
                            Value = "dragged on"
                        },
                        new Option
                        {
                            Value = "dragged in"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                }
            };
        }

        public static List<Question> GetPreImtermidiateQuestions()
        {
            return new List<Question>()
            {
                //Idioms
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I prefer to talk to people (face to face) rather than to talk on the phone.",
                    CorrectAnswer = "in person",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "in person"
                        },
                        new Option
                        {
                            Value = "facing them"
                        },
                        new Option
                        {
                            Value = "looking at them"
                        },
                        new Option
                        {
                            Value = "seeing them"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I simply can't stand sugar in my tea or coffee.",
                    CorrectAnswer = "can't tolerate",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "can't eat"
                        },
                        new Option
                        {
                            Value = "can't drink"
                        },
                        new Option
                        {
                            Value = "can't tolerate"
                        },
                        new Option
                        {
                            Value = "can't allow"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Don't worry, we pride ourselves on being helpful to all our customers and getting that booked for you is all in a day's work for us.",
                    CorrectAnswer = "perfectly normal",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "perfectly easy"
                        },
                        new Option
                        {
                            Value = "perfectly simple"
                        },
                        new Option
                        {
                            Value = "perfectly done"
                        },
                        new Option
                        {
                            Value = "perfectly normal"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "My advice to you is to make your mind up before it's too late and simply go for it.",
                    CorrectAnswer = "take the opportunity now",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "take the opportunity soon"
                        },
                        new Option
                        {
                            Value = "take the opportunity fairly"
                        },
                        new Option
                        {
                            Value = "take the opportunity now"
                        },
                        new Option
                        {
                            Value = "take the opportunity slowly"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I'd appreciate that. Go on, I'm all ears.",
                    CorrectAnswer = "I am listening very carefully",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "I can hear what you're saying"
                        },
                        new Option
                        {
                            Value = "I am listening very carefully"
                        },
                        new Option
                        {
                            Value = "I am listening to you"
                        },
                        new Option
                        {
                            Value = "I can't hear a word"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "No, not really except she did get angry sometimes but her bark was worse than her bite.",
                    CorrectAnswer = "not as dangerous as she seemed",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "not as stupid as she sounded"
                        },
                        new Option
                        {
                            Value = "not as dangerous as she seemed"
                        },
                        new Option
                        {
                            Value = "not as tall as she looked"
                        },
                        new Option
                        {
                            Value = "not as clear as she appeared"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "You'll just have to make a decision. You can't have it both ways.",
                    CorrectAnswer = "benefit by agreeing to two opposite views",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "benefit by cancelling two vertical views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two opposite views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two similar views"
                        },
                        new Option
                        {
                            Value = "benefit by agreeing to two average views"
                        }
                    },
                    QuizId = IdiomsCommonTestForPreIntermediate
                },
                //Slang
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Wow, that is a (great) car!",
                    CorrectAnswer = "awesome",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "awesome"
                        },
                        new Option
                        {
                            Value = "able"
                        },
                        new Option
                        {
                            Value = "action"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After (staying awake late at night) studying, I felt tired the next day.",
                    CorrectAnswer = "an all-nighter",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "an angel"
                        },
                        new Option
                        {
                            Value = "an all-nighter"
                        },
                        new Option
                        {
                            Value = "an atmosphere"
                        },
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I don't want to stay in this (dirty, smelly place).",
                    CorrectAnswer = "armpit",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "ark"
                        },
                        new Option
                        {
                            Value = "armpit"
                        },
                        new Option
                        {
                            Value = "apple"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Dave is (the best) player on the team.",
                    CorrectAnswer = "ace",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "action"
                        },
                        new Option
                        {
                            Value = "apple"
                        },
                        new Option
                        {
                            Value = "ace"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Where's (the alcohol) kept around here?",
                    CorrectAnswer = "booze",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "brains"
                        },
                        new Option
                        {
                            Value = "bacon"
                        },
                        new Option
                        {
                            Value = "booze"
                        },
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "I wouldn't live in such (a cheap) place if I didn't have to.",
                    CorrectAnswer = "cheesy",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "croak"
                        },
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "cheesy"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Watching T.V. all day is turning you into a (lazy, good-for-nothing).",
                    CorrectAnswer = "couch potato",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "carrot"
                        },
                        new Option
                        {
                            Value = "chair"
                        },
                        new Option
                        {
                            Value = "couch potato"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "He (lost control of himself suddenly) when he heard the news.",
                    CorrectAnswer = "flipped out",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "filled up"
                        },
                        new Option
                        {
                            Value = "flopped"
                        },
                        new Option
                        {
                            Value = "flipped out"
                        }
                    },
                    QuizId = SlangCommonTestForPreIntermediate
                },
                //PhraseVerbs
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Everyone could see by the grimace on his face that he didn't ___ the meal in front of him.",
                    CorrectAnswer = "care for",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "care on"
                        },
                        new Option
                        {
                            Value = "care for"
                        },
                        new Option
                        {
                            Value = "care of"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "After his illness, he worked hard to ___ on his missed schoolwork.",
                    CorrectAnswer = "catch up",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "catch back"
                        },
                        new Option
                        {
                            Value = "catch up"
                        },
                        new Option
                        {
                            Value = "catch"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Rick ___ a terrible cold this week.",
                    CorrectAnswer = "came down with",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "came up with"
                        },
                        new Option
                        {
                            Value = "came about with"
                        },
                        new Option
                        {
                            Value = "came down with"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForElementary
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "Lawrence is trying to ___ on fatty foods.",
                    CorrectAnswer = "cut down",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "cut down"
                        },
                        new Option
                        {
                            Value = "cut up"
                        },
                        new Option
                        {
                            Value = "cut about"
                        }
                    },
                    QuizId = PhraseVerbsCommonTestForPreIntermediate
                },
                new Question
                {
                    Id = Guid.NewGuid(),
                    Title = "The custody battle ___ for many months.",
                    CorrectAnswer = "dragged on",
                    Options = new List<Option>()
                    {
                        new Option
                        {
                            Value = "dragged down"
                        },
                        new Option
                        {
                            Value = "dragged on"
                        },
                        new Option
                        {
                            Value = "dragged in"
                        },
                    },
                    QuizId = PhraseVerbsCommonTestForPreIntermediate
                }
            };
        }
    }
}
