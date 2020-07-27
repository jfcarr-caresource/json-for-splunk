using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SplunkFormatter
{
	class Program
	{
		static void Main(string[] args)
		{
			OutputNewton();

			OutputCustom();
		}

		/// <summary>
		/// Uses the Newtonsoft JSON serializer to produce JSON-formatted strings from model instances.
		/// </summary>
		static void OutputNewton()
		{
			try
			{
				var inputFile = Path.Combine("data", "input.txt");
				var outputFile = Path.Combine("data", "output-newton.txt");

				if (System.IO.File.Exists(inputFile))
				{
					using (System.IO.StreamReader inputFileStream = new System.IO.StreamReader(inputFile))
					{
						using (System.IO.StreamWriter outputFileStream = new System.IO.StreamWriter(outputFile))
						{
							string line;
							var lineNumber = 1;
							while ((line = inputFileStream.ReadLine()) != null)
							{
								if (lineNumber > 1)
								{
									var fieldList = line.Split(",");
									var lineItemModel = new Models.Person
									{
										PersonID = fieldList[0],
										Name = fieldList[1],
										Status = fieldList[2]
									};

									var lineItemJson = JsonConvert.SerializeObject(lineItemModel);
									Console.WriteLine(lineItemJson);
									outputFileStream.WriteLine(lineItemJson);
								}
								lineNumber++;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] {ex.Message}");
			}
		}

		/// <summary>
		/// Produces JSON-formatted strings with no external library dependencies.
		/// </summary>
		static void OutputCustom()
		{
			try
			{
				var inputFile = Path.Combine("data", "input.txt");
				var outputFile = Path.Combine("data", "output-custom.txt");

				if (System.IO.File.Exists(inputFile))
				{
					using (System.IO.StreamReader inputFileStream = new System.IO.StreamReader(inputFile))
					{
						using (System.IO.StreamWriter outputFileStream = new System.IO.StreamWriter(outputFile))
						{
							string line;
							var lineNumber = 1;
							var columnNames = new List<string>();
							while ((line = inputFileStream.ReadLine()) != null)
							{
								if (lineNumber == 1) columnNames = line.Split(",").ToList();
								else
								{
									var fieldList = line.Split(",").ToList();

									var lineItemJson = string.Empty;
									for (int i = 0; i <= fieldList.Count - 1; i++)
									{
										lineItemJson += $"\"{columnNames[i]}\":\"{fieldList[i].Replace("\"", "\\\"")}\"";
										if (i < fieldList.Count - 1) lineItemJson += ",";
									}
									lineItemJson = $"{{{lineItemJson}}}";

									Console.WriteLine(lineItemJson);
									outputFileStream.WriteLine(lineItemJson);
								}
								lineNumber++;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] {ex.Message}");
			}

		}
	}
}
