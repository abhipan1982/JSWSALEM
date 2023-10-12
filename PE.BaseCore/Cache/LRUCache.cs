using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PE.Core.Cache
{
  public class LRUCache<K, V>
  {
    private readonly Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>> _cacheMap =
      new Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>>();

    private readonly int _capacity;
    private readonly LinkedList<LRUCacheItem<K, V>> _lruList = new LinkedList<LRUCacheItem<K, V>>();

    public LRUCache(int capacity)
    {
      this._capacity = capacity;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public V Get(K key)
    {
      LinkedListNode<LRUCacheItem<K, V>> node;
      if (_cacheMap.TryGetValue(key, out node))
      {
        V value = node.Value.value;
        _lruList.Remove(node);
        _lruList.AddLast(node);
        return value;
      }

      return default;
    }

    public bool Contains(K key)
    {
      return _cacheMap.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(K key, V val)
    {
      if (_cacheMap.Count >= _capacity)
      {
        RemoveFirst();
      }

      LRUCacheItem<K, V> cacheItem = new LRUCacheItem<K, V>(key, val);
      LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(cacheItem);
      _lruList.AddLast(node);
      _cacheMap.Add(key, node);
    }

    private void RemoveFirst()
    {
      // Remove from LRUPriority
      LinkedListNode<LRUCacheItem<K, V>> node = _lruList.First;
      _lruList.RemoveFirst();

      // Remove from cache
      _cacheMap.Remove(node.Value.key);
    }
  }

  internal class LRUCacheItem<K, V>
  {
    public K key;
    public V value;

    public LRUCacheItem(K k, V v)
    {
      key = k;
      value = v;
    }
  }
}
