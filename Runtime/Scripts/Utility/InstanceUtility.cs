using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class InstanceUtility
{
    public static Dictionary<Type, T> Create<T>() 
    {
        var _result = new Dictionary<Type, T>();

        var _baseType = typeof(T);
        var _assembly = Assembly.GetAssembly(_baseType);

        var _derivedTypes = _assembly.GetTypes()
            .Where(type => type.IsSubclassOf(_baseType) && !type.IsAbstract)
            .ToList();

        foreach (var _type in _derivedTypes)
        {
            if (Activator.CreateInstance(_type) is T _instance)
            {
                _result.Add(_type, _instance);
            }
        }

        return _result;
    }
}
