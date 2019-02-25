using CoreData.Common.Extensions;
using CoreData.Common.HostEnvironment;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CoreData.Desktop.Common.Http.Models
{
    /// <summary>CoreData json-object, representing node.</summary>
    [DataContract]
    [Serializable]
    public sealed partial class NavigationNode
    {
        [DataMember(Name = "id")]
        public string Id { get; private set; }

        #region Node type
        [DataMember(Name = "type")]
        private string _typeName;
        [DataMember(Name = "core_type")]
        private string _coreTypeName;

        [IgnoreDataMember]
        public NodeType Type =>
            Enum.TryParse<NodeType>(_coreTypeName.Or(_typeName), true, out var _type)
            ? _type : NodeType.Space;
        //            ?
        //        NodeType value;
        //        value = Enum.TryParse(TypeName, true, out value) ? value : NodeType.Container;
        //        if (value == NodeType.Space && !string.IsNullOrEmpty(CoreTypeName))
        //        {
        //            NodeType coreType = Enum.TryParse(CoreTypeName, true, out value) ? value : NodeType.Container;
        //            if (coreType == NodeType.FileSpace)
        //            {
        //                value = coreType;
        //            }
        //        }
        //        return value;
        //    }
        //    //private set { TypeName = value.ToString(); }
        //}
        #endregion Node type

        [DataMember(Name = "snapshot_id")]
        public string SnapshotId { get; private set; }

        [DataMember(Name = "version")]
        public string Version { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        #region CD attributes(don't confuse with FS attrs)
        [DataMember(Name = "access")]
        public byte Access { get; private set; }

        [DataMember(Name = "created")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty(PropertyName = "modified_by")]
        public string ModifiedBy { get; set; }

        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
        #endregion CD attributes

        #region Parent
        [JsonProperty(PropertyName = "parent_id")]
        private string _parentId;
        [JsonProperty(PropertyName = "vnode_parent_id")]
        private string _vnodeParentId;
        [JsonIgnore]
        public string ParentId => _vnodeParentId.Or(_parentId);
        #endregion Parent
    }

    [DebuggerDisplay("{" + nameof(ITraceView.Value) + "}")]
    public partial class NavigationNode : ITraceView
    {
        string ITraceView.Value => Path;
    }
}
