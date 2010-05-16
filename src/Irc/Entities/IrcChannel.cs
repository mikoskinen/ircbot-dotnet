namespace Irc.Entities
{
    public class IrcChannel
    {
        public string Name { get; private set; }

        public IrcChannel(string name)
        {
            this.Name = name;
        }
    }
}
