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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace PPWCode.Vernacular.Semantics.III
{
    /// <summary>
    ///     Abstract class that supports things
    ///     required by <see cref="ISemanticObject" />.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    [SuppressMessage("ReSharper", "BaseObjectEqualsIsObjectEquals", Justification = "Reviewed")]
    public abstract class AbstractSemanticObject : ISemanticObject
    {
        private bool _isSerialized;

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor", Justification = "Reviewed")]
        protected AbstractSemanticObject()
        {
            Initialize(false);
        }

        /// <summary>
        ///     Is raised whenever a property is changed, part of the INotifyPropertyChanged interface.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public sealed override bool Equals(object obj)
            => base.Equals(obj);

        [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode", Justification = "Reviewed")]
        public sealed override int GetHashCode()
            => base.GetHashCode();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            sb.Append("{ ");
            sb.AppendFormat("HashCode = '{0}'", GetHashCode());

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                sb.AppendFormat(", {0} = ", prop.Name);
                object value;

                try
                {
                    value = prop.GetValue(this, null);
                }
                catch (Exception e)
                {
                    value = e.GetBaseException().Message;
                }

                if (value == null)
                {
                    sb.Append("[null]");
                }
                else if (value is string)
                {
                    sb.AppendFormat("'{0}'", value);
                }
                else if (value is IEnumerable)
                {
                    if (value is ICollection collection)
                    {
                        sb.AppendFormat("[{0} elements]", collection.Count);
                    }
                    else
                    {
                        sb.AppendFormat("[? elements]");
                    }
                }
                else if (value is AbstractSemanticObject o)
                {
                    sb.Append(o.LimitedToString());
                }
                else
                {
                    sb.AppendFormat("'{0}'", value);
                }
            }

            sb.Append(" }");

            return sb.ToString();
        }

        public bool IsSerialized
            => _isSerialized;

        protected virtual void Initialize(bool onDeserializing)
        {
            _isSerialized = onDeserializing;
        }

        /// <summary>
        ///     Triggers the <see cref="PropertyChanged" /> event if this event is assigned.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     This method returns a limited string representation
        ///     of this object, intended for debugging purposes.
        /// </summary>
        /// <returns>
        ///     String representation of the object intended for
        ///     debugging.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The method is used in <see cref="ToString" />,
        ///         when this refers to other <c>AbstractSemanticObject</c>
        ///         instances via a simple reference.
        ///     </para>
        ///     <para>
        ///         The default implementation of this method does
        ///         not include representations of other objects, but only
        ///         a representation of the type and the <see cref="GetHashCode" />.
        ///     </para>
        ///     <para>
        ///         Subclasses can override this method to return
        ///         a simple string representation that better identifies this
        ///         object.
        ///     </para>
        /// </remarks>
        public virtual string LimitedToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append("{ ");
            sb.AppendFormat("HashCode = '{0}'", GetHashCode());
            sb.Append(" }");
            return sb.ToString();
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Initialize(true);
        }
    }
}
