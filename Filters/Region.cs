public class Region : IFilter
{
    enum GameRegion
    {
        World,
        USA,
        Europe,
        Japan,
        Brazil,
        Unknown
    }

    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        var group = files.GroupBy(x => Sanitizer.GetPureGameName(Path.GetFileNameWithoutExtension(x)));
        var @return = new List<string>();

        foreach (var items in group)
        {
            if (items.Count() > 1)
            {
                var keys = items.Select(x => new KeyValuePair<string, GameRegion>(x, GetRegion(x)));
                var precedence = new [] { GameRegion.World, GameRegion.USA, GameRegion.Europe, GameRegion.Brazil, GameRegion.Japan, GameRegion.Unknown };

                foreach (var item in precedence)
                {
                    if (keys.Any(x => x.Value == item))
                    {
                        @return.AddRange(keys.Where(x => x.Value != item).Select(x => x.Key));
                        break;
                    }
                }
            }
        }

        return @return;
    }

    private GameRegion GetRegion(string file)
    {
        var fileName = Path.GetFileNameWithoutExtension(file);
        var split = fileName.Split(' ');

        for (int i = split.Length - 1; i >= 0; i--)
        {
            var item = split[i].ToLower();

            if (item.Contains("("))
            {
                if (item.Contains("world"))
                    return GameRegion.World;
                if (item.Contains("usa") || item == "(u)")
                    return GameRegion.USA;
                if (item.Contains("europe") || item.Contains("(e)") || item.Contains("en"))
                    return GameRegion.Europe;
                if (item.Contains("japan") || item.Contains("(j)"))
                    return GameRegion.Japan;
                if (item.Contains("brazil"))
                    return GameRegion.Brazil;
            }
        }

        return GameRegion.Unknown;
    }
}