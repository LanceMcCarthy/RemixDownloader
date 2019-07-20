using RemixDownloader.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RemixDownloader.Core.Utilities
{
    public static class EnumHelpers
    {
        public static List<string> ReadibleAssetOptimizationTypes()
        {
            var list = new List<string>();

            foreach (AssetOptimizationType assetType in Enum.GetValues(typeof(AssetOptimizationType)))
            {
                list.Add(assetType.GetDescription());
            }

            return list;
        }

        public static string GetDescription(this Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();

            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (attributes != null && attributes.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }
    }
}
