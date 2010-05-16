using NUnit.Framework;

[TestFixture]
public abstract class ConcernFor<T>
{
    protected T sut;

    [SetUp]
    public void SetUp()
    {
        Context();
        sut = CreateSubjectUnderTest();
        Because();
    }

    protected virtual void Context() { }
    protected abstract T CreateSubjectUnderTest();
    protected virtual void Because() { }
}