using Irc.Tests;
using NUnit.Framework;

namespace Irc.Events.Specs.UserModeChangedSpecifications
{
    public abstract class UserModeChangedConcern : ConcernFor<UserModeChanged>
    {
        protected bool doesOccur;

        protected override UserModeChanged CreateSubjectUnderTest()
        {
            return new UserModeChanged();
        }
    }

    public class When_user_mode_is_changed : UserModeChangedConcern
    {
        protected override void Because()
        {
            doesOccur = sut.DoesOccurBecauseOf(ExampleMessages.UserChangeUserMode);
        }

        [Test]
        public void Should_occur()
        {
            Assert.That(doesOccur, Is.True);
        }
    }

    public class When_channel_mode_is_changed : UserModeChangedConcern
    {
        protected override void Because()
        {
            doesOccur = sut.DoesOccurBecauseOf(ExampleMessages.UserChangeChannelMode);
        }

        [Test]
        public void Should_not_occur()
        {
            Assert.That(doesOccur, Is.False);
        }
    }

    public class When_event_is_made : UserModeChangedConcern
    {
        private UserModeChanged createdEvent;
        private string message;

        protected override void Context()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel -o+o anotherUser stillAnotherUser";
            message = ExampleMessages.UserChangeUsersModes;
        }

        protected override void Because()
        {
            this.createdEvent = (UserModeChanged)sut.MakeFrom(message);
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
            Assert.That(createdEvent.ModeChanges[0].UserName, Is.EqualTo("anotherUser"));
            Assert.That(createdEvent.ModeChanges[0].Identifier, Is.EqualTo("o"));
            Assert.That(createdEvent.ModeChanges[0].IsOn, Is.False);

            Assert.That(createdEvent.ModeChanges[1].UserName, Is.EqualTo("stillAnotherUser"));
            Assert.That(createdEvent.ModeChanges[1].Identifier, Is.EqualTo("o"));
            Assert.That(createdEvent.ModeChanges[1].IsOn, Is.True);
        }
    }
}
