using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StudentsTestsResult
{
    internal class SearchController
    {
        private IEnumerable<StudentTest> Tests { get; set; }

        private Flags FlagsForSearch { get; set; }

        public SearchController(IEnumerable<StudentTest> tests, Flags flagsForSearch)
        {
            Tests = tests ?? throw new ArgumentNullException(nameof(tests));
            FlagsForSearch = flagsForSearch ?? throw new ArgumentNullException(nameof(flagsForSearch));
        }

        public IEnumerable<StudentTest> GetResults()
        {
            if (FlagsForSearch.SortProperty != null && FlagsForSearch.SortWay != null)
            {
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                var sortProp = ti.ToTitleCase(FlagsForSearch.SortProperty);

                if (FlagsForSearch.SortWay.Equals("asc"))
                {
                    return Tests
                        .Where(i => i.Equals(FlagsForSearch))
                        .OrderBy(i => i[sortProp]);
                }
                else
                {
                    return Tests
                        .Where(i => i.Equals(FlagsForSearch))
                        .OrderByDescending(i => i[sortProp]);
                }
            }
            else
            {
                return Tests.Where(i => i.Equals(FlagsForSearch));
            }
        }
    }
}