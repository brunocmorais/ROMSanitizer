public class Sanitizer
{
    private readonly string path;
    private readonly string[] whitelist;
    private readonly string[] ignoredExtensions = new [] { ".png", ".jpg", ".wav", ".mp4" };
    private IList<IFilter> filters = new List<IFilter>();

    public Sanitizer(string path, params string[] whitelist)
    {
        this.path = path;
        this.whitelist = whitelist;

        AddFilter<Alternate>();
        AddFilter<BIOS>();
        AddFilter<DeJap>();
        AddFilter<Extensions>();
        AddFilter<GamePack>();
        AddFilter<Hack>();
        AddFilter<PreRelease>();
        AddFilter<Region>();
        AddFilter<Revision>();
        AddFilter<Unlicensed>();
    }

    void AddFilter<T>() where T : IFilter, new()
    {
        filters.Add(new T());
    }

    public IEnumerable<string> GetFiles()
    {
        var @return = new List<string>();
        var directories = Directory.GetDirectories(path);
            
        directories.Append(path);

        foreach (var directory in directories)
        {
            var files = Directory.GetFiles(directory)
                .Where(x => !ignoredExtensions.Contains(Path.GetExtension(x)));
            
            foreach (var filter in filters)
            {
                var filteredFiles = filter.GetFiles(files);

                foreach (var file in filteredFiles)
                {
                    if (!@return.Contains(file))
                        @return.Add(file);
                }
            }
        }

        return FilterWhitelist(@return).Distinct().OrderBy(x => x);
    }

    public static string GetPureGameName(string fileNameWithoutExtension)
    {
        if (fileNameWithoutExtension.Contains("("))
            return fileNameWithoutExtension.Substring(0, fileNameWithoutExtension.IndexOf("(")).Trim();

        return fileNameWithoutExtension.Trim();
    }

    private IEnumerable<string> FilterWhitelist(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            var gameName = GetPureGameName(Path.GetFileNameWithoutExtension(file));

            if (whitelist.Any(x => gameName.ToLower().Contains(x)))
                continue;

            yield return file;
        }
    }
}