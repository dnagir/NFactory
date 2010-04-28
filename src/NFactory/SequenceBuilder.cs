using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFactory
{
    public class SequenceBuilder<TType>
    {
        protected Func<int, TType> nextFunction;
        protected int current = 0;

        public SequenceBuilder(Func<int, TType> nextFunction)
        {
            this.nextFunction = nextFunction;
        }

        internal TType GetNext()
        {
            return nextFunction.Invoke(current++);
        }
    }
}
