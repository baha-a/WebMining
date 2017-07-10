namespace WebMining
{
    public interface IMeasurable
    {
        double Distance(IMeasurable t);
        string ToString();
    }
}