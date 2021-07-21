using EmployeePortal.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortalModelsUnitTests
{
    [TestClass]
    public class DependentUnitTest : EmployeePortalUnitTestBase
    {
        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_ValidInputs()
        {
            Dependent depentent = new("FirstName", "LastName", new DateTime(2002, 1, 1));
            Assert.AreEqual("FirstName", depentent.FirstName);
            Assert.AreEqual("LastName", depentent.LastName);
            Assert.AreEqual(new DateTime(2002, 1, 1), depentent.Birthday);
            Assert.AreEqual(depentent.NetYearlyBenefitsCost, 1000m);

            Assert.IsTrue(!string.IsNullOrEmpty(depentent.Id));
            Assert.IsTrue(depentent.ParentEmployeeId == 0);
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_NullFirstName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new(null, "LastName", new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_EmptyFirstName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("", "LastName", new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_WhitespaceFirstName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("         ", "LastName", new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_NullLastName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", null, new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_EmptyLastName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "", new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_WhitespaceLastName()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "           ", new DateTime(2002, 1, 1)));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_BirthdayDateTimeMinValue()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.MinValue));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_BirthdayDateTimeMaxValue()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.MaxValue));
        }

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_BirthdayTooOldValue()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.Today.AddYears(-200)));
        }


        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_BirthdayInTheFuture()
        {
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.Today.AddDays(1)));
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.Today.AddMonths(1)));
            ExpectedClassException<ArgumentException, Employee>(() => new("FirstName", "LastName", DateTime.Today.AddYears(1)));
        }
    }
}
