using System;
using System.Linq;

namespace StudentsTestsResult
{
    internal class Program
    {
        /// <summary>
        /// How should work UI ConsoleApp.
        /// </summary>
        public static void Main()
        {
            var tests = TestsController.GetTestsResultsFromFile();
            foreach (var item in tests)
            {
                Console.WriteLine(item);
            }

            try
            {
                var searchParameters = "-name Robin -soname Scherbatsky -minmark 3 -maxmark 5 -dateto 20/12/2020 -sort date desc";

                var flagsController = new FlagsControllerSpan(searchParameters);

                var searchFlags = flagsController.FlagsForSearch;

                Console.WriteLine();

                var search = new SearchController(tests, searchFlags);
                var searchResults = search.GetResults();
                var topic = tests.FirstOrDefault().GetType().GetProperties();
                Array.ForEach(topic, i => Console.Write(i.Name + " "));
                Console.WriteLine();
                foreach (var item in searchResults)
                {
                    Console.WriteLine(item);
                }
            }
            catch (ArgumentNullException ex)
            {
                ShowMessage(ex.Message);
            }
            catch (FormatException ex)
            {
                ShowMessage(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private static void ShowMessage(string exceptionMessage)
        {
            Console.WriteLine();
            Console.WriteLine("Search parameters are not valid!");
            Console.WriteLine(exceptionMessage);
        }
    }
}