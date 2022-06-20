public class Extensions : IFilter
{
    private string[] unwanted = new [] { ".srm", ".sav" }; 
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
            if (unwanted.Contains(Path.GetExtension(file).ToLower()))
                yield return file;
    }
}
