﻿using FastSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSQL.Magento2.Integration.Mappers
{
    public class ParentCategoryMapperOptionManager : BaseOptionManager
    {
        public override IEnumerable<OptionItem> GetOptionsTemplate()
        {
            return new List<OptionItem>();
        }
    }
}
