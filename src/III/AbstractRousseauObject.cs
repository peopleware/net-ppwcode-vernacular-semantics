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

using System;
using System.Runtime.Serialization;

using PPWCode.Vernacular.Exceptions.II;

namespace PPWCode.Vernacular.Semantics.III
{
    /// <summary>
    ///     Abstract class that supports things
    ///     required by <see cref="IRousseauObject" />.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class AbstractRousseauObject
        : AbstractSemanticObject,
          IRousseauObject
    {
        public bool IsCivilized()
        {
            return WildExceptions().IsEmpty;
        }

        /// <summary>
        ///     <inheritdoc cref="IRousseauObject.WildExceptions" />
        ///     Default implementation.
        /// </summary>
        /// <returns>
        ///     An empty, unclosed
        ///     <see cref="CompoundSemanticException" />. This is not a post condition
        ///     that is demanded of subclasses. It is intended as a helper functionality
        ///     for subclasses.
        /// </returns>
        /// <remarks>
        ///     Subclasses that override this method should use the following
        ///     pattern:
        ///     <code>
        /// [Pure]
        /// public override CompoundSemanticException WildExceptions()
        /// {
        ///     CompoundSemanticException cse = base.WildExceptions();
        ///     if (!some requirement met)
        ///     {
        ///         cse.AddElement(new PropertyException(this, "PropertyName", "EXCEPTION_MESSAGE_KEY", null));
        ///                        // or another type of exception
        ///     }
        ///     ... // repeat for more requirements
        ///     return cse;
        /// }
        /// </code>
        /// </remarks>
        public virtual CompoundSemanticException WildExceptions()
        {
            return new CompoundSemanticException();
        }

        /// <summary>
        ///     <inheritdoc cref="IRousseauObject.ThrowIfNotCivilized" />
        /// </summary>
        public void ThrowIfNotCivilized()
        {
            CompoundSemanticException cse = WildExceptions();
            if (!cse.IsEmpty)
            {
                cse.Closed = true;
                throw cse;
            }
        }
    }
}
