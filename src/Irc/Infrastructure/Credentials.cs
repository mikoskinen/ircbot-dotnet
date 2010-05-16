using System;

namespace Irc.Infrastructure
{
    public class Credentials : IEquatable<Credentials>
    {
        private readonly string identifier;
        private readonly string name;

        public Credentials(string identifier, string name)
        {
            this.identifier = identifier;
            this.name = name;
        }

        public string Identifier
        {
            get {
                return identifier;
            }
        }

        public string Name
        {
            get {
                return name;
            }
        }

        #region Equals

        public bool Equals(Credentials other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.identifier, identifier) && Equals(other.name, name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Credentials)) return false;
            return Equals((Credentials)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((identifier != null ? identifier.GetHashCode() : 0) * 397) ^ (name != null ? name.GetHashCode() : 0);
            }
        }

        #endregion


    }
}