public class BIOS : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
            if (Path.GetFileName(file).ToLower().Contains("bios"))
                yield return file;
    }
}
