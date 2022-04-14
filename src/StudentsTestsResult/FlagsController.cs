using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StudentsTestsResult
{
    internal class FlagsController
    {
        private static readonly string[] _validFlags = new string[] { "name", "soname", "test", "datefrom", "dateto", "minmark", "maxmark", "sort", "date" };

        private static readonly string[] _validSortFlags = new string[] { "asc", "desc" };

        private string Filters { get; set; }

        public Flags FlagsForSearch { get; private set; }

        public FlagsController(string filters)
        {
            Filters = string.IsNullOrWhiteSpace(filters) ?
                throw new ArgumentNullException(nameof(filters), $"'{nameof(filters)}' cannot be null or whitespace.") :
                filters;

            FlagsForSearch = MapToFlags();
        }

        private Dictionary<string, string[]> FromStringToDict()
        {
            var result = Filters.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                .Select(str => str.Split(' '))
                                .ToDictionary(s => s[0], s => s[1..]);
            return result;
        }

        private Flags MapToFlags()
        {
            var filtersCollection = FromStringToDict();

            FlagsValidation(filtersCollection);

            var dateFrom = DateValidation(filtersCollection, "datefrom");
            var dateTo = DateValidation(filtersCollection, "dateto");

            return new Flags
            {
                Name = filtersCollection.ContainsKey("name")
                    ? filtersCollection["name"][0]
                    : default,
                Soname = filtersCollection.ContainsKey("soname")
                    ? filtersCollection["soname"][0]
                    : default,
                Test = filtersCollection.ContainsKey("test")
                    ? filtersCollection["test"][0]
                    : default,
                DateFrom =
                    filtersCollection.ContainsKey("datefrom")
                    ? dateFrom
                    : default,
                DateTo =
                    filtersCollection.ContainsKey("dateto")
                    ? dateTo
                    : DateTime.Now,
                MinMark =
                    filtersCollection.ContainsKey("minmark") && int.TryParse(filtersCollection["minmark"][0], out var minMark)
                    ? minMark
                    : default,
                MaxMark =
                    filtersCollection.ContainsKey("maxmark") && int.TryParse(filtersCollection["maxmark"][0], out var maxMark)
                    ? maxMark
                    : 5,
                SortProperty =
                    filtersCollection.ContainsKey("sort")
                    ? filtersCollection["sort"][0] :
                    default,
                SortWay =
                    filtersCollection.ContainsKey("sort")
                    ? filtersCollection["sort"][1] :
                    default
            };
        }

        private static DateTime DateValidation(Dictionary<string, string[]> filtersCollection, string dateFlag)
        {
            DateTime date = default;
            if (filtersCollection.ContainsKey(dateFlag) &&
                    !DateTime.TryParseExact(
                            filtersCollection[dateFlag][0],
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out date))
            {
                throw new FormatException($"Flag '-{dateFlag} ...' is not in correct format. Should be in format dd/MM/yyyy.");
            }

            return date;
        }

        private static void FlagsValidation(Dictionary<string, string[]> filtersCollection)
        {
            if (filtersCollection.Keys.Any(i => !_validFlags.Contains(i)) ||
                filtersCollection.Where(i => i.Key != "sort").Any(v => v.Value.Length != 1))
            {
                throw new FormatException("The input string contains NOT valid some '-key' parameter.");
            }

            if (filtersCollection.TryGetValue("sort", out var sort))
            {
                if (sort.Length != 2)
                {
                    throw new FormatException("The input string contains NOT valid '-sort ...' parameter. It should have two flags, but has less.");
                }

                if (!_validFlags.Contains(sort[0]) || !_validSortFlags.Contains(sort[1]))
                {
                    throw new FormatException("The input string contains NOT valid '-sort ...' parameter.");
                }
            }
        }
    }
}