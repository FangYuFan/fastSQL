﻿using FastSQL.API.ViewModels;
using FastSQL.Sync.Core;
using FastSQL.Sync.Core.Enums;
using FastSQL.Sync.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FastSQL.API.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesController: Controller
    {
        private readonly EntityRepository entityRepository;
        private readonly ConnectionRepository connectionRepository;
        private readonly IEnumerable<IProcessor> processors;
        private readonly IEnumerable<IEntityPuller> pullers;
        private readonly IEnumerable<IEntityIndexer> indexers;
        private readonly DbTransaction transaction;

        public EntitiesController(
            EntityRepository entityRepository,
            ConnectionRepository connectionRepository,
            IEnumerable<IProcessor> processors,
            IEnumerable<IEntityPuller> pullers,
            IEnumerable<IEntityIndexer> indexers,
            DbTransaction transaction)
        {
            this.entityRepository = entityRepository;
            this.connectionRepository = connectionRepository;
            this.processors = processors;
            this.pullers = pullers;
            this.indexers = indexers;
            this.transaction = transaction;
        }

        [HttpPost] 
        public IActionResult Create([FromBody] CreateEntityViewModel model)
        {
            var sourceConnection = connectionRepository.GetById(model.SourceConnectionId.ToString());
            var destConnection = connectionRepository.GetById(model.DestinationConnectionId.ToString());
            var processor = processors.FirstOrDefault(p => p.Id == model.ProcessorId && p.Type == ProcessorType.Entity);
            if (sourceConnection == null)
            {
                return NotFound("The requested source connection is not found.");
            }
            if (destConnection == null)
            {
                return NotFound("The requested destination connection is not found.");
            }
            if (processor == null)
            {
                return NotFound("The requested processor is not found.");
            }
            try
            {
                var result = entityRepository.Create(new
                {
                    model.Name,
                    model.Description,
                    model.SourceConnectionId,
                    model.DestinationConnectionId,
                    model.ProcessorId
                });

                entityRepository.LinkOptions(Guid.Parse(result), model.Options);

                transaction.Commit();
                return Ok(result);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] CreateEntityViewModel model)
        {
            var sourceConnection = connectionRepository.GetById(model.SourceConnectionId.ToString());
            var destConnection = connectionRepository.GetById(model.DestinationConnectionId.ToString());
            var processor = processors.FirstOrDefault(p => p.Id == model.ProcessorId && p.Type == ProcessorType.Entity);
            if (sourceConnection == null)
            {
                return NotFound("The requested source connection is not found.");
            }
            if (destConnection == null)
            {
                return NotFound("The requested destination connection is not found.");
            }
            if (processor == null)
            {
                return NotFound("The requested processor is not found.");
            }
            try
            {
                var result = entityRepository.Update(id, new
                {
                    model.Name,
                    model.Description,
                    model.SourceConnectionId,
                    model.DestinationConnectionId,
                    model.ProcessorId
                });

                entityRepository.LinkOptions(Guid.Parse(id), model.Options);

                transaction.Commit();
                return Ok(result);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
