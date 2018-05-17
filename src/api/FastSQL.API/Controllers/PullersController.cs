﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastSQL.Core;
using FastSQL.Sync.Core;
using FastSQL.Sync.Core.Indexer;
using FastSQL.Sync.Core.Puller;
using FastSQL.Sync.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastSQL.API.Controllers
{
    [Route("api/[controller]")]
    public class PullersController : Controller
    {
        private readonly IEnumerable<IEntityPuller> _entityPullers;
        private readonly IEnumerable<IAttributePuller> _attributePullers;
        private readonly IEnumerable<IIndexer> _indexers;
        private readonly EntityRepository entityRepository;
        private readonly AttributeRepository attributeRepository;
        private readonly ConnectionRepository connectionRepository;

        public PullersController(IEnumerable<IEntityPuller> entityPullers,
            IEnumerable<IAttributePuller> attributePullers,
            IEnumerable<IIndexer> indexers,
            EntityRepository entityRepository,
            AttributeRepository attributeRepository,
            ConnectionRepository connectionRepository)
        {
            _entityPullers = entityPullers;
            _attributePullers = attributePullers;
            _indexers = indexers;
            this.entityRepository = entityRepository;
            this.attributeRepository = attributeRepository;
            this.connectionRepository = connectionRepository;
        }
        
        [HttpPost("entity/{id}")]
        public IActionResult PullEntityData(string id, [FromBody] object nextToken = null)
        {
            var entity = entityRepository.GetById(id);
            var sourceConnection = connectionRepository.GetById(entity.SourceConnectionId.ToString());
            var puller = _entityPullers.FirstOrDefault(p => p.IsImplemented(entity.SourceProcessorId, sourceConnection.ProviderId));
            puller.SetIndex(entity);
            var data = puller.PullNext(nextToken);
            return Ok(data);
        }

        [HttpPost("attribute/{id}")]
        public IActionResult PullAttributeData(string id, [FromBody] object nextToken = null)
        {
            var attribute = attributeRepository.GetById(id);
            var entity = entityRepository.GetById(attribute.EntityId.ToString());
            var sourceConnection = connectionRepository.GetById(attribute.SourceConnectionId.ToString());
            var puller = _attributePullers.FirstOrDefault(p => p.IsImplemented(attribute.SourceProcessorId, entity.SourceProcessorId, sourceConnection.ProviderId));
            puller.SetIndex(attribute);
            var data = puller.PullNext(nextToken);
            return Ok(data);
        }
    }
}