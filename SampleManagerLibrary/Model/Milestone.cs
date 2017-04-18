using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleManagerLibrary
{
    public partial class Milestone
    {
        public static  Dictionary<Sort,Question> GetAllCorequisites(Question q)        {
            var questionsToBeAdded = new Dictionary<Sort,Question>();
            ProcessQuestion(q, questionsToBeAdded);
            return questionsToBeAdded;
        }

        private static void ProcessQuestion(Question q, Dictionary<Sort,Question> result)
        {
            using (var sql = new SampleTravellersEntities())
            {
            foreach (var question in q.Corequisites)
            {
                ProcessQuestion(question, result);
            }
            if (!result.Any(x=>x.Value.Id == q.Id))
            {
                var s = new Sort() { Question = q };
                result.Add(s,q);
            }
            }
        }

        private static void RefreshSortOrder(Dictionary<Sort,Question> refresh)
        {
            var q = refresh.OrderBy(x => x.Value.Team).ToList();
            foreach (var sort in refresh)
            {
                var so = q.Where(x => x.Value.Id == x.Key.Question.Id);
                if (so.Any())
                {
                    sort.Key.Order = q.IndexOf(so.First());
                }
                
            }
        }
    }
}
