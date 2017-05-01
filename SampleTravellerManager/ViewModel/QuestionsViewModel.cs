using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
using SampleTravellerManager.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Drawing;



namespace SampleTravellerManager.ViewModel
{
    public class QuestionsViewModel : ViewModelBase
    {
        private RelayCommand closeWindow;
        private RelayCommand copyCommand;
        private ObservableCollection<Question> corequisiteQuestions = null;
        private Question currentQuestion = null;
        private RelayCommand deleteCommand;
        private string filePath = string.Empty;
        private Bitmap helpImage = null;
        private bool isRequired = false;
        private bool isTemplate = false;
        private RelayCommand loadCommand;
        private string name = string.Empty;
        private RelayCommand newCommand;
        private ObservableCollection<Question> postrequisiteQuestions = null;
        private ObservableCollection<Question> prerequisiteQuestions = null;
        private ObservableCollection<Question> questionsList = null;
        private string request = string.Empty;
        private ObservableCollection<string> responseTypes = null;
        private RelayCommand savecommand;
        private ObservableCollection<string> teams = null;
        private string typeOfResponse = string.Empty;
        private string typeOfTeam = string.Empty;

        private RelayCommand openCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand
                    ?? (openCommand = new RelayCommand(
                    () =>
                    {
                        OpenSelectedQuestion();
                    }));
            }
        }

        private void OpenSelectedQuestion()
        {

        }

        public QuestionsViewModel()
        {
            var s = new SeedData();

            CreateNewQuestion();
           
        }
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow
                    ?? (closeWindow = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestCloseQuestionsWindow>(new RequestCloseQuestionsWindow());
                    }));
            }
        }
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand
                    ?? (copyCommand = new RelayCommand(
                    () =>
                    {
                        CopyQuestion();
                    }));
            }
        }

        private void CopyQuestion()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Question> CorequisiteQuestions
        {
            get
            {
                if (corequisiteQuestions == null)
                {
                    corequisiteQuestions = new ObservableCollection<Question>();
                }
                return corequisiteQuestions;
            }

            set
            {
                if (corequisiteQuestions == value)
                {
                    return;
                }

                corequisiteQuestions = value;
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
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand
                    ?? (deleteCommand = new RelayCommand(
                    () =>
                    {
                        DeleteQuestion();
                    }));
            }
        }

        private void DeleteQuestion()
        {
            throw new NotImplementedException();
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

        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand
                    ?? (loadCommand = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestLoadQuestionDialog>(new RequestLoadQuestionDialog());
                    }));
            }
        }


        private Question selectedQuestion = null;
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

                selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
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
        public RelayCommand NewCommand
        {
            get
            {
                return newCommand
                    ?? (newCommand = new RelayCommand(
                    () =>
                    {
                        CreateNewQuestion();
                    }));
            }
        }
        public ObservableCollection<Question> PostRequisiteQuestions
        {
            get
            {
                if (postrequisiteQuestions == null)
                {
                    prerequisiteQuestions = new ObservableCollection<Question>();
                }

                return postrequisiteQuestions;
            }

            set
            {
                if (postrequisiteQuestions == value)
                {
                    return;
                }

                postrequisiteQuestions = value;
                RaisePropertyChanged(() => PostRequisiteQuestions);
            }
        }
        public ObservableCollection<Question> PrerequisiteQuestions
        {
            get
            {
                if (prerequisiteQuestions == null)
                {
                    postrequisiteQuestions = new ObservableCollection<Question>();
                }
                return prerequisiteQuestions;
            }

            set
            {
                if (prerequisiteQuestions == value)
                {
                    return;
                }

                prerequisiteQuestions = value;
                RaisePropertyChanged(() => PrerequisiteQuestions);
            }
        }
        public ObservableCollection<Question> QuestionsList
        {
            get
            {
                if (questionsList == null)
                {
                    using (var sql = new SampleTravellersContext())
                    {
                        questionsList = new ObservableCollection<Question>(sql.Questions.ToList());
                    }
                }
                return questionsList;
            }

            set
            {
                if (questionsList == value)
                {
                    return;
                }

                questionsList = value;
                RaisePropertyChanged(() => QuestionsList);
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
        public RelayCommand SaveCommand
        {
            get
            {
                return savecommand
                    ?? (savecommand = new RelayCommand(
                    () =>
                    {

                    }));
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
        private void CorequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CreateNewQuestion()
        {
            CurrentQuestion = new Question();
            PrerequisiteQuestions = new ObservableCollection<Question>();
            PostRequisiteQuestions = new ObservableCollection<Question>();
            CorequisiteQuestions = new ObservableCollection<Question>();

            PrerequisiteQuestions.CollectionChanged += PrerequisiteQuestions_CollectionChanged;
            PostRequisiteQuestions.CollectionChanged += PostRequisiteQuestions_CollectionChanged;
            CorequisiteQuestions.CollectionChanged += CorequisiteQuestions_CollectionChanged;
        }
        private void PostRequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void PrerequisiteQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
