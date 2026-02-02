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

/// <summary>
///     A type that formalizes how to work with objects that represent
///     real-world objects during only a part of their life cycle.
///     <para>
///         For several reasons, some of which are technical, some of
///         which are best practices, some of which are more design related,
///         classes often have only a default constructor. On the other hand
///         often some properties cannot be given a semantically acceptable value
///         at instantiation. This triggers a pattern where instances of the
///         semantic class can exist in a state where it does not represent a
///         real-world object of the type the class is intended for, i.e.,
///         the instances do not conform to type invariants that would apply
///         direct representations of the real-world objects. Such objects
///         are created in a <em>wild</em> state, then
///         go through a setup phase where a number of properties are set,
///         and then enter a lifecycle phase where they do represent a
///         real-world object of the type the class is intended for (they become
///         <em>civilized</em>).
///         Typically, by changing one or more properties, such objects
///         can also leave the civilized state, which typically happens before
///         the object is terminated.
///     </para>
///     <para>This type offers a number of methods to support this pattern.</para>
///     <para>
///         Normally, invariants are specified and enforced as much as possible.
///         This is possible for all properties for which there exists a
///         <em>civilized</em> default value that can be set in the default
///         constructor. Typically, this is at least not possible with
///         associations, if the association is mandatory.
///     </para>
///     <para>
///         The extra rules that should apply in a <em>civilized</em> state
///         can be checked by calling <see cref="WildExceptions" />.
///         <see cref="IsCivilized" /> gives a simple boolean answer about the state
///         of the <c>ICivilizedObject</c>.
///     </para>
/// </summary>
public interface ICivilizedObject
{
    /// <summary>
    ///     Calls <see cref="WildExceptions" /> and returns the value of
    ///     the  <see cref="CompoundSemanticException.IsEmpty" /> property
    ///     of the <see cref="CompoundSemanticException" /> result.
    /// </summary>
    bool IsCivilized { get; }

    /// <summary>
    ///     <para>
    ///         Creates an instance of <see cref="CompoundSemanticException" />
    ///         and adds a <see cref="SemanticException" /> for every failing
    ///         validation of the object in its current state.
    ///     </para>
    ///     <para>
    ///         This method is called to validate an object.
    ///     </para>
    /// </summary>
    /// <returns>
    ///     An <strong>unclosed</strong> (see <see cref="CompoundSemanticException.Closed" />)
    ///     instance of <see cref="CompoundSemanticException" />.
    /// </returns>
    /// <remarks>
    ///     This method always returns an instance of <see cref="CompoundSemanticException" />.
    ///     In case of successful validation (no validation errors) the property
    ///     <see cref="CompoundSemanticException.IsEmpty" /> is <c>true</c>.
    /// </remarks>
    CompoundSemanticException WildExceptions();

    /// <summary>
    ///     Call <see cref="WildExceptions" />, and if the property
    ///     <see cref="CompoundSemanticException.IsEmpty" /> of the
    ///     returned <see cref="CompoundSemanticException" /> instance is <c>false</c>,
    ///     close the exception and throw it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method has no side effects. If it ends nominally,
    ///         and if it throws an exception, no state is changed.
    ///     </para>
    ///     <para>
    ///         It is not <c>[Pure]</c> however, since it changes
    ///         the state of the exception to
    ///         <see cref="CompoundSemanticException.Closed" />.
    ///     </para>
    /// </remarks>
    void ThrowIfNotCivilized();
}
