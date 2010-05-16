using NUnit.Framework;

namespace Irc.Events.Specs.ServerMessageReceivedSpecifications  
{
    public abstract class ServerMessageReceivedConcern : ConcernFor<ServerMessageReceived>
    {
        protected override ServerMessageReceived CreateSubjectUnderTest()
        {
            return new ServerMessageReceived();
        }
    }

    public class When_making_from_message : ServerMessageReceivedConcern
    {
        private ServerMessageReceived result;

        protected override void Because()
        {
            this.result = (ServerMessageReceived) sut.MakeFrom("any message at all");
        }

        [Test]
        public void Should_contain_whole_message()
        {
            Assert.That(result.Message == "any message at all");
        }

    }

    public class When_any_message_occuress : ServerMessageReceivedConcern
    {
        private bool result;

        protected override void Because()
        {
            this.result = sut.DoesOccurBecauseOf("any message at all");
        }

        [Test]
        public void Should_event_always_occur()
        {
            Assert.That(result, Is.True);
        }

    }
}
