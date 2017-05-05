using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
using SampleTravelerManager.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SampleTravelerManager.ViewModel
{
    public class QuestionsViewModel : ViewModelBase
    {
        #region Private Fields

        private ObservableCollection<Question> allQuestions = null;
        private List<Question> AllQuestionsCollection = null;
        private RelayCommand commandCopySelectedQuestion;
        private RelayCommand commandDeleteSelectedQuestion;
        private RelayCommand commandOpenSelectedQuestion;
        private ObservableCollection<Question> corequisiteQuestions = null;
        private Question currentQuestion = null;
        private string filePath = string.Empty;
        private Bitmap helpImage = null;
        private string helpText = string.Empty;
        private bool isOkToDelete = false;
        private bool isQuestionSelected = false;
        private bool isRequired = false;
        private bool isTemplate = false;
        private string name = string.Empty;
        private ObservableCollection<Question> postrequisiteQuestions = null;
        private ObservableCollection<Question> prerequisiteQuestions = null;
        private string request = string.Empty;
        private ObservableCollection<string> responseTypes = null;
        private Question selectedQuestion = null;
        private ObservableCollection<string> teams = null;
        private string title = "Questions Manager";
        private StandardActionsViewModel top = null;

        private string typeOfResponse = string.Empty;

        private string typeOfTeam = string.Empty;

        #endregion Private Fields

        #region Public Constructors

        public QuestionsViewModel()
        {
            Top.OnCloseCommand += (s, e) => { Messenger.Default.Send<RequestCloseQuestionsWindow>(new RequestCloseQuestionsWindow()); };
            Top.OnCopyCommand += (s, e) => { Messenger.Default.Send<RequestOpenCopyQuestionDialog>(new RequestOpenCopyQuestionDialog()); };
            Top.OnDeleteCommand += (s, e) => { Messenger.Default.Send<RequestOpenDeleteQuestionDialog>(new RequestOpenDeleteQuestionDialog()); };
            Top.OnLoadCommand += (s, e) => { Messenger.Default.Send<RequestOpenLoadQuestionDialog>(new RequestOpenLoadQuestionDialog()); };
            Top.OnNewCommand += (s, e) => { ExecuteCreateNewQuestion(); };
            Top.OnSaveCommand += (s, e) => { ExecuteSaveCurrentQuestion(); };
            this.OnSuccessfulSave += (s, e) =>
            {
                System.Windows.MessageBox.Show("Successfully Saved Question.");
                Messenger.Default.Send<RequestReloadAllQuestions>(new RequestReloadAllQuestions());
            };
            this.OnLoad += (s, e) => { ExecuteCreateNewQuestion(); };
            this.OnLoad(this, new EventArgs());
            Messenger.Default.Register<RequestReloadAllQuestions>(this, (action) => ReloadAllQuestions());
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler OnLoad;

        public event EventHandler OnSuccessfulSave;

        #endregion Public Events

        #region Public Properties

        public ObservableCollection<Question> AllQuestions
        {
            get
            {
                if (allQuestions == null)
                {
                    using (var sql = new SampleTravelersContext())
                    {
                        allQuestions = new ObservableCollection<Question>(sql.Questions.ToList());
                        AllQuestionsCollection = allQuestions.ToList();
                    }
                }
                return allQuestions;
            }

            private set
            {
                if (allQuestions == value)
                {
                    return;
                }

                allQuestions = value;
                RaisePropertyChanged(() => AllQuestions);
            }
        }

        public RelayCommand CommandCopySelectedQuestion
        {
            get
            {
                return commandCopySelectedQuestion
                    ?? (commandCopySelectedQuestion = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteCopySelectedQuestion();
                                              Messenger.Default.Send<RequestCloseCopyQuestionDialog>(new RequestCloseCopyQuestionDialog());
                                          },
                                          () => IsQuestionSelected));
            }
        }

        public RelayCommand CommandDeleteSelectedQuestion
        {
            get
            {
                return commandDeleteSelectedQuestion
                    ?? (commandDeleteSelectedQuestion = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteDeleteSelectedQuestion();
                                              Messenger.Default.Send<RequestCloseDeleteQuestionDialog>(new RequestCloseDeleteQuestionDialog());
                                          },
                                          () => IsQuestionSelected && IsOkToDelete));
            }
        }

        public RelayCommand CommandOpenSelectedQuestion
        {
            get
            {
                return commandOpenSelectedQuestion
                    ?? (commandOpenSelectedQuestion = new RelayCommand(
                    () =>
                    {
                        ExecuteOpenSelectedQuestion();
                    }));
            }
        }

        public ObservableCollection<Question> CorequisiteQuestions
        {
            get
            {
                if (corequisiteQuestions == null)
                {
                    corequisiteQuestions = new ObservableCollection<Question>();
                    CorequisiteQuestions.CollectionChanged += CorequisiteQuestions_CollectionChanged;
                }

                return corequisiteQuestions;
            }

            private set
            {
                if (corequisiteQuestions == value)
                {
                    return;
                }

                corequisiteQuestions = value;
                CorequisiteQuestions.CollectionChanged += CorequisiteQuestions_CollectionChanged;
                RaisePropertyChanged(() => CorequisiteQuestions);
            }
        }

        public Question CurrentQuestion
        {
            get
            {
                return currentQuestion;
            }

            set
            {
                if (currentQuestion == value)
                {
                    return;
                }

                currentQuestion = value;
                RaisePropertyChanged(() => CurrentQuestion);
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                if (filePath == value)
                {
                    return;
                }

                filePath = value;
                RaisePropertyChanged(() => FilePath);
            }
        }

        public Bitmap HelpImage
        {
            get
            {
                return helpImage;
            }

            set
            {
                if (helpImage == value)
                {
                    return;
                }

                helpImage = value;
                RaisePropertyChanged(() => HelpImage);
            }
        }

        public string HelpText
        {
            get
            {
                return helpText;
            }

            set
            {
                if (helpText == value)
                {
                    return;
                }

                helpText = value;
                RaisePropertyChanged(() => HelpText);
            }
        }

        public bool IsOkToDelete
        {
            get
            {
                return isOkToDelete;
            }

            set
            {
                if (isOkToDelete == value)
                {
                    return;
                }
                isOkToDelete = value;
                RaisePropertyChanged(() => IsOkToDelete);
            }
        }

        public bool IsQuestionSelected
        {
            get
            {
                return IsQuestionSelected;
            }

            set
            {
                if (isQuestionSelected == value)
                {
                    return;
                }

                isQuestionSelected = value;
                RaisePropertyChanged(() => IsQuestionSelected);
            }
        }

        public bool IsRequired
        {
            get
            {
                return isRequired;
            }

            set
            {
                if (isRequired == value)
                {
                    return;
                }

                isRequired = value;
                RaisePropertyChanged(() => IsRequired);
            }
        }

        public bool IsTemplate
        {
            get
            {
                return isTemplate;
            }

            set
            {
                if (isTemplate == value)
                {
                    return;
                }

                isTemplate = value;
                RaisePropertyChanged(() => IsTemplate);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public ObservableCollection<Question> PostrequisiteQuestions
        {
            get
            {
                if (postrequisiteQuestions == null)
                {
                    PostrequisiteQuestions = new ObservableCollection<Question>();
                    PostrequisiteQuestions.CollectionChanged += PostrequisiteQuestions_CollectionChanged;
                }

                return postrequisiteQuestions;
            }

            private set
            {
                if (postrequisiteQuestions == value)
                {
                    return;
                }

                postrequisiteQuestions = value;
                PostrequisiteQuestions.CollectionChanged += PostrequisiteQuestions_CollectionChanged;
                RaisePropertyChanged(() => PostrequisiteQuestions);
            }
        }

        public ObservableCollection<Question> PrerequisiteQuestions
        {
            get
            {
                if (prerequisiteQuestions == null)
                {
                    prerequisiteQuestions = new ObservableCollection<Question>();
                    PrerequisiteQuestions.CollectionChanged += PrerequisiteQuestions_CollectionChanged;
                }
                return prerequisiteQuestions;
            }

            private set
            {
                if (prerequisiteQuestions == value)
                {
                    return;
                }

                prerequisiteQuestions = value;
                PrerequisiteQuestions.CollectionChanged += PrerequisiteQuestions_CollectionChanged;
                RaisePropertyChanged(() => PrerequisiteQuestions);
            }
        }

        public string Request
        {
            get
            {
                return request;
            }

            set
            {
                if (request == value)
                {
                    return;
                }

                request = value;
                RaisePropertyChanged(() => Request);
            }
        }

        public ObservableCollection<string> ResponseTypes
        {
            get
            {
                if (responseTypes == null)
                {
                    var t = new ObservableCollection<string>();
                    foreach (var item in Enum.GetNames(typeof(ResponseType)))
                    {
                        t.Add(item.SplitCamelCase());
                    }
                    responseTypes = t;
                }
                return responseTypes;
            }
        }

        public Question SelectedQuestion
        {
            get
            {
                return selectedQuestion;
            }

            set
            {
                if (selectedQuestion == value)
                {
                    return;
                }
                if (value == null)
                {
                    isQuestionSelected = false;
                }
                else
                {
                    isQuestionSelected = true;
                }
                selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
            }
        }

        public ObservableCollection<string> Teams
        {
            get
            {
                if (teams == null)
                {
                    var t = new ObservableCollection<string>();
                    foreach (var item in Enum.GetNames(typeof(Team)))
                    {
                        t.Add(item.SplitCamelCase());
                    }
                    teams = t;
                }

                return teams;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                if (title == value)
                {
                    return;
                }
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public StandardActionsViewModel Top
        {
            get
            {
                if (top == null)
                {
                    top = new StandardActionsViewModel();
                }
                return top;
            }

            set
            {
                if (top == value)
                {
                    return;
                }
                top = value;
                RaisePropertyChanged(() => Top);
            }
        }

        public string TypeOfResponse
        {
            get
            {
                return typeOfResponse;
            }

            set
            {
                typeOfResponse = value;
                RaisePropertyChanged(() => TypeOfResponse);
            }
        }

        public string TypeOfTeam
        {
            get
            {
                return typeOfTeam;
            }

            set
            {
                typeOfTeam = value;
                RaisePropertyChanged(() => TypeOfTeam);
            }
        }

        #endregion Public Properties

        #region Private Properties

        private ResponseType CurrentResponseType
        {
            get
            {
                return (ResponseType)Enum.Parse(typeof(ResponseType), TypeOfResponse.RemoveSpecialCharacters());
            }
        }

        private Team CurrentTeam
        {
            get
            {
                return (Team)Enum.Parse(typeof(Team), TypeOfTeam.RemoveSpecialCharacters());
            }
        }

        #endregion Private Properties

        #region Private Methods

        private static void RemoveFromList(Question s, ObservableCollection<Question> l)
        {
            if (l.Any(x => x.Id == s.Id))
            {
                l.Remove(l.Where(x => x.Id == s.Id).First());
            }
        }

        private void CorequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems.Count > 0)
                {
                    foreach (var item in e.NewItems)
                    {
                        var s = item as Question;
                        RemoveFromList(s, PrerequisiteQuestions);
                        RemoveFromList(s, PostrequisiteQuestions);
                        AllQuestions = new ObservableCollection<Question>(AllQuestionsCollection);
                    }
                }
            }
        }

        private void ExecuteCopySelectedQuestion()
        {
            using (var sql = new SampleTravelersContext())
            {
                var p = sql.Questions.Where(x => x.Id == SelectedQuestion.Id);
                if (p.Any())
                {
                    var Q = p.First();

                    sql.Questions.Add(Q);

                    sql.SaveChanges();
                }
                AllQuestions = new ObservableCollection<Question>(sql.Questions);
                ExecuteOpenSelectedQuestion();
            }
        }

        private void ExecuteCreateNewQuestion()
        {
            CurrentQuestion = new Question();
            PrerequisiteQuestions = new ObservableCollection<Question>();
            PostrequisiteQuestions = new ObservableCollection<Question>();
            CorequisiteQuestions = new ObservableCollection<Question>();
        }

        private void ExecuteDeleteSelectedQuestion()
        {
            using (var sql = new SampleTravelersContext())
            {
                ExecuteCreateNewQuestion();
                var s = sql.Questions.Where(x => x.Id == SelectedQuestion.Id).First();
                sql.Questions.Remove(s);
                sql.SaveChanges();
                AllQuestions = new ObservableCollection<Question>(sql.Questions);
            }
        }

        private void ExecuteOpenSelectedQuestion()
        {
            if (SelectedQuestion != null)
            {
                var p = SelectedQuestion.GetCompleteQuestion();
                this.Name = p.Name;
                this.Request = p.Request;
                this.TypeOfResponse = p.ResponseType;
                this.TypeOfTeam = p.TeamName;
                this.IsRequired = p.RequiresResponse;
                this.HelpText = p.HelpText;
                //this.HelpImage = selectedQuestion.HelpImage
                this.IsTemplate = p.Template;
                Messenger.Default.Send<RequestCloseQuestionsDialog>(new RequestCloseQuestionsDialog());
                this.PrerequisiteQuestions = new ObservableCollection<Question>(p.Prerequisites);
                this.PostrequisiteQuestions = new ObservableCollection<Question>(p.Postrequisites);
                this.CorequisiteQuestions = new ObservableCollection<Question>(p.Corequisites);
                Messenger.Default.Send<RequestCloseLoadQuestionsDialog>(new RequestCloseLoadQuestionsDialog());
            }
        }

        private void ExecuteSaveCurrentQuestion()
        {
            using (var sql = new SampleTravelersContext())
            {
                var p = new Question();
                if (sql.Questions.Any(x => x.Name == Name))
                {
                    p = sql.Questions.Where(x => x.Name == this.Name).First();
                }
                else
                {
                    sql.Questions.Add(p);
                }
                foreach (var item in PrerequisiteQuestions)
                {
                    var i = sql.Questions.Where(x => x.Id == item.Id).First();
                    p.AddPrerequisite(i);
                }
                foreach (var item in CorequisiteQuestions)
                {
                    p.Corequisites.Add(sql.Questions.Where(x => x.Id == item.Id).First());
                }
                foreach (var item in PostrequisiteQuestions)
                {
                    var i = sql.Questions.Where(x => x.Id == item.Id).First();
                    p.AddPostrequisite(i);
                }

                p.Name = this.Name;
                p.Request = this.Request;
                p.RequiresResponse = this.IsRequired;
                p.Template = this.IsTemplate;
                p.Type = (int)this.CurrentResponseType;
                p.Team = (int)this.CurrentTeam;
                p.HelpText = this.HelpText;
                //p.HelpImage = this.HelpImage;

                var saved = true;
                try
                {
                    sql.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    saved = false;
                    System.Windows.MessageBox.Show(SampleTravelersContext.AlertUserErrors(e));
                }
                finally
                {
                    if (saved)
                    {
                        OnSuccessfulSave(this, null);
                    }
                }
            }
        }

        private void PostrequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems.Count > 0)
                {
                    foreach (var item in e.NewItems)
                    {
                        var s = item as Question;
                        RemoveFromList(s, PrerequisiteQuestions);
                        RemoveFromList(s, CorequisiteQuestions);
                        AllQuestions = new ObservableCollection<Question>(AllQuestionsCollection);
                    }
                }
            }
        }

        private void PrerequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                if (e.NewItems.Count > 0)
                {
                    foreach (var item in e.NewItems)
                    {
                        var s = item as Question;
                        RemoveFromList(s, PostrequisiteQuestions);
                        RemoveFromList(s, CorequisiteQuestions);
                        AllQuestions = new ObservableCollection<Question>(AllQuestionsCollection);
                    }
                }
            }
        }

        private void ReloadAllQuestions()
        {
            using (var sql = new SampleTravelersContext())
            {
                this.AllQuestions = new ObservableCollection<Question>(sql.Questions);
            }
        }

        #endregion Private Methods
    }
}