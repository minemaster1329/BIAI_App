using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace BIAI_App_sandbox
{
    internal class Output
    {
        [VectorType(15)]
        [ColumnName("dense_9")]
        public float[] Data { get; set; }
    }
}
