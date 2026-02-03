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

namespace PPWCode.Vernacular.Semantics.V;

/// <summary>
///     Interface used to represent an object that may or may not contain meaningful
///     data.
/// </summary>
/// <remarks>
///     This interface is used to model components that are always present in the
///     system (for example, due to persistence or relational constraints), but
///     whose underlying data may be absent.
/// </remarks>
public interface IDataContainer
{
    /// <summary>
    ///     Indicates whether the object contains meaningful data.
    /// </summary>
    bool IsDataPresent { get; }
}
