using System;
using Xunit;
using RingBuffer;

namespace RingBufferTests
{
  public class EmptyTests
  {
    protected readonly int[] SAMPLE = new int[] { 40, 41, 42 };

    protected RingBuffer<int> subject = new RingBuffer<int>(4);
    protected int capacity = 4;

    [Fact]
    public void CapacityIsCorrect() => Assert.Equal(capacity, subject.Capacity);

    [Fact]
    public void CountIsZero() => Assert.Equal(0, subject.Count);

    [Fact]
    public void IndexGetFails() => Assert.Throws<IndexOutOfRangeException>(() => subject[0]);

    [Fact]
    public void IndexSetFails() => Assert.Throws<IndexOutOfRangeException>(() => subject[0] = 42);

    [Fact]
    public void DecrementCountFails() => Assert.Throws<InvalidOperationException>(() => subject.DecrementCount(1));

    [Fact]
    public void ReadSpanIsEmpty() => Assert.Empty(subject.ReadSpan(int.MaxValue).ToArray());

    [Fact]
    public void CopyToIsUnmodified()
    {
      var targetArray = (int[])SAMPLE.Clone();
      subject.CopyTo(targetArray);
      Assert.Equal(SAMPLE, targetArray);
    }

    [Fact]
    public void CopyToLengthIsZero() => Assert.Equal(0, subject.CopyTo(SAMPLE));
  }
}
