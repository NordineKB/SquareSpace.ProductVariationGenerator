<a href='https://ko-fi.com/O5O41YSS0' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://cdn.ko-fi.com/cdn/kofi1.png?v=2' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>
---
## SquareSpace Product Variation Generator

To add a dropdown option in square space with different prices requires the addition of every single combination so 2 product choices means 4 variations.

This tool can be used to quickly create all the pontential combinations and calculates the total price on a CSV template that can then be imported with squarespaces CSV importer.

There are two limits with squarespace, one is that there can only be three dropdown options and a 64 maximum product combination.

To use this just update this and run the console app in visual studio, it'll output to the document folder in a file called "Output-Template.csv" (or change it to where you like)

Example Variation in Program.cs:

```csharp
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
```

Example CSV Output:

![Output CSV](Example.PNG "Output CSV")
