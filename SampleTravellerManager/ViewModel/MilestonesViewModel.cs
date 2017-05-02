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

namespace SampleTravellerManager.ViewModel
{
    public class MilestonesViewModel : ViewModelBase
    {
        private ObservableCollection<Question> _AllQuestions = new ObservableCollection<Question>();
        private List<Question> _AllQuestionsCollection = new List<Question>();
        private bool _Completed = false;
        private string _description = string.Empty;
        private User _Owner = null;
        private String _Product = string.Empty;
        private ObservableCollection<Question> _Questions = null;
        private ObservableCollection<Response> _Responses = null;
        private RelayCommand _SaveMilestone;
        private Traveller _SelectedTraveller = null;
        private DateTime _StartDate = DateTime.Now;
        private bool _Successful = false;
        private ObservableCollection<Traveller> allTravellers = null;
        private bool aTravellerIsSelected = false;
        private RelayCommand closeWindow;
        private RelayCommand copyCommand;
        private RelayCommand copyTraveller;
        private RelayCommand deleteCommand;
        private RelayCommand deleteTraveller;
        private bool isOKToDelete = false;
        private RelayCommand loadCommand;
        private string name = string.Empty;
        private RelayCommand newCommand;
        private RelayCommand openCommand;
        private RelayCommand openQuestionsMenu;
        private RelayCommand editQuestion;
        public RelayCommand EditQuestion
        {
            get
            {
                return editQuestion
                    ?? (editQuestion = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow(SelectedQuestion));
                    },
                    () => AQuestionIsSelected));
            }
        }

        private bool aQuestionIsSelected = false;
        public bool AQuestionIsSelected
        {
            get
            {
                return aQuestionIsSelected;
            }

            set
            {
                if (aQuestionIsSelected == value)
                {
                    return;
                }

                aQuestionIsSelected = value;
                RaisePropertyChanged(() => AQuestionIsSelected);
            }
        }

        private Question selectedQuestion = null;
        public Question SelectedQuestion
        {
            get
            {
                if (selectedQuestion == null)
                {
                    AQuestionIsSelected = false;
                }
                else
                {
                    AQuestionIsSelected = true;
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

        private string title = "Sample Traveller Manager";
        private bool travellerIsLoaded = false;
        private List<Question> TravellersCollection = new List<Question>();
        private Traveller WorkingTraveller;
        public MilestonesViewModel()
        {
            var s = new SeedData();
            NewCommandExecute();
            using (var sql = new SampleTravellersContext())
            {
                var t = sql.Questions
                    .ToList();

                AllQuestions = new ObservableCollection<Question>(t);
                AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;

                _AllQuestionsCollection = new List<Question>(t);
            }

            Questions.CollectionChanged += Questions_CollectionChanged;
        }
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
                return _AllQuestions;
            }

            set
            {
                if (_AllQuestions == value)
                {
                    return;
                }

                _AllQuestions = value;

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
        public bool ATravellerIsSelected
        {
            get
            {
                return aTravellerIsSelected;
            }

            set
            {
                if (aTravellerIsSelected == value)
                {
                    return;
                }
                aTravellerIsSelected = value;
                RaisePropertyChanged(() => ATravellerIsSelected);
            }
        }
        public RelayCommand CloseWindow
        {
            get
            {
                return closeWindow
                    ?? (closeWindow = new RelayCommand(
                    () =>
                    {
                        Messenger.Default.Send<RequestCloseTravellersWindow>(new RequestCloseTravellersWindow());
                    }));
            }
        }
        public bool Completed
        {
            get
            {
                return _Completed;
            }

            set
            {
                if (_Completed == value)
                {
                    return;
                }

                _Completed = value;
                RaisePropertyChanged(() => Completed);
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
                        Messenger.Default.Send<RequestLoadCopyTravellerDialog>(new RequestLoadCopyTravellerDialog());
                    }));
            }
        }
        public RelayCommand CopyTraveller
        {
            get
            {
                return copyTraveller
                    ?? (copyTraveller = new RelayCommand(
                                          () =>
                                          {
                                              CopySelectedTraveller();
                                          },
                                          () => ATravellerIsSelected));
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
                        Messenger.Default.Send<RequestLoadDeleteTravellerDialog>(new RequestLoadDeleteTravellerDialog());
                    }));
            }
        }
        public RelayCommand DeleteTraveller
        {
            get
            {
                return deleteTraveller
                    ?? (deleteTraveller = new RelayCommand(
                                          () =>
                                          {
                                              DeleteTravellerExecute();
                                          },
                                          () => IsOKToDelete && ATravellerIsSelected));
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
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
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
        public RelayCommand LoadCommand
        {
            get
            {
                return loadCommand
                    ?? (loadCommand = new RelayCommand(
                                          () =>
                                          {
                                              Messenger.Default.Send<RequestLoadTravellerDialog>(new RequestLoadTravellerDialog() { Message = string.Empty });
                                          },
                                          () => true));
            }
        }
        public bool MilestoneIsLoaded
        {
            get
            {
                return travellerIsLoaded;
            }

            set
            {
                if (travellerIsLoaded == value)
                {
                    return;
                }

                travellerIsLoaded = value;
                RaisePropertyChanged(() => MilestoneIsLoaded);
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
                        NewCommandExecute();
                    }));
            }
        }
        public RelayCommand OpenQuestionsMenu
        {
            get
            {
                return openQuestionsMenu
                    ?? (openQuestionsMenu = new RelayCommand(
                    () =>
                    {
                        OpenQuestionsMenuExecute();
                    }));
            }
        }
        public RelayCommand OpenTraveller
        {
            get
            {
                return openCommand
                    ?? (openCommand = new RelayCommand(
                    () =>
                    {
                        OpenSelectedTraveller();
                    },
                    () => ATravellerIsSelected));
            }
        }
        public User Owner
        {
            get
            {
                return _Owner;
            }

            set
            {
                if (_Owner == value)
                {
                    RaisePropertyChanged(() => Owner);
                    return;
                }

                _Owner = value;
                RaisePropertyChanged(() => Owner);
            }
        }
        public String Product
        {
            get
            {
                return _Product;
            }

            set
            {
                if (_Product == value)
                {
                    return;
                }

                _Product = value;
                RaisePropertyChanged(() => Product);
            }
        }
        public ObservableCollection<Question> Questions
        {
            get
            {
                if (_Questions == null)
                {
                    this._Questions = new ObservableCollection<Question>(WorkingTraveller.Questions);
                }
                return _Questions;
            }

            set
            {
                if (_Questions == value)
                {
                    return;
                }

                _Questions = value;
                Questions.CollectionChanged += Questions_CollectionChanged;
                RaisePropertyChanged(() => Questions);
            }
        }
        public ObservableCollection<Response> Responses
        {
            get
            {
                return _Responses;
            }

            set
            {
                if (_Responses == value)
                {
                    return;
                }

                _Responses = value;
                RaisePropertyChanged(() => Responses);
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _SaveMilestone
                    ?? (_SaveMilestone = new RelayCommand(
                    () =>
                    {
                        SaveCurrentMilestone();
                    }));
            }
        }
        public Traveller SelectedTraveller
        {
            get
            {
                return _SelectedTraveller;
            }

            set
            {
                if (_SelectedTraveller == value)
                {
                    return;
                }
                if (value == null)
                {
                    ATravellerIsSelected = false;
                }
                else
                {
                    ATravellerIsSelected = true;
                }
                _SelectedTraveller = value;
                RaisePropertyChanged(() => SelectedTraveller);
            }
        }
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }

            set
            {
                if (_StartDate == value)
                {
                    return;
                }

                _StartDate = value;
                RaisePropertyChanged(() => StartDate);
            }
        }
        public bool Successful
        {
            get
            {
                return _Successful;
            }

            set
            {
                if (_Successful == value)
                {
                    return;
                }

                _Successful = value;
                RaisePropertyChanged(() => Successful);
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
        private void CopySelectedTraveller()
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
        private void DeleteTravellerExecute()
        {
            using (var sql = new SampleTravellersContext())
            {
                var p = sql.Travellers.Where(x => x.Id == SelectedTraveller.Id);
                if (p.Any())
                {
                    NewCommandExecute();
                    sql.Travellers.Remove(p.First());
                    AllTravellers.Remove(AllTravellers.Where(x => x.Id == p.First().Id).First());
                    sql.SaveChanges();
                }
            }
            SelectedTraveller = null;
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
                this.WorkingTraveller = T;
                Messenger.Default.Send<RequestCloseTravellersDialog>(new RequestCloseTravellersDialog());
            }
        }
        private void NewCommandExecute()
        {
            LoadTraveller(new Traveller());
            this.Name = Traveller.GetUID(this.Owner);
        }
        private void OpenQuestionsMenuExecute()
        {
            Messenger.Default.Send<RequestOpenQuestionsWindow>(new RequestOpenQuestionsWindow());
        }
        private void OpenSelectedTraveller()
        {
            LoadTraveller(SelectedTraveller);
            SelectedTraveller = null;

        }
        private void Questions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
            var p = new List<Question>();
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                GetAllRelatedQuestions(e, p);
            }
            SortCurrentQuestions(TravellersCollection);
        }
        private void GetAllRelatedQuestions(System.Collections.Specialized.NotifyCollectionChangedEventArgs e, List<Question> p)
        {
            foreach (var q in e.NewItems)
            {
                var s = q as Question;
                var t = Traveller.GetAllRequisites(s);
                if (!TravellersCollection.Any(x => x.Id == s.Id))
                {
                    TravellersCollection.Add(s);
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
                if (!_Questions.Any(x => x.Id == item.Id))
                {
                    TravellersCollection.Add(item);
                }
            }

            var d = new List<Question>(TravellersCollection.Where(x => x.Template == true));
            if (d.Any())
            {
                foreach (var item in d)
                {
                    TravellersCollection.Remove(item);
                }
            }
            TravellersCollection = TravellersCollection.Distinct().ToList();
        }
        private void ReloadAllQuestions()
        {
            AllQuestions.CollectionChanged -= AllQuestions_CollectionChanged;
            AllQuestions = new ObservableCollection<Question>(_AllQuestionsCollection);
            RaisePropertyChanged(() => AdministratorQuestions);
            RaisePropertyChanged(() => EngineeringQuestions);
            RaisePropertyChanged(() => DepartmentQuestions);
            AllQuestions.CollectionChanged += AllQuestions_CollectionChanged;
        }
        private void SaveCurrentMilestone()
        {
            using (var sql = new SampleTravellersContext())
            {
                var travellersQry = sql.Travellers.Where(x => x.Id == this.WorkingTraveller.Id);
                if (travellersQry.Any())
                {
                    this.WorkingTraveller = travellersQry.First();
                }
                else
                {
                    sql.Travellers.Add(WorkingTraveller);
                }
                foreach (var item in _Questions)
                {
                    this.WorkingTraveller.Questions.Add(sql.Questions.Where(x => x.Id == item.Id).First());
                }
                this.WorkingTraveller.User = sql.Users.Where(x => x.Id == this.Owner.Id).First();
                this.WorkingTraveller.StartDate = this.StartDate;
                this.WorkingTraveller.Name = this.Name;
                this.WorkingTraveller.Product = this.Product;
                this.WorkingTraveller.Description = this.Description;

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
        private void SortCurrentQuestions(List<Question> q)
        {
            Questions.CollectionChanged -= Questions_CollectionChanged;

            var tmp2 = Traveller.SortByRequisites(q);

            Questions = new ObservableCollection<Question>(tmp2);
            Questions.CollectionChanged += Questions_CollectionChanged;
        }
        

    }
}