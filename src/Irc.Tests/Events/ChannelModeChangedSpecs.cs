using Irc.Tests;
using NUnit.Framework;

namespace Irc.Events.Specs.ChannelModeChangedSpecifications
{
    public abstract class ChannelModeChangedConcern : ConcernFor<ChannelModeChanged>
    {
        protected bool doesOccur;

        protected override ChannelModeChanged CreateSubjectUnderTest()
        {
            return new ChannelModeChanged();
        }
    }

    public class When_user_mode_is_changed : ChannelModeChangedConcern
    {
        protected override void Because()
        {
            doesOccur = sut.DoesOccurBecauseOf(ExampleMessages.UserChangeUsersModes);
        }

        [Test]
        public void Should_not_occur()
        {
            Assert.That(doesOccur, Is.True);
        }
    }

    public class When_channel_mode_is_changed : ChannelModeChangedConcern
    {
        protected override void Because()
        {
            doesOccur = sut.DoesOccurBecauseOf(ExampleMessages.UserChangeChannelMode);
        }

        [Test]
        public void Should_occur()
        {
            Assert.That(doesOccur, Is.False);
        }
    }

    public class When_event_is_made : ChannelModeChangedConcern
    {
        private ChannelModeChanged createdEvent;
        private string message;

        protected override void Context()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +mti-sn";
            message = ExampleMessages.UserChangeChannelModes;
        }

        protected override void Because()
        {
            this.createdEvent = (ChannelModeChanged)sut.MakeFrom(message);
        }

        [Test]
        public void Should_contain_correct_information_about_mode_changer()
        {
            Assert.That(createdEvent.Channel.Name, Is.EqualTo(ExampleMessages.Channel));
            Assert.That(createdEvent.User.Name, Is.EqualTo(ExampleMessages.UserName));
            Assert.That(createdEvent.User.HostMask, Is.EqualTo(ExampleMessages.HostMask));
        }

        [Test]
        public void Should_contain_correct_information_about_changed_modes()
        {
            Assert.That(createdEvent.ModeChanges[0].Identifier, Is.EqualTo("m"));
            Assert.That(createdEvent.ModeChanges[0].IsOn, Is.True);
            Assert.That(createdEvent.ModeChanges[0].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(createdEvent.ModeChanges[1].Identifier, Is.EqualTo("t"));
            Assert.That(createdEvent.ModeChanges[1].IsOn, Is.True);
            Assert.That(createdEvent.ModeChanges[1].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(createdEvent.ModeChanges[2].Identifier, Is.EqualTo("i"));
            Assert.That(createdEvent.ModeChanges[2].IsOn, Is.True);
            Assert.That(createdEvent.ModeChanges[2].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(createdEvent.ModeChanges[3].Identifier, Is.EqualTo("s"));
            Assert.That(createdEvent.ModeChanges[3].IsOn, Is.False);
            Assert.That(createdEvent.ModeChanges[3].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(createdEvent.ModeChanges[4].Identifier, Is.EqualTo("n"));
            Assert.That(createdEvent.ModeChanges[4].IsOn, Is.False);
            Assert.That(createdEvent.ModeChanges[4].UserName, Is.EqualTo(ExampleMessages.Channel));

            //Assert.That(createdEvent.ModeChanges[0].UserName, Is.EqualTo("anotherUser"));
            //Assert.That(createdEvent.ModeChanges[0].Identifier, Is.EqualTo("o"));
            //Assert.That(createdEvent.ModeChanges[0].IsOn, Is.False);

            //Assert.That(createdEvent.ModeChanges[1].UserName, Is.EqualTo("stillAnotherUser"));
            //Assert.That(createdEvent.ModeChanges[1].Identifier, Is.EqualTo("o"));
            //Assert.That(createdEvent.ModeChanges[1].IsOn, Is.True);
        }
    }
}
