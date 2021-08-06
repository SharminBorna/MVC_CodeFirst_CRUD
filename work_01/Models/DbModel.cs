using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace work_01.Models
{
    public class Categories
    {
        public Categories()
        {
            this.Toys = new List<Toys>();
        }

        [Required]
        [Key]
        public int cId { get; set; }
        [Required, StringLength(50), Display(Name = "Category")]
        public string cName { get; set; }
        //navigation
        public virtual ICollection<Toys> Toys { get; set; }
    }
    public class Toys
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string ToysName { get; set; }
        [Required]
        [Column(TypeName = "money"), Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        public DateTime StoreDate { get; set; }
        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        //fk
        [Required, ForeignKey("Categories")]
        [Display(Name = "Category")]
        public int cId { get; set; }
        //navigation
        public virtual Categories Categories { get; set; }
    }
    public class ToyStoreDbContext : DbContext
    {
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Toys> Toys { get; set; }
    }
}