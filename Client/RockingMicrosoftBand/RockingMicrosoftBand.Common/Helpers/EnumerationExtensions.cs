using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RockingMicrosoftBand.Common.Helpers
{
    public static class EnumerationExtensions
    {
        public static T Add<T>(this Enum type, T value)
        {
            return InvocationHelper.ExecuteWithIgnoreException<T>(() =>
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            });
        }

    

        public static bool Has<T>(this Enum type, T value)
        {
            return InvocationHelper.ExecuteWithIgnoreException<bool>(() =>
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
                                                                     ,
                () =>
                {
                    return false;
                });
        }

    
        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name), ex);
            }
        }

        public static List<EnumType> All<EnumType>()
        {
            var allValues = (EnumType[])Enum.GetValues(typeof(EnumType));
            var output = allValues.ToList();
            return output;
        }
        public static EnumType ToEnum<EnumType>(this String enumValue) where EnumType : struct
        {
            EnumType myreturn;
            if (Enum.TryParse<EnumType>(enumValue, out myreturn))
            {
                return myreturn;
            }
            return default(EnumType);
        }

        public static String ToStringFromEnum(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }
    }
}
