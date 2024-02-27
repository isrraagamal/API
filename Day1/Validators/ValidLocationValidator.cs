/*using Day1.Entity;
using Day1.Models;
using System.ComponentModel.DataAnnotations;

namespace Day1.Validators
{
    public class ValidLocationValidator : ValidationAttribute
    {
    

        public override bool IsValid(object value)
        {
            var location = value as string;

            if (location != "EG" && location != "USA")
            {
                return false;
            }

            return true;
        }

    }
}

*/