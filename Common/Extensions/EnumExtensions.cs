using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;

namespace Common.Extensions
{
    public static class EnumExtensions
    {

        public static string GetEnumName(this Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            DisplayAttribute displayAttribute = ((DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), inherit: false)).FirstOrDefault();
            if (displayAttribute == null)
            {
                return string.Empty;
            }

            DisplayAttribute displayAttribute2 = displayAttribute;
            if (displayAttribute2.ResourceType == null)
            {
                return displayAttribute.Name;
            }

            return new ResourceManager(displayAttribute2.ResourceType.FullName, displayAttribute2.ResourceType.GetTypeInfo().Assembly).GetString(displayAttribute2.Name);
        }

        public static string ToValueString(this Enum e)
        {
            if (e == null)
            {
                return "";
            }

            object obj = Enum.Parse(e.GetType(), e.ToString());
            try
            {
                return ((int)obj).ToString();
            }
            catch
            {
            }

            try
            {
                return ((byte)obj).ToString();
            }
            catch
            {
            }

            throw new Exception("轉換失敗");
        }

        public static string GetEnumGroupName(this Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            DisplayAttribute[] array = (DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
            if (array.Length == 0)
            {
                return string.Empty;
            }

            return array[0].GroupName;
        }

        public static string GetEnumDescription(this Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            DisplayAttribute[] array = (DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
            if (array.Length == 0)
            {
                return string.Empty;
            }

            return array[0].Description;
        }

        public static string GetEnumShortName(this Enum e)
        {
            if (e == null)
            {
                return string.Empty;
            }

            DisplayAttribute[] array = (DisplayAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DisplayAttribute), inherit: false);
            if (array.Length == 0)
            {
                return string.Empty;
            }

            return array[0].ShortName;
        }

        public static T GetAttribute<T>(this Enum e) where T : Attribute
        {
            FieldInfo[] fields = e.GetType().GetFields();
            foreach (FieldInfo fieldInfo in fields)
            {
                T val = (T)Attribute.GetCustomAttribute(fieldInfo, typeof(T));
                if (val != null && fieldInfo.Name == e.ToString())
                {
                    return val;
                }
            }

            return null;
        }
    }
}
