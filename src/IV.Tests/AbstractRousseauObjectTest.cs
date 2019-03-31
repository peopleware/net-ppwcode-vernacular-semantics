// Copyright 2019 by PeopleWare n.v..
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using NUnit.Framework;

using PPWCode.Vernacular.Exceptions.IV;

namespace PPWCode.Vernacular.Semantics.IV.Tests
{
    [TestFixture]
    public class AbstractRousseauObjectTest
    {
        [SetUp]
        public void MyTestInitialize()
        {
            _subjects = new AbstractRousseauObject[2];
            _subjects[0] =
                new RousseauObject
                {
                    MockWild = false
                };
            _subjects[1] =
                new RousseauObject
                {
                    MockWild = true
                };
        }

        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        private AbstractRousseauObject[] _subjects;

        /// <summary>
        ///     A test for AbstractRousseauObject Constructor.
        /// </summary>
        [Test]
        public void AbstractRousseauObjectConstructorTest()
        {
            AbstractRousseauObject target = new RousseauObject();
        }

        [Test]
        public void IsCivilizedTest()
        {
            foreach (AbstractRousseauObject aro in _subjects)
            {
                aro.IsCivilized();
            }
        }

        [Test]
        public void MyTestCleanup()
        {
            _subjects = null;
        }

        /// <summary>
        ///     A test for ThrowIfNotCivilized.
        /// </summary>
        [Test]
        public void ThrowIfNotCivilizedTest()
        {
            foreach (AbstractRousseauObject aro in _subjects)
            {
                try
                {
                    aro.ThrowIfNotCivilized();
                }
                catch (CompoundSemanticException)
                {
                    // NOP
                }
            }
        }

        [Test]
        public void WildExceptionsTest()
        {
            foreach (AbstractRousseauObject aro in _subjects)
            {
                CompoundSemanticException cpe = aro.WildExceptions();
            }
        }
    }
}
