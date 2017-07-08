namespace WebMining
{
    public class Pair<T>
    {
        public double Weight { get; set; }
        public T Value { get; set; }

        public Pair(double weight, T value)
        {
            Weight = weight;
            Value = value;
        }
    }
}
