﻿using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace CrmSvcUtilExtensions
{
    public class MappingDefinition
    {
        static MappingDefinition()
        {
            Current = LoadMappingDefinitions();
        }

        public MappingDefinition()
        {
            Entities = new List<EntityMappingDefinition>();
            Attributes = new AttributeMappingDefinitionCollection();
        }

        public string Prefix { get; set; }

        public List<EntityMappingDefinition> Entities { get; set; }
        public AttributeMappingDefinitionCollection Attributes { get; set; }

        public static MappingDefinition Current { get; private set; }

        public string RemovePrefix(string name)
        {
            if (!string.IsNullOrEmpty(Prefix))
            {
                if (name.StartsWith(Prefix))
                {
                    return name.Substring(Prefix.Length);
                }
            }

            return name;
        }

        public bool Generate(EntityMetadata entityMetadata)
        {
            EntityMappingDefinition entityMapping = GetEntityMapping(entityMetadata);

            if (entityMapping != null)
            {
                return !entityMapping.Skip;
            }

            return false; // do not generate
        }

        public EntityMappingDefinition GetEntityMapping(EntityMetadata entityMetadata)
        {
            return Entities.SingleOrDefault(_ => _.LogicalName == entityMetadata.LogicalName);
        }

        public AttributeMappingDefinition GetAttributeMapping(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata)
        {
            var entityMapping = GetEntityMapping(entityMetadata);
            if (entityMapping != null)
            {
                return entityMapping.Attributes.SingleOrDefault(_ => _.LogicalName == attributeMetadata.LogicalName);
            }

            // didnt find a specific entity attribute mapping, is it a global mapping
            return Attributes.SingleOrDefault(_ => _.LogicalName == attributeMetadata.LogicalName);
        }

        private static MappingDefinition LoadMappingDefinitions()
        {
            // TODO: load from json
            MappingDefinition mappings = new MappingDefinition();
            mappings.Prefix = "ssg_";

            mappings.Attributes.Add(new AttributeMappingDefinition { LogicalName = "statecode", Name = "StateCode" });
            mappings.Attributes.Add(new AttributeMappingDefinition { LogicalName = "statuscode", Name = "StatusCode" });

            // skip any of these fields if we dont need them
            mappings.Attributes.Skip("createdby", "createdbyname", "createdon", "createdonbehalfby", "createdonbehalfbyname","createdbyyominame","createdonbehalfbyyominame", "overriddencreatedon");
            mappings.Attributes.Skip("modifiedby","modifiedbyname", "modifiedon", "modifiedonbehalfby", "modifiedbyyominame","modifiedonbehalfbyyominame");
            mappings.Attributes.Skip("ownerid", "owneridname", "owneridtype", "owneridyominame", "owningbusinessunit", "owningteam", "owninguser");
            mappings.Attributes.Skip("importsequencenumber", "versionnumber", "utcconversiontimezonecode", "timezoneruleversionnumber");

            // add the entity mappings
            mappings.Entities.Add(new EntityMappingDefinition
            {
                LogicalName = "ssg_csrsparty",
                Name = "SSG_CsrsParty",
                Attributes = new AttributeMappingDefinitionCollection
                {
                    new AttributeMappingDefinition() { LogicalName = "ssg_areapostalcode", Name =  "PostalCode" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_bceid_displayname", Name =  "BCeIDDisplayName" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_bceid_guid", Name =  "BCeIDGuid" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_bceid_userid", Name =  "BCeID_UserId" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrspartyid", Name =  "PartyId" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_fullname", Name =  "FullName" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_street1", Name =  "AddressStreet1" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_street2", Name =  "AddressStreet2" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_provinceterritory", Name =  "Province" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsoptoutedocuments", Name =  "OptOutElectronicDocuments" },

                }
            });

            mappings.Entities.Add(new EntityMappingDefinition
            {
                LogicalName = "ssg_csrsfile",
                Name = "SSG_CsrsFile",
                Attributes = new AttributeMappingDefinitionCollection
                {
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsfileid", Name =  "CsrsFileId" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_filenumber", Name =  "FileNumber" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_autonumber", Name =  "AutoNumber" },
                    // skipped
                    new AttributeMappingDefinition() { LogicalName = "ssg_recipientschildsupportonorder", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_recipientschildsupportonorder_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_recipientsincomeonorder", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_recipientsincomeonorder_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeonorder", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeonorder_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear1", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear1_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear2", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear2_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear3", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_incomeyear3_base", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_offsetchildsupportamountonorder", Skip =  true },
                    new AttributeMappingDefinition() { LogicalName = "ssg_offsetchildsupportamountonorder_base", Skip =  true },
                }
            });

            mappings.Entities.Add(new EntityMappingDefinition
            {
                LogicalName = "ssg_csrsfeedback",
                Name = "SSG_CsrsFeedback",
                Attributes = new AttributeMappingDefinitionCollection
                {
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsfeedbackid", Name =  "FeedbackId" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsfeedbackmessage", Name =  "Message" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsfeedbacksubject", Name =  "Subject" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsparty", Name =  "Party" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_name", Name =  "Name" },
                }
            });

            mappings.Entities.Add(new EntityMappingDefinition
            {
                LogicalName = "ssg_csrsportalmessage",
                Name = "SSG_CsrsPortalMessage",
                Attributes = new AttributeMappingDefinitionCollection
                {
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsfile", Name =  "File" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsmessageattachment", Name =  "HasAttachment" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsmessagedate", Name =  "Date" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsmessagesubject", Name =  "Subject" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsportalmessageid", Name =  "MessageId" },
                    new AttributeMappingDefinition() { LogicalName = "ssg_csrsmessageread", Name =  "Read" },                    
                    new AttributeMappingDefinition() { LogicalName = "ssg_name", Name =  "Name" },
                }
            });

            return mappings;
        }

    }
}
