using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using MoqedCalculator;
using System.Security.Principal;

namespace MoqedCalculatorTests
{
    [TestClass]
    public sealed class MoqedCalculatorTests
    {
        [TestMethod]

        public void Divide_WithValidNumber_returnsResult()
        {
            //Arrange
            Calculator c = new Calculator();
            double left = 1;
            double right = 2;

            //Act
            double result = c.Divide(left, right);

            //Assert
            double trueresult = left / right;
            Assert.AreEqual(result, trueresult);
        }
        [TestMethod]

        public void DividebyZero_Returnsdividebyzeroexception()
        {
            // Arrange
            Calculator c = new Calculator();
            double left = 1;
            double right = 0;
            string expectedMessage = "Attempted to divide by zero.";

            // Act & Assert
            Exception actualException = null;
            try
            {
                c.Divide(left, right);  // Call here, expect throw
            }
            catch (DivideByZeroException ex)
            {
                actualException = ex;
            }

            Assert.IsNotNull(actualException, "DivideByZeroException was not thrown.");
            Assert.AreEqual(expectedMessage, actualException.Message);
        }
    }
}
