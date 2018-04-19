﻿using FastSQL.Core;
using FastSQL.Sync.Core;
using FastSQL.Sync.Core.Enums;
using FastSQL.Sync.Core.Processors;
using FastSQL.Sync.Core.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace FastSQL.MsSql.Integration
{
    public class EntityPuller : BaseEntityPuller
    {
        private readonly FastAdapter adapter;

        public EntityPuller(EntityPullerOptionManager optionManager,
            EntityProcessor processor,
            FastProvider provider,
            FastAdapter adapter,
            EntityRepository entityRepository) : base(optionManager, processor, provider, entityRepository)
        {
            this.adapter = adapter;
        }

        public override PullResult PullNext(object lastToken = null)
        {
            var options = EntityRepository.LoadOptions(EntityModel.Id);
            var sqlScript = options.FirstOrDefault(o => o.Key == "puller_sql_script").Value;
            int limit = 100;
            int offset = 0;
            if (lastToken != null)
            {
                var jToken = JObject.FromObject(lastToken);
                limit = int.Parse(jToken.GetValue("Limit").ToString());
                offset = int.Parse(jToken.GetValue("Offset").ToString());
                offset = offset + limit;
            }
            var results = adapter.Query(sqlScript, new
            {
                Limit = limit,
                Offset = offset
            }).FirstOrDefault()?.Rows;
            return new PullResult
            {
                Status = results.Count() > 0 ? SyncState.HasData : SyncState.Invalid,
                LastToken = new
                {
                    Limit = limit,
                    Offset = offset
                },
                Data = results
            };
        }
    }
}
