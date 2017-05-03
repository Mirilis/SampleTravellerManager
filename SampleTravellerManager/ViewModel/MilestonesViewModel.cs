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
using SampleTravellerManager.Converters;

namespace SampleTravellerManager.ViewModel
{
    public class MilestonesViewModel : ViewModelBase
    {

        #region Private Fields

        private ObservableCollection<Question> allQuestions = new ObservableCollection<Question>();
        private List<Question> allQuestionsCollection = new List<Question>();
        private ObservableCollection<Traveller> allTravellers = null;
        private RelayCommand command_CopySelectedTraveller;
        private RelayCommand command_DeleteSelectedTraveller;
        private RelayCommand command_EditSelectedQuestion;
        private RelayCommand command_OpenQuestionsWindow;
        private RelayCommand command_OpenSelectedTraveller;
        private string description = string.Empty;
        private bool isCompleted = false;
        private bool isOKToDelete = false;
        private bool isQuestionSelected = false;
        private bool isSuccessful = false;
        private bool isTravellerLoaded = false;
        private bool isTravellerSelected = false;
        private string name = string.Empty;
        private User owner = null;
        private String product = string.Empty;
        private ObservableCollection<Question> questions = null;
        private List<Question> questionsCollection = new List<Question>();
        private ObservableCollection<Response> responses = null;
        private Question selectedQuestion = null;
        private Traveller selectedTraveller = null;
        private DateTime startDate = DateTime.Now;
        private string title = "Sample Traveller Manager";
        private StandardActionsViewModel top = null;
        private Traveller workingTraveller;
        

        #endregion Private Fields

        #region Public Constructors

        public MilestonesViewModel()
        {
            var p = new SeedData();
            ExecuteCreateNewTraveller();
            using (var sql = new SampleTravellersContext())
            {
                var t = sql.Questions
                    .ToList();

                AllQuestions = new ObservableCollection<Question>(t);
                AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;

                allQuestionsCollection = new List<Question>(t);
            }

            Top.OnCloseCommand += (s, e) => { Messenger.Default.Send<RequestCloseTravellersWindow>(new RequestCloseTravellersWindow()); };
            Top.OnCopyCommand += (s, e) => { Messenger.Default.Send<RequestOpenCopyTravellerDialog>(new RequestOpenCopyTravellerDialog()); };
            Top.OnDeleteCommand += (s, e) => { Messenger.Default.Send<RequestOpenDeleteTravellerDialog>(new RequestOpenDeleteTravellerDialog()); };
            Top.OnLoadCommand += (s, e) => { Messenger.Default.Send<RequestOpenLoadTravellerDialog>(new RequestOpenLoadTravellerDialog()); };
            Top.OnNewCommand += (s, e) => { ExecuteCreateNewTraveller(); };
            Top.OnSaveCommand += (s, e) => { ExecuteSaveCurrentTraveller(); };


        }


        #endregion Public Constructors

        #region Public Properties

        public ICollectionView AdministratorQuestions
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

        public ObservableCollection<Question> AllQuestions
        {
            get
            {
                return allQuestions;
            }

            set
            {
                if (allQuestions == value)
                {
                    return;
                }

                allQuestions = value;

                RaisePropertyChanged(() => AllQuestions);
            }
        }

        public ObservableCollection<Traveller> AllTravellers
        {
            get
            {
                if (allTravellers == null)
                {
                    using (var sql = new SampleTravellersContext())
                    {
                        allTravellers = new ObservableCollection<Traveller>(sql.Travellers.ToList());
                    }
                }
                return allTravellers;
            }

            set
            {
                if (allTravellers == value)
                {
                    return;
                }

                allTravellers = value;
                RaisePropertyChanged(() => AllTravellers);
            }
        }

