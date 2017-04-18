using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleManagerLibrary
{
    public partial class Milestone
    {
        public static List<Question> GetAllRequisites(Question q)
        {
            var result = new List<Question>();
            using (var sql = new SampleTravellersEntities())
            {
                var liveQ = sql.Questions.Where(x => x.Id == q.Id).First();
                ProcessQuestion(liveQ.Prerequisites, result, sql);
                ProcessQuestion(liveQ.Corequisites, result, sql);
                ProcessQuestion(liveQ.Postrequisites, result, sql);
            }
            return result.Distinct().ToList();
        }

        private static void ProcessQuestion(ICollection<Question> question, List<Question> result, SampleTravellersEntities sql)
        {

            foreach (var q in question)
            {
                result.Add(q);
                ProcessQuestion(q.Prerequisites, result, sql);
                ProcessQuestion(q.Corequisites, result, sql);
                ProcessQuestion(q.Postrequisites, result, sql);
            }


        }
    }
}
