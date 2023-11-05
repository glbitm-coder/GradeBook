
namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public string Name
        {
            get;
            set;
        }
        public NamedObject(string name)
        {
            Name = name;
        }

    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name 
        {
            get; 
        }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        protected Book(string name) : base(name)
        {

        }

        public virtual event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public virtual Statistics GetStatistics()
        {
            throw new NotImplementedException();
        }
    }


     public class InMemoryBook : Book, IBook
    {
        private List<double> grades;   

        public string Name
        {
            get; set;
        }         
 
        public InMemoryBook(string name)  : base(name)
        {
            grades = new List<double>();   
            this.Name = name;      
        }

        public void AddGrade(char letter)
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

        public override void AddGrade(double grade)
        {
            if(grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)                      //1
                {
                    GradeAdded(this, new EventArgs());      //2
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