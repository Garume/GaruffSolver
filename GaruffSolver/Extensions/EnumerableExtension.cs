using System;
using System.Collections.Generic;
using System.Linq;

namespace GaruffSolver.Extensions;

public static class EnumerableExtension
{
    public static void RemoveAll<T>(ref IEnumerable<T> source, Func<T, bool> predicate)
    {
        source = source.Where(x => !predicate(x));
    }
}