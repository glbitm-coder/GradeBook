
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
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
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
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
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
                       
            foreach(var grade in grades)
            {
                result.Add(grade);
            }

            return result;
        }
    }
}