using System;
using Irc.Tests;
using NUnit.Framework;

namespace Irc.Events.Specs.TopicChangedSpecifications
{
    public abstract class TopicChangedConcern : ConcernFor<TopicChanged>
    {
        protected override TopicChanged CreateSubjectUnderTest()
        {
            return new TopicChanged();
        }
    }

    public class When_topic_changed_message_arrives : TopicChangedConcern
    {
        private bool result;

        protected override void Because()
        {
            result = sut.DoesOccurBecauseOf(ExampleMessages.TopicChanged);
        }

        [Test]
        public void Should_topic_changed_event_occur()
        {
            Assert.That(result, Is.True);
        }
    }

    public class When_event_is_made : TopicChangedConcern
    {
        private TopicChanged result;

        protected override void Because()
        {
            result = (TopicChanged)sut.MakeFrom(ExampleMessages.TopicChanged);
        }

        [Test]
        public void Should_contain_correct_data()
        {
            Assert.That(result.Channel.Name, Is.EqualTo(ExampleMessages.Channel));
            Assert.That(result.User.Name, Is.EqualTo(ExampleMessages.UserName));
            Assert.That(result.NewTopic, Is.EqualTo("New topic with many words"));
        }
    }
}
