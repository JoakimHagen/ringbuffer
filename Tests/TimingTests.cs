using Xunit;
using RingBuffer;

namespace Tests
{
  public class TimingTests
  {
    protected readonly int[] SAMPLE = new int[] { 40, 41, 42 };

    protected int[] targetArray = new int[3];

    protected RingBuffer<int> subject = new RingBuffer<int>(4);

    protected int item = 41;

    public TimingTests()
    {
      subject.Add(SAMPLE);
    }

    [Fact]
    public void AddSingle()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        subject.Add(item);
      }
    }

    [Fact]
    public void AddSpan()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        subject.Add(SAMPLE);
      }
    }

    [Fact]
    public void CopyTo()
    {
      // add another few items to divide the buffer in two segments that will have to be copied separately
      subject.Add(SAMPLE);

      for (var i = 0; i < 1_000_000; i++) {
        subject.CopyTo(targetArray);
      }
    }

    [Fact]
    public void RandomAccessRead()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        for (var j = 0; j < subject.Count; j++)
        {
          item = subject[j];
        }
      }
    }

    [Fact]
    public void RandomAccessWrite()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        for (var j = 0; j < subject.Count; j++)
        {
          subject[j] = item;
        }
      }
    }

    [Fact]
    public void IEnumerator()
    {
      for (var i = 0; i < 1_000_000; i++)
      {
        foreach (var x in subject)
        {
          item = x;
        }
      }
    }
  }
}