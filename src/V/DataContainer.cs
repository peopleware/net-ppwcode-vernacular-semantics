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
///     Class that can be used as a base for <see cref="IDataContainer" /> objects.
/// </summary>
public abstract class DataContainer
    : CivilizedObject,
      IDataContainer
{
    /// <inheritdoc />
    public abstract bool IsDataPresent { get; }

    /// <inheritdoc />
    public sealed override CompoundSemanticException WildExceptions()
        => IsDataPresent
               ? WildExceptionsForPresentData()
               : new CompoundSemanticException();

    /// <summary>
    ///     Method to implement <see cref="ICivilizedObject.WildExceptions" /> for
    ///     the data that is hold by this container (<see cref="IDataContainer" />).
    /// </summary>
    /// <returns>
    ///     An instance of <see cref="CompoundSemanticException" />.
    /// </returns>
    protected virtual CompoundSemanticException WildExceptionsForPresentData()
        => base.WildExceptions();
}
