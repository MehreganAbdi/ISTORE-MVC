﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace I_STORE.Models
{
    public class AppUser : IdentityUser
    {
        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int CartTotalCost { get; set; } = 0;

    }
}
