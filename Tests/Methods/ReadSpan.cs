using System;
using Tests;
using Xunit;
using RingBuffer;

namespace Methods
{
  public class ReadSpan
  {
    [Theory]
    [ClassData(typeof(AllRingBuffers))]
    public void ReadSpanFitsMaxLength(RingBuffer<int> buffer)
    {
      var span = buffer.ReadSpan(2);

      Assert.True(span.Length <= 2);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void ReadSpanFirstItemIsCorrect(RingBuffer<int> buffer)
    {
      var span = buffer.ReadSpan(int.MaxValue);

      Assert.Equal(buffer[0], span[0]);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void ReadSpanIndexing(RingBuffer<int> buffer)
    {
      var span = buffer.ReadSpan(int.MaxValue, 2);

      Assert.Equal(buffer[2], span[0]);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void ReadSpanNegativeIndexing(RingBuffer<int> buffer)
    {
      var span = buffer.ReadSpan(int.MaxValue, -1);

      Assert.Equal(buffer[-1], span[0]);
    }

    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    public void ReadSpanEmpty(RingBuffer<int> buffer)
    {
      var span = buffer.ReadSpan(int.MaxValue);

      Assert.Equal(0, span.Length);
    }
  }
}
