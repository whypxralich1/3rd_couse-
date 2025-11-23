public class Item
{
    public string Id { get; }
    public string Name { get; }
    public Rarity Rarity { get; }

    private readonly List<Effect> _effects = new List<Effect>();
    public IReadOnlyList<Effect> Effects => _effects.AsReadOnly();

    public Item(string id, string name, Rarity rarity, IEnumerable<Effect> effects = null)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id cannot be null or whitespace.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        Id = id;
        Name = name;
        Rarity = rarity;
        if (effects != null)
        {
            foreach (var e in effects)
            {
                AddEffect(e);
            }
        }
    }
    internal void AddEffect(Effect effect)
    {
        if (effect == null)
            throw new ArgumentNullException(nameof(effect));
        _effects.Add(effect);
    }
}