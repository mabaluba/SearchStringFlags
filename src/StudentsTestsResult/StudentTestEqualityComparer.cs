using System;
using System.Collections;

namespace StudentsTestsResult
{
    internal class StudentTestEqualityComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            _ = x ?? throw new ArgumentNullException(nameof(x));
            _ = y ?? throw new ArgumentNullException(nameof(y));

            if (x is StudentTest xS && y is StudentTest yS)
            {
                return Equals(xS, yS);
            }
            else
            {
                return Equals(x as StudentTest, y as Flags);
            }
        }

        private bool Equals(StudentTest x, StudentTest y)
        {
            _ = x ?? throw new ArgumentNullException(nameof(x));
            _ = y ?? throw new ArgumentNullException(nameof(y));

            return x.Name == y.Name &&
                   x.Soname == y.Soname &&
                   x.Test == y.Test &&
                   x.Date == y.Date &&
                   x.Mark == y.Mark;
        }

        private bool Equals(StudentTest x, Flags y)
        {
            _ = x ?? throw new ArgumentNullException(nameof(x));
            _ = y ?? throw new ArgumentNullException(nameof(y));

            return
                (y.Name == null || x.Name == y.Name) &&

                (y.Soname == null || x.Soname == y.Soname) &&

                (y.Test == null || x.Test == y.Test) &&

                x.Date >= y.DateFrom &&
                x.Date <= y.DateTo &&

                x.Mark >= y.MinMark &&
                x.Mark <= y.MaxMark;
        }

        public int GetHashCode(object obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}