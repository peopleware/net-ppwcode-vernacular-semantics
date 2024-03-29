﻿// Copyright 2019 by PeopleWare n.v..
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.ComponentModel;

using JetBrains.Annotations;

namespace PPWCode.Vernacular.Semantics.IV
{
    /// <summary>
    ///     A type that formalizes how to work with objects that represent real-world objects
    ///     (objects with semantic meaning, non-utility objects, non-value objects).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Semantic objects should always be used <em>by reference</em>. For this
    ///         reason, the contracts of <see cref="Equals" /> and <see cref="GetHashCode" />
    ///         inherited from <see cref="object" />, are frozen here.
    ///     </para>
    ///     <para>
    ///         Furthermore, in an ideal world, to keep things under control, there should
    ///         be exactly <strong>1</strong> computer object for each relevant real-world object
    ///         (abstraction function should be a surjection). Duplicate representatives of
    ///         real-world objects make things very hard. Sadly, in very many circumstances,
    ///         we cannot do without multiple representatives. In any case, to guard
    ///         against accidental copy proliferation, semantic objects are non-cloneable.
    ///     </para>
    ///     <para>
    ///         Note that, as a consequence of the above, reference identity nor
    ///         <see cref="Equals" /> can detect whether two semantic objects represent the
    ///         same real-world object or not. If this is a concern, a semantically relevant
    ///         separate method, e.g., <c>RepresentsSame(_T_ other)</c> should be added.
    ///         Furthermore, note that even if another semantic object has exactly the same
    ///         properties, it is not necessarily so that they both represent the same
    ///         real-world object. Indeed, there <em>is</em> at least 1 other person in Belgium
    ///         that has &quot;Jan Dockx&quot; as his name.
    ///     </para>
    ///     <para>
    ///         <see cref="ToString" /> should only be used for debugging and logging
    ///         purposes. For semantic object, <see cref="ToString" /> should return a
    ///         <see cref="string" /> that shows the class name and hash code (like
    ///         <see cref="object.ToString" /> does, followed by a comma-separated list of
    ///         <c>PropertyName = propertyValue</c> entries. Experience shows that this is not
    ///         that hard, but that great care should be taken that no infinite loop is introduced
    ///         in the code when navigating over the object graph. Therefore, the general rule
    ///         should be that <em>upstream</em> reference properties are listed (usually
    ///         to-one associations), but <em>downstream</em> reference properties
    ///         (usually to-many associations) should not.
    ///     </para>
    ///     <para>
    ///         <see cref="AbstractSemanticObject" /> offers hard implementations that
    ///         enforce the above rules. As this interface doesn't really enforce anything through
    ///         the compiler, it can be seen as a tagging interface.
    ///     </para>
    /// </remarks>
    public interface ISemanticObject : INotifyPropertyChanged
    {
        /// <summary>
        ///     Indicates whether this object went through a serialization-deserialization cycle.
        /// </summary>
        bool IsSerialized { get; }

        /// <summary>
        ///     Override to make sealed.
        /// </summary>
        /// <param name="other">The object to compare with.</param>
        /// <returns>
        ///     A boolean indicating whether this object
        ///     is equal to the <paramref name="other" /> object.
        /// </returns>
        bool Equals(object other);

        /// <summary>
        ///     Override to make sealed.
        /// </summary>
        /// <returns>The hash code for this object.</returns>
        int GetHashCode();

        /// <summary>
        ///     Override to return a string representation intended for debugging and logging.
        /// </summary>
        /// <remarks>
        ///     <strong>Never</strong> try to print out a representation of objects reached
        ///     via a to-many association.
        ///     Otherwise you'll get infinite loops.
        ///     You can show the number of elements in an association.
        /// </remarks>
        /// <returns>A string representation of this object.</returns>
        [NotNull]
        string ToString();
    }
}
