using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using SortingLibrary;

namespace SortingLibraryTests
{
	[TestClass]
	public class SorterTests
	{
		private int itemsInArray = 100;
		protected int[] hunRand;
		protected int[] hunDesc;
		protected int[] hunAsc;
		protected string expected;

		#region setupCode

		public SorterTests()
		{
			hunRand = new int[itemsInArray];
			hunDesc = new int[itemsInArray];
			hunAsc = new int[itemsInArray];
			Random rand = new Random(12271978);
			for (int i = 0; i < hunRand.Length; i++)
			{
				hunRand[i] = rand.Next(100001);
			}

			//ten = mil.Take(10).ToArray();
			hunDesc = hunRand.OrderByDescending(x => x).ToArray();
			hunAsc = hunRand.OrderBy(x => x).ToArray();
			expected = ArrayToString(hunAsc);
		}

		protected int[] CloneRand
		{
			get { return (int[])hunRand.Clone(); }
		}

		protected int[] CloneDesc
		{
			get { return (int[])hunDesc.Clone(); }
		}

		protected int[] CloneAsc
		{
			get { return (int[])hunAsc.Clone(); }
		}

		protected string ArrayToString(int[] a)
		{
			StringBuilder sb = new StringBuilder();

			if (a.Length > 0)
			{
				sb.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					sb.Append(", ");
					sb.Append(a[i]);
				}
			}

			return sb.ToString();
		}

