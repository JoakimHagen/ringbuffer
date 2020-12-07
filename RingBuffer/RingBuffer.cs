using System;
using System.Collections;
using System.Collections.Generic;

namespace RingBuffer
{
  /// <summary>
  /// A random-access mutable buffer with fixed length that will wrap around and overlap when written to.
  /// Positive indexing reads from the first item [0] towards the end, negative reads from the last [-1] towards the beginning.
  /// </summary>
  public class RingBuffer<T> : IMutableRingBuffer<T>
  {
    private readonly T[] allocated;
    private int cursor;

    public RingBuffer(int capacity)
    {
      allocated = new T[capacity];
      Clear();
    }

    public int Capacity => allocated.Length;

    public int Count { get; private set; }

    public long TotalCount { get; set; }

    /// <summary>
    /// Supports negative indexing, -1 being the most recent/last item
    /// </summary>
    public T this[int index]
    {
      get => allocated[TranslateIndex(index)];
      set => allocated[TranslateIndex(index)] = value;
    }

    public void Add(T item)
    {
      allocated[cursor] = item;
      if (++cursor >= Capacity) cursor = 0;
      if (Count < Capacity) Count++;
      TotalCount++;
    }

    public int Add(ReadOnlySpan<T> source)
    {
      if (source.Length >= Capacity)
        return ReplaceAll(source);
      else
        return AddSpan(source);
    }

    public T Pop()
    {
      DecrementCount(1);
      cursor = (cursor - 1) % Capacity;

      return this[-1];
    }

    public void Clear()
    {
      cursor = 0;
      Count = 0;
      TotalCount = 0;
    }

    /// <summary>
    /// Gets a read-only span of the most convenient size of contiguous data starting at the index
    /// </summary>
    public ReadOnlySpan<T> ReadSpan(int maxLength, int index = 0)
    {
      if (index < 0)
      {
        index += cursor;
      }
      else
      {
        index += cursor - Count;
      }

      if (index < 0)
      {
        index += Capacity;
      }

      var readStart = index;

      maxLength = Math.Min(Math.Min(Count, Capacity - readStart), maxLength);

      return new ReadOnlySpan<T>(allocated, readStart, maxLength);
    }

    /// <summary>
    /// Removes items at the beginning of the buffer, shifting positive indexes but not negative.
    /// </summary>
    public void DecrementCount(int amount)
    {
      if (Count < amount) throw new InvalidOperationException("The amount is larger than the current Count");
      Count -= amount;
    }

    public int CopyTo(Span<T> target, int sourceIndex = 0)
    {
      var span = ReadSpan(target.Length, sourceIndex);
      span.CopyTo(target);

      sourceIndex += span.Length;
      var remaining = target.Length - span.Length;

      if (remaining > 0 && sourceIndex < Count)
        ReadSpan(remaining, sourceIndex).CopyTo(target.Slice(span.Length));

      return Math.Min(Count, target.Length);
    }

    private int AddSpan(ReadOnlySpan<T> source)
    {
      var sourceLength = source.Length;
      var segmentLength = Capacity - cursor;

      if (segmentLength > 0)
        source.CopyTo(allocated.AsSpan().Slice(cursor, segmentLength));

      if (segmentLength < sourceLength)
        source.Slice(segmentLength).CopyTo(allocated);

      cursor = (cursor + sourceLength) % Capacity;
      Count = Math.Min(Capacity, Count + sourceLength);
      TotalCount += sourceLength;
      return sourceLength;
    }

    private int ReplaceAll(ReadOnlySpan<T> source)
    {
      source = source.Slice(source.Length - Capacity, Capacity);
      source.CopyTo(allocated);

      Count = source.Length;
      TotalCount += source.Length;

      cursor = Count < Capacity ? Count : 0;
      return source.Length;
    }

    private int TranslateIndex(int index)
    {
      if (index < -Count || index >= Count)
        throw new IndexOutOfRangeException($"There are {Count} items in the buffer. Cannot use index {index}");

      else if (index < 0)
      {
        index += Count;
      }
      return ((cursor - Count) % Capacity + index) % Capacity;
    }

    #region IEnumerable
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);
    #endregion

    public sealed class Enumerator : IEnumerator<T>
    {
      internal RingBuffer<T> source;
      private long initTotalCount;
      private int countOffset;

      public Enumerator(RingBuffer<T> source)
      {
        this.source = source;
        initTotalCount = source.TotalCount;
        countOffset = -source.Count;
      }

      public T Current { get; private set; }

      object IEnumerator.Current => Current;

      public bool MoveNext()
      {
        int index = countOffset - (int)(source.TotalCount - initTotalCount);
        if (index == 0) return false;

        Current = source[index];
        countOffset++;
        return true;
      }

      public void Reset()
      {
        Current = default;
        countOffset = 0;
      }

      public void Dispose() { }
    }
  }
}
