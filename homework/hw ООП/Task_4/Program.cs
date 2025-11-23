using System;

class Program
{
    static void Main()
    {
        Document doc = new Document("Документ", "Этот документ содержит основные сведения о проекте, его целях, задачах и ожидаемых результатах.");
        Console.WriteLine(doc.Print());
        Console.WriteLine($"Количество слов: {doc.WordCount()}");
        Console.WriteLine($"Превью (10 символов): {doc.Preview(10)}");

        Report report = new Report("Ежегодный доклад", "Содержание:", "Иван Петров");
        Console.WriteLine(report.Print());

        Note note = new Note("Заметка о встрече", "Обсуждение проекта");
        note.Pin();
        Console.WriteLine(note.Print()); 

        StickyNote sticky = new StickyNote("Напоминалка", "Сходить в магазин.", "Красный");
        Console.WriteLine(sticky.Print());
        sticky.Recolor("Зеленый");
        Console.WriteLine(sticky.Print());
    }
}