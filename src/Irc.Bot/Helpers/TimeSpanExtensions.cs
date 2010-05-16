using System;

namespace Irc.Bot.Helpers
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Seconds(this int number)
        {
            return new TimeSpan(0, 0, number);
        }
    }
}