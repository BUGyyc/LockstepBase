using System;
using System.Collections.Generic;
using System.Text;

namespace Entitas
{
    public class Matcher<TEntity> : IAllOfMatcher<TEntity>, IAnyOfMatcher<TEntity>, INoneOfMatcher<TEntity>, ICompoundMatcher<TEntity>, IMatcher<TEntity> where TEntity : class, IEntity
    {
        private int[] _indices;

        private int[] _allOfIndices;

        private int[] _anyOfIndices;

        private int[] _noneOfIndices;

        private string _toStringCache;

        private StringBuilder _toStringBuilder;

        private int _hash;

        private bool _isHashCached;

        private static readonly List<int> _indexBuffer = new List<int>();

        private static readonly HashSet<int> _indexSetBuffer = new HashSet<int>();

        public int[] indices
        {
            get
            {
                if (_indices == null)
                {
                    _indices = mergeIndices(_allOfIndices, _anyOfIndices, _noneOfIndices);
                }
                return _indices;
            }
        }

        public int[] allOfIndices => _allOfIndices;

        public int[] anyOfIndices => _anyOfIndices;

        public int[] noneOfIndices => _noneOfIndices;

        public string[] componentNames;

        public void set_componentNames(string[] arr)
        {
            this.componentNames = arr;
        }

        private Matcher()
        {
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params int[] indices)
        {
            _anyOfIndices = distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params IMatcher<TEntity>[] matchers)
        {
            return ((IAllOfMatcher<TEntity>)this).AnyOf(mergeIndices(matchers));
        }

        public INoneOfMatcher<TEntity> NoneOf(params int[] indices)
        {
            _noneOfIndices = distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        public INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers)
        {
            return NoneOf(mergeIndices(matchers));
        }

        public bool Matches(TEntity entity)
        {
            if ((_allOfIndices == null || entity.HasComponents(_allOfIndices)) && (_anyOfIndices == null || entity.HasAnyComponent(_anyOfIndices)))
            {
                if (_noneOfIndices != null)
                {
                    return !entity.HasAnyComponent(_noneOfIndices);
                }
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                if (_toStringBuilder == null)
                {
                    _toStringBuilder = new StringBuilder();
                }
                _toStringBuilder.Length = 0;
                if (_allOfIndices != null)
                {
                    appendIndices(_toStringBuilder, "AllOf", _allOfIndices, componentNames);
                }
                if (_anyOfIndices != null)
                {
                    if (_allOfIndices != null)
                    {
                        _toStringBuilder.Append(".");
                    }
                    appendIndices(_toStringBuilder, "AnyOf", _anyOfIndices, componentNames);
                }
                if (_noneOfIndices != null)
                {
                    appendIndices(_toStringBuilder, ".NoneOf", _noneOfIndices, componentNames);
                }
                _toStringCache = _toStringBuilder.ToString();
            }
            return _toStringCache;
        }

        private static void appendIndices(StringBuilder sb, string prefix, int[] indexArray, string[] componentNames)
        {
            sb.Append(prefix);
            sb.Append("(");
            int num = indexArray.Length - 1;
            for (int i = 0; i < indexArray.Length; i++)
            {
                int num2 = indexArray[i];
                if (componentNames == null)
                {
                    sb.Append(num2);
                }
                else
                {
                    sb.Append(componentNames[num2]);
                }
                if (i < num)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(")");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType() || obj.GetHashCode() != GetHashCode())
            {
                return false;
            }
            Matcher<TEntity> matcher = (Matcher<TEntity>)obj;
            if (!equalIndices(matcher.allOfIndices, _allOfIndices))
            {
                return false;
            }
            if (!equalIndices(matcher.anyOfIndices, _anyOfIndices))
            {
                return false;
            }
            if (!equalIndices(matcher.noneOfIndices, _noneOfIndices))
            {
                return false;
            }
            return true;
        }

