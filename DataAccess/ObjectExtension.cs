﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageTetst.DataAccess
{
    public static class ObjectExtension
    {
        public static T CopyObject<T>(this object objSource)
        {
            var serialized = JsonConvert.SerializeObject(objSource);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
