using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akel.Domain.Core
{
    public class SampleFilterModel : FilterModelBase
    {
        

        public SampleFilterModel() : base()
        {
            this.Limit = 3;
        }


        public override object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }
    }
}
