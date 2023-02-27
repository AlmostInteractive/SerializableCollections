using System;
using System.Collections;
using System.Collections.Generic;
using YamaGames.SerializableCollections;
using UnityEngine;

[Serializable]
public class SerializableSortedSet<T>
    : ISerializationCallbackReceiver
        , ISet<T>
        , IReadOnlyCollection<T>
        , ISingleFieldDrawable {
    [SerializeField] private List<T> SerializableEntries = new List<T>();
    private SortedSet<T> _hashSet = new SortedSet<T>();


    #region Constructors

    // empty constructor required for Unity serialization
    public SerializableSortedSet() { }

    public SerializableSortedSet(IEnumerable<T> collection) { _hashSet = new SortedSet<T>(collection); }

    #endregion Constructors


    #region Interface forwarding to the _hashset

    public int Count => _hashSet.Count;
    public bool IsReadOnly => false;
    public bool Remove(T item) { return _hashSet.Remove(item); }
    public void ExceptWith(IEnumerable<T> other) => _hashSet.ExceptWith(other);
    public void IntersectWith(IEnumerable<T> other) => _hashSet.IntersectWith(other);
    public bool IsProperSubsetOf(IEnumerable<T> other) => _hashSet.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => _hashSet.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => _hashSet.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => _hashSet.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => _hashSet.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => _hashSet.SetEquals(other);
    public void SymmetricExceptWith(IEnumerable<T> other) => _hashSet.SymmetricExceptWith(other);
    public void UnionWith(IEnumerable<T> other) => _hashSet.UnionWith(other);
    public void Clear() => _hashSet.Clear();
    public bool Contains(T item) => _hashSet.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => _hashSet.CopyTo(array, arrayIndex);
    public bool Add(T item) { return _hashSet.Add(item); }
    void ICollection<T>.Add(T item) { _hashSet.Add(item); }
    public IEnumerator<T> GetEnumerator() => _hashSet.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion Interface forwarding to the _hashset


    public void OnBeforeSerialize() {
        var cur = new HashSet<T>(SerializableEntries);

        foreach (var val in this) {
            if (!cur.Contains(val)) {
                SerializableEntries.Add(val);
            }
        }
    }

    public void OnAfterDeserialize() {
        Clear();

        foreach (var val in SerializableEntries) {
            Add(val);
        }
    }
}