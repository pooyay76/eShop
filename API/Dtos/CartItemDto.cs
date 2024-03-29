﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0.1")]
        public decimal Price { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be atleast 1")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}
