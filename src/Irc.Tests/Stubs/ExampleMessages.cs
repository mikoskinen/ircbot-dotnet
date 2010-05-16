namespace Irc.Tests
{
    public static class ExampleMessages
    {

        public const string HostMask = "~user@as34-4334-3434.inter.net";
        public const string UserName = "UserNick";
        public const string Channel = "#mychannel";

        public const string UserPrivateMessageChannel               = ":UserNick!~user@as34-4334-3434.inter.net PRIVMSG #mychannel :#auth user password";
        public const string UserPrivateMessageChannelWithSmilies    = ":UserNick!~user@as34-4334-3434.inter.net PRIVMSG #mychannel :hello :) there :'(";
        public const string UserPrivateMessageUser                  = ":UserNick!~user@as34-4334-3434.inter.net PRIVMSG anotherUser :hello";
        public const string UserNoticeUser                          = ":UserNick!~user@as34-4334-3434.inter.net NOTICE anotherUser :hi there with notice";
        public const string UserNoticeChannel                       = ":UserNick!~user@as34-4334-3434.inter.net NOTICE #mychannel :notice to channel";
        public const string UserPartChannel                         = ":UserNick!~user@as34-4334-3434.inter.net PART #mychannel :";
        public const string UserPartChannelAlternative              = ":UserNick!~user@as34-4334-3434.inter.net PART #mychannel";
        public const string UserJoinChannel                         = ":UserNick!~user@as34-4334-3434.inter.net JOIN :#mychannel";
        public const string UserJoinChannelAlternative              = ":UserNick!~user@as34-4334-3434.inter.net JOIN #mychannel";
        public const string TopicChanged                            = ":UserNick!~user@as34-4334-3434.inter.net TOPIC #mychannel :New topic with many words";
        public const string UserChangeUserMode                      = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +o anotherUser";
        public const string UserChangeUsersMode                     = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel -oo anotherUser stillAnotherUser";
        public const string UserChangeUsersModes                    = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel -o+o anotherUser stillAnotherUser";
        public const string UserChangeChannelMode                   = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +n";
        public const string UserChangeChannelModes                  = ":UserNick!~user@as34-4334-3434.inter.net MODE #mychannel +mti-sn";
        public const string UserQuit                                = ":UserNick!~user@as34-4334-3434.inter.net QUIT :\"quit message\"";
        public const string UserSplit                               = ":UserNick!~user@as34-4334-3434.inter.net QUIT :ircnet.server.net ircnet.anotherserver.com";

    }
}
