using System.Reflection;

namespace BerorinApp
{
    public static class UtilExtension
    {
        /// <summary>
        /// バイト配列の「0」のデータが続く末尾を取り除いた要素を返します。
        /// </summary>
        public static byte[] TrimEnd(this byte[] value)
        {
            var count = value.Reverse().SkipWhile(x => x == 0).Count();
            return value.Take(count == 0 ? 1 : count).ToArray();
        }

        /// <summary>
        /// 指定したカスタム属性が設定されている場合は true を返します。
        /// </summary>
        public static bool AnyCustomAttribute<T>(this MemberInfo info) where T : Attribute
        {
            return info.GetCustomAttributes(typeof(T), false).Any();
        }

        /// <summary>
        /// 指定したカスタム属性を取り出します。
        /// </summary>
        public static T FirstCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
        }

        /// <summary>
        /// メソッドがオーバーライドしたものなら true を返します。
        /// </summary>
        public static bool IsOverrided(this MethodInfo method)
        {
            return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
        }

        public static T GetBoundItem<T>(this DataGridViewRow row)
        {
            return (T)row.DataBoundItem;
        }

        /// <summary>
        /// プロパティの型がnull許容型の場合、秘匿された型を返します。
        /// </summary>
        public static Type TrueType(this PropertyInfo prop)
        {
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return prop.PropertyType.GenericTypeArguments[0];
            }
            else
            {
                return prop.PropertyType;
            }
        }
    }
}
