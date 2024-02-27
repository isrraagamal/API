/*using Day1.Entity;
using Day1.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml;

namespace Day1.Validators
{
    public class UniqueDepartmentNameValidator : ValidationAttribute
    {

        private readonly FacultyContext db;

        public UniqueDepartmentNameValidator(IServiceProvider serviceProvider)
        {
            db = serviceProvider.GetService<FacultyContext>();
            if (db == null)
            {
                throw new InvalidOperationException("FacultyContext service not registered.");
            }
        }

        public override bool IsValid(object value)
        {
            string departmentName = value as string;
            if (string.IsNullOrEmpty(departmentName))
            {
                return false;
            }

           
            return !db.Departments.Any(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase));
        }

    }
}


*/