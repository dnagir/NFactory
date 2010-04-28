using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NFactory.Tests
{ 
    [TestFixture]
    public class TestFactory
    {
        Factory f;

        [SetUp]
        public void Init()
        {
            f = new Factory();
        }

        [Test]
        public void CanDefineDefaultFactory()
        {
            f.Define<Person>();
            Assert.NotNull(f.New<Person>());
        }

        [Test]
        public void CanDefineNamedFactory()
        {
            f.Define<Person>("naughty");
            Assert.NotNull(f.New<Person>("naughty"));
        }

        [Test]
        public void CanDefineFactoryWithOverride()
        {
            f.Define<Person>()
                .With(p => p.Name = "name");
            Assert.AreEqual("name", f.New<Person>().Name);
        }

        [Test]
        public void CanDefineFactoryWithNamedSequenceForProperty()
        {
            var s = new Sequence();
            s.Define<string>("named", n => "a" + n);
            f.Define<Person>().With(p =>
            {
                p.Name = s.Next<string>("named");
            });

            Assert.AreEqual("a0", f.New<Person>().Name);
            Assert.AreEqual("a1", f.New<Person>().Name);
        }

        [Test]
        public void CanDefineFactoryWithUnNamedSequenceForProperty()
        {
            var s = new Sequence();
            s.Define<string>(n => "a" + n);
            f.Define<Person>().With(p => {
                p.Name = s.Next<string>();
            });

            Assert.AreEqual("a0", f.New<Person>().Name);
            Assert.AreEqual("a1", f.New<Person>().Name);
        }

        [Test]
        public void CanDefineFactoryWithConstructor()
        {
            f.Define<Person>()
                .With(p => p.Name = "name")
                .ConstructingAs(() => new Person() { Age = 88 });

            Assert.AreEqual(88, f.New<Person>().Age);
        }

        [Test]
        public void CanCreateAndOverrideProperties()
        {
            f.Define<Person>().With(x => x.Name = "initial");

            var p = f.New<Person>(x => x.Name = "overriden");
            Assert.NotNull(p);
            Assert.AreEqual("overriden", p.Name);
        }

        [Test]
        public void ThrowsForUnknownFactory()
        {
            Assert.Throws<InvalidOperationException>(() => f.New<Person>("undefined factory"));
        }

    }
}
