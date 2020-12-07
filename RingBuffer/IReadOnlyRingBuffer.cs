using System;
using System.Collections.Generic;

namespace RingBuffer
{
  public interface IReadOnlyRingBuffer<T> : IEnumerable<T>
  {
    int Capacity { get; }

    int Count { get; }

    long TotalCount { get; }

    T this[int index] { get; }

    ReadOnlySpan<T> ReadSpan(int index, int maxLength);

    int CopyTo(Span<T> target, int sourceIndex);
  }
}
