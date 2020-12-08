using Xunit;
using RingBuffer;
using Tests;

namespace Methods
{
  public class CopyTo
  {
    private int[] array = new int[] { 0, 0, 0, 0 };

    [Theory]
    [ClassData(typeof(EmptyRingBuffers))]
    public void EmptyCopyTo(RingBuffer<int> buffer)
    {
      var copyLength = buffer.CopyTo(array);

      Assert.Equal(new int[] { 0, 0, 0, 0 }, array);
      Assert.Equal(0, copyLength);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    [ClassData(typeof(OverfilledRingBuffers))]
    public void FullCopyTo(RingBuffer<int> buffer)
    {
      var copyLength = buffer.CopyTo(array);

      Assert.Equal(new int[] { 1, 2, 3, 0 }, array);
      Assert.Equal(3, copyLength);
    }

    [Theory]
    [ClassData(typeof(NotFullRingBuffers))]
    public void NotFullCopyTo(RingBuffer<int> buffer)
    {
      var copyLength = buffer.CopyTo(array);

      Assert.Equal(new int[] { 1, 3, 0, 0 }, array);
      Assert.Equal(2, copyLength);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void CopyToTruncated(RingBuffer<int> buffer)
    {
      array = new int[2];

      var copyLength = buffer.CopyTo(array);

      Assert.Equal(new int[] { 1, 2 }, array);
      Assert.Equal(2, copyLength);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void CopyToUsingIndex(RingBuffer<int> buffer)
    {
      var copyLength = buffer.CopyTo(array, 1);

      Assert.Equal(new int[] { 2, 3, 0, 0 }, array);
      Assert.Equal(2, copyLength);
    }

    [Theory]
    [ClassData(typeof(FullRingBuffers))]
    public void CopyToUsingNegativeIndex(RingBuffer<int> buffer)
    {
      var copyLength = buffer.CopyTo(array, -2);

      Assert.Equal(new int[] { 2, 3, 0, 0 }, array);
      Assert.Equal(2, copyLength);
    }
  }
}
