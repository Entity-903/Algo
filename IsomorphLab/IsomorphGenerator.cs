using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				exactIsomorphsDictionary.ContainsKey(words[i]);
				exactIsomorphsDictionary[words[i]] = CalculateExactIsomorphicValue(words[i]);
				looseIsomorphsDictionary.ContainsKey(words[i]);
				looseIsomorphsDictionary[words[i]] = CalculateLooseIsomorphicValue(words[i]);
			}
			nonIsomorphs = looseIsomorphsDictionary.Keys.Distinct().ToList();
			for (int i = 0; i < nonIsomorphs.Count(); i++)
			{
				looseIsomorphsDictionary.Remove(nonIsomorphs[i]);
				exactIsomorphsDictionary.Remove(nonIsomorphs[i]);
			}

			// display keys with identical isomorphic values
			foreach (string key in exactIsomorphsDictionary.Keys.ToArray())
			{
				string isomorphicValue = exactIsomorphsDictionary[key]
				if (exactIsomorphsDictionary.ContainsValue(isomorphicValue))
				{

				}
			}
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
			uint[] conversion = tempDictionary.Values.ToArray();
			uint[] looseIsomorph = InsertionSort(conversion);

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
			uint uniqueCharacter = 0;
			// fills the local dictionary with every unique character along with an associated value
			// if it encounters a character already in the dictionary, the character adopts that value
			for (int i = 0; i < input.Length; i++)
			{
				if (tempDictionary.ContainsKey(input[i]))
				{
					// character becomes predefined value
					input[i] = tempDictionary.TryGetValue(input[i]);
				}
				else
				{
					// add character to the dictionary with unique value
					tempDictionary[input[i]] = uniqueCharacter++;
		}
			}

			uint[] exactIsomorph = tempDictionary.;
			string exactIsomorphicString = "";
			for (int i = 0; i < exactIsomorph.Length; i++)
			{
				if (i != exactIsomorph.Length - 1)
				{
					exactIsomorphicString += exactIsomorph[i] + " ";
				}
				else
				{
					exactIsomorphicString += exactIsomorph[i];
				}
			}

			return exactIsomorphicString;
		}
	}
}
