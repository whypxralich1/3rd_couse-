using System.Collections.Generic;

namespace Task2;

public class PhoneBook
{
    public IList<Person> Contacts {get; set;}

    public PhoneBook()
    {
        Contacts = new List<Person>();
    }

    public void AddContact(Person person)
    {
        if (!Contacts.Contains(person)) Contacts.Add(person);
    }

    public bool RemoveContact(Person person)
    {
        return Contacts.Remove(person);
    }

    public string GetDescription()
    {
        var result = "";
        foreach (var contact in Contacts)
        {
            result += $"{contact}\n";
        }
        return result.Trim();
    }

    public override string ToString()
    {
        return GetDescription();
    }
}