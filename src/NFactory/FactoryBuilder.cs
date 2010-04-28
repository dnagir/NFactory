using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace NFactory
{

    public class FactoryBuilder<TObject>
    {
        Func<TObject> constuctor;
        Action<TObject> initialiser;
        IDictionary<string, string> propertySequences = new Dictionary<string, string>();

        public FactoryBuilder() { }

        public FactoryBuilder<TObject> With(Action<TObject> initialiser)
        {
            this.initialiser = initialiser;
            return this;
        }

        public FactoryBuilder<TObject> ConstructingAs(Func<TObject> constructor)
        {
            this.constuctor = constructor;
            return this;
        }

        internal TObject CreateInstance(Action<TObject> overrider)
        {
            // Create instance            
            TObject instance;
            if (constuctor != null)
                instance = constuctor.Invoke();
            else
                instance = Activator.CreateInstance<TObject>();

            // Set properties from 'With' definition
            if (initialiser != null)
                initialiser.Invoke(instance);

            // Set local properties from initialiser
            if (overrider != null)
                overrider.Invoke(instance);

            return instance;
        }
    }
}
