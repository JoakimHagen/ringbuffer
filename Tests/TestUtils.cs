using System;
using System.Collections.Generic;
using RingBuffer;

namespace Tests
{
  public static class TestUtils
  {
    public static readonly int[] SHORT = new int[] { 40, 41 };
    public static readonly int[] FULL = new int[] { 40, 41, 42 };
    public static readonly int[] OVERFLOW = new int[] { 40, 41, 42, 43 };

    // set internal cursor at different places when testing
    public static IEnumerable<object[]> FuzzBufferStates(ReadOnlyMemory<int> content)
    {
      var buffer = new RingBuffer<int>(3);
      AddIndividualItems(buffer, content);
      yield return new object[] { buffer };


      buffer = new RingBuffer<int>(3);
      buffer.Add(0);
      buffer.DecrementCount(1);
      AddIndividualItems(buffer, content);
      yield return new object[] { buffer };

      buffer = new RingBuffer<int>(3);
      buffer.Add(0);
      buffer.Add(0);
      buffer.DecrementCount(2);
      AddIndividualItems(buffer, content);
      yield return new object[] { buffer };
    }

    // Adding content items individually to avoid internal optimizing
    public static void AddIndividualItems(RingBuffer<int> buffer, ReadOnlyMemory<int> content)
    {
      foreach (var item in content.Span)
      {
        buffer.Add(item);
      }
    }
  }
}
