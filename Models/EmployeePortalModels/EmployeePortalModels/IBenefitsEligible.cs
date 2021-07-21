using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Models
{
    public interface IBenefitsEligible
    {
        public bool IsEmployee { get; set; }

        public decimal NetYearlyBenefitsCost { get; set; }

        public decimal CalculateYearlyBenefitsCost();

        public bool IsBenefitDiscountEligible();
    }
}
