public class Animal
{
    public string Name { get; private set; }

    public Animal(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        }
        Name = name;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Animal animal = new Animal("");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Final block");
        }
    }   
}
