using System;
using DocumentHierarchy;

class Program
{
    static void Main()
    {
        //создание документа
        Document doc = new Document("Мой документ", "Это пример содержимого документа.");
        Console.WriteLine("Документ:");
        Console.WriteLine(doc.Print());
        Console.WriteLine($"Количество слов: {doc.WordCount()}");
        Console.WriteLine($"Превью 10 символов: {doc.Preview(10)}");
        Console.WriteLine();
        //счоздание отчета
        Report report = new Report("Отчет за год", "Это содержание отчета.", "Иван Иванов");
        Console.WriteLine("Отчет:");
        Console.WriteLine(report.Print());
        Console.WriteLine();

        //создание заметки
        Note note = new Note("Заметка", "Это важная заметка.");
        note.Pin();
        Console.WriteLine("Заметка:");
        Console.WriteLine($"Пинning: {note.Pinned}");
        Console.WriteLine();

        // создание stickynote
        StickyNote sticky = new StickyNote("Цветная заметка", "Это заметка с цветом.", "Красный");
        Console.WriteLine("StickyNote:");
        Console.WriteLine(sticky.Print());
        sticky.Recolor("Зеленый");
        Console.WriteLine("После перекраски:");
        Console.WriteLine(sticky.Print());
    }
}