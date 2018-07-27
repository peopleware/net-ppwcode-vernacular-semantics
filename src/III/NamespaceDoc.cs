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

namespace PPWCode.Vernacular.Semantics.III
{
    /// <summary>
    ///     Library expressing the PPWCode Semantics Vernacular.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="ISemanticObject" /> is the basis of our vernacular.
    ///         It expresses that semantic objects should be used by reference, and
    ///         that they should feature a <see cref="ISemanticObject.ToString" />
    ///         method aimed at debugging and logging. <see cref="AbstractSemanticObject" />
    ///         provides help in implementing these rules.
    ///     </para>
    ///     <para>
    ///         <see cref="IRousseauObject" /> introduces the notion of
    ///         <em>civilized</em> and <em>wild</em> states for semantic objects, making
    ///         explicit the difference in state that is acceptable for naked computer
    ///         objects, and the state that is acceptable when those computer objects
    ///         truly represent a domain object. This depends on the vernacular for
    ///         semantic exceptions explained in <see cref="PPWCode.Vernacular.Exceptions.II" />.
    ///         <see cref="AbstractRousseauObject" />
    ///         provides help in implementing this contract.
    ///     </para>
    /// </remarks>
    internal sealed class NamespaceDoc
    {
    }
}
