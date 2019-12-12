using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class PagedCollectionResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public int AllCount { get; set; }
    }
}
