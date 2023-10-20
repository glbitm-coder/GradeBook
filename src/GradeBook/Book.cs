
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

        public void AddLetterGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                
                case 'B':
                    AddGrade(80);
                    break;
                
                case 'C':
                    AddGrade(70);
                    break;
                
                default:
                    AddGrade(0);
                    break;
            }
        }

        public void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
            }          
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
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
                 Console.WriteLine(grade);
                result.Low = Math.Min(result.Low, grade);
                result.High = Math.Max(result.High, grade);
                sum += grade;
            }
            
            result.Average = sum / grades.Count;

            switch(result.Average)
            {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;

                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;

                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }
    }
}