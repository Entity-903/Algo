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