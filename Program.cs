using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SquareSpace.ProductVariationGenerator
{
	class Variation
	{
		public string Name { get; set; }
		public List<Option> Options { get; set; }
	}
	class Option
	{
		public string Acronym { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
	}
	static class Program
	{
		private static readonly int _basePrice = 899;

		private static readonly string _startOfLine = "\"Product ID [Non Editable]\",\"Variant ID [Non Editable]\",\"Product Type [Non Editable]\",\"Product Page\",\"Product URL\",\"Title\",\"Description\",\"SKU\",";
		private static readonly string _options = "\"Option Name 1\",\"Option Value 1\",\"Option Name 2\",\"Option Value 2\",\"Option Name 3\",\"Option Value 3\",";
		private static readonly string _tailOfLine = "\"Price\",\"Sale Price\",\"On Sale\",\"Stock\",\"Categories\",\"Tags\",\"Weight\",\"Length\",\"Width\",\"Height\",\"Visible\",\"Hosted Image URLs\"";
		private static readonly List<string> _csvFile = new List<string> { $"{_startOfLine}{_options}{_tailOfLine}" };

		static void Main()
		{
			var optionsandPrice = new List<Variation>
			{
				new  Variation { Name = "Dropdown 1", Options = new List<Option> {
										new Option { Name = "Red", Price = 0 },
										new Option { Name = "Black", Price = 20 },
										new Option { Name = "Gold", Price = 40 },
										} },

				new  Variation { Name = "Dropdown 2", Options = new List<Option> {
										new Option { Name = "No", Price = 0 },
										new Option { Name = "Yes", Price = 80 },
										} },

				new  Variation { Name = "Dropdown 3", Options = new List<Option> {
										new Option { Name = "No", Price = 0 },
										new Option { Name = "Yes", Price = 1 },
										} },
			};

			var listofinputs = new List<string>() { };

			foreach (var variation in optionsandPrice)
			{
				var test = new StringBuilder();

				var count = 0;

				foreach (var option in variation.Options)
				{
					option.Acronym = count.ToString();
					test.Append(option.Acronym);
					count++;
				}

				listofinputs.Add(test.ToString());
			}

			var combinations = listofinputs.MultiCartesian(x => new string(x));
			foreach (var result in combinations)
			{
				var totalPrice = _basePrice;
				var setOptions = new StringBuilder();

				for (int x = 0; x < 3; x++)
				{
					try
					{
						var combination = result[x];

						var matchingAcroynm = optionsandPrice[x].Options.Where(x => x.Acronym == combination.ToString()).FirstOrDefault();

						var matchName = matchingAcroynm.Name;
						var matchingPrice = matchingAcroynm.Price;
						var specificName = optionsandPrice[x].Name;

						setOptions.Append($"\"{specificName}\",\"{matchName}\",");

						totalPrice += matchingPrice;

					}
					catch
					{
						setOptions.Append($"\"\",\"\",");
					}
				}

				var startOfOption = "\"\",\"\",\"PHYSICAL\",\"shop\",\"product - name - 3p4le - 35x28\",\"Product Name\",\" Description\",\"\",";
				setOptions.Append($"\"{totalPrice}\",");
				var tailOfOption = "\"0.00\",\"No\",\"Unlimited\",\"\",\"\",\"1.0\",\"0.0\",\"0.0\",\"0.0\",\"Yes\",\"\"";

				_csvFile.Add($"{startOfOption}{setOptions.ToString()}{tailOfOption}");

				var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

				using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output-Template.csv")))
				{
					foreach (string line in _csvFile)
						outputFile.WriteLine(line);
				}

				Console.WriteLine($"Adding combination {result} {setOptions.ToString()} Price {totalPrice}");
			}

			Console.WriteLine($"{combinations.Count()} Combinations");
			if(combinations.Count() > 64)
			{
				Console.WriteLine("Warning: Combination exceeds squarespace maximum allowed amount of 64");
			}
		}
	}
}
