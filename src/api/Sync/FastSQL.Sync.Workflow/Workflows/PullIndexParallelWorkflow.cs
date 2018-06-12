﻿using FastSQL.Core;
using FastSQL.Sync.Core.Indexer;
using FastSQL.Sync.Core.Models;
using FastSQL.Sync.Core.Repositories;
using FastSQL.Sync.Workflow.Steps;
using Serilog;
using System;
using System.ComponentModel;
using System.Linq;
using WorkflowCore.Interface;

namespace FastSQL.Sync.Workflow.Workflows
{
    [Description("Pull Indexes in Parallel Mode")]
    public class PullIndexParallelWorkflow : BaseWorkflow
    {
        private readonly EntityRepository entityRepository;
        private readonly AttributeRepository attributeRepository;
        private readonly ScheduleOptionRepository scheduleOptionRepository;
        private readonly IndexerManager indexerManager;
        private readonly ILogger logger;

        public override string Id => nameof(PullIndexParallelWorkflow);

        public override int Version => 1;

        public PullIndexParallelWorkflow(
           EntityRepository entityRepository,
           AttributeRepository attributeRepository,
           ScheduleOptionRepository scheduleOptionRepository,
           IndexerManager indexerManager,
           ResolverFactory resolverFactory)
        {
            this.entityRepository = entityRepository;
            this.attributeRepository = attributeRepository;
            this.scheduleOptionRepository = scheduleOptionRepository;
            this.indexerManager = indexerManager;
            this.logger = resolverFactory.Resolve<ILogger>("Workflow");
        }

        public override void Build(IWorkflowBuilder<object> builder)
        {
            
            builder.StartWith(x => { })
               .While(d => true)
               .Do(x =>
               {
                   var scheduleOptions = scheduleOptionRepository
                        .GetByWorkflow(Id)
                        .Where(o => o.IsParallel && o.Enabled);
                   var entities = entityRepository
                       .GetAll()
                       .Where(e => e.Enabled && scheduleOptions.Any(o => o.TargetEntityId == e.Id && o.TargetEntityType == e.EntityType));
                   var attributes = attributeRepository
                       .GetAll()
                       .Where(e => e.Enabled && scheduleOptions.Any(o => o.TargetEntityId == e.Id && o.TargetEntityType == e.EntityType));
                   var indexes = entities
                       .Select(e => e as IIndexModel)
                       .Union(attributes.Select(a => a as IIndexModel));
                   if (indexes.Count() <= 0)
                   {
                       x.StartWith(c => { })
                        .Delay(d => TimeSpan.FromMinutes(10));
                   }
                   else
                   {
                       x.StartWith(s => { })
                           .ForEach(ff => indexes)
                           .Do(dd =>
                           {
                               dd.StartWith<UpdateIndexChangesStep>()
                                .Input(s => s.IndexModel, (d, ctx) => ctx.Item);
                           });
                   }
               });
        }
    }
}