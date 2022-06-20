public class GamePack : IFilter
{
    private string[] unwanted = new [] { "2 game", "3 game", "4 game" };
    public IEnumerable<string> GetFiles(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            
            if (unwanted.Any(x => fileName.ToLower().Contains(x)))
                yield return file;
        }
    }
}