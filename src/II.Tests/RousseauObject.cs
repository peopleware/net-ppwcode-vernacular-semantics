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

using System.Diagnostics.Contracts;

using PPWCode.Vernacular.Exceptions.II;
using PPWCode.Vernacular.Semantics.II;

namespace PPWCode.Vernacular.Semantics.II.Tests
{
    public class RousseauObject : AbstractRousseauObject
    {
        [ContractInvariantMethod]
        private void TypeInvariants()
        {
        }

        public RousseauObject()
        {
        }

        public bool MockWild { get; set; }

        public override CompoundSemanticException WildExceptions()
        {
            CompoundSemanticException cpe = base.WildExceptions();
            if (MockWild)
            {
                cpe.AddElement(new SemanticException("TEST"));
            }

            return cpe;
        }
    }
}