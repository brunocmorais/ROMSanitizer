public class PreRelease : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            if (Path.GetFileName(file).ToLower().Contains("(alpha") || 
                Path.GetFileName(file).ToLower().Contains("(beta") || 
                Path.GetFileName(file).ToLower().Contains("(debug"))
                yield return file;
        }
    }
}
