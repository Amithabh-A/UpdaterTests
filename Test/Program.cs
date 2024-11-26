namespace new; 


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
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


}
