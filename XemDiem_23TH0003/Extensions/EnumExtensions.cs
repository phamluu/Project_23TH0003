using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace XemDiem_23TH0003.Lib
{
    
    public static class EnumExtensions
    {

        public static string GetDisplayName(this Enum enumValue)
        {
            var attribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();

            return attribute?.Name ?? enumValue.ToString();
        }
    }
}
