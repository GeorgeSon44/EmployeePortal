using System;
using EmployeePortal.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeePortalModelsUnitTests
{
    [TestClass]
    public class EmployeeUnitTest : EmployeePortalUnitTestBase
    {
        #region Helpers

        #endregion

        [TestCategory("ConstructorTests")]
        [TestMethod]
        public void EmployeeConstructorTest_ValidInputs()
        {
            Employee employee = new("FirstName", "LastName", new DateTime(2002, 1, 1));
            Assert.AreEqual("FirstName", employee.FirstName);
            Assert.AreEqual("LastName", employee.LastName);
            Assert.AreEqual(new DateTime(2002, 1, 1), employee.Birthday);
            Assert.AreEqual(employee.NetYearlyBenefitsCost, 1000m);
            Assert.AreEqual(employee.NetPaycheck, 2000m);

            Assert.IsTrue(!string.IsNullOrEmpty(employee.Id));
            Assert.IsTrue(employee.EmployeeId >= 0 && employee.EmployeeId <= 100000);
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
