using System;

namespace Task2;

class Program
{
    static void Main()
    {
        Person ilya = new Person("Илья Котлов", 20);
        Person pavel = new Person("Павел Бураков", 20);
        
        Phone iphoneIvan = new Phone("iPhone 11", "MTC", ilya);
        Phone samsungAlex = new Phone("Samsung Galaxy S20", "TELE 2", pavel);
        
        ilya.AddPhone(iphoneIvan);
        pavel.AddPhone(samsungAlex);
        PhoneBook book = new PhoneBook();
        book.AddContact(ilya);
        book.AddContact(pavel);
        
        Console.WriteLine(book.ToString());
    }
}