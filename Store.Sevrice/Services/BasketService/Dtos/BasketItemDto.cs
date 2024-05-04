using System.ComponentModel.DataAnnotations;

namespace Store.Sevrice.Services.BasketService.Dtos
{
    public class BasketItemDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue, ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,10, ErrorMessage ="Quantity Must be Between 1 and 10 Pieces")]
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}