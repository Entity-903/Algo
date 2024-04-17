using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Reflection;

namespace SortingLibrary
{
	public class Sorter<T> where T : IComparable<T>
	{
		/* 
		 * Arranges numerical elements in a collection from least to greatest
		Until nothing has changed
		Iterate over all elements of array except for the last element
		Compare current element to next element
			IF current > next
				swap
		*/
		public static void BubbleSort(T[] arr)
		{
			int value = 1;
			int otherValue = 1;
			string thisKey = "thisOne";
			string otherKey = "otherOne";
			string alternateKey = "alternateOne";
			Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();

			dictionary1.ContainsKey(thisKey);
			dictionary1[thisKey] = value;

			dictionary2.ContainsKey(thisKey);
			dictionary2[thisKey] = value;
			dictionary2.ContainsKey(otherKey);
			dictionary2[otherKey] = otherValue;
			dictionary2.ContainsKey(alternateKey);
			dictionary2[alternateKey] = otherValue;


			dictionary1.ContainsValue(dictionary1[thisKey]);
			//dictionary1.Keys.Where("value", true); //Keys.ToList();
			//foreach (string key in dictionary1.Keys.ToArray()) 
			//{ 

			//}

			bool hasChanged = false;
			do
			{
				hasChanged = false;
				for (int i = 0; i < arr.Length - 1; i++)
				{
					if (arr[i].CompareTo(arr[i + 1]) > 0)
					{
						Swap(arr, i, i + 1);
						hasChanged = true;
					}
				}
			} while (hasChanged);
		} // O(n^2)

		public static void Swap(T[] a, int indexA, int indexB)
		{
			T temp = a[indexA];
			a[indexA] = a[indexB];
			a[indexB] = temp;
		}

		// Arranges numerical elements in a collection from least to greatest
		public static void InsertionSort(T[] arr)
		{
			//int[] numbers = { 5, 2, 3, 1, 4 };
			for (int i = 1; i < arr.Length; i++)
			{
				T insertedValue = arr[i];
				for (int j = 1; j <= i; j++)
				{
					if (insertedValue.CompareTo(arr[i - j]) < 0) // insertedValue < arr[i - j]
					{
						InsertSwap(arr, i - j, i - j + 1);
						if (i == j) arr[0] = insertedValue;
					}
					else
					{
						arr[i - j + 1] = insertedValue;
						break;
					}
				}
			}
		} // O(n^2) Due to a for loop within a for loop

		// Copies the value from the old index over to the new index
		// THIS DOES CHANGE THE VALUE AT indexOld!!!
		public static void InsertSwap(T[] arr, int indexOld, int indexNew)
		{
			arr[indexNew] = arr[indexOld];
		}

		// Sort from least to greatest
		public static void SelectionSort(T[] arr)
		{
			// [0, 1, 2, 3, 4]
			//     X
			for (int i = 0; i < arr.Length; i++)
			{
				T selected = arr[i];
				for (int j = 1; j < arr.Length - i; j++)
				{
					// if selected > array at index, swap values
					if (selected.CompareTo(arr[i + j]) > 0)
					{
						SelectionSwap(arr, i + j, ref selected);
					}
				}
				arr[i] = selected;
			}
		} // O(n^2) due to a for loop within a for loop


		public static void SelectionSwap(T[] arr, int index, ref T selected)
		{
			T temp = arr[index];
			arr[index] = selected;
			selected = temp;
		}

		public static int BinarySearch(T searchValue, T[] arr, int rangeStart, int rangeEnd) // 13
		{
			int range = rangeEnd - rangeStart;
			int middleValueIndex = rangeStart + (rangeEnd - rangeStart) / 2; // 4
			T middleValue = arr[middleValueIndex]; // 1
			if (range < 0) return -1;
			if (range == 0) // 1
			{
				if (!(middleValue.Equals(searchValue))) // 1
				{
					return -1;
				}
			}

			if (middleValue.Equals(searchValue)) // 1
			{
				return middleValueIndex;
			}
			else if (middleValue.CompareTo(searchValue) > 0) // 1
			{
				int newRangeEnd = middleValueIndex - 1; // 2
				return BinarySearch(searchValue, arr, rangeStart, newRangeEnd);
			}
			else
			{
				int newRangeStart = middleValueIndex + 1; // 2
				return BinarySearch(searchValue, arr, newRangeStart, rangeEnd);
			}
		} // O(log(n))
	}
}
