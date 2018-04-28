﻿using FastSQL.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastSQL.Sync.Core.Indexer
{
    public class EntityIndexerOptionManager : BaseOptionMananger
    {
        public override IEnumerable<OptionItem> GetOptionsTemplate()
        {
            return new List<OptionItem>
            {
                new OptionItem
                {
                    Name = "indexer_value_columns",
                    DisplayName = "@Value Column",
                    Description = "A name of a column which hold value of the entity.",
                    Value = string.Empty,
                    Example = "value",
                    OptionGroupNames = new List<string>{ "Indexer" },
                },
                new OptionItem
                {
                    Name = "indexer_alias_name",
                    DisplayName = "@Alias Column",
                    Description = "An alias name which might be useful when export to CSV, Excel files.",
                    Value = string.Empty,
                    Example = "Sku",
                    OptionGroupNames = new List<string>{ "Indexer" },
                },
                new OptionItem
                {
                    Name = "indexer_column_spliter",
                    DisplayName = "@Column Spliter",
                    Description = "A spliter character between columns which might be useful when export to CSV, Excel file.",
                    Value = "|",
                    Example = "|",
                    OptionGroupNames = new List<string>{ "Indexer" },
                },
                new OptionItem
                {
                    Name = "indexer_key_columns",
                    DisplayName = "@Key Columns",
                    Description = @"A comma separated list of columns that are marked as [KEY] and useful for system to track for changes including [@Value Column]",
                    Value = string.Empty,
                    Example = "sku,name",
                    OptionGroupNames = new List<string>{ "Indexer" },
                }
            };
        }
    }
}
