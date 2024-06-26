﻿using System;
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
			// Isomophic value, words
			Dictionary<string, List<string>> exactIsomorphsDictionary = new Dictionary<string, List<string>>();
			Dictionary<string, List<string>> looseIsomorphsDictionary = new Dictionary<string, List<string>>();
			// Misc
			List<string> nonIsomorphs = new List<string>();  // stores non-isomorphic words

			// Fills the isomorphic dictionaries with isomorphic values and any words associated with them.
			for (int i = 0; i < words.Count(); i++)
			{
				string exactIsomorph = CalculateExactIsomorphicValue(words.ElementAt(i));
				string looseIsomorph = CalculateLooseIsomorphicValue(words.ElementAt(i));

				if (!exactIsomorphsDictionary.ContainsKey(exactIsomorph))
				{
					exactIsomorphsDictionary.Add(exactIsomorph, new List<string>());
				}
				if (!looseIsomorphsDictionary.ContainsKey(looseIsomorph))
				{
					looseIsomorphsDictionary.Add(looseIsomorph, new List<string>());
				}
				
				exactIsomorphsDictionary[exactIsomorph].Add(words.ElementAt(i));
				looseIsomorphsDictionary[looseIsomorph].Add(words.ElementAt(i));

			}

			// Finds and moves any words with a unique isomorphic value into a list of non-isomorphs
			foreach (string key in looseIsomorphsDictionary.Keys.ToArray())
			{
				if (looseIsomorphsDictionary[key].Count() == 1)
				{
					nonIsomorphs.Add(looseIsomorphsDictionary[key][0]);
					exactIsomorphsDictionary.Remove(CalculateExactIsomorphicValue(looseIsomorphsDictionary[key][0]));
					looseIsomorphsDictionary.Remove(key);
				}
			}

			// display keys with identical isomorphic values
			string isomorphs = "Exact Isomorphs:\n";

			foreach (string key in exactIsomorphsDictionary.Keys)
			{
				List<string> listedWords = exactIsomorphsDictionary[key];
				isomorphs += key + ": ";

				for (int i = 0; i < listedWords.Count; i++) 
				{
					isomorphs += listedWords[i] + " ";
				}
				isomorphs += "\n";

				// ---------------------------------------------------------------------------
				// ---------------------------------------------------------------------------
				// ---------------------------------------------------------------------------
				// For every unique isomorph
				// Concatenate to variable, isomorphs, as follows:
				// Exact Isomorphs:\n
				// <key>: <value>, <value>, <value> ... \n
				// <key>: ... \n
				// ... \n
				// \n
				// Loose Isomorphs:\n
				// <key>: <value>, <value>, <value> ... \n
				// <key>: ... \n
				// ... \n
				// Non-Isomorphs:\n
				// <key>, <key>, <key> 
			}

			isomorphs += "\nLoose Isomorphs:\n";
			foreach (string key in looseIsomorphsDictionary.Keys)
			{
				List<string> listedWords = looseIsomorphsDictionary[key];
				isomorphs += key + ": ";
				for (int i = 0; i < listedWords.Count(); i++)
				{
							isomorphs += listedWords[i] + " ";
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
				if ((!conversion.ContainsKey(encryption[i].ToString().ToUpperInvariant())) && (char.ToUpperInvariant(encryption[i]) >= 'A' && char.ToUpperInvariant(encryption[i]) <= 'Z'))
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


	//Calculate Isomorphic values 
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
