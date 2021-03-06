﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastSQL.Core;
using FastSQL.Sync.Core.Enums;
using FastSQL.Sync.Core.Repositories;

namespace FastSQL.Sync.Core.IndexExporters
{
    public abstract class BaseIndexExporter : IIndexExporter
    {
        public abstract string Id { get; }
        public abstract string Name { get; }

        protected virtual string IndexId { get; set; }
        protected virtual EntityType IndexType { get; set; }

        protected readonly IOptionManager OptionManager;

        protected IndexExporterRepository IndexExporterRepository { get; }

        public virtual IEnumerable<OptionItem> Options => OptionManager.Options;

        public BaseIndexExporter(IOptionManager optionManager, IndexExporterRepository indexExporterRepository)
        {
            OptionManager = optionManager;
            IndexExporterRepository = indexExporterRepository;
        }

        public virtual IEnumerable<OptionItem> GetOptionsTemplate()
        {
            return OptionManager.GetOptionsTemplate();
        }

        public abstract bool Export(out string message);

        public IIndexExporter SetIndex(string id, EntityType entityType)
        {
            IndexId = id;
            IndexType = entityType;
            return this;
        }

        public virtual IIndexExporter SetOptions(IEnumerable<OptionItem> options)
        {
            OptionManager.SetOptions(options);
            return this;
        }

        public virtual IIndexExporter LoadOptions()
        {
            var options = IndexExporterRepository.LoadOptions(Id);
            OptionManager.SetOptions(options.Select(o => new OptionItem { Name = o.Key, Value = o.Value }));
            return this;
        }

        public virtual IIndexExporter Save()
        {
            IndexExporterRepository.LinkOptions(Id, OptionManager.Options);
            return this;
        }
    }
}
