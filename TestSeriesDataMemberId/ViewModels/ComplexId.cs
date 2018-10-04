using System;
using System.Collections;

namespace TestSeriesDataMemberId.ViewModels
{
    public class ComplexId : IEquatable<ComplexId>, IComparable<ComplexId>, IComparable
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }

        public override string ToString()
        {
            return Key1;
        }

        #region Equality operators
        public static bool operator ==(ComplexId complexId1, ComplexId complexId2)
        {
            if (ReferenceEquals(complexId1, null))
            {
                return ReferenceEquals(complexId2, null);
            }

            return complexId1.Equals(complexId2);
        }

        public static bool operator !=(ComplexId complexId1, ComplexId complexId2)
        {
            if (ReferenceEquals(complexId1, null))
            {
                return !ReferenceEquals(complexId2, null);
            }

            return !complexId1.Equals(complexId2);
        }
        #endregion


        #region Equality
        public bool Equals(ComplexId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Key1, other.Key1) && string.Equals(Key2, other.Key2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ComplexId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Key1 != null ? Key1.GetHashCode() : 0) * 397) ^ (Key2 != null ? Key2.GetHashCode() : 0);
            }
        }
        #endregion

        #region Comparable
        public int CompareTo(ComplexId other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var key1Comparison = string.Compare(Key1, other.Key1, StringComparison.Ordinal);
            if (key1Comparison != 0) return key1Comparison;
            return string.Compare(Key2, other.Key2, StringComparison.Ordinal);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is ComplexId other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(ComplexId)}");
        }
        #endregion
    }
}