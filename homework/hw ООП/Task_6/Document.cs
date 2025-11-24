public abstract class Document
{
    private readonly int baseCost;

    public int BaseCost => baseCost;

    protected Document(int baseCost)
    {
        this.baseCost = baseCost;
    }

    public virtual int GetPrintCost(PrintContext context)
    {
        int cost = BaseCost;

        if (context.IsColor)
        {
            cost += 2;
        }

        if (context.IsDuplex)
        {
            int discount = (int)Math.Floor(cost * 0.9);
            cost = Math.Max(discount, 1);
        }

        if (context.HasBulkDiscount)
        {
            cost = Math.Max(cost - 1, 1);
        }

        return cost;
    }
}

public class Text : Document
{
    public Text() : base(1) { }
}

public class Report : Document
{
    public Report() : base(2) { }
}

public class Photo : Document
{
    public Photo() : base(5) { }
}

public class Poster : Document
{
    public Poster() : base(8) { }
}

public class InternalMemo : Document
{
    public InternalMemo() : base(1) { }

    public override int GetPrintCost(PrintContext context)
    {
        return BaseCost;
    }
}