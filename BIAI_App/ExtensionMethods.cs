using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIAI_App
{
    internal static class ExtensionMethods
    {
        public static int IndexOfMax<T>(this IEnumerable<T> seq) where T : IComparable<T>
        {
            if (seq is null) throw new ArgumentNullException(nameof(seq));
            if (!seq.Any()) return -1;

            return seq.Select((f, idx) => new {Val = f, Idx = idx}).Aggregate(new {Max = default(T), MaxIndex = -1},
                (mp, fp) => mp.MaxIndex == -1 || fp.Val.CompareTo(mp.Max) > 0
                    ? new {Max = fp.Val, MaxIndex = fp.Idx}
                    : mp).MaxIndex;
        }
    }
}
