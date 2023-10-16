using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace system_developer_exam.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("PermitNo.")]
        public string PermitNumber { get; set; }

        [DisplayName("Branch Manager")]
        public string BranchManager { get; set; }

        public DateTime DateOpened { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = false;

        [Required]
        [DisplayName("Branch Code")]
        public string BranchCode { get; set; }

        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        
        [Required]
        public string Address { get; set; }

        [Required]
        public string Barangay { get; set; }

        [Required]
        public string City { get; set; }

       


    }
}
