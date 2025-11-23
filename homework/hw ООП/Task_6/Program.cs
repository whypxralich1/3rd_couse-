class Program
{
    static void Main()
    {
        // enum + switch
        int cost1 = PrintCostCalculatorEnum.CalculateCost(DocumentType.Report, true, false, true);
        Console.WriteLine($"Cost (enum + switch): {cost1}");

        // ООП
        var context = new PrintContext(isColor: true, isDuplex: false, hasBulkDiscount: true);
        var document = new Report();

        var calculator = new PrintCostCalculatorOop(document, context);
        int cost2 = calculator.Calculate();
        Console.WriteLine($"Cost (ООП): {cost2}");
    }
}