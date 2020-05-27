using System;

namespace GradeBook
{
    public class Statistics
    {
        public Statistics()
        {
            Sum = 0.0;
            Count = 0;
            High = double.MinValue;
            Low = double.MaxValue;
        }

        public void Add(double grade)
        {
            Sum += grade;
            Count++;
            High = Math.Max(High, grade);
            Low = Math.Min(Low, grade);
        }

        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';
                    case var d when d >= 80.0:
                        return 'B';
                    case var d when d >= 70.0:
                        return 'C';
                    case var d when d >= 60.0:
                        return 'D';
                    default:
                        return 'F';
                }
            }
        }
        public double Sum;
        public int Count;
    }
}