using DS.Core.Attributes;
using DS.Frameowrk.Repository.Infrastructure.Pattern;
using DS.Framework.Repository.Pattern.MySQL;
using System;
using System.ComponentModel;

namespace DS.Core.Domain
{
    public abstract class GlobalBaseEntity : Entity
    {

        public override ObjectState ObjectState
        {
            get { return base.ObjectState; }
            set { base.ObjectState = value; }
        }

        [DSFieldDescription(Enums.DataType.Boolean, "Is Deleted", false)]
        public bool IsDeleted { get; set; }


        [DSFieldDescription(Enums.DataType.Boolean, "Is Published", true)]
        public bool IsPublished { get; set; }


        [DSFieldDescription(Enums.DataType.Number, "Created By", DefaultValue: 0)]
        [DefaultValue(0)]
        public int CreatedBy { get; set; }

        [DSFieldDescription(Enums.DataType.Number, "Modified By", DefaultValue: 0)]
        [DefaultValue(0)]
        public int ModifiedBy { get; set; }

        [DSFieldDescription(Enums.DataType.DateTime, "Created On")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [DSFieldDescription(Enums.DataType.DateTime, "Modified On")]
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }



    public abstract class BaseEntity : GlobalBaseEntity
    {


        [DSFieldDescription(Enums.DataType.Number, "Institute ID", false,DefaultValue:0)]
        public int? ID_Institute { get; set; }

    }
}
