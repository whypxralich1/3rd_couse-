using System;

class Program
{
    static void Main()
    {
        var pipeline = new OrderPipeline();
        OrderStep checkStock = CheckStock;
        OrderStep calculateDelivery = CalculateDelivery;
        OrderStep sendConfirmation = (ctx) => ctx.IsConfirmed = true;

        pipeline.OrderStep += checkStock;
        pipeline.OrderStep += calculateDelivery;
        pipeline.OrderStep += sendConfirmation;
        var ctx = new OrderContext();

        pipeline.Run(ctx);

        Console.WriteLine($"HasStock: {ctx.HasStock}");
        Console.WriteLine($"DeliveryCost: {ctx.DeliveryCost}");
        Console.WriteLine($"IsConfirmed: {ctx.IsConfirmed}");

        pipeline.OrderStep -= checkStock;
        pipeline.OrderStep -= calculateDelivery;
        pipeline.OrderStep -= sendConfirmation;
    }

    static void CheckStock(OrderContext ctx)
    {
        ctx.HasStock = true;
    }

    static void CalculateDelivery(OrderContext ctx)
    {
        ctx.DeliveryCost = 200;
    }
}