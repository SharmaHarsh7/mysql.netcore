using DS.Core.Domain;
using DS.Core.Enums;
using System.Collections.Generic;

namespace DS.Domain.Models.Users
{
    public class Client : BaseEntity
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}
