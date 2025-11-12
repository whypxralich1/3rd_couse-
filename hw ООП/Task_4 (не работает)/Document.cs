namespace DocumentHierarchy
{
    public class Document
    {
        private string _title;
        private string _content;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title не может быть пустым");
                _title = value;
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value ?? "";
            }
        }

        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public int WordCount()
        {
            if (string.IsNullOrWhiteSpace(Content))
                return 0;
            return Content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public string Preview(int chars)
        {
            if (chars < 0)
                throw new ArgumentException("Chars не может быть отрицательным");
            if (string.IsNullOrEmpty(Content))
                return "";
            return Content.Length <= chars ? Content : Content.Substring(0, chars);
        }

        public virtual string Print()
        {
            return $"{Title}\n{Content}";
        }
    }
}