using System;
using Xunit;
using RingBuffer;

namespace Tests
{
  public class ContentTests
  {
    protected readonly int[] SAMPLE = new int[] { 40, 41, 42 };

    protected RingBuffer<int> subject = new RingBuffer<int>(4);
    protected int[] truth;

    public ContentTests()
    {
      subject.Add(SAMPLE);
      truth = (int[])SAMPLE.Clone();
    }

    [Fact]
    public void CountIsCorrect() => Assert.Equal(truth.Length, subject.Count);

    [Fact]
    public void CopyToFullLengthIsCorrect() => Assert.Equal(truth.Length, subject.CopyTo(new int[truth.Length]));

    [Fact]
    public void CopyToSmallerLengthIsCorrect() => Assert.Equal(truth.Length - 1, subject.CopyTo(new int[truth.Length - 1]));

    [Fact]
    public void CopyToLargerLengthIsCorrect() => Assert.Equal(truth.Length, subject.CopyTo(new int[truth.Length + 1]));

    [Fact]
    public void ReadSpanIsEqual() => Assert.Equal(truth, subject.ReadSpan(int.MaxValue).ToArray());

    [Fact]
    public void CopyToIsEqual()
    {
      var array = new int[truth.Length];
      subject.CopyTo(array);
      Assert.Equal(truth, array);
    }

    [Fact]
    public void IndexedItemsAreEqual()
    {
      for (int i = 0; i < truth.Length; i++)
      {
        Assert.Equal(truth[i], subject[i]);
      }
    }

    [Fact]
    public void NegativeIndexedItemsAreEqual()
    {
      var n = truth.Length;
      for (int i = 0; i < truth.Length; i++)
      {
        Assert.Equal(truth[i], subject[i - n]);
      }
    }

    [Fact]
    public void IndexOverboundFails() => Assert.Throws<IndexOutOfRangeException>(() => subject[truth.Length]);

    [Fact]
    public void IndexUnderboundFails() => Assert.Throws<IndexOutOfRangeException>(() => subject[-truth.Length - 1]);

    [Fact]
    public void DecrementCountRemovesFirstItem()
    {
      subject.DecrementCount(1);
      Assert.Equal(truth.AsSpan().Slice(1).ToArray(), subject.ReadSpan(int.MaxValue).ToArray());
    }
  }
}
