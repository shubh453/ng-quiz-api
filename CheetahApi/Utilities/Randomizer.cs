namespace CheetahApi.Utilities
{
    public class Randomizer
    {
        public static T[] Randomize<T>(IEnumerable<T> arr, int length, int take)
        {
            var randomArray = arr.ToArray();
            for (var i = length - 1; i > 0; i--)
            {
                var j = Random.Shared.Next(0, i + 1);
                (randomArray[j], randomArray[i]) = (randomArray[i], randomArray[j]);
            }

            if (length <= take)
            {
                return randomArray;
            }

            var startIndex = Random.Shared.Next(0, length - take);
            return randomArray.Take(new Range(startIndex, startIndex + take)).ToArray();
        }
    }
}