        private static bool equalIndices(int[] i1, int[] i2)
        {
            if (i1 == null != (i2 == null))
            {
                return false;
            }
            if (i1 == null)
            {
                return true;
            }
            if (i1.Length != i2.Length)
            {
                return false;
            }
            for (int j = 0; j < i1.Length; j++)
            {
                if (i1[j] != i2[j])
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            if (!_isHashCached)
            {
                int hashCode = GetType().GetHashCode();
                hashCode = applyHash(hashCode, _allOfIndices, 3, 53);
                hashCode = applyHash(hashCode, _anyOfIndices, 307, 367);
                hashCode = (_hash = applyHash(hashCode, _noneOfIndices, 647, 683));
                _isHashCached = true;
            }
            return _hash;
        }

        private static int applyHash(int hash, int[] indices, int i1, int i2)
        {
            if (indices != null)
            {
                for (int j = 0; j < indices.Length; j++)
                {
                    hash ^= indices[j] * i1;
                }
                hash ^= indices.Length * i2;
            }
            return hash;
        }

        public static IAllOfMatcher<TEntity> AllOf(params int[] indices)
        {
            return new Matcher<TEntity>
            {
                _allOfIndices = distinctIndices(indices)
            };
        }

        public static IAllOfMatcher<TEntity> AllOf(params IMatcher<TEntity>[] matchers)
        {
            Matcher<TEntity> obj = (Matcher<TEntity>)AllOf(mergeIndices(matchers));
            setComponentNames(obj, matchers);
            return obj;
        }

        public static IAnyOfMatcher<TEntity> AnyOf(params int[] indices)
        {
            return new Matcher<TEntity>
            {
                _anyOfIndices = distinctIndices(indices)
            };
        }

        public static IAnyOfMatcher<TEntity> AnyOf(params IMatcher<TEntity>[] matchers)
        {
            Matcher<TEntity> obj = (Matcher<TEntity>)AnyOf(mergeIndices(matchers));
            setComponentNames(obj, matchers);
            return obj;
        }

        private static int[] mergeIndices(int[] allOfIndices, int[] anyOfIndices, int[] noneOfIndices)
        {
            if (allOfIndices != null)
            {
                _indexBuffer.AddRange(allOfIndices);
            }
            if (anyOfIndices != null)
            {
                _indexBuffer.AddRange(anyOfIndices);
            }
            if (noneOfIndices != null)
            {
                _indexBuffer.AddRange(noneOfIndices);
            }
            int[] result = distinctIndices(_indexBuffer);
            _indexBuffer.Clear();
            return result;
        }

        private static int[] mergeIndices(IMatcher<TEntity>[] matchers)
        {
            int[] array = new int[matchers.Length];
            for (int i = 0; i < matchers.Length; i++)
            {
                IMatcher<TEntity> matcher = matchers[i];
                if (matcher.indices.Length != 1)
                {
                    throw new MatcherException(matcher.indices.Length);
                }
                array[i] = matcher.indices[0];
            }
            return array;
        }

        private static string[] getComponentNames(IMatcher<TEntity>[] matchers)
        {
            for (int i = 0; i < matchers.Length; i++)
            {
                if (matchers[i] is Matcher<TEntity> matcher && matcher.componentNames != null)
                {
                    return matcher.componentNames;
                }
            }
            return null;
        }

        private static void setComponentNames(Matcher<TEntity> matcher, IMatcher<TEntity>[] matchers)
        {
            string[] array = getComponentNames(matchers);
            if (array != null)
            {
                matcher.componentNames = array;
            }
        }

        private static int[] distinctIndices(IList<int> indices)
        {
            foreach (int index in indices)
            {
                _indexSetBuffer.Add(index);
            }
            int[] array = new int[_indexSetBuffer.Count];
            _indexSetBuffer.CopyTo(array);
            Array.Sort(array);
            _indexSetBuffer.Clear();
            return array;
        }
    }
}