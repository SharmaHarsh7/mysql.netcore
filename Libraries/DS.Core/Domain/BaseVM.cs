using System;
using System.Collections.Generic;
using System.ComponentModel;
using DS.Core.Attributes;
using DS.Core.Enums;
using Newtonsoft.Json;

namespace DS.Core.Domain
{
    public abstract class BaseVM 
    {

        [DSFieldDescription(Enums.DataType.Boolean, "Is Published", true)]
        public bool IsPublished { get; set; }

    }
}
