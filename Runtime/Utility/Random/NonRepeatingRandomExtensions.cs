using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Random = UnityEngine.Random;

namespace Gamecore
{
    public static class NonRepeatingRandomExtensions
    {
        private static Dictionary<int, bool[]> arrayUsageStates = new();
        
        public static T GetNonRepeatingRandom<T>(this IEnumerable<T> enumerable)
        {
            var _key = RuntimeHelpers.GetHashCode(enumerable);
            return enumerable.ToArray().GetNonRepeatingRandom(_key);
        }
        public static T GetNonRepeatingRandom<T>(this T[] origins)
        {
            var _key = RuntimeHelpers.GetHashCode(origins);
            return origins.GetNonRepeatingRandom(_key);
        }
        private static T GetNonRepeatingRandom<T>(this T[] origins, int key)
        {
            if (origins == null || origins.Length == 0)
                return default;
            
            if (!arrayUsageStates.ContainsKey(key))
            {
                arrayUsageStates.Add(key, new bool[origins.Length]);
            }
    
            var _values = arrayUsageStates[key];
            List<int> _reValues = new();
            for (int i = 0; i < _values.Length; i++)
            {
                if (!_values[i])
                {
                    _reValues.Add(i);
                }
            }
    
            var _randomElement = Random.Range(0, _reValues.Count);
            var _resultIndex = _reValues[_randomElement];
            _values[_resultIndex] = true;
    
            if (_values.All(value => value))
            {
                for (int i = 0; i < _values.Length; i++)
                {
                    if (i == _resultIndex) continue;
                    _values[i] = false;
                }
            }
    
            return origins[_resultIndex];
        }
        public static void SetValueNonRepeatingRandom<T>(this T[] origins, int index, bool value)
        {
            if (origins == null || origins.Length == 0) return;
            var _key = RuntimeHelpers.GetHashCode(origins);
            if (!arrayUsageStates.ContainsKey(_key))
            {
                arrayUsageStates.Add(_key, new bool[origins.Length]);
            }
            
            var _values = arrayUsageStates[_key];
            if (index < 0 || index >= _values.Length) return;
            
            _values[index] = value;
        }
    }
}

