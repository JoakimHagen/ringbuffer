using Xunit;

namespace Tests
{
  public class OverfillTests : ContentTests
  {
    protected int[] bigArray = new int[] { 40, 41, 42, 43, 44 };
    protected int[] slicedArray = new int[] { 41, 42, 43, 44 };

    public OverfillTests()
    {
      subject.Clear();
      subject.Add(bigArray);
      truth = slicedArray;
    }

    [Fact]
    public void AddsItem()
    {
      subject.Clear();
      subject.Add(42);
    }

    [Fact]
    public void AddsMultipleItems()
    {
      subject.Clear();
      subject.Add(new int[] { 41, 42, 43 });
      subject.Add(bigArray);
    }
  }
}
