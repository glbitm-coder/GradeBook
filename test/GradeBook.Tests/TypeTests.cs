
namespace GradeBook.Tests;

public delegate string WriteLogDelegate(string logMessage); // 1
public class TypeTests
{
    int count = 0;

    [Fact]
    public void WriteLogDelegateCanPointToMethod()
    {
        WriteLogDelegate log = ReturnMessage;                         
        
        log += ReturnMessage;        
        log += IncrementCount;                      

        var result = log("Hello!");                         
        Assert.Equal(3, count);    
        Assert.Equal("hello!", result);      
    }
    string IncrementCount(string message)
    {
        ++count;
        return message.ToLower();
    }
    string ReturnMessage(string mesage)
    {
        ++count;
        return mesage;
    }

    [Fact]
    public void BookCalculatesAnAverageGrade()
    {
        var book1 = GetBook("Book 1");
        var book2 = GetBook("Book 2");

        Assert.Equal("Book 1", book1.Name);
        Assert.Equal("Book 2", book2.Name);
        Assert.NotSame(book1, book2);
    }

    [Fact]
    public void TwoVarsCanReferenceSameObject()
    {
        var book1 = GetBook("Book 1");
        var book2 = book1;

        Assert.Same(book1, book2);
        Assert.True(Object.ReferenceEquals(book1, book2));
    }

    [Fact]
    public void CanSetNameForReference()
    {
        var book1  = GetBook("Book 1");
        SetName(book1, "New Name");

        Assert.Equal("New Name", book1.Name);

    }

    [Fact]
    public void CSharpIsPassByValue()
    {
        var book1 = GetBook("Book 1");
        GetBookSetName(book1, "New Name");

        Assert.Equal("Book 1",book1.Name);
    }

    [Fact]
    public void CSharpCanPassByRef()
    {
        var book1 = GetBook("Book 1");
        GetBookSetNameForRef(out book1, "New Name");

        Assert.Equal("New Name", book1.Name);
    }

    [Fact]
    public void ValueTypesAlsoPassByValue()
    {
        var x = GetInt();
        SetInt(x);

        Assert.Equal(3, x);
    }

    [Fact]
    public void StringsBehaveLikeValueTypes()
    {
        string name = "Scott";
        MakeUpperCase(name);
        Assert.Equal("Scott", name);
    }

    private void MakeUpperCase(string name)
    {
        name.ToUpper();
    }

    [Fact]
    public void StringsBehaveLikeRefTypes()
    {
        string name = "Scott";
        var upperCaseName = returnUpperCase(name);
        Assert.Equal("Scott", name);
        Assert.Equal("SCOTT", upperCaseName);
    }

    private string returnUpperCase(string name)
    {
        return name.ToUpper();
    }

    [Fact]
    public void ValueTypesPassByRef()
    {
        var x = GetInt();
        SetInt(ref x);

        Assert.Equal(42, x);
    }

    private void SetInt(ref int x)
    {
        x = 42;
    }

    private void SetInt(int x)
    {
        x = 42;
    }
    private int GetInt()
    {
        return 3;
    }

    private void GetBookSetNameForRef(out Book book, string name)
    {
        book = new Book(name);
    }

    private void GetBookSetName(Book book, string name)
    {
        book = new Book(name);
    }
    private void SetName(Book book, string name)
    {
        book.Name = name;
    }
    Book GetBook(string name)
    {
        return new Book(name);
    }
}