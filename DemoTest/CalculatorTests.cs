using DemoLibrary;    // Import the DemoLibrary namespace
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using Constants;

namespace DemoLibrary.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _calculator;

        // Absolute file paths for config and output files
        private static readonly string configFilePath = AppConstants.configFilePath;
        private static readonly string outputFilePath = AppConstants.outputFilePath;  // Path to the output file

        // This will be injected by MSTest during test execution
        public required TestContext TestContext { get; set; }

        // This will run before each test
        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();

            // Set IsTest to true in the JSON configuration file
            SetIsTest(true);

            // Clear the output file at the start of each test
            ClearOutputFile();
        }

        // This will run after each test
        [TestCleanup]
        public void Cleanup()
        {
            // Set IsTest to false in the JSON configuration file
            SetIsTest(false);

            // Clear the output file at the end of each test
            ClearOutputFile();
        }

        // Helper method to set the IsTest flag in the JSON file
        private void SetIsTest(bool value)
        {
            try
            {
                var config = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(configFilePath));
                config.IsTest = value;
                File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error writing to config file: {ex.Message}");
            }
        }

        // Helper method to clear the output file
        private void ClearOutputFile()
        {
            try
            {
                // Clear the content of the output.txt file
                File.WriteAllText(outputFilePath, string.Empty);
            }
            catch (Exception ex)
            {
                TestContext.WriteLine($"Error clearing the output file: {ex.Message}");
            }
        }

        [TestMethod]
        public void Add_AddsTwoNumbers_ReturnsCorrectSum()
        {
            var result = _calculator.Add(2, 3);
            Assert.AreEqual(5, result);

            // Verify that "Hello - Add" was written to the file
            Assert.IsTrue(File.ReadAllText(outputFilePath).Contains("Hello - Add"));
        }

        [TestMethod]
        public void Subtract_SubtractsSecondNumberFromFirst_ReturnsCorrectDifference()
        {
            var result = _calculator.Subtract(5, 3);
            Assert.AreEqual(2, result);

            // Verify that "Hello - Subtract" was written to the file
            Assert.IsTrue(File.ReadAllText(outputFilePath).Contains("Hello - Subtract"));
        }

        [TestMethod]
        public void Multiply_MultipliesTwoNumbers_ReturnsCorrectProduct()
        {
            var result = _calculator.Multiply(4, 3);
            Assert.AreEqual(12, result);

            // Verify that "Hello - Multiply" was written to the file
            Assert.IsTrue(File.ReadAllText(outputFilePath).Contains("Hello - Multiply"));
        }

        [TestMethod]
        public void Divide_DividesFirstNumberBySecond_ReturnsCorrectQuotient()
        {
            var result = _calculator.Divide(10, 2);
            Assert.AreEqual(5.0, result);

            // Verify that "Hello - Divide" was written to the file
            Assert.IsTrue(File.ReadAllText(outputFilePath).Contains("Hello - Divide"));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_DividesByZero_ThrowsDivideByZeroException()
        {
            _calculator.Divide(10, 0);
        }
    }
}

