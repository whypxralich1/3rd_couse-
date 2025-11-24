public interface IScalable
{
    double Scale { get; set; }
    void Resize(double factor);
}