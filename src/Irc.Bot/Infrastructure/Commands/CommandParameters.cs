using System.Collections;
using System.Collections.Generic;

namespace ClassicIRCBot.Infrastructure
{
    public class CommandParameters : IEnumerable<string>
    {
        private List<string> parameters = new List<string>();

        public string this[int index]
        {
            get
            {
                return parameters[index];
            }
        }

        public int Count
        {
            get
            {
                return parameters == null ? 0 : parameters.Count;
            }
        }

        public void AddParameter(string parameter)
        {
            if (parameters == null)
                parameters = new List<string>();

            parameters.Add(parameter);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
