using System.Collections.Generic;
using System.Linq;

namespace Malzamaty.Utils {
    public class DataResult<T> {
        private int _pagesCount;
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }

        public int PagesCount {
            get => (this.TotalCount - 1) / Util.PageSize + 1;
            private set => _pagesCount = value;
        }
    }

    public class RawResult<T> {
        private int _pagesCount;
        public IQueryable<T> Raw { get; set; }
        public int TotalCount { get; set; }

        public int PagesCount {
            get => (this.TotalCount - 1) / Util.PageSize + 1;
            private set => _pagesCount = value;
        }
    }
}