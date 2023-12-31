﻿

public class DefaultDictionary<TKey, TValue> :IEnumerable<KeyValuePair<TKey, TValue>>
{

    public bool ContainsKey(TKey index)
    {
        return entries.ContainsKey(index);
    }

    public Dictionary<TKey, TValue>.ValueCollection Values => entries.Values;

    public Dictionary<TKey, TValue>.KeyCollection Keys => entries.Keys;


    public TValue this[TKey index]
    {
        get
        {
            TValue value = default(TValue);
            entries.TryGetValue(index, out value);
            return value;
        }
        set
        {
            if (value.Equals(default(TValue)))
            {
                entries.Remove(index);
            }
            else
            {
                entries[index] = value;
            }
        }
    }

    public int Count => entries.Count;

    public DefaultDictionary<TKey, TValue> Clone()
    {
        var dict = new DefaultDictionary<TKey, TValue>();

        dict.entries = entries.ToDictionary(x => x.Key, x=>x.Value );
        return dict;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<TKey, TValue>>)entries).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)entries).GetEnumerator();
    }

    private Dictionary<TKey, TValue> entries = new Dictionary<TKey, TValue>();
}