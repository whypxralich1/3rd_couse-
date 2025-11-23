using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory : IEnumerable<Item>
{
    private readonly List<Item> _items = new List<Item>();
    private readonly Dictionary<string, Item> _byId = new Dictionary<string, Item>();

    public int Count => _items.Count;

    public Item this[int index]
    {
        get
        {
            if (index < 0 || index >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _items[index];
        }
    }

    public Item this[string id]
    {
        get
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            if (!_byId.TryGetValue(id, out var item))
                throw new KeyNotFoundException($"Item with Id '{id}' not found.");
            return item;
        }
    }
    
    public void Add(Item item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        if (_byId.ContainsKey(item.Id))
            throw new ArgumentException($"An item with Id '{item.Id}' already exists.");
        _items.Add(item);
        _byId[item.Id] = item;
    }

    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= _items.Count)
            return false;
        var item = _items[index];
        _items.RemoveAt(index);
        _byId.Remove(item.Id);
        return true;
    }

    public bool RemoveById(string id)
    {
        if (id == null)
            return false;
        if (_byId.TryGetValue(id, out var item))
        {
            _byId.Remove(id);
            _items.Remove(item);
            return true;
        }
        return false;
    }

    public IEnumerable<Item> EnumerateByRarity(Rarity minRarity)
    {
        foreach (var item in _items)
        {
            if (item.Rarity >= minRarity)
                yield return item;
        }
    }

    public IEnumerator<Item> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}