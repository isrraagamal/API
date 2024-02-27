using Day1.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Day1.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }

       /* [UniqueDepartmentNameValidator]*/

        public string? Name { get; set; }
      /*  [ValidLocationValidator]*/
        public string? Location { get; set; }
        public string? ManagerName { get; set; }
        /*[JsonIgnore]*/
        public virtual ICollection <Student>? Students { get; set; }
    }
}
