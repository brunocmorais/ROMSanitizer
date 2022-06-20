public class CLI
{
    private void PrintHelp()
    {
        Console.WriteLine("Use: ROMSanitizer [OPTIONS] -path [path] [-whitelist [terms]]\n");
        Console.WriteLine("-path: Main path to ROM files");
        Console.WriteLine("-whitelist: terms that must be ignored in criteria");
        Console.WriteLine("-print0: print NUL character instead of EOL during output");
        Console.WriteLine("-h or --help: show this help");
    }

    public void Execute(string[] args)
    {
        if (args.Any(x => x == "-h" || x == "--help"))
        {
            PrintHelp();
            return;
        }

        if (!args.Any(x => x == "-path"))
            throw new Exception("Argument -path is mandatory!");

        string path = args[Array.IndexOf(args, "-path") + 1];

        int whitelistStart = Array.IndexOf(args, "-whitelist");
        string[] whitelist;

        if (whitelistStart != -1)
            whitelist = args.Skip(whitelistStart + 1).ToArray();
        else
            whitelist = new string[0];

        var sanitizer = new Sanitizer(path, whitelist);
        var files = sanitizer.GetFiles();

        char eol = args.Any(x => x == "-print0") ? (char) 0 : (char) 0x0A;

        foreach (var file in files)
            Console.Write(file + eol);
    }
}