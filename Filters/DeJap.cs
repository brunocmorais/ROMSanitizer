public class DeJap : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file).ToLower();

            if (fileName.Contains("(j)") || fileName.Contains("(japan)"))
                yield return file;
        }
    }
}
