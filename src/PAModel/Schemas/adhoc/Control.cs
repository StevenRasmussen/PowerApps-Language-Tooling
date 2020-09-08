﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.AppMagic.Authoring.Persistence
{
    // $$$ todo - get real definition
    public class ControlInfoJson
    {
        public class RuleEntry
        {
            public string Property { get; set; }

            // The PA formulas!
            public string InvariantScript { get; set; }

            public string NameMap { get; set; }

            //[JsonExtensionData]
            //public Dictionary<string, JsonElement> ExtensionData { get; set; }

            // Duplicate, present in template as well
            public string Category { get; set; }

            public string RuleProviderType { get; set; } // = "Unknown";
        }

        public class Template
        {
            public const string DataComponentId = "http://microsoft.com/appmagic/DataComponent";
            public const string UxComponentId = "http://microsoft.com/appmagic/Component";
            public string Id { get; set; }

            // Very important for data components.
            public string Name { get; set; }

            public string Version { get; set; }
            public string LastModifiedTimestamp {get;set;}

            // Used with templates. 
            public bool? IsComponentDefinition { get; set; }
            public ComponentDefinitionInfoJson ComponentDefinitionInfo { get; set; }

            [JsonExtensionData]
            public Dictionary<string, JsonElement> ExtensionData { get; set; }

            public Template() { }

            public Template(Template other)
            {
                Id = other.Id;
                Name = other.Name;
                Version = other.Version;
                LastModifiedTimestamp = other.LastModifiedTimestamp;
                IsComponentDefinition = other.IsComponentDefinition;
                ComponentDefinitionInfo = other.ComponentDefinitionInfo;
                ExtensionData = other.ExtensionData;
            }
        }

        public class Item
        {
            public string Name { get; set; } // Control name 
            public string ControlUniqueId { get; set; }
            public string VariantName { get; set; } = string.Empty;
            public string Parent { get; set; } = string.Empty;
            public Template Template { get; set; }
            public RuleEntry[] Rules { get; set; }
            public Item[] Children { get; set; }

            public string Type { get; set; } = "ControlInfo";

            // Added later. Don't emit false. 
            // $$$ Or, remove from ExtensionData?
            // public bool HasDynamicProperties { get; set; }

            // Recreatable if missing
            //public string LayoutName { get; set; } = "";
            //public string MetaDataIDKey { get; set; } = "";
            //public bool PersistMetaDataIDKey { get; set; } = false;
            //public bool IsFromScreenLayout { get; set; } = false;
            //public bool IsDataControl { get; set; } = false;
            //public bool IsGroupControl { get; set; } = false;
            //public bool IsAutoGenerated { get; set; } = false;
            //public string StyleName { get; set; } = "";

            // Skippable entirely with server update
            //public readonly string Type = "ControlInfo";
            //public readonly double Index = 0;
            //public readonly int PublishOrderIndex = 0;


            [JsonExtensionData]
            public Dictionary<string, JsonElement> ExtensionData { get; set; }


            public Dictionary<string, RuleEntry> GetRules()
            {
                var rules = new Dictionary<string, ControlInfoJson.RuleEntry>();
                foreach (var rule in this.Rules)
                {
                    rules[rule.Property] = rule;
                }
                return rules;
            }
        }

        public Item TopParent { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> ExtensionData { get; set; }
    }
}