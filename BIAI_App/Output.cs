using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace BIAI_App
{
    class Output
    {
        [VectorType(15)]
        [ColumnName("dense_9")]
        public float[] Data { get; set; }
    }
}
