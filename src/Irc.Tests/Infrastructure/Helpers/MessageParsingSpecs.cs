using System.Diagnostics;
using Irc.Infrastructure.Messages.Parsers;
using Irc.Tests;
using NUnit.Framework;
using System.Collections.Generic;

namespace Irc.Infrastructure.Helpers.Specs.MessageParsingSpecifications
{
    [TestFixture]
    public class When_parsing_user_details_from_message
    {
        private List<string> inputMessages;

        [SetUp]
        public void Context()
        {
            this.inputMessages = new List<string>
                                {
                                    "",
                                    ExampleMessages.UserPrivateMessageChannel,
                                    ExampleMessages.UserPrivateMessageUser,
                                    ExampleMessages.UserNoticeUser,
                                    ExampleMessages.UserNoticeChannel,
                                    ExampleMessages.UserPartChannel,
                                    ExampleMessages.UserJoinChannel,
                                    ExampleMessages.TopicChanged,
                                    ExampleMessages.UserChangeUserMode,
                                    ExampleMessages.UserChangeUsersMode,
                                    ExampleMessages.UserChangeUsersModes,
                                    ExampleMessages.UserChangeChannelMode,
                                    ExampleMessages.UserChangeChannelModes,
                                    ExampleMessages.UserQuit,
                                    ExampleMessages.UserSplit
                                };
        }

        [Test]
        public void Should_parse_the_user_name_correctly()
        {
            inputMessages.ForEach(x =>
                                      {
                                          var assertMessage = string.Format("When: {0}", x);
                                          AssertStringIsIn(MessageParser.GetSenderName(x), new string[] { "UserNick", "" }, assertMessage);
                                      });
        }

        [Test]
        public void Should_parse_user_correctly()
        {
            inputMessages.ForEach(x =>
            {
                var assertMessage = string.Format("When: {0}", x);
                AssertStringIsIn(MessageParser.GetUser(x).Name, new string[] { "UserNick", "" }, assertMessage);
                AssertStringIsIn(MessageParser.GetHostmask(x), new string[] { "~user@as34-4334-3434.inter.net", "" }, assertMessage);

            });
        }

        [Test]
        public void Should_parse_hostmask_correctly()
        {
            inputMessages.ForEach(x =>
            {
                var assertMessage = string.Format("When: {0}", x);
                AssertStringIsIn(MessageParser.GetDetails(x).Hostmask, new string[] { "~user@as34-4334-3434.inter.net", "" }, assertMessage);
            });
        }

        [Test]
        public void Should_create_join_message_correctly()
        {
            var joinMessage = MessageParser.CreateJoinMessage(ExampleMessages.UserJoinChannel);
            Assert.That(joinMessage.Channel.Name, Is.EqualTo(ExampleMessages.Channel));
            Assert.That(joinMessage.User.Name, Is.EqualTo(ExampleMessages.UserName));
            Assert.That(joinMessage.User.HostMask, Is.EqualTo(ExampleMessages.HostMask));
        }



        [Test]
        public void Should_known_correcly_if_user_mode_changed()
        {
            Assert.That(MessageParser.IsUserModeChangedMessage(ExampleMessages.UserChangeUserMode), Is.True);
            Assert.That(MessageParser.IsUserModeChangedMessage(ExampleMessages.UserChangeUsersMode), Is.True);
            Assert.That(MessageParser.IsUserModeChangedMessage(ExampleMessages.UserChangeUsersModes), Is.True);
            Assert.That(MessageParser.IsUserModeChangedMessage(ExampleMessages.UserChangeChannelMode), Is.False, ExampleMessages.UserChangeChannelMode);
            Assert.That(MessageParser.IsUserModeChangedMessage(ExampleMessages.UserChangeChannelModes), Is.False, ExampleMessages.UserChangeChannelModes);
        }

        [Test]
        public void Should_parse_user_mode_change_correctly()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +o anotherUser";
            var modeChangeMessage = ExampleMessages.UserChangeUserMode;

            var modeChanges = MessageParser.GetModeChanges(modeChangeMessage);

            Assert.That(modeChanges[0].UserName, Is.EqualTo("anotherUser"));
            Assert.That(modeChanges[0].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[0].IsOn, Is.True);
        }

        [Test]
        public void Should_parse_user_mode_changes_correctly()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel -o+o anotherUser stillAnotherUser";
            var modeChangeMessage = ExampleMessages.UserChangeUsersModes;

            var modeChanges = MessageParser.GetModeChanges(modeChangeMessage);

            Assert.That(modeChanges[0].UserName, Is.EqualTo("anotherUser"));
            Assert.That(modeChanges[0].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[0].IsOn, Is.False);

