using NUnit.Framework;

namespace Irc.Events.Specs.PingReceivedSpecifications
{

    public abstract class PingReceivedConcern : ConcernFor<PingReceived>
    {
        protected override PingReceived CreateSubjectUnderTest()
        {
            return new PingReceived();
        }
    }

    public class When_making_from_ping_message : PingReceivedConcern
    {
        private PingReceived result;

        protected override void Because()
        {
            this.result = (PingReceived) sut.MakeFrom("PING :627133754");
        }

        [Test]
        public void Should_contain_whole_message()
        {
            Assert.That(result.PingMessage, Is.EqualTo("PING :627133754"));
        }
    }

    public class When_receiving_ping_message : PingReceivedConcern
    {
        private bool result;

        protected override void Because()
        {
            this.result = sut.DoesOccurBecauseOf("PING :627133754");
        }

        [Test]
        public void Should_occur()
        {
            Assert.That(result, Is.EqualTo(true));
        }
    }


}