        public RelayCommand Command_CopySelectedTraveller
        {
            get
            {
                return command_CopySelectedTraveller
                    ?? (command_CopySelectedTraveller = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteCopySelectedTraveller();
                                          },
                                          () => IsTravellerSelected));
            }
        }

        public RelayCommand Command_DeleteSelectedTraveller
        {
            get
            {
                return command_DeleteSelectedTraveller
                    ?? (command_DeleteSelectedTraveller = new RelayCommand(
                                          () =>
                                          {
                                              ExecuteDeleteSelectedTraveller();
                                          },
                                          () => IsOKToDelete && IsTravellerSelected));
            }
        }

        public RelayCommand Command_EditSelectedQuestion
        {
            get
            {
                return command_EditSelectedQuestion
                    ?? (command_EditSelectedQuestion = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow(SelectedQuestion));
                    },
                    () => IsQuestionSelected));
            }
        }

        public RelayCommand Command_OpenQuestionsMenu
        {
            get
            {
                return command_OpenQuestionsWindow
                    ?? (command_OpenQuestionsWindow = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow());
                    }));
            }
        }

        public RelayCommand Command_OpenSelectedTraveller
        {
            get
            {
                return command_OpenSelectedTraveller
                    ?? (command_OpenSelectedTraveller = new RelayCommand(
                    () =>
                    {
                        ExecuteOpenSelectedTraveller();
                    },
                    () => IsTravellerSelected));
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

        public ICollectionView DepartmentQuestions
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

        public ICollectionView EngineeringQuestions
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

        public bool IsOKToDelete
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
                RaisePropertyChanged(() => IsOKToDelete);
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

        public bool IsTravellerLoaded
        {
            get
            {
                return isTravellerLoaded;
            }

            set
            {
                if (isTravellerLoaded == value)
                {
                    return;
                }

                isTravellerLoaded = value;
                RaisePropertyChanged(() => IsTravellerLoaded);
            }
        }

        public bool IsTravellerSelected
        {
            get
            {
                return isTravellerSelected;
            }

            set
            {
                if (isTravellerSelected == value)
                {
                    return;
                }
                isTravellerSelected = value;
                RaisePropertyChanged(() => IsTravellerSelected);
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
                    this.Questions = new ObservableCollection<Question>(workingTraveller.Questions);
                }

                return questions;
            }

            set
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

            set
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

        public Traveller SelectedTraveller
        {
            get
            {
                return selectedTraveller;
            }

            set
            {
                if (selectedTraveller == value)
                {
                    return;
                }
                if (value == null)
                {
                    IsTravellerSelected = false;
                }
                else
                {
                    IsTravellerSelected = true;
                }
                selectedTraveller = value;
                RaisePropertyChanged(() => SelectedTraveller);
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

        private void ExecuteCopySelectedTraveller()
        {
            using (var sql = new SampleTravellersContext())
            {
                Messenger.Default.Send<RequestCloseCopyTravellerDialog>(new RequestCloseCopyTravellerDialog());
                var p = sql.Travellers.Where(x => x.Id == SelectedTraveller.Id);
                if (p.Any())
                {
                        var T = p.First();
                    
                        sql.Travellers.Add(T);

                    sql.SaveChanges();
                }
                AllTravellers = new ObservableCollection<Traveller>(sql.Travellers.ToList());
            }
        }

        private void ExecuteCreateNewTraveller()
        {
            LoadTraveller(new Traveller());
            this.Name = Traveller.GetUID(this.Owner);
        }

        private void ExecuteDeleteSelectedTraveller()
        {
            using (var sql = new SampleTravellersContext())
            {
                var p = sql.Travellers.Where(x => x.Id == SelectedTraveller.Id);
                if (p.Any())
                {
                    ExecuteCreateNewTraveller();
                    sql.Travellers.Remove(p.First());
                    AllTravellers.Remove(AllTravellers.Where(x => x.Id == p.First().Id).First());
                    sql.SaveChanges();
                }
            }
            SelectedTraveller = null;
        }

        private void ExecuteOpenQuestionsMenu()
        {

        }

        private void ExecuteOpenSelectedTraveller()
        {
            LoadTraveller(SelectedTraveller);
            SelectedTraveller = null;

        }

        private void ExecuteSaveCurrentTraveller()
        {
            using (var sql = new SampleTravellersContext())
            {
                var travellersQry = sql.Travellers.Where(x => x.Id == this.workingTraveller.Id);
                if (travellersQry.Any())
                {
                    this.workingTraveller = travellersQry.First();
                }
                else
                {
                    sql.Travellers.Add(workingTraveller);
                }
                foreach (var item in questions)
                {
                    this.workingTraveller.Questions.Add(sql.Questions.Where(x => x.Id == item.Id).First());
                }
                this.workingTraveller.User = sql.Users.Where(x => x.Id == this.Owner.Id).First();
                this.workingTraveller.StartDate = this.StartDate;
                this.workingTraveller.Name = this.Name;
                this.workingTraveller.Product = this.Product;
                this.workingTraveller.Description = this.Description;

                var saved = true;
                try
                {
                    sql.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    saved = false;
                    var s = new StringBuilder();
                    s.Append("The following issues are preventing the saving of this document:" + Environment.NewLine);
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            s.AppendFormat("Property: \"{0}\", Current Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage);
                            s.Append(Environment.NewLine);
                        }
                    }
                    System.Windows.MessageBox.Show(s.ToString());
                }
                finally
                {
                    if (saved)
                    {
                        System.Windows.MessageBox.Show("Successfully Saved Traveller.");
                    }
                }
            }
        }

        private void GetAllRelatedQuestions(System.Collections.Specialized.NotifyCollectionChangedEventArgs e, List<Question> p)
        {
            foreach (var q in e.NewItems)
            {
                var s = q as Question;
                var t = Traveller.GetAllRequisites(s);
                if (!questionsCollection.Any(x => x.Id == s.Id))
                {
                    questionsCollection.Add(s);
                }
                foreach (var u in t)
                {
                    if (!p.Any(x => x.Id == u.Id))
                    {
                        p.Add(u);
                    }
                }
            }
            foreach (var item in p)
            {
                if (!questions.Any(x => x.Id == item.Id))
                {
                    questionsCollection.Add(item);
                }
            }

            var d = new List<Question>(questionsCollection.Where(x => x.Template == true));
            if (d.Any())
            {
                foreach (var item in d)
                {
                    questionsCollection.Remove(item);
                }
            }
            questionsCollection = questionsCollection.Distinct().ToList();
        }

        private void LoadTraveller(Traveller T)
        {
            using (var sql = new SampleTravellersContext())
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
                var questionsQry = sql.Travellers.Where(x => x.Id == T.Id);
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
                this.workingTraveller = T;
                Messenger.Default.Send<RequestCloseTravellersDialog>(new RequestCloseTravellersDialog());
            }
        }

        private void Questions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
            var p = new List<Question>();
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                GetAllRelatedQuestions(e, p);
            }
            SortCurrentQuestions(questionsCollection);
        }

        private void ReloadAllQuestions()
        {
            AllQuestions.CollectionChanged -= AllQuestions_CollectionChanged;
            AllQuestions = new ObservableCollection<Question>(allQuestionsCollection);
            RaisePropertyChanged(() => AdministratorQuestions);
            RaisePropertyChanged(() => EngineeringQuestions);
            RaisePropertyChanged(() => DepartmentQuestions);
            AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;
        }

        private void SortCurrentQuestions(List<Question> q)
        {
            Questions.CollectionChanged -= Questions_CollectionChanged;

            var tmp2 = Traveller.SortByRequisites(q);

            Questions = new ObservableCollection<Question>(tmp2);
            Questions.CollectionChanged += Questions_CollectionChanged;
        }

        #endregion Private Methods

    }
}