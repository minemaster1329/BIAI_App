using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace BIAI_App
{
    class Input
    {
        [VectorType(224 * 224 * 3)]
        [ColumnName("sequential_input")]
        public float[] Data { get; set; }
    }
}
