using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecKeyTest_Folomeev.ViewModels
{
    public struct RecordViewModel
    {
        public string Id { get; set; }
        public string StringKey { get; set; }
        public int DigitalKey { get; set; }
        public int RandomDigit { get; set; }

        public RecordViewModel(string id, string stringKey, int digitalKey, int randomDigit)
        {
            Id = id;
            StringKey = stringKey;
            DigitalKey = digitalKey;
            RandomDigit = randomDigit;
        }
    }
}
