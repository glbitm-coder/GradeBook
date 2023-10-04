
namespace GradeBook
{
    class Book
    {
        List<double> grades;    // 1
        string name;            // 2
        public Book(string name)    // 3
        {
            grades = new List<double>();    // 4
            this.name = name;               // 5
        }
        public void AddGrade(double grade)
        {
            grades.Add(grade);          // 6
            this.grades.Add(grade);     // 7
        }

        internal void ShowStatistics()
        {
            var sum = 0.0;
            var highGrade = double.MinValue;
            var lowGrade = double.MaxValue;
            foreach(var number in grades){
                lowGrade = Math.Min(lowGrade, number);
                highGrade = Math.Max(highGrade, number);
                sum += number;
            }
            var avgGrade = sum / grades.Count;
            Console.WriteLine($"The lowest grade is { lowGrade }");
            Console.WriteLine($"The highest grade is { highGrade }");
            Console.WriteLine($"The avreage grade is { avgGrade:N1}");
        }
    }
}