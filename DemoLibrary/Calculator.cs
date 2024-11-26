using Newtonsoft.Json;
using System;
using System.IO;
using Constants;

namespace DemoLibrary
{
    public class Calculator
    {
        // Absolute file paths for config and output files
        private static readonly string configFilePath = AppConstants.configFilePath;
        private static readonly string outputFilePath = AppConstants.outputFilePath;  // Path to the output file

        // Read the IsTest flag from the JSON configuration file
        private static bool IsTest
        {
            get
            {
                try
                {
                    var config = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(configFilePath));
                    return config?.IsTest ?? false; // Default to false if the key is not found
                }
                catch
                {
                    return false; // Default to false if there is an error reading the file
                }
            }
            set
            {
                try
                {
                    var config = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(configFilePath));
                    config.IsTest = value;
                    File.WriteAllText(configFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to config file: {ex.Message}");
                }
            }
        }

        // Helper method to append "Hello" to the output file
        private static void AppendHelloToFile(string message)
        {
            try
            {
                // Append the message to the output file
                // This ensures that content is never overwritten, it always appends
                File.AppendAllText(outputFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending to output file: {ex.Message}");
            }
        }

        public int Add(int a, int b)
        {
            if (IsTest) // Check if the application is in test mode
            {
                AppendHelloToFile("Hello - Add");
            }
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            if (IsTest)
            {
                AppendHelloToFile("Hello - Subtract");
            }
            return a - b;
        }

        public int Multiply(int a, int b)
        {
            if (IsTest)
            {
                AppendHelloToFile("Hello - Multiply");
            }
            return a * b;
        }

        public double Divide(int a, int b)
        {
            if (IsTest)
            {
                AppendHelloToFile("Hello - Divide");
            }
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return (double)a / b;
        }
    }
}

