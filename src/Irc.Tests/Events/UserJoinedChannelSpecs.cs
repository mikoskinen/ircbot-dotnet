using Irc.Tests;
using NUnit.Framework;

namespace Irc.Events.Specs.UserJoinedChannelSpecifications  
{
    public abstract class UserJoinedChannelConcern : ConcernFor<UserJoinedChannel>
    {
        protected bool doesOccur;
        protected string message;

        protected override UserJoinedChannel CreateSubjectUnderTest()
        {
            return new UserJoinedChannel();
        }
    }

    public class When_user_joins_server_message_arrives_at_ircnet : UserJoinedChannelConcern
    {
        protected override void Context()
        {
            message = ExampleMessages.UserJoinChannel;
        }

        protected override void Because()
        {
            this.doesOccur = sut.DoesOccurBecauseOf(message);
        }

        [Test]
        public void Should_event_occur()
        {
            Assert.That(doesOccur, Is.True);
        }
    }

    public class When_user_joins_server_message_arrives_at_qnet : UserJoinedChannelConcern
    {
        protected override void Context()
        {
            message = ExampleMessages.UserJoinChannelAlternative;
        }

        protected override void Because()
        {
            this.doesOccur = sut.DoesOccurBecauseOf(message);
        }

        [Test]
        public void Should_event_occur()
        {
            Assert.That(doesOccur, Is.True);
        }
    }

    public class When_event_is_made : UserJoinedChannelConcern
    {
        private UserJoinedChannel createdEvent;

        protected override void Context()
        {
            message = ExampleMessages.UserJoinChannel;
        }

        protected override void Because()
        {
            this.createdEvent = (UserJoinedChannel)sut.MakeFrom(message);
        }

        [Test]
        public void Should_contain_correct_information()
        {
            Assert.That(createdEvent.Channel.Name, Is.EqualTo(ExampleMessages.Channel));
            Assert.That(createdEvent.User.Name, Is.EqualTo(ExampleMessages.UserName));
            Assert.That(createdEvent.User.HostMask, Is.EqualTo(ExampleMessages.HostMask));
        }
    }
}
