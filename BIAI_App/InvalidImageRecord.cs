using System;
using System.Collections.Generic;
using System.Text;

namespace BIAI_App
{
    [Serializable()]
    public class InvalidImageRecord
    {
        public string ImagePath { get; set; }
        public string Label { get; set; }
    }
}
