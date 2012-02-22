using System;
using NSpec;

namespace KarateChop
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Karate
    {
        public int RecursiveChop(int searchFor, int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return -1;

            if (numbers.Length == 1)
                return numbers[0] == searchFor ? 0 : -1;

            var medianIndex = GetMedian(0, numbers.Length - 1);

            if (numbers[medianIndex] == searchFor)
                return medianIndex;

            if (numbers[medianIndex] > searchFor)
            {
                var firstHalfLength = medianIndex;
                var firstHalf = new int[firstHalfLength];
                Array.Copy(numbers, firstHalf, firstHalfLength);
                return RecursiveChop(searchFor, firstHalf);
            }

            var lastHalfLength = numbers.Length - medianIndex - 1;
            var lastHalf = new int[lastHalfLength];
            Array.Copy(numbers, medianIndex + 1, lastHalf, 0, lastHalfLength);
            var foundIndex = RecursiveChop(searchFor, lastHalf);
            return foundIndex >= 0 ? foundIndex + medianIndex + 1 : foundIndex;
        }

        public int WhileChop(int searchFor, int[] numbers)
        {
            var min = 0;
            var max = numbers.Length - 1;
            while (min <= max)
            {
                var medianIndex = GetMedian(min, max);
                var medianValue = numbers[medianIndex];
                if (searchFor == medianValue)
                    return medianIndex;
                if (searchFor > medianValue)
                    min = medianIndex + 1;
                else
                    max = medianIndex - 1;
            }
            return -1;
        }

        public int Chop(int searchFor, int[] numbers)
        {
            //return RecursiveChop(searchFor, numbers);
            return WhileChop(searchFor, numbers);
        }

        public int GetMedian(int low, int high)
        {
            return low + ((high - low)/2);
        }
    }

    class Describe_KarateChop : nspec
    {
        private Karate karate;
        public void before_all()
        {
            karate = new Karate();
        }
        public void given_an_empty_array()
        {
            var array = new int[]{};
            it["should return -1."] = () => karate.Chop(1, array).should_be(-1);
        }
        public void given_an_array_with_1_element()
        {
            var array = new[] { 1 };
            it["should return 0 if found."] = () => karate.Chop(1, array).should_be(0);
            it["should return -1 if not found."] = () => karate.Chop(0, array).should_be(-1);
        }
        public void given_an_array_with_2_elements()
        {
            var array = new[] { 1, 3 };
            it["should return 0 trying to find 1."] = () => karate.Chop(1, array).should_be(0);
            it["should return 1 trying to find 3."] = () => karate.Chop(3, array).should_be(1);
            it["should return -1 trying to find 0."] = () => karate.Chop(0, array).should_be(-1);
            it["should return -1 trying to find 2."] = () => karate.Chop(2, array).should_be(-1);
            it["should return -1 trying to find 4."] = () => karate.Chop(4, array).should_be(-1);
            it["should return -1 trying to find -1."] = () => karate.Chop(-1, array).should_be(-1);
        }
        public void given_an_array_with_3_negative_elements()
        {
            var array = new[] { -10, -5, -1 };
            it["should return 0 trying to find -10."] = () => karate.Chop(-10, array).should_be(0);
            it["should return 1 trying to find -5."] = () => karate.Chop(-5, array).should_be(1);
            it["should return 2 trying to find -1."] = () => karate.Chop(-1, array).should_be(2);
            it["should return -1 trying to find 0."] = () => karate.Chop(0, array).should_be(-1);
            it["should return -1 trying to find 2."] = () => karate.Chop(-2, array).should_be(-1);
            it["should return -1 trying to find 4."] = () => karate.Chop(-4, array).should_be(-1);
            it["should return -1 trying to find 6."] = () => karate.Chop(-6, array).should_be(-1);
            it["should return -1 trying to find 8."] = () => karate.Chop(-11, array).should_be(-1);
        }
        public void given_an_array_with_3_mixed_sign_elements()
        {
            var array = new[] { -10, 0, 10 };
            it["should return 0 trying to find -10."] = () => karate.Chop(-10, array).should_be(0);
            it["should return 1 trying to find 0."] = () => karate.Chop(0, array).should_be(1);
            it["should return 2 trying to find 10."] = () => karate.Chop(10, array).should_be(2);
            it["should return -1 trying to find -12."] = () => karate.Chop(-12, array).should_be(-1);
            it["should return -1 trying to find -1."] = () => karate.Chop(-1, array).should_be(-1);
            it["should return -1 trying to find 4."] = () => karate.Chop(4, array).should_be(-1);
            it["should return -1 trying to find 15."] = () => karate.Chop(15, array).should_be(-1);
        }
        public void given_an_array_with_4_elements()
        {
            var array = new[] { 1, 3, 5, 7 };
            it["should return 0 trying to find 1."] = () => karate.Chop(1, array).should_be(0);
            it["should return 1 trying to find 3."] = () => karate.Chop(3, array).should_be(1);
            it["should return 2 trying to find 5."] = () => karate.Chop(5, array).should_be(2);
            it["should return 3 trying to find 7."] = () => karate.Chop(7, array).should_be(3);
            it["should return -1 trying to find 0."] = () => karate.Chop(0, array).should_be(-1);
            it["should return -1 trying to find 2."] = () => karate.Chop(2, array).should_be(-1);
            it["should return -1 trying to find 4."] = () => karate.Chop(4, array).should_be(-1);
            it["should return -1 trying to find 6."] = () => karate.Chop(6, array).should_be(-1);
            it["should return -1 trying to find 8."] = () => karate.Chop(8, array).should_be(-1);
            it["should return -1 trying to find -1."] = () => karate.Chop(-1, array).should_be(-1);
        }
        public void given_an_array_with_10_elements()
        {
            var array = new[] { 0, 30, 50, 70, 120, 190, 310, 500, 810, 1310 };
            it["should return 0 trying to find 0."] = () => karate.Chop(0, array).should_be(0);
            it["should return 1 trying to find 30."] = () => karate.Chop(30, array).should_be(1);
            it["should return 2 trying to find 50."] = () => karate.Chop(50, array).should_be(2);
            it["should return 3 trying to find 70."] = () => karate.Chop(70, array).should_be(3);
            it["should return 4 trying to find 120."] = () => karate.Chop(120, array).should_be(4);
            it["should return 5 trying to find 190."] = () => karate.Chop(190, array).should_be(5);
            it["should return 6 trying to find 310."] = () => karate.Chop(310, array).should_be(6);
            it["should return 7 trying to find 500."] = () => karate.Chop(500, array).should_be(7);
            it["should return 8 trying to find 810."] = () => karate.Chop(810, array).should_be(8);
            it["should return 9 trying to find 1310."] = () => karate.Chop(1310, array).should_be(9);
            it["should return -1 trying to find 20."] = () => karate.Chop(20, array).should_be(-1);
            it["should return -1 trying to find 40."] = () => karate.Chop(40, array).should_be(-1);
            it["should return -1 trying to find 60."] = () => karate.Chop(60, array).should_be(-1);
            it["should return -1 trying to find 200."] = () => karate.Chop(200, array).should_be(-1);
            it["should return -1 trying to find 800."] = () => karate.Chop(800, array).should_be(-1);
            it["should return -1 trying to find 1200."] = () => karate.Chop(1200, array).should_be(-1);
            it["should return -1 trying to find -100."] = () => karate.Chop(-100, array).should_be(-1);
        }
        public void when_trying_to_find_the_median()
        {
            it["2 and 4 should be 3"] = () => karate.GetMedian(2, 4).should_be(3);
            it["2 and 5 should be 3"] = () => karate.GetMedian(2, 5).should_be(3);
            it["2 and 6 should be 4"] = () => karate.GetMedian(2, 6).should_be(4);
            it["2 and 7 should be 4"] = () => karate.GetMedian(2, 7).should_be(4);
            it["1 and 10 should be 5"] = () => karate.GetMedian(1, 10).should_be(5);
            it["5 and 10 should be 7"] = () => karate.GetMedian(5, 10).should_be(7);
            it["10 and 40 should be 25"] = () => karate.GetMedian(10, 40).should_be(25);
            it["0 and 4 should be 2"] = () => karate.GetMedian(0, 4).should_be(2);
        }
    }
}
