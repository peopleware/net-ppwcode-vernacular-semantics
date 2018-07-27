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

namespace PPWCode.Vernacular.Semantics.III.Tests
{
    public class SemanticObjectB : AbstractSemanticObject
    {
        private readonly List<SemanticObjectC> _semanticObjectCs = new List<SemanticObjectC>();
        private int _intProperty;

        private SemanticObjectA _semanticObjectA;

        private string _stringProperty;

        public int IntProperty
        {
            get => _intProperty;
            set
            {
                if (_intProperty != value)
                {
                    _intProperty = value;
                    OnPropertyChanged(nameof(IntProperty));
                }
            }
        }

        public string StringProperty
        {
            get => _stringProperty;
            set
            {
                if (_stringProperty != value)
                {
                    _stringProperty = value;
                    OnPropertyChanged(nameof(StringProperty));
                }
            }
        }

        public SemanticObjectA SemanticObjectA
        {
            get => _semanticObjectA;
            set
            {
                _semanticObjectA?.RemoveB(this);
                _semanticObjectA = value;
                _semanticObjectA?.AddB(this);
            }
        }

        public IList<SemanticObjectC> SemanticObjectCs
            => new List<SemanticObjectC>(_semanticObjectCs);

        internal void AddC(SemanticObjectC c)
        {
            if (!_semanticObjectCs.Contains(c))
            {
                _semanticObjectCs.Add(c);
            }
        }

        internal void RemoveC(SemanticObjectC c)
        {
            _semanticObjectCs.Remove(c);
        }
    }
}
