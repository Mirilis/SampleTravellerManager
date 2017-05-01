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
        /// <summary>
        /// The <see cref="IsRequired" /> property's name.
        /// </summary>
        public const string IsRequiredPropertyName = "IsRequired";

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

        public QuestionsViewModel()
        {
            var s = new SeedData();
           
            CurrentQuestion = new Question();
            PrerequisiteQuestions = new ObservableCollection<Question>();
            PostRequisiteQuestions = new ObservableCollection<Question>();
            CorequisiteQuestions = new ObservableCollection<Question>();

            PrerequisiteQuestions.CollectionChanged += PrerequisiteQuestions_CollectionChanged;
            PostRequisiteQuestions.CollectionChanged += PostRequisiteQuestions_CollectionChanged;
            CorequisiteQuestions.CollectionChanged += CorequisiteQuestions_CollectionChanged;
           
        }

        /// <summary>
        /// Gets the CopyCommand.
        /// </summary>
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand
                    ?? (copyCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Sets and gets the CorequisiteQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the CurrentQuestion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Gets the DeleteCommand.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand
                    ?? (deleteCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Sets and gets the FilePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the HelpImageFile property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the IsRequired property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        private RelayCommand closeWindow;

        /// <summary>
        /// Gets the CloseWindow.
        /// </summary>
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

        /// <summary>
        /// Sets and gets the CorequisiteQuestionsEnabled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Gets the LoadCommand.
        /// </summary>
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand
                    ?? (loadCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Gets the NewCommand.
        /// </summary>
        public RelayCommand NewCommand
        {
            get
            {
                return newCommand
                    ?? (newCommand = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Sets and gets the PostRequisiteQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the PrerequisiteQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the QuestionsList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the Request property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the responseTypes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Gets the SaveCommand.
        /// </summary>
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

        /// <summary>
        /// Sets and gets the Teams property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the TypeOfResponse property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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
