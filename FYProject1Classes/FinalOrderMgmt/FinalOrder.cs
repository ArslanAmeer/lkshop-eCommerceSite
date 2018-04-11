using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FYProject1Classes.CartManagment;

namespace FYProject1Classes.FinalOrderMgmt
{
    public sealed class FinalOrder
    {
        public FinalOrder()
        {
            ShoppingCartItem = new List<ShoppingCartItem>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullAddress { get; set; }

        [Required]
        public long Phone { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }

        public string OrderNumber { get; set; }

        public string OrderStatus { get; set; }

    }
}
