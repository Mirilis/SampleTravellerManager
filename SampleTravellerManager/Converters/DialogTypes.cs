using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTravellerManager.Converters
{
    [Flags]
    public enum DialogType
    {
        Traveller = 1,
        Question =2 ,
        Load=4,
        Copy=8,
        Delete=16
    }
}
