using System;

namespace RingBuffer
{
  public interface IMutableRingBuffer<T> : IReadOnlyRingBuffer<T>
  {
    new T this[int index] { get; set; }

    void Add(T item);

    int Add(ReadOnlySpan<T> source);

    void Clear();

    void DecrementCount(int amount);
  }
}
