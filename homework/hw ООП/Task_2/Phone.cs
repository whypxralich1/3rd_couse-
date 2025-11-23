namespace Task2;

public class Phone
{
    public string Model {get; init;}
    public string Operator {get; init;}
    public Person Owner {get; init;}

    public Phone(string model, string @operator, Person owner)
    {
        Model = model;
        Operator = @operator;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"{Model} ({Operator})";
    }
}