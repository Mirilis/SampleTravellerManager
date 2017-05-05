using NWCSampleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTravelerManager.Messages
{
    internal class RequestOpenQuestionsWindow : MessagesBase
    {
        public Question Question { get; set; }

        public RequestOpenQuestionsWindow()
        {
        }

        public RequestOpenQuestionsWindow(Question q)
        {
            this.Question = q;
        }
    }

    internal class RequestOpenLoadQuestionDialog : MessagesBase
    {
    }

    internal class RequestOpenLoadtravelerDialog : MessagesBase
    {
    }

    internal class RequestOpenCopyTravelerDialog : MessagesBase
    {
    }

    internal class RequestOpenDeleteTravelerDialog : MessagesBase
    {
    }

    internal class RequestOpenCopyQuestionDialog : MessagesBase
    {
    }

    internal class RequestOpenDeleteQuestionDialog : MessagesBase
    {
    }
}