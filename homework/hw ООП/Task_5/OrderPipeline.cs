using System;

public class OrderPipeline
{
    public OrderStep OrderStep;

    public void Run(OrderContext ctx)
    {
        OrderStep?.Invoke(ctx);
    }
}