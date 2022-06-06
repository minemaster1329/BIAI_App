using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace BIAI_App_sandbox2
{
    class Input
    {
        [VectorType(224 * 224 * 3)]
        [ColumnName("sequential_input")]
        public float[] Data { get; set; }
    }
}
