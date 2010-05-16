using System.Collections.Generic;

namespace Irc.Infrastructure.Messages
{
    public class Modes
    {
        private List<Mode> modes;

        public Mode this[int index]
        {
            get
            {
                return modes[index];
            }
        }

        public int Count
        {
            get
            {
                return modes == null ? 0 : modes.Count;
            }
        }

        public void Add(Mode mode)
        {
            if  (modes == null)
                modes = new List<Mode>();

            modes.Add(mode);
        }
    }
}