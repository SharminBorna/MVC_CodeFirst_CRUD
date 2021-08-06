using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using work_01.CustomValidation;

namespace work_01.ViewModels
{
    public class ToyVM
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string ToysName { get; set; }
        [Required]
        [Column(TypeName = "money"), Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        [CustomStoreDate(ErrorMessage = "Product entry date must be less than or equal to Today's Date")]
        public DateTime StoreDate { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        [Display(Name = "Category")]
        public int cId { get; set; }
        public string cName { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}