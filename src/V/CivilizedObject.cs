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

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using PPWCode.Vernacular.Exceptions.V;

namespace PPWCode.Vernacular.Semantics.V;

public abstract class CivilizedObject : ICivilizedObject
{
    /// <inheritdoc />
    [JsonIgnore]
    public virtual bool IsCivilized
        => WildExceptions().IsEmpty;

    /// <inheritdoc />
    public virtual CompoundSemanticException WildExceptions()
    {
        CompoundSemanticException result = new();
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        if (Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true))
        {
            return result;
        }

        foreach (ValidationResult validationResult in validationResults)
        {
            result.AddElement(new ValidationViolationException(validationResult));
        }

        return result;
    }

    /// <inheritdoc />
    public virtual void ThrowIfNotCivilized()
    {
        CompoundSemanticException cse = WildExceptions();
        if (!cse.IsEmpty)
        {
            cse.Close();
            throw cse;
        }
    }
}
