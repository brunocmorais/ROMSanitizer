public class Unlicensed : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            if (Path.GetFileName(file).ToLower().Contains("(unl)"))
                yield return file;
        }
    }
}
