using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StudentsTestsResult
{
    internal static class TestsController
    {
        public static IReadOnlyCollection<StudentTest> GetTestsResultsFromFile()
        {
            var jsonData = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestsStudents.json"));

            return JsonSerializer.Deserialize<IReadOnlyCollection<StudentTest>>(
                jsonData,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}