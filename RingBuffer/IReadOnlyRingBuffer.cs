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

    ReadOnlySpan<T> ReadSpan(int maxLength, int index = 0);

    int CopyTo(Span<T> target, int sourceIndex = 0);
  }
}
