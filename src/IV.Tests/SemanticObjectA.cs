// Copyright 2018 by PeopleWare n.v..
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

namespace PPWCode.Vernacular.Semantics.IV.Tests
{
    public class SemanticObjectA : AbstractSemanticObject
    {
        private readonly List<SemanticObjectB> _semanticObjectBs = new List<SemanticObjectB>();

        public IList<SemanticObjectB> SemanticObjectBs
            => new List<SemanticObjectB>(_semanticObjectBs);

        /// <summary>
        ///     Closing the loop.
        ///     This could create a loop, as it is downstream, but not a collection.
        /// </summary>
        public SemanticObjectC C { get; set; }

        internal void AddB(SemanticObjectB b)
        {
            if (!_semanticObjectBs.Contains(b))
            {
                _semanticObjectBs.Add(b);
            }
        }

        internal void RemoveB(SemanticObjectB b)
        {
            _semanticObjectBs.Remove(b);
        }
    }
}
