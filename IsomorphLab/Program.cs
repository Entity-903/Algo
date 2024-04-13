using System;

namespace IsomorphLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Right click TxtFiles/IsomorphInput1.txt and choose "Copy full path"
            Console.WriteLine("Please provide a path to file:");
            string? path;
            do
            {
                path = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(path));

            //Create the isomorph output
            string output = "";
            try
            {
                string[] lines = File.ReadAllLines(path);
                output = IsomorphGenerator.CreateIsomorphs(lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //write the output to file and console
            if (!string.IsNullOrWhiteSpace(output))
            {
                Console.WriteLine(output);
                try
                {
                    string rootPath = path.Substring(0, path.LastIndexOf('\\'));
                    File.WriteAllText(rootPath + "/Output.txt", output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Nothing generated");
            }
        }
    }
}
