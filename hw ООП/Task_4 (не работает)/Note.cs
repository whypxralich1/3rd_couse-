namespace DocumentHierarchy
{
    public class Note : Document
    {
        private bool _pinned;

        public bool Pinned
        {
            get => _pinned;
            private set => _pinned = value;
        }

        public Note(string title, string content)
            : base(title, content)
        {
            _pinned = false;
        }

        public void Pin()
        {
            Pinned = true;
        }
    }
}