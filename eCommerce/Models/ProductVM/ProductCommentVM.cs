using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.ProductVM
{
    public class ProductCommentVM
    {
        public int ProductCommentId { get; set; }
        public string UserNameComment { get; set; }
        public int ProductId { get; set; }
        public int ProductRating { get; set; }

        [Required(ErrorMessage = "Mandatory!")]
        public string Comment { get; set; }
        
        [Required(ErrorMessage = "Mandatory!")]
        public string CommentTitle { get; set; }
        public string CommentDate { get; set; }
    }
}
