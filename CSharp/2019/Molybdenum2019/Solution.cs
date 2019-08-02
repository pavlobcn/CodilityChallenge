using System.Linq;

class Solution
{
    public int[] solution(int K, int M, int[] A)
    {
        var numberCandidates = A.GroupBy(x => x).Where(x => x.Count() >= A.Length / 4).SelectMany(x => new[] { x.Key, x.Key + 1 }).ToList();
        var result = numberCandidates.Where(x => CheckCandidate(x, K, A)).OrderBy(x => x).Distinct().ToArray();
        return result;
    }

    private bool CheckCandidate(int candidate, int k, int[] a)
    {
        int entryCount = 0;
        int[] array = a.ToArray();
        for (int i = 0; i < a.Length - k + 1; i++)
        {
            if (i == 0)
            {
                for (int j = 0; j < k; j++)
                {
                    array[j]++;
                }

                entryCount = array.Count(x => x == candidate);

                if (entryCount > a.Length / 2)
                {
                    return true;
                }

                continue;
            }

            if (array[i - 1] - 1 == candidate)
            {
                entryCount++;
            }
            else if (array[i - 1] == candidate)
            {
                entryCount--;
            }
            if (array[i + k - 1] + 1 == candidate)
            {
                entryCount++;
            }
            else if (array[i + k - 1] == candidate)
            {
                entryCount--;
            }

            array[i - 1]--;
            array[i + k - 1]++;

            if (entryCount > a.Length / 2)
            {
                return true;
            }
        }

        return false;
    }
}
