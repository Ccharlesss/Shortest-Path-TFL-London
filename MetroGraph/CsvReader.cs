using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace MetroGraph
{
    public class CsvReader
    {

        public List<StationConnection> ReadStationConnections(string filePath)
            {
                var connections = new List<StationConnection>();
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.ReadLine();
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        try
                        {
                            connections.Add(new StationConnection
                            {
                                StartStation = fields[2].Trim(), 
                                EndStation = fields[3].Trim(), 
                                TravelTime = double.Parse(fields[5].Trim(), CultureInfo.InvariantCulture), 
                                Line = fields[0].Trim() 
                            });
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine($"Error parsing data: {ex.Message}");
                            Console.WriteLine($"Data: Start={fields[2]}, End={fields[3]}, Time={fields[5]}, Line={fields[0]}");
                        }
                    }
                }
                return connections;
            }

        }
}
