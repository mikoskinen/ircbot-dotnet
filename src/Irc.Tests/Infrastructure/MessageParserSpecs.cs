using NUnit.Framework;

namespace Irc.Bot.Specs.MessageParserSpecifications
{
    public class MessageParserConcern : ConcernFor<PrivateMessageParserImpl>
    {
        protected IrcMessage result;
        protected string receiverName;

        protected override void Context()
        {
            this.receiverName = "IrcBot";
        }

        protected override PrivateMessageParserImpl CreateSubjectUnderTest()
        {
            return new PrivateMessageParserImpl();
        }
    }

    public class When_received_message_from_channel : MessageParserConcern
    {
        private string inputLine;

        protected override void Because()
        {
            this.inputLine = ":UserNick!~user@as34-4334-3434.inter.net PRIVMSG #mychannel :#auth user password";
            result = sut.Parse(inputLine, receiverName);
        }

        [Test]
        public void Should_parse_message()
        {
            Assert.That(result.Message, Is.EqualTo("#auth user password"));
        }

        [Test]
        public void Shoud_parse_message_sender()
        {
            Assert.That(result.Sender.Name, Is.EqualTo("UserNick"));
        }

        [Test]
        public void Should_parse_message_hostmask()
        {
            Assert.That(result.Sender.HostMask, Is.EqualTo("~user@as34-4334-3434.inter.net"));
        }

        [Test]
        public void Should_parse_message_channel()
        {
            Assert.That(result.Sender.Channel, Is.EqualTo("#mychannel"));
        }

        [Test]
        public void Should_contain_original_message()
        {
            Assert.That(result.OriginalMessage, Is.EqualTo(inputLine));
        }
    }

    public class When_received_private_message_from_user : MessageParserConcern
    {
        protected override void Because()
        {
            result = sut.Parse(":UserNick!~user@as34-4334-3434.inter.net PRIVMSG IrcBot :#auth user password", receiverName);
        }

        [Test]
        public void Should_message_channel_be_same_as_message_sender()
        {
            Assert.That(result.Sender.Channel, Is.EqualTo("UserNick"));
        }
    }

    public class When_received_wrong_message_from_user : MessageParserConcern
    {
        protected override void Because()
        {
            result =
                sut.Parse(
                    ":stockholm.se.quakenet.org 005 ClassicBot WHOX WALLCHOPS WALLVOICES USERIP CPRIVMSG CNOTICE SILENCE=15 MODES=6 MAXCHANNELS=20 MAXBANS=45 NICKLEN=15 :are supported by this server", receiverName);
        }

        [Test]
        public void Should_message_be_empty()
        {
            Assert.That(result, Is.EqualTo(IrcMessage.Empty));
        }
    }
}