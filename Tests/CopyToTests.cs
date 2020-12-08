using System.Collections.Generic;
using Xunit;
using RingBuffer;

namespace Tests
{
  public class CopyToTests
  {
    protected RingBuffer<int> subject = new RingBuffer<int>(3);

    [Theory]
    [MemberData(nameof(FullBuffers))]
    public void FullCopyTo(RingBuffer<int> buffer)
    {
      var array = new int[4];
      buffer.CopyTo(array);
      Assert.Equal(new int[] { 40, 41, 42, 0 }, array);
    }

    [Theory]
    [MemberData(nameof(ShortBuffers))]
    public void ShortCopyTo(RingBuffer<int> buffer)
    {
      var array = new int[3];
      buffer.CopyTo(array);
      Assert.Equal(new int[] { 40, 41, 0 }, array);
    }

    [Theory]
    [MemberData(nameof(OverflowedBuffers))]
    public void OverflowCopyTo(RingBuffer<int> buffer)
    {
      var array = new int[4];
      buffer.CopyTo(array);
      Assert.Equal(new int[] { 41, 42, 43, 0 }, array);
    }

    [Theory]
    [MemberData(nameof(FullBuffers))]
    public void CopyToTruncated(RingBuffer<int> buffer)
    {
      var array = new int[2];
      buffer.CopyTo(array);
      Assert.Equal(new int[] { 40, 41 }, array);
    }

    public static IEnumerable<object[]> ShortBuffers => TestUtils.FuzzBufferStates(TestUtils.SHORT);

    public static IEnumerable<object[]> FullBuffers => TestUtils.FuzzBufferStates(TestUtils.FULL);

    public static IEnumerable<object[]> OverflowedBuffers => TestUtils.FuzzBufferStates(TestUtils.OVERFLOW);
  }
}
