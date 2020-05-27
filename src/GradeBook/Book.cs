using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }
        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name{ get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            /*var path = @"C:\User\Users\gradebook\src\GradeBook\Book.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{grade.ToString()}");
            }*/
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var grade = double.Parse(line);
                    result.Add(grade);
                    line = reader.ReadLine();
                }
            }
            
            return result;
        }
    }

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100.0 && grade >= 0.0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
            }

            return result;
        }

        private List<double> grades;
        /*public string Name
        {
            get;
            //private set;
            set;
            get
            {
                return name;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException($"Invalid {nameof(value)}");
                }
            }
        }*/
        //private string name;
    }
}