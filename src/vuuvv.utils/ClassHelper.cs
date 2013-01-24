using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace vuuvv.utils
{
    public static class ClassHelper
    {
        public static T Call<T>(object obj, string method, params object[] args)
        {
            Type t = obj.GetType();
            return (T)t.GetMethod(method).Invoke(obj, args);
        }

        public static T StaticCall<T>(object obj, string method, params object[] args)
        {
            Type t = obj.GetType();
            return (T)t.GetMethod(method).Invoke(null, args);
        }

        public static T StaticCall<T>(string type_name, string method, params object[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin/site.dll");
            Assembly assembly = Assembly.LoadFile(path);
            Type t = assembly.GetType(type_name);
            return (T)t.GetMethod(method).Invoke(null, args);
        }

        public static T Field<T>(object obj, string name)
        {
            Type t = obj.GetType();
            return (T)t.GetField(name).GetValue(obj);
        }

        public static void Field(object obj, string name, object value)
        {
            Type t = obj.GetType();
            t.GetField(name).SetValue(obj, value);
        }

        public static T StaticField<T>(object obj, string name)
        {
            Type t = obj.GetType();
            return (T)t.GetField(name).GetValue(null);
        }

        public static void StaticField(object obj, string name, object value) 
        {
            Type t = obj.GetType();
            t.GetField(name).SetValue(null, value);
        }

        public static T Property<T>(object obj, string name)
        {
            Type t = obj.GetType();
            return (T)t.GetProperty(name).GetValue(obj, null);
        }

        public static void Property(object obj, string name, object value)
        {
            Type t = obj.GetType();
            t.GetProperty(name).SetValue(obj, value, null);
        }

        public static T StaticProperty<T>(object obj, string name)
        {
            Type t = obj.GetType();
            return (T)t.GetProperty(name).GetValue(null, null);
        }

        public static void StaticProperty(object obj, string name, object value)
        {
            Type t = obj.GetType();
            t.GetProperty(name).SetValue(null, value, null);
        }

        public static bool IsNullable(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetNullableType(Type t)
        {
            return t.GetGenericArguments()[0];
        }
    }
}