            Assert.That(modeChanges[1].UserName, Is.EqualTo("stillAnotherUser"));
            Assert.That(modeChanges[1].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[1].IsOn, Is.True);
        }

        [Test]
        public void Should_parse_user_mode_changes_correctly_advanced()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel -o+o anotherUser stillAnotherUser";
            var modeChangeMessage = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +oo+v-o-v anotherUser stillAnotherUser third fourth fifth";

            var modeChanges = MessageParser.GetModeChanges(modeChangeMessage);

            Assert.That(modeChanges[0].UserName, Is.EqualTo("anotherUser"));
            Assert.That(modeChanges[0].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[0].IsOn, Is.True);

            Assert.That(modeChanges[1].UserName, Is.EqualTo("stillAnotherUser"));
            Assert.That(modeChanges[1].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[1].IsOn, Is.True);

            Assert.That(modeChanges[2].UserName, Is.EqualTo("third"));
            Assert.That(modeChanges[2].Identifier, Is.EqualTo("v"));
            Assert.That(modeChanges[2].IsOn, Is.True);

            Assert.That(modeChanges[3].UserName, Is.EqualTo("fourth"));
            Assert.That(modeChanges[3].Identifier, Is.EqualTo("o"));
            Assert.That(modeChanges[3].IsOn, Is.False);

            Assert.That(modeChanges[4].UserName, Is.EqualTo("fifth"));
            Assert.That(modeChanges[4].Identifier, Is.EqualTo("v"));
            Assert.That(modeChanges[4].IsOn, Is.False);
        }

        [Test]
        public void Should_parse_channel_mode_changes_corretly()
        {
            //":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +mti-sn";
            var modeChanges = MessageParser.GetModeChanges(ExampleMessages.UserChangeChannelModes);

            Assert.That(modeChanges[0].Identifier, Is.EqualTo("m"));
            Assert.That(modeChanges[0].IsOn, Is.True);
            Assert.That(modeChanges[0].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(modeChanges[1].Identifier, Is.EqualTo("t"));
            Assert.That(modeChanges[1].IsOn, Is.True);
            Assert.That(modeChanges[1].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(modeChanges[2].Identifier, Is.EqualTo("i"));
            Assert.That(modeChanges[2].IsOn, Is.True);
            Assert.That(modeChanges[2].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(modeChanges[3].Identifier, Is.EqualTo("s"));
            Assert.That(modeChanges[3].IsOn, Is.False);
            Assert.That(modeChanges[3].UserName, Is.EqualTo(ExampleMessages.Channel));

            Assert.That(modeChanges[4].Identifier, Is.EqualTo("n"));
            Assert.That(modeChanges[4].IsOn, Is.False);
            Assert.That(modeChanges[4].UserName, Is.EqualTo(ExampleMessages.Channel));

        }

        [Test]
        public void Should_parse_incorrect_irc_message_correctly()
        {
            var message = ":irc.server.net 020 * :Please wait while we process your connection.";
            var details = MessageParser.GetDetails(message);

            Assert.That(details.Action, Is.EqualTo(""));
        }

        [Test]
        public void Should_parse_private_message_correctly()
        {
            var message = ExampleMessages.UserPrivateMessageChannel;
            var details = MessageParser.GetDetails(message);

            Assert.That(details.Action, Is.EqualTo("PRIVMSG"));
            Assert.That(details.Parameters, Is.EqualTo("#auth user password"));
        }

        [Test]
        public void Should_parse_mode_change_message_correctly()
        {
            var message = ExampleMessages.UserChangeUsersModes;
            var details = MessageParser.GetDetails(message);

            Assert.That(details.Action, Is.EqualTo("MODE"));
            Assert.That(details.Parameters, Is.EqualTo("-o+o anotherUser stillAnotherUser"));
            Assert.That(details.Target, Is.EqualTo("#mychannel"));
        }

        [Test]
        public void Should_parse_message_with_smilies_correctly()
        {
            var message = ExampleMessages.UserPrivateMessageChannelWithSmilies;
            var details = MessageParser.GetDetails(message);

            Assert.That(details.Action, Is.EqualTo("PRIVMSG"));
            Assert.That(details.Target, Is.EqualTo("#mychannel"));
            Assert.That(details.Parameters, Is.EqualTo("hello :) there :'("));

        }

        public void AssertStringIsIn(string actual, string[] expected, string assertMessage)
        {
            AssertStringContains(actual, expected, assertMessage);
        }

        public void AssertStringContains(string actual, string[] expected, string assertMessage)
        {

            var allExpected = "";
            foreach (var expectedString in expected)
            {
                if (expectedString == actual)
                {
                    var matchFoundMessage = string.Format("Match found. Expected: {0} Actual: {1}", expectedString, actual);
                    Debug.WriteLine(matchFoundMessage);
                    return;

                }

                var formattedExpected = string.Format("'{0}'", expectedString);
                allExpected += formattedExpected + " ";
            }

            var message = string.Format("{0}: Was '{1}' Excpected {2}", assertMessage, actual, allExpected);
            Assert.Fail(message);
        }

    }
}
