using JetBrains.Annotations;
using Sukt.Core.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.Shared.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// 基础类型
        /// </summary>
        private static readonly Type[] BasicTypes =
        {
            typeof(bool),

            typeof(sbyte),
            typeof(byte),
            typeof(int),
            typeof(uint),
            typeof(short),
            typeof(ushort),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),

            typeof(Guid),

            typeof(DateTime),// IsPrimitive:False
            typeof(TimeSpan),// IsPrimitive:False
            typeof(DateTimeOffset),

            typeof(char),
            typeof(string),// IsPrimitive:False

            //typeof(object),// IsPrimitive:False
        };

        /// <summary>
        /// get TypeCode for specific type
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static TypeCode GetTypeCode(this Type type) => Type.GetTypeCode(type);

        /// <summary>
        /// 是否是 ValueTuple
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static bool IsValueTuple([NotNull] this Type type)
                => type.IsValueType && type.FullName?.StartsWith("System.ValueTuple`", StringComparison.Ordinal) == true;

        /// <summary>
        /// GetDescription
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static string GetDescription([NotNull] this Type type) =>
            type.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;

        /// <summary>
        /// 判断是否基元类型，如果是可空类型会先获取里面的类型，如 int? 也是基元类型
        /// The primitive types are Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        public static bool IsPrimitiveType([NotNull] this Type type)
            => (Nullable.GetUnderlyingType(type) ?? type).IsPrimitive;

        public static bool IsPrimitiveType<T>() => typeof(T).IsPrimitiveType();

        public static bool IsBasicType([NotNull] this Type type) => BasicTypes.Contains(type) || type.IsEnum;

        public static bool IsBasicType<T>() => typeof(T).IsBasicType();

        public static bool IsBasicType<T>(this T value) => typeof(T).IsBasicType();

        /// <summary>
        /// Finds best constructor, least parameter
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="parameterTypes"></param>
        /// <returns>Matching constructor or default one</returns>
        [CanBeNull]
        public static ConstructorInfo GetConstructor<T>(this Type type, params Type[] parameterTypes)
        {
            if (parameterTypes == null || parameterTypes.Length == 0)
                return GetEmptyConstructor(type);

            var constructors = type.GetConstructors();

            var ctors = constructors
                .OrderBy(c => c.IsPublic ? 0 : (c.IsPrivate ? 2 : 1))
                .ThenBy(c => c.GetParameters().Length)
                .ToArray();

            foreach (var ctor in ctors)
            {
                var parameters = ctor.GetParameters();
                if (parameters.All(p => parameterTypes.Contains(p.ParameterType)))
                {
                    return ctor;
                }
            }

            return null;
        }

        [CanBeNull]
        public static ConstructorInfo GetEmptyConstructor(this Type type)
        {
            var constructors = type.GetConstructors();

            var ctor = constructors.OrderBy(c => c.IsPublic ? 0 : (c.IsPrivate ? 2 : 1))
                .ThenBy(c => c.GetParameters().Length).FirstOrDefault();

            return ctor?.GetParameters().Length == 0 ? ctor : null;
        }

        /// <summary>
        /// Determines whether this type is assignable to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to test assignability to.</typeparam>
        /// <param name="this">The type to test.</param>
        /// <returns>True if this type is assignable to references of type
        /// <typeparamref name="T"/>; otherwise, False.</returns>
        public static bool IsAssignableTo<T>(this Type @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            return typeof(T).IsAssignableFrom(@this);
        }

        /// <summary>
        /// Finds a constructor with the matching type parameters.
        /// </summary>
        /// <param name="type">The type being tested.</param>
        /// <param name="constructorParameterTypes">The types of the contractor to find.</param>
        /// <returns>The <see cref="ConstructorInfo"/> is a match is found; otherwise, <c>null</c>.</returns>
        [CanBeNull]
        public static ConstructorInfo GetMatchingConstructor(this Type type, Type[] constructorParameterTypes)
        {
            if (constructorParameterTypes == null || constructorParameterTypes.Length == 0)
                return GetEmptyConstructor(type);

            return type.GetConstructors().FirstOrDefault(c => c.GetParameters().Select(p => p.ParameterType).SequenceEqual(constructorParameterTypes));
        }

        /// <summary>
        /// Get ImplementedInterfaces
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>当前类型实现的接口的集合。</returns>
        public static IEnumerable<Type> GetImplementedInterfaces([NotNull] this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        /// <summary>
        /// 得到特性下描述
        /// </summary>
        /// <typeparam name="TAttribute">动态特性</typeparam>
        /// <param name="member"></param>
        /// <returns></returns>
        public static string ToDescription<TAttribute>(this MemberInfo member)
            where TAttribute : AttributeBase
        {
            var attributeBase = (AttributeBase)member.GetCustomAttribute<TAttribute>();
            if (!attributeBase.IsNull())
            {
                return attributeBase.Description();
            }
            return member.Name;
        }

        public static bool HasMatchingGenericArity(this Type interfaceType, TypeInfo typeInfo)
        {
            if (typeInfo.IsGenericType)
            {
                var interfaceTypeInfo = interfaceType.GetTypeInfo();

                if (interfaceTypeInfo.IsGenericType)
                {
                    var argumentCount = interfaceType.GenericTypeArguments.Length;
                    var parameterCount = typeInfo.GenericTypeParameters.Length;

                    return argumentCount == parameterCount;
                }

                return false;
            }

            return true;
        }

        public static Type GetRegistrationType(this Type interfaceType, TypeInfo typeInfo)
        {
            if (typeInfo.IsGenericTypeDefinition)
            {
                var interfaceTypeInfo = interfaceType.GetTypeInfo();

                if (interfaceTypeInfo.IsGenericType)
                {
                    return interfaceType.GetGenericTypeDefinition();
                }
            }

            return interfaceType;
        }

        /// <summary>
        /// 是原始的扩展包括空
        /// </summary>
        /// <param name="type"></param>
        /// <param name="includeEnums"></param>
        /// <returns></returns>
        public static bool IsPrimitiveExtendedIncludingNullable(this Type type, bool includeEnums = false)
        {
            if (IsPrimitiveExtended(type, includeEnums)) return true;

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return IsPrimitiveExtended(type.GenericTypeArguments[0], includeEnums);

            return false;
        }

        private static bool IsPrimitiveExtended(Type type, bool includeEnums)
        {
            if (type.GetTypeInfo().IsPrimitive) return true;

            if (includeEnums && type.GetTypeInfo().IsEnum) return true;

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }
    }
}