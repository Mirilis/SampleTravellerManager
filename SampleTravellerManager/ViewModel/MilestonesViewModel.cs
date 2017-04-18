using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SampleManagerLibrary;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace SampleTravellerManager.ViewModel
{
    public class MilestonesViewModel : ViewModelBase
    {
        private ObservableCollection<Question> _AllQuestions = null;

        private String _Product = string.Empty;

        private DateTime _StartDate = DateTime.Now;

        private bool _Completed = false;

        private bool _Successful = false;
        
        private ObservableCollection<Question> _Questions = null;

        private ObservableCollection<Response> _Responses = null;

        private ObservableCollection<Sort> _QuestionOrder = null;

        private RelayCommand _NewMilestone;

        private RelayCommand _SaveMilestone;

        private RelayCommand _LoadMilestone;

        private RelayCommand _DeleteMilestone;

        private bool _MilestoneIsLoaded = false;

        private Milestone _CurrentlyLoadedMilestone;

        private List<Question> _MilestoneCollection = new List<Question>();

        private List<Question> _AllQuestionsCollection = new List<Question>();

        /// <summary>
        /// The <see cref="AllQuestions" /> property's name.
        /// </summary>
        public const string AllQuestionsPropertyName = "AllQuestions";

        /// <summary>
        /// The <see cref="QuestionOrder" /> property's name.
        /// </summary>
        public const string QuestionOrderPropertyName = "QuestionOrder";

        /// <summary>
        /// The <see cref="Responses" /> property's name.
        /// </summary>
        public const string ResponsesPropertyName = "Responses";

        /// <summary>
        /// The <see cref="Questions" /> property's name.
        /// </summary>      
        public const string QuestionsPropertyName = "Questions";

        /// <summary>
        /// The <see cref="Owner" /> property's name.
        /// </summary>  
        public const string OwnerPropertyName = "Owner";

        /// <summary>
        /// The <see cref="Successful" /> property's name.
        /// </summary>
        public const string SuccessfulPropertyName = "Successful";

        /// <summary>
        /// The <see cref="Completed" /> property's name.
        /// </summary>
        public const string CompletedPropertyName = "Completed";

        /// <summary>
        /// The <see cref="Product" /> property's name.
        /// </summary>
        public const string ProductPropertyName = "Product";

        /// <summary>
        /// The <see cref="StartDate" /> property's name.
        /// </summary>
        public const string StartDatePropertyName = "StartDate";

        /// <summary>
        /// The <see cref="MilestoneIsLoaded" /> property's name.
        /// </summary>
        public const string MilestoneIsLoadedPropertyName = "MilestoneIsLoaded";

        /// <summary>
        /// Sets and gets the Product property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the StartDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the Completed property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the Successful property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the Owner property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public User Owner
        {
            get
            {
                return _CurrentlyLoadedMilestone.User;
            }

            set
            {
                if (_CurrentlyLoadedMilestone.User == value)
                {
                    RaisePropertyChanged(() => Owner);
                    return;
                }

                _CurrentlyLoadedMilestone.User = value;
                RaisePropertyChanged(() => Owner);
            }
        }

        /// <summary>
        /// Sets and gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Question> Questions
        {
            get
            {
                if (_Questions == null)
                {
                    this._Questions = new ObservableCollection<Question>(_CurrentlyLoadedMilestone.Questions);
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
                RaisePropertyChanged(() => Questions);
            }
        }

        /// <summary>
        /// Sets and gets the Responses property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the QuestionOrder property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Sort> QuestionOrder
        {
            get
            {
                return _QuestionOrder;
            }

            set
            {
                if (_QuestionOrder == value)
                {
                    return;
                }

                _QuestionOrder = value;
                RaisePropertyChanged(() => QuestionOrder);
            }
        }

        /// <summary>
        /// Sets and gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Gets the NewMilestone.
        /// </summary>
        public RelayCommand NewMilestone
        {
            get
            {
                return _NewMilestone
                    ?? (_NewMilestone = new RelayCommand(
                    () =>
                    {
                        LoadNewMilestone();
                    }));
            }
        }

        /// <summary>
        /// Gets the SaveMilestone.
        /// </summary>
        public RelayCommand SaveMilestone
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

        /// <summary>
        /// Gets the LoadMilestone.
        /// </summary>
        public RelayCommand LoadMilestone
        {
            get
            {
                return _LoadMilestone
                    ?? (_LoadMilestone = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Gets the DeleteMilestone.
        /// </summary>
        public RelayCommand DeleteMilestone
        {
            get
            {
                return _DeleteMilestone
                    ?? (_DeleteMilestone = new RelayCommand(
                    () =>
                    {

                    }));
            }
        }

        /// <summary>
        /// Sets and gets the MilestoneIsLoaded property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool MilestoneIsLoaded
        {
            get
            {
                return _MilestoneIsLoaded;
            }

            set
            {
                if (_MilestoneIsLoaded == value)
                {
                    return;
                }

                _MilestoneIsLoaded = value;
                RaisePropertyChanged(() => MilestoneIsLoaded);
            }
        }

        public void LoadNewMilestone()
        {
            this._CurrentlyLoadedMilestone = new Milestone();
            this.Owner = User.GetCurrentUser();
        }

        public void SaveCurrentMilestone()
        {
            foreach (var item in _Questions)
            {
                this._CurrentlyLoadedMilestone.Questions.Add(item);

            }
        }

        private void SortCurrentQuestions(List<Question> q)
        {
            Questions.CollectionChanged -= Questions_CollectionChanged;
            
            var tmp2 =  SortByRequisites(q);

            Questions = new ObservableCollection<Question>(tmp2);
            Questions.CollectionChanged += Questions_CollectionChanged;
        }

        private List<Question> SortByRequisites(List<Question> input)
        {
            var output = new List<Question>();
            var tmp = new List<Question>();
            using (var sql = new SampleTravellersEntities())
            {
                foreach (var question in input)
                {
                    var q1 = sql.Questions.Where(x => x.Id == question.Id).First();
                    tmp.Add(q1);
                }
                tmp = tmp.OrderBy(x => x.TeamSort).ToList();
                output = new List<Question>(tmp);
                foreach (var tmpQ in tmp)
                {
                    foreach (var TmpQPre in tmpQ.Prerequisites)
                    {
                        if (output.IndexOf(tmpQ) > output.IndexOf(TmpQPre))
                        {
                            output.Remove(tmpQ);
                            output.Insert(output.IndexOf(TmpQPre), tmpQ);
                        }
                    }
                    foreach (var TmpQPre in tmpQ.Postrequisites)
                    {
                        if (output.IndexOf(tmpQ) < output.IndexOf(TmpQPre))
                        {
                            output.Remove(tmpQ);
                            output.Insert(output.IndexOf(TmpQPre)+1, tmpQ);
                        }
                    }
                }
            }
            return output;
        }
        
        public MilestonesViewModel()
        {
            var s = new SampleManagerLibrary.Model.SeedData();
            LoadNewMilestone();
            using (var sql = new SampleTravellersEntities())
            {
                var t = sql.Questions.Include("Corequisites").ToList();
                AllQuestions = new ObservableCollection<Question>(t);
                _AllQuestionsCollection = t;
            }

            Questions.CollectionChanged += Questions_CollectionChanged;
        }

        private void Questions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var p = new List<Question>();
            foreach (var q in e.NewItems)
            {
                var s = q as Question;
                var t = Milestone.GetAllRequisites(s);
                if (!s.Template && !_MilestoneCollection.Any(x => x.Id == s.Id))
                {

                    _MilestoneCollection.Add(s);
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
                    _MilestoneCollection.Add(item);
                }
            }
            SortCurrentQuestions(_MilestoneCollection);
            AllQuestions = new ObservableCollection<Question>(_AllQuestionsCollection);

        }






    }

}
