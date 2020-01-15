//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet(VerbsData.Update, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "DiskEncryptionSet", DefaultParameterSetName = "DefaultParameter", SupportsShouldProcess = true)]
    [OutputType(typeof(PSDiskEncryptionSet))]
    public partial class UpdateAzureRmDiskEncryptionSet : ComputeAutomationBaseCmdlet
    {
        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            ExecuteClientAction(() =>
            {
                if (ShouldProcess(this.Name, VerbsData.Update))
                {
                    string resourceGroupName;
                    string diskEncryptionSetName;
                    switch (this.ParameterSetName)
                    {
                        case "ResourceIdParameter":
                            resourceGroupName = GetResourceGroupName(this.ResourceId);
                            diskEncryptionSetName = GetResourceName(this.ResourceId, "Microsoft.Compute/diskEncryptionSets");
                            break;
                        case "ObjectParameter":
                            resourceGroupName = GetResourceGroupName(this.InputObject.Id);
                            diskEncryptionSetName = GetResourceName(this.InputObject.Id, "Microsoft.Compute/diskEncryptionSets");
                            break;
                        default:
                            resourceGroupName = this.ResourceGroupName;
                            diskEncryptionSetName = this.Name;
                            break;
                    }

                    if (this.InputObject == null)
                    {
                        BuildPatchObject();
                    }
                    else
                    {
                        BuildPutObject();
                    }
                    DiskEncryptionSetUpdate diskEncryptionSetupdate = this.DiskEncryptionSetUpdate;
                    DiskEncryptionSet parameter = new DiskEncryptionSet();
                    ComputeAutomationAutoMapperProfile.Mapper.Map<PSDiskEncryptionSet, DiskEncryptionSet>(this.InputObject, parameter);

                    var result = (this.DiskEncryptionSetUpdate == null)
                                 ? DiskEncryptionSetsClient.CreateOrUpdate(resourceGroupName, diskEncryptionSetName, parameter)
                                 : DiskEncryptionSetsClient.Update(resourceGroupName, diskEncryptionSetName, diskEncryptionSetupdate);
                    var psObject = new PSDiskEncryptionSet();
                    ComputeAutomationAutoMapperProfile.Mapper.Map<DiskEncryptionSet, PSDiskEncryptionSet>(result, psObject);
                    WriteObject(psObject);
                }
            });
        }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Alias("DiskEncryptionSetName")]
        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [ResourceNameCompleter("Microsoft.Compute/diskEncryptionSets", "ResourceGroupName")]
        public string Name { get; set; }

        [Parameter(
            ParameterSetName = "ResourceIdParameter",
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public string ResourceId { get; set; }

        [Alias("DiskEncryptionSet")]
        [Parameter(
            ParameterSetName = "ObjectParameter",
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public PSDiskEncryptionSet InputObject { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public string KeyUrl { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public string SourceVaultId { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public Hashtable Tag { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        private DiskEncryptionSetUpdate DiskEncryptionSetUpdate { get; set; }

        private void BuildPatchObject()
        {
            if (this.IsParameterBound(c => c.KeyUrl))
            {
                if (this.DiskEncryptionSetUpdate == null)
                {
                    this.DiskEncryptionSetUpdate = new DiskEncryptionSetUpdate();
                }
                if (this.DiskEncryptionSetUpdate.ActiveKey == null)
                {
                    this.DiskEncryptionSetUpdate.ActiveKey = new KeyVaultAndKeyReference();
                }
                this.DiskEncryptionSetUpdate.ActiveKey.KeyUrl = this.KeyUrl;
            }

            if (this.IsParameterBound(c => c.SourceVaultId))
            {
                if (this.DiskEncryptionSetUpdate == null)
                {
                    this.DiskEncryptionSetUpdate = new DiskEncryptionSetUpdate();
                }
                if (this.DiskEncryptionSetUpdate.ActiveKey == null)
                {
                    this.DiskEncryptionSetUpdate.ActiveKey = new KeyVaultAndKeyReference();
                }
                if (this.DiskEncryptionSetUpdate.ActiveKey.SourceVault == null)
                {
                    this.DiskEncryptionSetUpdate.ActiveKey.SourceVault = new SourceVault();
                }
                this.DiskEncryptionSetUpdate.ActiveKey.SourceVault.Id = this.SourceVaultId;
            }

            if (this.IsParameterBound(c => c.Tag))
            {
                if (this.DiskEncryptionSetUpdate == null)
                {
                    this.DiskEncryptionSetUpdate = new DiskEncryptionSetUpdate();
                }
                this.DiskEncryptionSetUpdate.Tags = this.Tag.Cast<DictionaryEntry>().ToDictionary(ht => (string)ht.Key, ht => (string)ht.Value);
            }
        }

        private void BuildPutObject()
        {
            if (this.IsParameterBound(c => c.KeyUrl))
            {
                if (this.InputObject.ActiveKey == null)
                {
                    this.InputObject.ActiveKey = new KeyVaultAndKeyReference();
                }
                this.InputObject.ActiveKey.KeyUrl = this.KeyUrl;
            }

            if (this.IsParameterBound(c => c.SourceVaultId))
            {
                if (this.InputObject.ActiveKey == null)
                {
                    this.InputObject.ActiveKey = new KeyVaultAndKeyReference();
                }
                if (this.InputObject.ActiveKey.SourceVault == null)
                {
                    this.InputObject.ActiveKey.SourceVault = new SourceVault();
                }
                this.InputObject.ActiveKey.SourceVault.Id = this.SourceVaultId;
            }

            if (this.IsParameterBound(c => c.Tag))
            {
                this.InputObject.Tags = this.Tag.Cast<DictionaryEntry>().ToDictionary(ht => (string)ht.Key, ht => (string)ht.Value);
            }
        }
    }
}