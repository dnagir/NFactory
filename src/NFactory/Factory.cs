using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFactory
{
    public class Factory
    {
        // Use something better: IDictionary<string, FactoryBuilder<????>>
        readonly IDictionary<string, object> factories = new Dictionary<string, object>();

        public FactoryBuilder<TObject> Define<TObject>()
        {
            return Define<TObject>(null);
        }

        public FactoryBuilder<TObject> Define<TObject>(string name)
        {
            var builder = new FactoryBuilder<TObject>();
            factories[name ?? typeof(TObject).Name] = builder;
            return builder;
        }


        public TObject New<TObject>()
        {
            return New<TObject>(null, null);
        }

        public TObject New<TObject>(string factoryName)
        {
            return New<TObject>(factoryName, null);
        }

        public TObject New<TObject>(Action<TObject> initialiser)
        {
            return New<TObject>(null, initialiser);
        }
        
        public TObject New<TObject>(string factoryName, Action<TObject> overrider)
        {
            factoryName = factoryName ?? typeof(TObject).Name;
            object untypedBuilder;
            if (!factories.TryGetValue(factoryName, out untypedBuilder))
                throw new InvalidOperationException( string.Format("No factory {0} defined.", factoryName ?? "<NULL>") );
            var builder = (FactoryBuilder<TObject>)untypedBuilder;

            return builder.CreateInstance(overrider);
        }
    }

}
