using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace BIAI_App_sandbox
{
    internal class Input
    {
        [VectorType(224 * 224 * 3)]
        [ColumnName("sequential_9")]
        public float[] Data { get; set; }
    }
}
