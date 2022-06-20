public class Revision : IFilter
{
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        var group = files.GroupBy(x => Sanitizer.GetPureGameName(Path.GetFileNameWithoutExtension(x)));
        var @return = new List<string>();

        foreach (var items in group)
        {
            if (items.Count() > 1)
            {
                var maxRevision = items.Max(y => GetRevision(Path.GetFileNameWithoutExtension(y)));
                var ignored = items.Where(x => GetRevision(Path.GetFileNameWithoutExtension(x)) != maxRevision);
                @return.AddRange(ignored);
            }
        }

        return @return;
    }

    private int GetRevision(string fileNameWithoutExtension)
    {
        if (!fileNameWithoutExtension.ToLower().Contains("(rev "))
            return 1;

        var index = fileNameWithoutExtension.ToLower().IndexOf("(rev ");
        char revision = fileNameWithoutExtension[index + 5];

        if (char.IsLetter(revision))
            return char.ToUpper(revision) - 65;

        if (char.IsNumber(revision))
            return Convert.ToInt32(revision.ToString());

        return 1;
    }
}