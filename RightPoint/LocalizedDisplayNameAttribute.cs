using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.Reflection;

namespace RightPoint
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId)
            : base()
        {

            GetMessageFromResource(resourceId);
        }

        private static string GetMessageFromResource(string resourceId)
        {
            if (RPC.Common.Language.ToLower().Trim().Contains("persian"))
            {
                PropertyInfo nameProperty = typeof(Resource_fa).GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public);
                // TODO: Return the string from the resource file           
                if (nameProperty == null)
                {
                    return String.Empty;
                }
                return (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }
            else
            {
                PropertyInfo nameProperty = typeof(Resource).GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public);
                // TODO: Return the string from the resource file           
                if (nameProperty == null)
                {
                    return String.Empty;
                }
                return (string)nameProperty.GetValue(nameProperty.DeclaringType, null);
            }
        }
    }

}