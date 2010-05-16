using Irc.Tests;
using NUnit.Framework;

namespace Irc.Events.Specs.UserPartChannelSpecifications  
{
    public abstract class UserPartChannelConcern : ConcernFor<UserPartChannel>
    {
        protected bool doesOccur;
        protected string message;

        protected override UserPartChannel CreateSubjectUnderTest()
        {
            return new UserPartChannel();
        }
    }

    public class When_user_parts_server_message_arrives : UserPartChannelConcern
    {
        protected override void Context()
        {
            message = ExampleMessages.UserPartChannel;
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

    public class When_user_parts_server_message_arrives_alternative : UserPartChannelConcern
    {
        protected override void Context()
        {
            message = ExampleMessages.UserPartChannelAlternative;
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


    public class When_event_is_made : UserPartChannelConcern
    {
        private UserPartChannel createdEvent;

        protected override void Context()
        {
            message = ExampleMessages.UserPartChannel;
        }

        protected override void Because()
        {
            this.createdEvent = (UserPartChannel)sut.MakeFrom(message);
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
