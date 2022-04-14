using System;
using System.Text.Json.Serialization;

namespace StudentsTestsResult
{
    internal class StudentTest
    {
        public string Name { get; init; }

        public string Soname { get; init; }

        public string Test { get; init; }

        [JsonConverter(typeof(DMY_DateTimeConverter))]
        public DateTime Date { get; init; }

        public int Mark { get; init; }

        public object this[string propertyName] => propertyName switch
        {
            "Name" => Name,
            "Soname" => Soname,
            "Test" => Test,
            "Date" => Date,
            "Mark" => Mark,
            _ => throw new ArgumentException("propertyName")
        };

        public override bool Equals(object obj)
        {
            StudentTestEqualityComparer stec = new();
            return stec.Equals(this, obj);
        }

        public sealed override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public sealed override string ToString()
        {
            return $"{Name} {Soname} {Test} {Date.ToShortDateString()} {Mark}";
        }
    }
}