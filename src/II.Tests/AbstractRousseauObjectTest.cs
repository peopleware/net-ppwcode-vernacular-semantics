﻿// Copyright 2014 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using NUnit.Framework;

using PPWCode.Vernacular.Exceptions.II;
using PPWCode.Vernacular.Semantics.II;

namespace PPWCode.Vernacular.Semantics.II.Tests
{
    [TestFixture]
    public class AbstractRousseauObjectTest
    {
        /// <summary>
        ///     Gets or sets the test context which provides
        ///     information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        private AbstractRousseauObject[] m_Subjects;

        [SetUp]
        public void MyTestInitialize()
        {
            m_Subjects = new AbstractRousseauObject[2];
            m_Subjects[0] = new RousseauObject
                            {
                                MockWild = false
                            };
            m_Subjects[1] = new RousseauObject
                            {
                                MockWild = true
                            };
        }

        [Test]
        public void MyTestCleanup()
        {
            m_Subjects = null;
        }

        [Test]
        public void WildExceptionsTest()
        {
            foreach (AbstractRousseauObject aro in m_Subjects)
            {
                CompoundSemanticException cpe = aro.WildExceptions();
            }
        }

        /// <summary>
        ///     A test for ThrowIfNotCivilized.
        /// </summary>
        [Test]
        public void ThrowIfNotCivilizedTest()
        {
            foreach (AbstractRousseauObject aro in m_Subjects)
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
        public void IsCivilizedTest()
        {
            foreach (AbstractRousseauObject aro in m_Subjects)
            {
                aro.IsCivilized();
            }
        }

        /// <summary>
        ///     A test for AbstractRousseauObject Constructor.
        /// </summary>
        [Test]
        public void AbstractRousseauObjectConstructorTest()
        {
            AbstractRousseauObject target = new RousseauObject();
        }
    }
}