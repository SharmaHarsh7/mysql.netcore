﻿using DS.Core.Domain;
using System;

namespace DS.Code.Domain.Models.Authentication
{
    public class RefreshToken : BaseEntity
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }
}
