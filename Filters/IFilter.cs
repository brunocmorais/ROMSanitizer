
public interface IFilter
{
    IEnumerable<string> GetFiles(IEnumerable<string> files);
}