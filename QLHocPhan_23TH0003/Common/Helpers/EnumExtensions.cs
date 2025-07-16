using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType()
                              .GetMember(enumValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                {
                    return attr.Name;
                }
            }

            return ""; 
        }
    }
}
