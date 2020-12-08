using System;
using System.Collections;
using System.Collections.Generic;
using RingBuffer;

namespace Tests
{
  public class EmptyRingBuffers : IEnumerable<object[]>
  {
    protected virtual int Capacity => 3;
    protected virtual int[] Content => new int[0];

    // Fuzzing different cursor states
    public IEnumerator<object[]> GetEnumerator()
    {
      var buffer = new RingBuffer<int>(Capacity);
      AddIndividualItems(buffer, Content);
      yield return new object[] { buffer };

      buffer = new RingBuffer<int>(Capacity);
      buffer.Add(99);
      buffer.DecrementCount(1);
      AddIndividualItems(buffer, Content);
      yield return new object[] { buffer };

      buffer = new RingBuffer<int>(Capacity);
      buffer.Add(99);
      buffer.Add(99);
      buffer.DecrementCount(2);
      AddIndividualItems(buffer, Content);
      yield return new object[] { buffer };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    // Adding content items individually to avoid internal optimizing
    public static void AddIndividualItems(RingBuffer<int> buffer, ReadOnlyMemory<int> content)
    {
      foreach (var item in content.Span)
      {
        buffer.Add(item);
      }
    }
  }

  public class FullRingBuffers : EmptyRingBuffers
  {
    protected override int[] Content => new int[] { 1, 2, 3 };
  }

  public class OverfilledRingBuffers : EmptyRingBuffers
  {
    protected override int[] Content => new int[] { 99, 1, 2, 3 };
  }

  public class NotFullRingBuffers : EmptyRingBuffers
  {
    protected override int[] Content => new int[] { 1, 3 };
  }

  public class AllRingBuffers : IEnumerable<object[]>
  {
    public IEnumerator<object[]> GetEnumerator()
    {
      foreach (var item in new EmptyRingBuffers())
        yield return item;
      foreach (var item in new NotFullRingBuffers())
        yield return item;
      foreach (var item in new FullRingBuffers())
        yield return item;
      foreach (var item in new OverfilledRingBuffers())
        yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
