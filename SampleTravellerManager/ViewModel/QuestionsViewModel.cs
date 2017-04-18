using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SampleManagerLibrary;

namespace SampleTravellerManager.ViewModel
{
    public class QuestionsViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="QuestionsList" /> property's name.
        /// </summary>
        public const string QuestionsListPropertyName = "QuestionsList";

        private List<Question> Questions = new List<Question>();

        /// <summary>
        /// Sets and gets the QuestionsList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<Question> QuestionsList
        {
            get
            {
                return Questions;
            }

            set
            {
                if (Questions == value)
                {
                    return;
                }

                Questions = value;
                RaisePropertyChanged(() => QuestionsList);
            }
        }

      
        public QuestionsViewModel()
        {
            var s = new SampleManagerLibrary.Model.SeedData();
            using (var sql = new SampleManagerLibrary.SampleTravellersEntities())
            {
                QuestionsList = sql.Questions.ToList();
            }
        }

    }
}