		#endregion
		/* Psuedo-Code:
			Test_Sort_HappyPath
				input = [5, 3, 4, 1, 2]
				expectedOutput = [1, 2, 3, 4, 5]
				Assert.IsTrue(sort(input).SequenceEqual(expectedOutput));
		*/
		[TestMethod]
		public void Test_BubbleSort_HappyPath()
		{
			int[] numbers = { 5, 2, 3, 1, 4 };
			int[] expectedNumbers = { 1, 2, 3, 4, 5 };
			Sorter<int>.BubbleSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		/* Pseudo-Code:
			Test_Swap_HappyPath
				array = [5, 3, 4, 1, 2]
				indexA = 3
				indexB = 4
				expectedOutput = [5, 3, 4, 2, 1]
				swap(array, indexA, indexB)
				Assert.IsTrue(SortingLibrary(array).SequenceEqual(expectedOutput))
		*/
		[TestMethod]
		public void Test_Swap_HappyPath()
		{
			int[] numbers = { 5, 3, 4, 1, 2 };
			int indexA = 3;
			int indexB = 4;
			int[] expectedNumbers = { 5, 3, 4, 2, 1 };
			Sorter<int>.Swap(numbers, indexA, indexB);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		/* Pseudo-Code:
		[ExpectedException(typeof(NullReferenceException))]
		Test_Sort_NullCollection
			input = null
			sort(input)
		*/
		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_BubbleSort_NullCollection()
		{
			int[] input = null;
			Sorter<int>.BubbleSort(input);
		}

		/* Pseudo-Code:
			[ExpectedException(typeof(NullReferenceException))]
			Test_Swap_NullCollection
				array = null
				indexA = null
				indexB = null
				swap(array, indexA, indexB)
		*/
		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_Swap_NullCollection()
		{
			int[] input = null;
			int indexA = 999;
			int indexB = 999;
			Sorter<int>.Swap(input, indexA, indexB);
		}

		/* Pseudo-Code:
			[ExpectedException(typeof(IndexOutOfRangeException))]
			Test_Swap_OutOfBounds
				array = [1, 6, 3]
				indexA = 3
				indexB = 4
				swap(array, indexA, indexB)
		*/
		[TestMethod,
		ExpectedException(typeof(IndexOutOfRangeException))]
		public void Test_Swap_OutOfBounds()
		{
			int[] input = { 1, 6, 3 };
			int indexA = 3;
			int indexB = 4;
			Sorter<int>.Swap(input, indexA, indexB);
		}
		// Test for the input of a collection with one element
		[TestMethod]
		public void OnlyOne_Bubble()
		{
			int[] numbers = { 1 };
			int[] expectedNumbers = { 1 };
			Sorter<int>.BubbleSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_InsertionSort_HappyPath()
		{
			int[] numbers = { 5, 2, 3, 1, 4 };
			int[] expectedNumbers = { 1, 2, 3, 4, 5 };
			Sorter<int>.InsertionSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		[TestMethod]
		public void OnlyOne_Insertion()
		{
			int[] numbers = { 1 };
			int[] expectedNumbers = { 1 };
			Sorter<int>.InsertionSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_InsertionSort_NullCollection()
		{
			int[] input = null;
			Sorter<int>.BubbleSort(input);
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_InsertSwap_NullCollection()
		{
			int[] input = null;
			int indexA = 999;
			int indexB = 999;
			Sorter<int>.InsertSwap(input, indexA, indexB);
		}

		[TestMethod,
		ExpectedException(typeof(IndexOutOfRangeException))]
		public void Test_InsertSwap_OutOfBounds()
		{
			int[] input = { 1, 6, 3 };
			int indexA = 3;
			int indexB = 4;
			Sorter<int>.InsertSwap(input, indexA, indexB);
		}

		[TestMethod]
		public void Test_InsertSwap_HappyPath()
		{
			int[] numbers = { 5, 3, 4, 1, 2 };
			int indexA = 3;
			int indexB = 4;
			int[] expectedNumbers = { 5, 3, 4, 1, 1 };
			Sorter<int>.InsertSwap(numbers, indexA, indexB);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_SelectionSort_HappyPath()
		{
			int[] numbers = { 5, 2, 3, 1, 4 };
			int[] expectedNumbers = { 1, 2, 3, 4, 5 };
			Sorter<int>.SelectionSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		[TestMethod]
		public void Test_SelectionSwap_HappyPath()
		{
			int[] numbers = { 5, 3, 4, 1, 2 };
			int index = 3;
			int selected = 4;
			int[] expectedNumbers = { 5, 3, 4, 4, 2 };
			Sorter<int>.SelectionSwap(numbers, index, ref selected);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
			Assert.IsTrue(selected == 1);
		}

		[TestMethod]
		public void OnlyOne_Selection()
		{
			int[] numbers = { 1 };
			int[] expectedNumbers = { 1 };
			Sorter<int>.SelectionSort(numbers);
			Assert.IsTrue(numbers.SequenceEqual(expectedNumbers));
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_SelectionSort_NullCollection()
		{
			int[] input = null;
			Sorter<int>.SelectionSort(input);
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_SelectionSwap_NullCollection()
		{
			int[] input = null;
			int index = 999;
			int selected = 999;
			Sorter<int>.SelectionSwap(input, index, ref selected);
		}

		[TestMethod,
		ExpectedException(typeof(IndexOutOfRangeException))]
		public void Test_SelectionSwap_OutOfBounds()
		{
			int[] input = { 1, 6, 3 };
			int indexA = 3;
			int selected = 4;
			Sorter<int>.SelectionSwap(input, indexA, ref selected);
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_BinarySearch_HappyPath()
		{
			int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			int searchValue = 2;
			int expectedOutput = 1;
			int rangeStart = 0;
			int rangeEnd = 9;
			Assert.IsTrue(Sorter<int>.BinarySearch(searchValue, input, rangeStart, rangeEnd) == expectedOutput);
		}

		[TestMethod]
		public void Test_BinarySearch_NotInCollection()
		{
			int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			int searchValue = 17;
			int expectedOutput = -1;
			int rangeStart = 0;
			int rangeEnd = 9;
			Assert.IsTrue(Sorter<int>.BinarySearch(searchValue, input, rangeStart, rangeEnd) == expectedOutput);
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_BinarySearch_NullCollection()
		{
			int[] input = null;
			int searchValue = 5;
			int rangeStart = 0;
			int rangeEnd = 9;
			Sorter<int>.BinarySearch(searchValue, input, rangeStart, rangeEnd);
		}

		[TestMethod]
		public void Test_BinarySearch_EndBeforeStart()
		{
			// The failsafe of -1 is in place because, otherwise, the function will call itself infinitely,
			// with rangeEnd being stuck on 7 and rangeStart stuck on 9, not only searching in the opposite
			// direction of the searchValue, but never reaching any return statement
			int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			int searchValue = 2;
			int rangeStart = 9;
			int rangeEnd = 0;
			int expectedOutput = -1;
			Assert.IsTrue(Sorter<int>.BinarySearch(searchValue, input, rangeStart, rangeEnd) == expectedOutput);
		}

		[TestMethod,
		ExpectedException(typeof(IndexOutOfRangeException))]
		public void Test_BinarySearch_RangeValuesOutOfBounds()
		{
			int[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			int searchValue = 2;
			int rangeStart = 25;
			int rangeEnd = 50;
			Sorter<int>.BinarySearch(searchValue, input, rangeStart, rangeEnd);
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_QuickSort_HappyPath()
		{
			int[] input = { 8, 5, 7, 6, 1, 4, 2, 3 }; // { 8, 5, 7, 6, 1, 4, 2, 3 } // { 3, 48, 17, 30, 12, 9 }
			int[] expectedOutput = { 1, 2, 3, 4, 5, 6, 7, 8 }; // { 1, 2, 3, 4, 5, 6, 7, 8 } // { 3, 9, 12, 17, 30, 48 }
			Sorter<int>.QuickSort(input, 0, input.Length - 1);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
			// 1, 2, 3, 6, 7, 4, 5, 8 without newPivotIndex
			// 1, 2, 3, 5, 6, 4, 7, 8 with newPivotIndex (actively improves result)

			// EXPECT: { 8, 5, 7, 6, 1, 4, 2, 3 }
			// RETURN: { 1, 2, 3, 4, 5, 6, 7, 8 } CORRECT
			
			// EXPECT: { 3, 9, 12, 17, 30, 48 }
			// RETURN: { 3, 9, 12, 17, 30, 30 } I have plans for ChoosePivot... plans produced desired results!
		}

		[TestMethod,
		ExpectedException(typeof(NullReferenceException))]
		public void Test_QuickSort_NullCollection()
		{
			int[] input = null;
			Sorter<int>.QuickSort(input, 0, input.Length - 1);
		}

		[TestMethod]
		public void Test_QuickSort_DuplicateValues()
		{
			int[] input = { 0, 1, 0, 1, 0, 1, 0, 1 };
			int[] expectedOutput = { 0, 0, 0, 0, 1, 1, 1, 1 };
			Sorter<int>.QuickSort(input, 0, input.Length - 1);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_QuickSort_OneElement()
		{
			int[] input = { 9 };
			int[] expectedOutput = { 9 };
			Sorter<int>.QuickSort(input, 0, input.Length - 1);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_QuickSort_TwoElements()
		{
			int[] input = { 4, 2 };
			int[] expectedOutput = { 2, 4 };
			Sorter<int>.QuickSort(input, 0, input.Length - 1);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_MergeSort_HappyPath()
		{
			int[] input = { 8, 5, 7, 6, 1, 4, 2, 3 };
			int[] expectedOutput = { 1, 2, 3, 4, 5, 6, 7, 8 };
			Sorter<int>.MergeSort(ref input);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_MergeSort_OneElement()
		{
			int[] input = { 9 };
			int[] expectedOutput = { 9 };
			Sorter<int>.MergeSort(ref input);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_MergeSort_TwoElements()
		{
			int[] input = { 8, 5 };
			int[] expectedOutput = { 5, 8 };
			Sorter<int>.MergeSort(ref input);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_MergeSort_NullCollection()
		{
			int[] input = { };
			int[] expectedOutput = { };
			Sorter<int>.MergeSort(ref input);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}

		[TestMethod]
		public void Test_MergeSort_DuplicateElements()
		{
			int[] input = { 0, 1, 0, 1, 0, 1, 0, 1 };
			int[] expectedOutput = { 0, 0, 0, 0, 1, 1, 1, 1 };
			Sorter<int>.MergeSort(ref input);
			Assert.IsTrue(input.SequenceEqual(expectedOutput));
		}
		//------------------------------------------------
		[TestMethod]
		public void Test_nQueens_HappyPath()
		{
			// In its current state, it does not increment currentSolution
			// Solve_nQueens should not finish this quickly
			// It never finds any solutions
			// n can equal 20 and it will return 0 solutions
			// Turns out the recursive function is, in fact, not recursive.
			// Now it appears to be infinitely looping
			int n = 4; // 9
			int result = Sorter<int>.Create_nQueens(n);
			int expectedOutput = 2; // 352
			Assert.AreEqual(result, expectedOutput);
			// 1, 1
			// 4, 2
			// 9, 352
		}
		[TestMethod]
		public void Test_GetLength_Function()
		{
			bool[,] board = new bool[4, 40];
			int expectedLength = 4;
			Assert.AreEqual(board.GetLength(0), expectedLength);
		}
	}
}

//------------------------------------------------------------------------------------------------------------------------------

// Insignificant Testing of Tests

//		[TestMethod]
//		public void TestEquality()
//		{
//			Assert.AreEqual(true, true);
//		}
//
//		[TestMethod]
//		public void TestRacism()
//		{
//			Assert.AreNotEqual(true, false);
//		}
//
//		[TestMethod]
//		public void TestLowTierExecutable()
//		{
//			Assert.Fail("You should exception error yourself NOW!!!");
//		}