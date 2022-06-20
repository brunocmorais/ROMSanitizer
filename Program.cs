public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var cli = new CLI();
            cli.Execute(args);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Fatal: " + ex.Message);
            Environment.Exit(1);
        }

        Environment.Exit(0);
    }
}