using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstractions
{
    public class SearchResults<T>
        where T : class
    {
        public IReadOnlyCollection<T> Results { get; set; }
    }
}
