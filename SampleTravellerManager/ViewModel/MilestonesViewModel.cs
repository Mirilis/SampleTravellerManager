using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NWCSampleManager;
using SampleTravelerManager.Converters;
using SampleTravelerManager.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SampleTravelerManager.ViewModel
{
    public class MilestonesViewModel : ViewModelBase
    {
        #region Private Fields

        private ObservableCollection<Question> allQuestions = new ObservableCollection<Question>();
        private List<Question> allQuestionsCollection = new List<Question>();
        private ObservableCollection<Traveler> allTravelers = null;
        private RelayCommand commandCopySelectedTraveler;
        private RelayCommand commandDeleteSelectedTraveler;
        private RelayCommand commandEditSelectedQuestion;
        private RelayCommand commandOpenQuestionsWindow;
        private RelayCommand commandOpenSelectedTraveler;
        private string description = string.Empty;
        private bool isCompleted = false;
        private bool isOKToDelete = false;
        private bool isQuestionSelected = false;
        private bool isSuccessful = false;
        private bool isTravelerLoaded = false;
        private bool isTravelerSelected = false;
        private string name = string.Empty;
        private User owner = null;
        private String product = string.Empty;
        private ObservableCollection<Question> questions = null;
        private ObservableCollection<Response> responses = null;
        private Question selectedQuestion = null;
        private Traveler selectedTraveler = null;
        private DateTime startDate = DateTime.Now;
        private string title = "Sample Traveler Manager";
        private StandardActionsViewModel top = null;
        private Traveler workingtraveler;

        #endregion Private Fields

        #region Public Constructors

        public MilestonesViewModel()
        {
            this.Top.OnCloseCommand += (s, e) => { Messenger.Default.Send<RequestCloseTravelersWindow>(new RequestCloseTravelersWindow()); };
            this.Top.OnCopyCommand += (s, e) => { Messenger.Default.Send<RequestOpenCopyTravelerDialog>(new RequestOpenCopyTravelerDialog()); };
            this.Top.OnDeleteCommand += (s, e) => { Messenger.Default.Send<RequestOpenDeleteTravelerDialog>(new RequestOpenDeleteTravelerDialog()); };
            this.Top.OnLoadCommand += (s, e) => { Messenger.Default.Send<RequestOpenLoadtravelerDialog>(new RequestOpenLoadtravelerDialog()); };
            this.Top.OnNewCommand += (s, e) => { ExecuteCreateNewTraveler(); };
            this.Top.OnSaveCommand += (s, e) => { ExecuteSaveCurrentTraveler(); };
            this.OnLoad += OnLoadEvent;
            this.OnLoad(this, new EventArgs());
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler OnLoad;

        #endregion Public Events

        #region Public Properties

        public ObservableCollection<Question> AllQuestions
        {
            get
            {
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

        public ICollectionView AllQuestionsAdministratorSort
        {
            get
            {
                ICollectionView source = new CollectionViewSource { Source = AllQuestions }.View;
                source.Filter = o =>
                {
                    return FilterByTeam(o, Team.Administrator);
                };
                source.Refresh();

                return source;
            }
        }

        public ICollectionView AllQuestionsDepartmentSort
        {
            get
            {
                ICollectionView source = new CollectionViewSource { Source = AllQuestions }.View;
                source.Filter = o =>
                {
                    return FilterByTeam(o, Team.CoreDepartment) ||
                    FilterByTeam(o, Team.MoldDepartment) ||
                    FilterByTeam(o, Team.MeltDepartment) ||
                    FilterByTeam(o, Team.CleanDepartment) ||
                    FilterByTeam(o, Team.QualityDepartment) ||
                    FilterByTeam(o, Team.ShippingDepartment);
                };
                source.Refresh();
                return source;
            }
        }

        public ICollectionView AllQuestionsEngineeringSort
        {
            get
            {
                ICollectionView source = new CollectionViewSource { Source = AllQuestions }.View;
                source.Filter = o =>
                {
                    return FilterByTeam(o, Team.MetallurgicalEngineering) ||
                    FilterByTeam(o, Team.FoundryEngineering) ||
                    FilterByTeam(o, Team.QualityEngineering) ||
                    FilterByTeam(o, Team.IndustrialEngineering) ||
                    FilterByTeam(o, Team.ProductionControl);
                };
                source.Refresh();
                return source;
            }
        }

        public ObservableCollection<Traveler> AllTravelers
        {
            get
            {
                if (allTravelers == null)
                {
                    using (var sql = new SampleTravelersContext())
                    {
                        allTravelers = new ObservableCollection<Traveler>(sql.travelers.ToList());
                    }
                }
                return allTravelers;
            }

            private set
            {
                if (allTravelers == value)
                {
                    return;
                }

                allTravelers = value;
                RaisePropertyChanged(() => AllTravelers);
            }
        }

        public RelayCommand CommandCopySelectedTraveler
        {
            get
            {
                return commandCopySelectedTraveler
                    ?? (commandCopySelectedTraveler = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteCopySelectedTraveler();
                                          },
                                          () => IsTravelerSelected));
            }
        }

        public RelayCommand CommandDeleteSelectedTraveler
        {
            get
            {
                return commandDeleteSelectedTraveler
                    ?? (commandDeleteSelectedTraveler = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteDeleteSelectedTraveler();
                                          },
                                          () => IsOkToDelete && IsTravelerSelected));
            }
        }

        public RelayCommand CommandEditSelectedQuestion
        {
            get
            {
                return commandEditSelectedQuestion
                    ?? (commandEditSelectedQuestion = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow(SelectedQuestion));
                    },
                    () => IsQuestionSelected));
            }
        }

        public RelayCommand CommandOpenQuestionsMenu
        {
            get
            {
                return commandOpenQuestionsWindow
                    ?? (commandOpenQuestionsWindow = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow());
                    }));
            }
        }

        public RelayCommand CommandOpenSelectedTraveler
        {
            get
            {
                return commandOpenSelectedTraveler
                    ?? (commandOpenSelectedTraveler = new RelayCommand(
                    () =>
                    {
                        ExecuteOpenSelectedTraveler();
                    },
                    () => IsTravelerSelected));
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }

            set
            {
                if (isCompleted == value)
                {
                    return;
                }

                isCompleted = value;
                RaisePropertyChanged(() => IsCompleted);
            }
        }

        public bool IsOkToDelete
        {
            get
            {
                return isOKToDelete;
            }

            set
            {
                if (isOKToDelete == value)
                {
                    return;
                }
                isOKToDelete = value;
                RaisePropertyChanged(() => IsOkToDelete);
            }
        }

        public bool IsQuestionSelected
        {
            get
            {
                return isQuestionSelected;
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

        public bool IsSuccessful
        {
            get
            {
                return isSuccessful;
            }

            set
            {
                if (isSuccessful == value)
                {
                    return;
                }

                isSuccessful = value;
                RaisePropertyChanged(() => IsSuccessful);
            }
        }

        public bool IsTravelerLoaded
        {
            get
            {
                return isTravelerLoaded;
            }

            set
            {
                if (isTravelerLoaded == value)
                {
                    return;
                }

                isTravelerLoaded = value;
                RaisePropertyChanged(() => IsTravelerLoaded);
            }
        }

        public bool IsTravelerSelected
        {
            get
            {
                return isTravelerSelected;
            }

            set
            {
                if (isTravelerSelected == value)
                {
                    return;
                }
                isTravelerSelected = value;
                RaisePropertyChanged(() => IsTravelerSelected);
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

        public User Owner
        {
            get
            {
                return owner;
            }

            set
            {
                if (owner == value)
                {
                    RaisePropertyChanged(() => Owner);
                    return;
                }

                owner = value;
                RaisePropertyChanged(() => Owner);
            }
        }

        public String Product
        {
            get
            {
                return product;
            }

            set
            {
                if (product == value)
                {
                    return;
                }

                product = value;
                RaisePropertyChanged(() => Product);
            }
        }

        public ObservableCollection<Question> Questions
        {
            get
            {
                if (questions == null)
                {
                    this.Questions = new ObservableCollection<Question>(workingtraveler.Questions);
                }

                return questions;
            }

            private set
            {
                if (questions == value)
                {
                    return;
                }

                questions = value;
                Questions.CollectionChanged += Questions_CollectionChanged;
                RaisePropertyChanged(() => Questions);
            }
        }

        public ObservableCollection<Response> Responses
        {
            get
            {
                return responses;
            }

            private set
            {
                if (responses == value)
                {
                    return;
                }

                responses = value;
                RaisePropertyChanged(() => Responses);
            }
        }

        public Question SelectedQuestion
        {
            get
            {
                if (selectedQuestion == null)
                {
                    IsQuestionSelected = false;
                }
                else
                {
                    IsQuestionSelected = true;
                }
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

        public Traveler SelectedTraveler
        {
            get
            {
                return selectedTraveler;
            }

            set
            {
                if (selectedTraveler == value)
                {
                    return;
                }
                if (value == null)
                {
                    IsTravelerSelected = false;
                }
                else
                {
                    IsTravelerSelected = true;
                }
                selectedTraveler = value;
                RaisePropertyChanged(() => SelectedTraveler);
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                if (startDate == value)
                {
                    return;
                }

                startDate = value;
                RaisePropertyChanged(() => StartDate);
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

        #endregion Public Properties

        #region Private Methods

        private static bool FilterByTeam(object o, Team t)
        {
            var i = o as Question;
            if (i.Team == (int)t)
            {
                return true;
            }
            return false;
        }

        private void AllQuestions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems.Count > 0)
            {
                ReloadAllQuestions();
            }
        }

        private void ExecuteCopySelectedTraveler()
        {
            using (var sql = new SampleTravelersContext())
            {
                Messenger.Default.Send<RequestCloseCopyTravelerDialog>(new RequestCloseCopyTravelerDialog());
                var p = sql.travelers.Where(x => x.Id == SelectedTraveler.Id);
                if (p.Any())
                {
                    var T = p.First();

                    sql.travelers.Add(T);

                    sql.SaveChanges();
                }
                AllTravelers = new ObservableCollection<Traveler>(sql.travelers.ToList());
            }
        }

        private void ExecuteCreateNewTraveler()
        {
            Loadtraveler(new Traveler());
            this.Name = Traveler.GetUID(this.Owner);
        }

        private void ExecuteDeleteSelectedTraveler()
        {
            using (var sql = new SampleTravelersContext())
            {
                var p = sql.travelers.Where(x => x.Id == SelectedTraveler.Id);
                if (p.Any())
                {
                    ExecuteCreateNewTraveler();
                    sql.travelers.Remove(p.First());
                    AllTravelers.Remove(AllTravelers.Where(x => x.Id == p.First().Id).First());
                    sql.SaveChanges();
                }
            }
            SelectedTraveler = null;
            Messenger.Default.Send<RequestCloseDeleteTravelerDialog>(new RequestCloseDeleteTravelerDialog());
        }

        private void ExecuteOpenSelectedTraveler()
        {
            Loadtraveler(SelectedTraveler);
            SelectedTraveler = null;
        }

        private void ExecuteSaveCurrentTraveler()
        {
            using (var sql = new SampleTravelersContext())
            {
                var travelersQry = sql.travelers.Where(x => x.Id == this.workingtraveler.Id);
                if (travelersQry.Any())
                {
                    this.workingtraveler = travelersQry.First();
                }
                else
                {
                    sql.travelers.Add(workingtraveler);
                }
                foreach (var item in questions)
                {
                    this.workingtraveler.Questions.Add(sql.Questions.Where(x => x.Id == item.Id).First());
                }
                this.workingtraveler.User = sql.Users.Where(x => x.Id == this.Owner.Id).First();
                this.workingtraveler.StartDate = this.StartDate;
                this.workingtraveler.Name = this.Name;
                this.workingtraveler.Product = this.Product;
                this.workingtraveler.Description = this.Description;

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
                        System.Windows.MessageBox.Show("Successfully Saved traveler.");
                    }
                }
            }
        }

        private List<Question> GetAllRelatedQuestions(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var questionList = new List<Question>(Questions);
            foreach (var q in e.NewItems)
            {
                var s = q as Question;
                var t = Traveler.GetAllRequisites(s);
                questionList.AddRange(t);
            }
            var id = questionList.Where(y => y.Template == false).Select(x => x.Id).Distinct();
            var nl = new List<Question>();
            foreach (var idi in id)
            {
                nl.Add(questionList.First(x => x.Id == idi));
            }
            return nl;
        }

        private void LoadAllQuestions()
        {
            using (var sql = new SampleTravelersContext())
            {
                var t = sql.Questions
                    .ToList();

                AllQuestions = new ObservableCollection<Question>(t);
                AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;

                allQuestionsCollection = new List<Question>(t);
            }
            ReloadAllQuestions();
        }

        private void Loadtraveler(Traveler T)
        {
            using (var sql = new SampleTravelersContext())
            {
                var usersList = sql.Users.Where(x => x.Id == T.OwnerId);
                if (usersList.Any())
                {
                    this.Owner = usersList.First();
                    this.StartDate = T.StartDate;
                }
                else
                {
                    this.Owner = User.GetCurrentUser();
                    this.StartDate = DateTime.Now;
                }
                var questionsQry = sql.travelers.Where(x => x.Id == T.Id);
                if (questionsQry.Any())
                {
                    this.Questions = new ObservableCollection<Question>(questionsQry.First().Questions);
                }
                else
                {
                    this.Questions = new ObservableCollection<Question>();
                }

                this.Product = T.Product;

                this.Description = T.Description;
                this.Name = T.Name;
                this.workingtraveler = T;
                Messenger.Default.Send<RequestClosetravelersDialog>(new RequestClosetravelersDialog());
            }
        }

        private void OnLoadEvent(object sender, EventArgs e)
        {
            Messenger.Default.Register<RequestReloadAllQuestions>(this, (action) => LoadAllQuestions());

            LoadAllQuestions();

            ExecuteCreateNewTraveler();
        }

        private void Questions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var p = new List<Question>();
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                p = GetAllRelatedQuestions(e);
            }
            SortCurrentQuestions(p);
        }

        private void ReloadAllQuestions()
        {
            AllQuestions.CollectionChanged -= AllQuestions_CollectionChanged;
            AllQuestions = new ObservableCollection<Question>(allQuestionsCollection);
            RaisePropertyChanged(() => AllQuestionsAdministratorSort);
            RaisePropertyChanged(() => AllQuestionsEngineeringSort);
            RaisePropertyChanged(() => AllQuestionsDepartmentSort);
            AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;
        }

        private void SortCurrentQuestions(List<Question> q)
        {
            Questions.CollectionChanged -= Questions_CollectionChanged;

            var tmp2 = Traveler.SortByRequisites(q);

            Questions = new ObservableCollection<Question>(tmp2);
            Questions.CollectionChanged += Questions_CollectionChanged;
        }

        #endregion Private Methods
    }
}