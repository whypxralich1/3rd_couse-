public enum DocumentType
{
    Text, Report, Photo, Poster, InternalMemo
}

public class PrintCostCalculatorEnum
{
    public static int CalculateCost(DocumentType type, bool isColor, bool isDuplex, bool hasBulkDiscount)
    {
        int baseCost = 0;

        switch (type)
        {
            case DocumentType.Text:
                baseCost = 1;
                break;
            case DocumentType.Report:
                baseCost = 2;
                break;
            case DocumentType.Photo:
                baseCost = 5;
                break;
            case DocumentType.Poster:
                baseCost = 8;
                break;
            case DocumentType.InternalMemo:
                baseCost = 1;
                break;
            default:
                throw new ArgumentException("Неизвестный тип документа");
        }

        if (isColor)
            baseCost += 2;

        if (isDuplex)
        {
            int discount = (int)Math.Floor(baseCost * 0.9);
            baseCost = Math.Max(discount, 1);
        }

        if (hasBulkDiscount)
        {
            baseCost = Math.Max(baseCost - 1, 1);
        }

        return baseCost;
    }
}