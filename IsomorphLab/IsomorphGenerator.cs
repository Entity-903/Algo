using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingLibrary;

namespace IsomorphLab
{
	public static class IsomorphGenerator
	{
		public static string CreateIsomorphs(IEnumerable<string> words)
		{
			Dictionary<string, string> exactIsomorphsDictionary = new Dictionary<string, string>(); //to store exact isomorphic values;
			Dictionary<string, string> looseIsomorphsDictionary = new Dictionary<string, string>();// to store loose isomorphic values;
			List<string> nonIsomorphs = new List<string>();  // stores non-isomorphic words
			List<string> allIsomorphicValues;  // stores all loose isomorphic values

			// Fills the isomorphic dictionaries with every word and respective isomorphic value
			for (int i = 0; i < words.Count(); i++)
			{
				exactIsomorphsDictionary.ContainsKey(words.ElementAt(i));
				exactIsomorphsDictionary[words.ElementAt(i)] = CalculateExactIsomorphicValue(words.ElementAt(i));
				looseIsomorphsDictionary.ContainsKey(words.ElementAt(i));
				looseIsomorphsDictionary[words.ElementAt(i)] = CalculateLooseIsomorphicValue(words.ElementAt(i));
			}
			allIsomorphicValues = looseIsomorphsDictionary.Values.ToList();
			int numberOfOccurrences = 0;
			foreach (string key in looseIsomorphsDictionary.Keys.ToArray())
			{
				for (int i = 0; i < allIsomorphicValues.Count(); i++)
				{
					if (looseIsomorphsDictionary[key] == allIsomorphicValues[i])
					{
						numberOfOccurrences++;
						if (numberOfOccurrences > 1)
						{
							break;
						}
					}
				}
				if (numberOfOccurrences == 1) nonIsomorphs.Add(looseIsomorphsDictionary[key]);
			}

			//nonIsomorphs
			for (int i = 0; i < nonIsomorphs.Count(); i++)
			{
				looseIsomorphsDictionary.Remove(nonIsomorphs[i]);
				exactIsomorphsDictionary.Remove(nonIsomorphs[i]);
			}

			// display keys with identical isomorphic values
			string isomorphs = "Exact Isomorphs:\n";
			string[] exactValues = exactIsomorphsDictionary.Values.ToArray();
			string[] exactKeys = exactIsomorphsDictionary.Keys.ToArray();
			string[] looseValues = looseIsomorphsDictionary.Values.ToArray();
			string[] looseKeys = looseIsomorphsDictionary.Keys.ToArray();
			for (int i = 0; i < exactKeys.Length; i++)
			{
				// isomorphs are stored as values while words are stored as keys
				// how do we compare values of key/value pairs?
				// furthermore, how do we obtain the key(s) that point to that value?
				string isomorphicValue = exactValues[i];
				isomorphs += isomorphicValue + ": ";
				for (int j = 0; j < exactValues.Length; j++) 
				{
					if (exactValues[j] == isomorphicValue)
					{
							isomorphs += exactKeys[j] + " ";
							exactIsomorphsDictionary.Remove(exactKeys[j]);
                    }
				}
				isomorphs += "\n";
				// For every unique isomorph
				// Concatenate to variable, isomorphs, as follows:
				// Exact Isomorphs:\n
				// <isomorphicValue>: <key>, <key>, <key> ... \n
				// <isomorphicValue>: ... \n
				// ... \n
				// \n
				// Loose Isomorphs:\n
				// <isomorphicValue>: <key>, <key>, <key> ... \n
				// <isomorphicValue>: ... \n
				// ... \n
				// Non-Isomorphs:\n
				// <key>, <key>, <key> ...

				// As the string is formed, remove used keys from its respective dictionary
			}
			isomorphs += "\n\nLoose Isomorphs:\n";
			for (int i = 0; i < looseKeys.Length; i++)
			{
				// isomorphs are stored as values while words are stored as keys
				// how do we compare values of key/value pairs?
				// furthermore, how do we obtain the key(s) that point to that value?
				string isomorphicValue = looseValues[i];
				isomorphs += isomorphicValue + ": ";
				for (int j = 0; j < looseValues.Length; j++)
				{
					if (looseValues[j] == isomorphicValue)
					{
							isomorphs += looseKeys[j] + " ";
							//looseIsomorphsDictionary.Remove(looseKeys[i]);
					}
				}
				isomorphs += "\n";
			}
			isomorphs += "\n\nNon Isomorphs: ";
			for (int i = 0; i < nonIsomorphs.Count(); i++)
			{
					isomorphs += nonIsomorphs[i] + " ";
			}

			return isomorphs;
		}

