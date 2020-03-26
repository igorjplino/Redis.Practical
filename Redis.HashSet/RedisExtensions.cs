using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Redis.HashSet
{
    public static class RedisExtensions
    {
        public static IEnumerable<HashEntry> ConvertToHashEntries<T>(this T obj)
        {
            var properties = obj.GetType().GetProperties();

            List<HashEntry> entries = null;

            foreach (var property in properties)
            {
                if (property.GetValue(obj) != null)
                {
                    if (property.GetType().IsClass)
                    {
                        var jsonObj = JsonConvert.SerializeObject(property);
                        entries.Add(new HashEntry(property.Name, jsonObj));
                    }
                    else
                    {
                        entries.Add(new HashEntry(property.Name, property.GetValue(obj).ToString()));
                    }
                }
            }

            return entries;
        }

        public static T ConvertFromHashEntry<T>(T obj, HashEntry[] entries)
        {
            var properties = obj.GetType().GetProperties();
            var hashDict = entries.ToDictionary();

            foreach (var property in properties)
            {
                if (!hashDict.TryGetValue(property.Name, out RedisValue hashPair))
                {
                    continue;
                }

                var type = property.PropertyType;
                var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
                TypeCode typeCode = Type.GetTypeCode(underlyingType);

                switch (typeCode)
                {
                    case (TypeCode.Int32):

                        if (type.IsEnum && Enum.TryParse(type, hashPair, out object enumIntResult))
                        {
                            property.SetValue(obj, enumIntResult);
                        } 
                        else if (int.TryParse(hashPair, out int intResult))
                        {
                            property.SetValue(obj, intResult);
                        }
                        break;
                    case (TypeCode.Int64):
                        if (type.IsEnum && Enum.TryParse(type, hashPair, out object enumLongResult))
                        {
                            property.SetValue(obj, enumLongResult);
                        }
                        else if (long.TryParse(hashPair, out long longResult))
                        {
                            property.SetValue(obj, longResult);
                        }
                        break;
                    case (TypeCode.Double):

                        if (double.TryParse(hashPair, out double doubleResult))
                        {
                            property.SetValue(obj, doubleResult);
                        }
                        break;
                    case (TypeCode.DateTime):

                        if (DateTime.TryParse(hashPair, out DateTime date))
                        {
                            property.SetValue(obj, date);
                        }
                        break;
                    case (TypeCode.Boolean):

                        if (bool.TryParse(hashPair, out bool boolResult))
                        {
                            property.SetValue(obj, boolResult);
                        }
                        break;

                    case (TypeCode.String):

                        property.SetValue(obj, (string)hashPair);
                        break;
                    case (TypeCode.Object):

                        try
                        {
                            var jsonObj = JsonConvert.DeserializeObject(hashPair, type);
                            property.SetValue(obj, jsonObj);
                        }
                        catch { }
                        break;
                    default:
                        break;
                }
            }

            return obj;
        }
    }
}
