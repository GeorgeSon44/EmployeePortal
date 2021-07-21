using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EmployeePortalModelsUnitTests
{
    public class EmployeePortalUnitTestBase
    {
        protected static void ExpectedClassException<T, V>(Func<V> func) where T : Exception where V : class
        {
            try
            {
                V employee = func();
            }
            catch (T)
            {
                //expected Exception returned
                return;
            }
            catch
            {
                //we are expecting failure but an ArgumentException so if it gets here this test fails
                Assert.Fail();
            }

            //we are expecting failure so if it gets here this test fails
            Assert.Fail();
        }
    }
}