		// Returns a cipher where a sentence is referenced to produce the encryption
		// The easiest way to decrypt an encoded message is to possess the key
		public static string GenerateIsomorphicCipher(string encryption)
		{
			Dictionary<string, uint> conversion = new Dictionary<string, uint>();
			List<string> letters;
			List<uint> numbers;
			uint uniqueCharacter = 1;
			// fills the local dictionary with every unique character along with an associated value
			for (int i = 0; i < encryption.Length; i++)
			{
				// Check if character is unique and not a whitespace character
				if ((!conversion.ContainsKey(encryption[i].ToString().ToUpperInvariant())) && encryption[i].ToString() != " ")
				{
					// add character to the dictionary with unique value
					conversion[encryption[i].ToString().ToUpperInvariant()] = uniqueCharacter++;
				}
			}

			letters = conversion.Keys.ToList();
			numbers = conversion.Values.ToList();
			string cipher = "Key = " + encryption + "\n\n";
			for (int i = 0; i < letters.Count(); i++)
			{
				cipher += letters[i].ToString() + " = " + numbers[i].ToString() + "\n";
			}
			return cipher;
		}

		public static string CalculateLooseIsomorphicValue(string input)
		{
			Dictionary<char, uint> tempDictionary = new Dictionary<char, uint>();
			// fills the local dictionary with every character with number of occurrences
			for (int i = 0; i < input.Length; i++)
			{
				if (tempDictionary.ContainsKey(input[i]))
				{
					tempDictionary[input[i]]++;
				}
				else
				{
					tempDictionary[input[i]] = 1;
				}
			}

			// sorts elements for proper order
			uint[] looseIsomorph = tempDictionary.Values.ToArray();
			Sorter<uint>.InsertionSort(looseIsomorph);

			// converts the array to a string (with spaces)
			string looseIsomorphicString = "";
			for (int i = 0; i < looseIsomorph.Length; i++)
			{
				looseIsomorphicString += looseIsomorph[i] + " ";
			}

			return looseIsomorphicString;
		}

		public static string CalculateExactIsomorphicValue(string input)
		{
			Dictionary<string, uint> tempDictionary = new Dictionary<string, uint>();
			string tempString = "";
			uint uniqueCharacter = 0;
			// fills the local dictionary with every unique character along with an associated value
			// if it encounters a character already in the dictionary, the character adopts that value
			for (int i = 0; i < input.Length; i++)
			{
				if (tempDictionary.ContainsKey(input[i].ToString()))
				{
					// character becomes predefined value
					tempString += tempDictionary[input[i].ToString()];
				}
				else
				{
					// add character to the dictionary with unique value
					tempDictionary[input[i].ToString()] = uniqueCharacter++;
					// character becomes predefined value
					tempString += tempDictionary[input[i].ToString()];
				}
			}

			// uint[] exactIsomorph = tempDictionary.ToArray(); // line is incomplete
			string exactIsomorphicString = "";
			for (int i = 0; i < tempString.Length; i++)
			{
				if (i != tempString.Length - 1)
				{
					exactIsomorphicString += tempString[i] + " ";
				}
				else
				{
					exactIsomorphicString += tempString[i];
				}
			}

			return exactIsomorphicString;
		}
	}
}
