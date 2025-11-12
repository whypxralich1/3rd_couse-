namespace DocumentHierarchy
{
    public class StickyNote : Note
    {
        private string _color;

        public string Color
        {
            get => _color;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Color не может быть пустым");
                _color = value;
            }
        }

        public StickyNote(string title, string content, string color)
            : base(title, content)
        {
            Color = color;
        }

        public void Recolor(string c)
        {
            Color = c;
        }

        public override string Print()
        {
            return $"[{Color}] " + base.Print();
        }
    }
}