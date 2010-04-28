using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NFactory.Tests
{
    [TestFixture]
    public class TestSequence
    {
        Sequence s;
        [SetUp]
        public void Init()
        {
            s = new Sequence();
        }

        [Test]
        public void CanDefineUnnamedSequence()
        {
            s.Define<int>(n => n * 10);
            s.Next<int>();
            Assert.AreEqual(10, s.Next<int>());
        }

        [Test]
        public void CanDefineNamedSequence()
        {
            s.Define<int>("tens", n => n * 10);
            s.Next<int>("tens");
            Assert.AreEqual(10, s.Next<int>("tens"));
        }

        [Test]
        public void UnknownSequenceThrows()
        {
            Assert.Throws<InvalidOperationException>(() => s.Next<int>("definitely unknown sequence"));
        }

    }

}
