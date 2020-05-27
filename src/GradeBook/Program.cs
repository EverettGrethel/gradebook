using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[] numbers = new double[3] { 12.7, 10.3, 6.11 };
            //var numbers = new[] { 12.7, 10.3, 6.11, 4.1 };
            var book = new DiskBook("Everett's Grade Book");
            book.GradeAdded += OnGradeAdded;
            /*while (true)
            {
                Console.WriteLine("Enter a name.");
                var name = Console.ReadLine();
                try
                {
                    book.SetName(name);
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (book.Name != "Unnamed")
                {
                    break;
                }
            }*/
            /*book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);*/

            EnterGrades(book);

            var stats = book.GetStatistics();
            //book.Name = "";

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}.");
            Console.WriteLine($"The highest grade is {stats.High}.");
            Console.WriteLine($"The average grade is {stats.Average:N1}.");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit.");
                var input = Console.ReadLine();
                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added.");
        }
    }
}
