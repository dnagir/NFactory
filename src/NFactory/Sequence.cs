using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NFactory
{
    public class Sequence
    {
        readonly IDictionary<string, object> sequences = new Dictionary<string, object>();

        public SequenceBuilder<TType> Define<TType>(Func<int, TType> nextFunction)
        {
            return Define<TType>(null, nextFunction);
        }

        public SequenceBuilder<TType> Define<TType>(string name, Func<int, TType> nextFunction)
        {
            var seq = new SequenceBuilder<TType>(nextFunction);
            sequences[name ?? typeof(TType).Name] = seq;
            return seq;
        }

        public TType Next<TType>()
        {
            return Next<TType>(null);
        }

        public TType Next<TType>(string name)
        {
            name = name ?? typeof(TType).Name;
            object untypedBuilder;
            if (!sequences.TryGetValue(name, out untypedBuilder))
                throw new InvalidOperationException( string.Format("No sequence {0} registered.", name ?? "<NULL>") );
            var nextFunctoin = (SequenceBuilder<TType>)untypedBuilder; // TODO: Tech. debt - do something with casting
            return nextFunctoin.GetNext();
        }
    }  

}
