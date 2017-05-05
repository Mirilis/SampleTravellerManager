using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTravelerManager.Converters
{
    [Flags]
    public enum DialogTypes
    {
        Traveler = 1,
        Question = 2,
        Load = 4,
        Copy = 8,
        Delete = 16
    }
}