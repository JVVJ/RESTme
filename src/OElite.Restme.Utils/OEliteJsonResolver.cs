﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OElite
{
    public class OEliteJsonResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = null;
            try
            {
                property = base.CreateProperty(member, memberSerialization);
                if (typeof(Stream).IsAssignableFrom(property.PropertyType))
                {
                    property.Ignored = true;
                }
            }
            catch
            {
                property = new JsonProperty();
            }
            return property;
        }
    }
}