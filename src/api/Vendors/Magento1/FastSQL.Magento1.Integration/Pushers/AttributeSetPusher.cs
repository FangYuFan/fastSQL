﻿using FastSQL.Core;
using FastSQL.Sync.Core;
using FastSQL.Sync.Core.Enums;
using FastSQL.Sync.Core.Processors;
using FastSQL.Sync.Core.Pusher;
using FastSQL.Sync.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSQL.Magento1.Integration.Pushers
{
    public class AttributeSetPusher : BaseEntityPusher
    {
        public AttributeSetPusher(
            AttributeSetPusherOptionManager optionManager, 
            AttributeSetProcessor processor, 
            FastProvider provider, 
            FastAdapter adapter, 
            EntityRepository entityRepository, 
            ConnectionRepository connectionRepository) : base(optionManager, processor, provider, adapter, entityRepository, connectionRepository)
        {
        }

        public override PushState Create(out string destinationId)
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
