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
    class RequestOpenLoadQuestionDialog : MessagesBase
    {

    }
    class RequestOpenLoadTravellerDialog : MessagesBase
    {

    }
    class RequestOpenCopyTravellerDialog : MessagesBase
    {

    }
    class RequestOpenDeleteTravellerDialog : MessagesBase
    {

    }
    class RequestOpenCopyQuestionDialog : MessagesBase
    {

    }
    class RequestOpenDeleteQuestionDialog : MessagesBase
    {

    }
}
