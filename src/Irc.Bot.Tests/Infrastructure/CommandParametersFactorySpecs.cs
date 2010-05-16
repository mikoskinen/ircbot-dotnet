using ClassicIRCBot.Infrastructure;
using NUnit.Framework;

namespace Irc.Bot.Infrastructure.Specs.CommandParametersFactorySpecifications
{
    public abstract class CommandParametersFactoryConcern : ConcernFor<ParameterFactory>
    {
        protected string message;
        protected CommandParameters result;

        protected override ParameterFactory CreateSubjectUnderTest()
        {
            return new ParameterFactory();
        }

        protected override void Because()
        {
            result = sut.CreateParametersFromMessage(message);
        }
    }

    public class When_creating_command_parameters_from_message : CommandParametersFactoryConcern
    {
        protected override void Context()
        {
            this.message = "#auth user password";
        }

        [Test]
        public void Should_create_correct_parameters()
        {
            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0], Is.EqualTo("user"));
            Assert.That(result[1], Is.EqualTo("password"));
        }
    }

    public class When_creating_parameters_when_message_only_contains_command : CommandParametersFactoryConcern
    {
        protected override void Context()
        {
            this.message = "#command";
        }

        [Test]
        public void Should_parameters_be_empty()
        {
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}
