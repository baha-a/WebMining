namespace WebMining
{
    public interface IDistancable
    {
        double Distance(IDistancable t);
        string ToString();
    }
}