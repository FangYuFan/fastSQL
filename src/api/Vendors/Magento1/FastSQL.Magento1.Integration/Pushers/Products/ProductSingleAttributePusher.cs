﻿using FastSQL.Core;
using FastSQL.Sync.Core;
using FastSQL.Sync.Core.Processors;
using FastSQL.Sync.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastSQL.Magento1.Integration.Pushers.Products
{
    public class ProductSingleAttributePusher : BaseAttributePusher
    {
        public ProductSingleAttributePusher(
            ProductSingleAttributePusherOptionManager optionManager,
            ProductProcessor entityProcessor, 
            SingleAttributeProcessor attributeProcessor, 
            FastProvider provider, 
            EntityRepository entityRepository,
            AttributeRepository attributeRepository) : base(optionManager, entityProcessor, attributeProcessor, provider, entityRepository, attributeRepository)
        {
        }

        public override string Create()
        {
            throw new NotImplementedException();
        }

        public override string GetDestinationId()
        {
            throw new NotImplementedException();
        }

        public override string Remove(string destinationId = null)
        {
            throw new NotImplementedException();
        }

        public override string Update(string destinationId = null)
        {
            throw new NotImplementedException();
        }
    }
}
