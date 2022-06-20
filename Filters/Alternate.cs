public class Alternate : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file).ToLower();

            if (fileName.Contains("(alt "))
                yield return file;
        }
    }
}
