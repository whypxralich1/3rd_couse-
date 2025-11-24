public class PrintContext
{
    public bool IsColor { get; }
    public bool IsDuplex { get; }
    public bool HasBulkDiscount { get; }

    public PrintContext(bool isColor, bool isDuplex, bool hasBulkDiscount)
    {
        IsColor = isColor;
        IsDuplex = isDuplex;
        HasBulkDiscount = hasBulkDiscount;
    }
}