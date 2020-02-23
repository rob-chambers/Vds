using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vds.Data
{
    public class Person
    {
        [Key]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(25, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email is too long")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Column(TypeName = "tinyint")]
        public PersonStatus Status { get; set; }
    }

    public enum PersonStatus
    {
        New,
        Invalid,
        PendingProcessing,
        Complete
    }
}