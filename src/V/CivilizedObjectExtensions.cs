// Copyright 2026 by PeopleWare n.v..
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using PPWCode.Vernacular.Exceptions.V;

namespace PPWCode.Vernacular.Semantics.V;

public static class CivilizedObjectExtensions
{
    extension(ICivilizedObject? civilizedObject)
    {
        public void CheckForWildExceptions(CompoundSemanticException cse)
        {
            if (civilizedObject is not null)
            {
                cse.AddElement(civilizedObject.WildExceptions());
            }
        }
    }

    extension(IEnumerable<ICivilizedObject?>? civilizedObjects)
    {
        public void CheckForWildExceptions(CompoundSemanticException cse)
        {
            if (civilizedObjects is not null)
            {
                foreach (ICivilizedObject? civilizedObject in civilizedObjects)
                {
                    civilizedObject.CheckForWildExceptions(cse);
                }
            }
        }
    }
}
