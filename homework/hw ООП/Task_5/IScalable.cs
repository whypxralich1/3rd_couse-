public interface IScalable
{
    double Scale { get; set; }
    void Resize(double factor);

    void IScalable.Resize(double factor)
{
    this.Scale *= factor;
    if (this.Scale < 0)
        this.Scale = 0;
}
}

