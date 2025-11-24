public class PrintCostCalculatorOop
{
    private readonly Document document;
    private readonly PrintContext context;

    public PrintCostCalculatorOop(Document document, PrintContext context)
    {
        this.document = document;
        this.context = context;
    }

    public int Calculate()
    {
        return document.GetPrintCost(context);
    }
}