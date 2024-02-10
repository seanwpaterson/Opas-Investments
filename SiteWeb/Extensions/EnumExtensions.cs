using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Extensions;

public static class EnumExtensions
{
    public static string GetName(this Enum genericEnum)
    {
        var genericEnumType = genericEnum.GetType();

        var memberInfo = genericEnumType.GetMember(genericEnum.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attrs != null && attrs.Count() > 0)
            {
                return ((DisplayAttribute)attrs[0]).Name!.ToString();
            }
        }

        return genericEnum.ToString();
    }

    public static string GetDescription(this Enum genericEnum)
    {
        var genericEnumType = genericEnum.GetType();

        var memberInfo = genericEnumType.GetMember(genericEnum.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attrs != null && attrs.Count() > 0)
            {
                return ((System.ComponentModel.DescriptionAttribute)attrs[0]).Description;
            }
        }

        return genericEnum.ToString();
    }
}
