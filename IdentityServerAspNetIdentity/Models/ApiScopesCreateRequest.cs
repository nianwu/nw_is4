// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Models
{
    public class ApiScopesCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public List<string> UserClaims { get; set; }
    }
}
