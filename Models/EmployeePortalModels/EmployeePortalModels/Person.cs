using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePortal.Models
{
    public abstract class Person
    {
        public string Id { get; protected set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        #region Constructors

        internal Person()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Person(string firstName, string lastName, DateTime birthday) : this()
        {
            FirstName = string.IsNullOrEmpty(firstName?.Trim()) ? throw new ArgumentException($"{nameof(firstName)} must be provided.") : firstName;
            LastName = string.IsNullOrEmpty(lastName?.Trim()) ? throw new ArgumentException($"{nameof(lastName)} must be provided.") : lastName;
            if (birthday == DateTime.MinValue || birthday == DateTime.MaxValue || birthday.Date > DateTime.Today || birthday.Date < DateTime.Today.AddYears(-122))
                throw new ArgumentException($"{nameof(birthday)} must be a valid date.");
            Birthday = birthday;
        }

        #endregion
    }

    public abstract class BenefitsEligiblePerson : Person, IBenefitsEligible
    {
        public abstract bool IsEmployee { get; set; }

        public abstract decimal NetYearlyBenefitsCost { get; set; }

        public BenefitsEligiblePerson() : base()
        { }

        public BenefitsEligiblePerson(string firstName, string lastName, DateTime birthday) : base(firstName, lastName, birthday)
        { }

        public virtual bool IsBenefitDiscountEligible() => FirstName?.StartsWith("A", StringComparison.InvariantCultureIgnoreCase) ?? false;

        public virtual decimal CalculateYearlyBenefitsCost() => IsBenefitDiscountEligible() ? NetYearlyBenefitsCost * .9m : NetYearlyBenefitsCost;
    }

    public class Employee : BenefitsEligiblePerson
    {
        public int EmployeeId { get; set; }

        public override bool IsEmployee { get; set; } = true;

        public override decimal NetYearlyBenefitsCost { get; set; } = 1000m;

        public virtual decimal NetPaycheck { get; set; } = 2000m;

        public readonly List<Dependent> Dependents = new();

        public void AddDependent(Dependent newDependent)
        {
            if (newDependent == null)
                return;

            if (!Dependents.Any(x => x.Id.Equals(newDependent.Id, StringComparison.CurrentCultureIgnoreCase)))
                Dependents.Add(newDependent);
        }

        public void RemoveDependent(Dependent existingDependent)
        {
            RemoveDependent(existingDependent?.Id);
        }

        public void RemoveDependent(string id)
        {
            if (string.IsNullOrEmpty(id?.Trim()))
                return;

            Dependent d = Dependents.FirstOrDefault(x => x.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            if (d != null)
                Dependents.Remove(d);
        }

        public Employee(string firstName, string lastName, DateTime birthday) : base(firstName, lastName, birthday) 
        {
            //This is bad in practice.  In terms of real world scenarios, I would have some distinct incrementing ID value
            //but for our purposes this is how we'll derive it based on ticks
            EmployeeId = Math.Abs((int)DateTime.Now.Ticks % 100000);
        }
    }

    public class Dependent : BenefitsEligiblePerson
    {
        public int ParentEmployeeId { get; internal set; }

        public override bool IsEmployee { get; set; } = false;

        public override decimal NetYearlyBenefitsCost { get; set; } = 500m;

        public Dependent(string firstName, string lastName, DateTime birthday) : base(firstName, lastName, birthday)
        { }
    }
}
