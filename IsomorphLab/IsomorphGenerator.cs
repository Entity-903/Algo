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
			List<string> nonIsomorphs;  // stores non-isomorphic words

			// Fills the isomorphic dictionaries with every word and respective isomorphic value
			for (int i = 0; i < words.Count(); i++)
			{
				exactIsomorphsDictionary.ContainsKey(words.ElementAt(i));
				exactIsomorphsDictionary[words.ElementAt(i)] = CalculateExactIsomorphicValue(words.ElementAt(i));
				looseIsomorphsDictionary.ContainsKey(words.ElementAt(i));
				looseIsomorphsDictionary[words.ElementAt(i)] = CalculateLooseIsomorphicValue(words.ElementAt(i));
			}
			nonIsomorphs = looseIsomorphsDictionary.Keys.Distinct().ToList();
			for (int i = 0; i < nonIsomorphs.Count(); i++)
			{
				looseIsomorphsDictionary.Remove(nonIsomorphs[i]);
				exactIsomorphsDictionary.Remove(nonIsomorphs[i]);
			}

			// display keys with identical isomorphic values
			string isomorphs = "Exact Isomorphs:\n";
			foreach (string key in exactIsomorphsDictionary.Keys.ToArray())
			{
				string isomorphicValue = exactIsomorphsDictionary[key];
				if (exactIsomorphsDictionary.ContainsValue(isomorphicValue))
				{
					// For every unique isomorph
					// Concatenate to variable isomorphs as follows:
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
				}
			}

			return isomorphs;
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
