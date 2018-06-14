﻿using FastSQL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastSQL.Magento2.Integration.Pushers.Products
{
    public class ProductSingleAttributePusherOptionManager : BaseOptionManager
    {
        public override IEnumerable<OptionItem> GetOptionsTemplate()
        {
            return new List<OptionItem>();
        }
    }
}
