namespace DocumentHierarchy
{
    public class Report : Document
    {
        private string _author;

        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author не может быть пустым");
                _author = value;
            }
        }

        public Report(string title, string content, string author)
            : base(title, content)
        {
            Author = author;
        }

        public string Header()
        {
            return $"Author: {Author}";
        }

        public override string Print()
        {
            return Header() + "\n" + base.Print();
        }
    }
}