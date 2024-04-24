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

		// It would appear that the quicksort algorithm is just wrong...
		public static void QuickSort(T[] arr, int partitionStart, int partitionEnd)
		{
			// End result of the first QuickSort should proceed as follows:
			// { 8, 5, 7, 6, 1, 4, 2, 3}
			//   ^        ^           ^
			// { 3, 5, 7, 6, 1, 4, 2, 8}
			//   ^        ^           ^
			// { 3, 5, 7, 6, 1, 4, 2, 8}
			//         ^  ^        ^
			// { 3, 5, 2, 6, 1, 4, 7, 8}
			//         ^  ^        ^
			// { 3, 5, 2, 6, 1, 4, 7, 8}
			//            ^     ^
			// When the pivot value first moves,
			// we know that every value less than the pivot at new index is to the left
			// and greater than is to the right
			// { 3, 5, 2, 4, 1, (6), 7, 8}
			//            ^  ^
			// { 3, 5, 2, 1, 4, (6), 7, 8}

			// { 3, 48, 17, 30, 12, 9 }
			//   ^       ^          ^
			// { 3, 48, 9, 30, 12, 17 }
			//   ^      ^           ^
			// { 3, 48, 9, 30, 12, 17 }
			//       ^  ^           ^
			// { 3, (9), 48, 30, 12, 17 }
			//       ^    ^           ^

			// If only one element, return. It is sorted
			if (partitionEnd - partitionStart < 1)
			{
				return;
			}

			int pivotIndex = ChoosePivot(arr, partitionStart, partitionEnd); // pivotIndex = 6
			int newPivotIndex = -1;
			int currentRightIndex = partitionEnd;
			bool swapLeftMade = false;

			for (int i = partitionStart; i < pivotIndex; i++) // Elements before pivot
			{
				// consider adding more to this if statement.
				// if arr[i] is less than arr[pivotIndex], proceed (i++) until we find an element less than arr[pivotIndex]
				if (arr[i].CompareTo(arr[pivotIndex]) > 0)
				{
					swapLeftMade = true;
					bool swapRightMade = false;
					for (int j = currentRightIndex; j > pivotIndex; j--) // Elements after pivot
					{
						if (arr[j].CompareTo(arr[pivotIndex]) < 0)
						{
							Swap(arr, i, j);
							swapRightMade = true;
							currentRightIndex = j - 1;
							break;
						}
					}
					if (!swapRightMade)
					{
						// Swap left side with pivot if no right element satifies the condition
						// consider a for loop here to iterate through all elements left of the pivot
						for (int j = partitionStart; j < pivotIndex; j++)
						{
							if (arr[j].CompareTo(arr[pivotIndex]) > 0)
							{
								Swap(arr, j, pivotIndex);
								if (newPivotIndex == -1)
								{
									newPivotIndex = j;
								}
							}
						}

					}
				}

			} // end of for loop
			  // check to ensure that:
			  // we have a right element less than the pivot, but no left elements greater than the pivot
			if (!swapLeftMade)
			{
				for (int j = partitionEnd; j > pivotIndex; j--) // Elements after pivot
				{
					if (arr[j].CompareTo(arr[pivotIndex]) < 0)
					{
						Swap(arr, pivotIndex, j);
						if (newPivotIndex == -1)
						{
							newPivotIndex = j;
						}
					}
				}
			}

			// Check if the pivotIndex has changed
			if (newPivotIndex == -1)
			{
				// QuickSort the left of the pivot
				QuickSort(arr, partitionStart, pivotIndex - 1);
				// Quicksort the right of the pivot
				QuickSort(arr, pivotIndex + 1, partitionEnd);
			}
			else // use newPivotIndex
			{
				// QuickSort the left of the pivot
				QuickSort(arr, partitionStart, newPivotIndex - 1);
				// Quicksort the right of the pivot
				QuickSort(arr, newPivotIndex + 1, partitionEnd);
			}
		}

		// ChoosePivot should not cause any issues. Problems are likely due to QuickSort
		public static int ChoosePivot(T[] arr, int partitionStart, int partitionEnd)
		{
			// 1, 2, 3
			// 0, 1, 2
			//    ^
			// 1, 2, 3, 4, 5
			// 0, 1, 2, 3, 4
			//       ^
			// 1, 2, 3, 4, 5, 6, 7
			// 0, 1, 2, 3, 4, 5, 6
			//          ^
			// 4, 5, 6
			// 3, 4, 5
			//    ^
			// 
			int middleElementIndex;

			// partitionStart = 5
			// partitionEnd = 6

			int tempValue = partitionEnd - partitionStart; // 1
			if (tempValue % 2 == 0)
			{
				middleElementIndex = (tempValue) / 2 + partitionStart;
			}
			else
			{
				middleElementIndex = (tempValue) / 2 + partitionStart + 1;
			}

			int firstElementIndex = partitionStart;
			int lastElementIndex = partitionEnd;
			T maxValue;
			T minValue;
			T destinedPivotValue;

			// find maxValue
			if (arr[firstElementIndex].CompareTo(arr[middleElementIndex]) > 0)
			{
				if (arr[firstElementIndex].CompareTo(arr[lastElementIndex]) > 0)
				{
					maxValue = arr[firstElementIndex];
				}
				else
				{
					maxValue = arr[lastElementIndex];
				}
			}
			else
			{
				if (arr[middleElementIndex].CompareTo(arr[lastElementIndex]) > 0)
				{
					maxValue = arr[middleElementIndex];
				}
				else
				{
					maxValue = arr[lastElementIndex];
				}
			}

			// find minValue and destinedPivotValue excluding maxValue
			if (maxValue.Equals(arr[lastElementIndex]))
			{
				if (arr[firstElementIndex].CompareTo(arr[middleElementIndex]) < 0)
				{
					minValue = arr[firstElementIndex];
					destinedPivotValue = arr[middleElementIndex];
				}
				else
				{
					minValue = arr[middleElementIndex];
					destinedPivotValue = arr[firstElementIndex];
				}
			}
			else if (maxValue.Equals(arr[middleElementIndex]))
			{
				if (arr[firstElementIndex].CompareTo(arr[lastElementIndex]) < 0)
				{
					minValue = arr[firstElementIndex];
					destinedPivotValue = arr[lastElementIndex];
				}
				else
				{
					minValue = arr[lastElementIndex];
					destinedPivotValue = arr[firstElementIndex];
				}
			}
			else
			{
				if (arr[middleElementIndex].CompareTo(arr[lastElementIndex]) < 0)
				{
					minValue = arr[middleElementIndex];
					destinedPivotValue = arr[lastElementIndex];
				}
				else
				{
					minValue = arr[lastElementIndex];
					destinedPivotValue = arr[middleElementIndex];
				}
			}


			// maxValue = 9
			// minValue = 1
			// lastElementIndex = 1
			// firstElementIndex = 0
			// middleElementIndex = partitionEnd

			// arr[lastElementIndex] = maxValue;
			// arr[firstElementIndex] = minValue;
			// arr[middleElementIndex] = destinedPivotValue;

			arr[lastElementIndex] = maxValue;
			arr[firstElementIndex] = minValue;

			if (middleElementIndex == partitionStart)
			{
				arr[middleElementIndex] = arr[firstElementIndex];
			}
            else
            {
				if (middleElementIndex == partitionEnd)
				{
					arr[middleElementIndex] = arr[lastElementIndex];
				}
				else
				{
					arr[middleElementIndex] = destinedPivotValue;
				}
            }
			return middleElementIndex;
		}

		public static void MergeSort(ref T[] arr)
		{
			// { 3, 2, 1 }
			// { (3), 2, 1 }
			// { 3 }, { 2, 1 }
			// { 3 }, { 2 }, { 1 }
			// { 3 }, { 1, 2 }
			// { 1, 2, 3 }

			if (arr == null)
			{
				return;
			}

			if (arr.Length > 1)
			{
				int middleIndex = (arr.Length) / 2; // Don't subtract one from arr.Length! // 1
				T[] firstHalf = new T[middleIndex]; // 1
				T[] secondHalf = new T[arr.Length - middleIndex]; // 1

				for (int i = 0; i < arr.Length; i++)
				{
					if (i < middleIndex)
					{
						firstHalf[i] = arr[i];
					}
					else
					{
						secondHalf[i - middleIndex] = arr[i];
					}
				}

				MergeSort(ref firstHalf);
				MergeSort(ref secondHalf);

				arr = Merge(firstHalf, secondHalf);
			}
			else
			{
				// errors on 8th continue
				// I don't think we are merging the pieces back together
				// Actually, it appears that the values aren't sorted at all, how?
				// It would appear that we never return the mergedCollection, which never updates the original array
				return;
			}
		}

		public static T[] Merge(T[] firstHalf, T[] secondHalf)
		{
			T[] mergedCollection = new T[firstHalf.Length + secondHalf.Length];
			int firstHalfTracker = 0;
			int secondHalfTracker = 0;

			for (int i = 0; i < mergedCollection.Length; i++)
			{
				if (firstHalfTracker != firstHalf.Length)
				{
					if (secondHalfTracker != secondHalf.Length)
					{
						if (firstHalf[firstHalfTracker].CompareTo(secondHalf[secondHalfTracker]) < 0)
						{
							mergedCollection[i] = firstHalf[firstHalfTracker];
							firstHalfTracker++;

						}
						else
						{
							mergedCollection[i] = secondHalf[secondHalfTracker];
							secondHalfTracker++;
						}
					}
					else
					{
						for (int j = firstHalfTracker; j < firstHalf.Length; j++)
						{
							mergedCollection[i++] = firstHalf[j];
						}
						break;
					}
				}
				else
				{
					for (int j = secondHalfTracker; j < secondHalf.Length; j++)
					{
						mergedCollection[i++] = secondHalf[j];
					}
					break;
				}
			}
			return mergedCollection;
		}
	}
}
