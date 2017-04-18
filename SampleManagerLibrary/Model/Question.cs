using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleManagerLibrary
{
    public partial class Question
    {
        public string TeamName
        { get => ((Team)this.Team).ToString().SplitCamelCase(); }

        public string ResponseType
        { get => ((ResponseType)this.Type).ToString().SplitCamelCase(); }

        public int TeamSort { get => ProductFlow.Order.IndexOf(((Team)this.Team).ToString()); }
    }
}
