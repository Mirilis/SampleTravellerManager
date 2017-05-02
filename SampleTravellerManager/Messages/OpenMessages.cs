using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWCSampleManager;

namespace SampleTravellerManager.Messages
{
    class RequestOpenQuestionsWindow
    {
        public string Message { get; set; }
        public Question Question { get; set; }

        public RequestOpenQuestionsWindow()
        {

        }
        public RequestOpenQuestionsWindow(Question q)
        {
            this.Question = q;
        }
    }

}
