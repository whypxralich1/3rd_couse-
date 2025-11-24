using System.Collections.Generic;

namespace Task2;

public class Person
{
    public string FullName {get; init;}
    public int Age {get; set;}
    public ICollection<Phone> Phones {get; set;}

    public Person(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
        Phones = new List<Phone>();
    }

    public void AddPhone(Phone phone)
    {
        if (!Phones.Contains(phone)) Phones.Add(phone);
    }

    public bool RemovePhone(Phone phone)
    {
        return Phones.Remove(phone);
    }

    public override string ToString()
    {
        var phonesList = string.Join(", ", Phones);
        return $"{FullName}, {Age}, Телефон: {phonesList}";
    }
}