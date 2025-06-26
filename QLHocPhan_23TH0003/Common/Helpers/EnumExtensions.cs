using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QLHocPhan_23TH0003.Common.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            ?.Name ?? enumValue.ToString();
        }
    }
}
