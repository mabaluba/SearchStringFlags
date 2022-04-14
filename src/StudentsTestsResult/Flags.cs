using System;

namespace StudentsTestsResult
{
    internal class Flags
    {
        public string Name { get; init; }

        public string Soname { get; init; }

        public string Test { get; init; }

        public DateTime DateFrom { get; init; }

        public DateTime DateTo { get; init; } = DateTime.Now;

        public int MinMark { get; init; }

        public int MaxMark { get; init; } = 5;

        public string SortProperty { get; init; }

        public string SortWay { get; init; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Flags);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        private bool Equals(Flags other)
        {
            return other != null &&
                   Name == other.Name &&
                   Soname == other.Soname &&
                   Test == other.Test &&
                   DateFrom == other.DateFrom &&
                   DateTo == other.DateTo &&
                   MinMark == other.MinMark &&
                   MaxMark == other.MaxMark &&
                   SortProperty == other.SortProperty &&
                   SortWay == other.SortWay;
        }

        public sealed override string ToString()
        {
            return $"{Name} {Soname} {Test} {DateFrom.ToShortDateString()} {DateTo.ToShortDateString()} {MinMark} {MaxMark} {SortProperty} {SortWay}";
        }
    }
}