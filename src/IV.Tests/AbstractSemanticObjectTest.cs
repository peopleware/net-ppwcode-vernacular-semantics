// Copyright 2025 by PeopleWare n.v..
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

namespace PPWCode.Vernacular.Semantics.IV.Tests
{
    [TestFixture]
    public class AbstractSemanticObjectTest
    {
        private static readonly string[] _someStrings =
        {
            "A string",
            "another string",
            string.Empty,
            null,
            "You name here"
        };

        public AbstractSemanticObject[] BuildBs()
        {
            AbstractSemanticObject[] result = new AbstractSemanticObject[5];
            SemanticObjectA a = new SemanticObjectA();
            SemanticObjectC lastC = null;
            for (int i = 0; i < 5; i++)
            {
                SemanticObjectB b =
                    new SemanticObjectB
                    {
                        IntProperty = i,
                        StringProperty = _someStrings[i]
                    };

                for (int j = 0; j < 10; j++)
                {
                    SemanticObjectC c =
                        new SemanticObjectC
                        {
                            B = b,
                            A = a
                        };
                    lastC = c;
                }

                b.SemanticObjectA = a;
                result[i] = b;
            }

            // this creates a loop, as it is downstream, but not a collection
            a.C = lastC;
            return result;
        }

        public class CallMock
        {
            public object Obj { get; set; }

            public PropertyChangedEventArgs PceArgs { get; set; }
        }

        private CallMock _delegateMethodeCalled;

        public void DelegateMethod(object obj, PropertyChangedEventArgs pceArgs)
        {
            _delegateMethodeCalled =
                new CallMock
                {
                    Obj = obj,
                    PceArgs = pceArgs
                };
        }

        [Test]
        public void CreateAbstractSemanticObject()
        {
            AbstractSemanticObject result = new SemanticObjectB();
            Assert.That(result, Is.Not.Null);
            result = new SemanticObjectA();
            Assert.That(result, Is.Not.Null);
            result = new SemanticObjectC();
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        ///     A test for the Equals method.
        /// </summary>
        [Test]
        [SuppressMessage(
            "Assertion",
            "NUnit2009:The same value has been provided as both the actual and the expected argument",
            Justification = "Reviewed")]
        public void EqualsTest()
        {
            List<AbstractSemanticObject> all = new List<AbstractSemanticObject>(BuildBs());
            AbstractSemanticObject abstractSemanticObject = all[0];
            SemanticObjectB semanticObjectB = (SemanticObjectB)abstractSemanticObject;
            all.Add(semanticObjectB.SemanticObjectA);
            all.AddRange(semanticObjectB.SemanticObjectCs);
            bool result = false;
            foreach (AbstractSemanticObject aso1 in all)
            {
                foreach (AbstractSemanticObject aso2 in all)
                {
                    result = aso1.Equals(aso2);
                    Assert.That(result, Is.EqualTo(result));
                }

                result = aso1.Equals(null);
            }

            Assert.That(result, Is.EqualTo(result));
        }

        [Test]
        public void GetHashCodeTest()
        {
            AbstractSemanticObject[] subjects = BuildBs();
            foreach (AbstractSemanticObject so in subjects)
            {
                int result = so.GetHashCode();
            }
        }

#if NETSTANDARD2_0 || NET462_OR_GREATER
        [Test]
        public void IsSerializedTest()
        {
            AbstractSemanticObject[] subjects = BuildBs();
            foreach (AbstractSemanticObject so in subjects)
            {
                bool result = so.IsSerialized;
            }
        }
#endif

        [Test]
        public void OnPropertyChangedTest()
        {
            AbstractSemanticObject[] subjects = BuildBs();
            foreach (AbstractSemanticObject so in subjects)
            {
                if (so is SemanticObjectB b)
                {
                    b.PropertyChanged += DelegateMethod;
                    _delegateMethodeCalled = null;
                    for (int i = -7; i < 7; i++)
                    {
                        b.IntProperty = i;
                    }

                    Assert.That(_delegateMethodeCalled, Is.Not.Null);
                    Assert.That(_delegateMethodeCalled.Obj, Is.EqualTo(b));
                    Assert.That(_delegateMethodeCalled.PceArgs, Is.Not.Null);
                    Assert.That(_delegateMethodeCalled.PceArgs.PropertyName, Is.EqualTo("IntProperty"));
                    _delegateMethodeCalled = null;
                    foreach (string s in _someStrings)
                    {
                        b.StringProperty = s;
                    }

                    Assert.That(_delegateMethodeCalled, Is.Not.Null);
                    Assert.That(_delegateMethodeCalled.Obj, Is.EqualTo(b));
                    Assert.That(_delegateMethodeCalled.PceArgs, Is.Not.Null);
                    Assert.That(_delegateMethodeCalled.PceArgs.PropertyName, Is.EqualTo("StringProperty"));
                }
            }
        }

        [Test]
        public void ToLimitedStringTest()
        {
            List<AbstractSemanticObject> all = new List<AbstractSemanticObject>(BuildBs());
            AbstractSemanticObject abstractSemanticObjectB = all[0];
            SemanticObjectB semanticObjectB = (SemanticObjectB)abstractSemanticObjectB;
            all.Add(semanticObjectB.SemanticObjectA);
            all.AddRange(semanticObjectB.SemanticObjectCs);
            foreach (AbstractSemanticObject aso in all)
            {
                string result = aso.LimitedToString();
                Trace.WriteLine(result);
                Trace.Flush();
            }
        }

        [Test]
        public void ToStringTest()
        {
            AbstractSemanticObject[] subjects = BuildBs();
            foreach (AbstractSemanticObject so in subjects)
            {
                string result = so.ToString();
                Trace.WriteLine(result);
                Trace.Flush();
                SemanticObjectB b = (SemanticObjectB)so;
                result = b.SemanticObjectA.ToString();
                Trace.WriteLine(result);
                Trace.Flush();
                foreach (SemanticObjectC c in b.SemanticObjectCs)
                {
                    result = c.ToString();
                    Trace.WriteLine(result);
                }

                Trace.Flush();
            }
        }
    }
}
