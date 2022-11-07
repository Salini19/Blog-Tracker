using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog_MVC.Models
{
    public class Admin
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class Emp
    {
        [Key, Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofJoining { get; set; }
        [Required]
        public int Passcode { get; set; }

        //public virtual ICollection<Blog> BlogDetails { get; set; }
    }
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfCreation { get; set; }
        [Url]
        public string BlogUrl { get; set; }
        [EmailAddress]
        public string EmailId { get; set; }

        //[ForeignKey("EmailId")]
        //public virtual Emp EmpDetails { get; set; }

    }
}
