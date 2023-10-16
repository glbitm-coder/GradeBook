
namespace GradeBook
{
     public class Book
    {
        private List<double> grades;    
        public string Name;            
        public Book(string name)    // 3
        {
            grades = new List<double>();    // 4
            this.Name = name;               // 5
        }
        public void AddGrade(double grade)
        {
            grades.Add(grade);          // 6
            this.grades.Add(grade);     // 7
        }

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            var sum = 0.0;
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach(var grade in grades)
            {
                result.Low = Math.Min(result.Low, grade);
                result.High = Math.Max(result.High, grade);
                sum += grade;
            }
            
            result.Average = sum / grades.Count;
            
            return result;
        }
    }
}