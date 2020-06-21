using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(string S)
    {
        return new Algorithm().Solution(S.ToCharArray());
    }

    public class Algorithm : MaxLengthOfSubArray<char, ulong>
    {
        protected override ulong GetStateDif(ulong prevState, char element)
        {
            ulong dif;
            if (element >= 'a' && element <= 'z')
            {
                dif = 1UL << (element - 'a');
            }
            else
            {
                dif = (1UL << 26) << (element - '0');
            }

            return prevState ^ dif;
        }
    }

    public abstract class MaxLengthOfSubArray<TElement, TState>
    {
        public int Solution(TElement[] array)
        {
            TState[] states = GetStates(array);
            IList<List<int>> groups = GetGroups(states);
            int result = groups.Max(GetGroupMaximum);
            return result;
        }

        private TState[] GetStates(TElement[] array)
        {
            var states = new TState[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                states[i + 1] = GetStateDif(states[i], array[i]);
            }
            return states;
        }

        protected abstract TState GetStateDif(TState prevState, TElement element);

        private IList<List<int>> GetGroups(TState[] states)
        {
            var positions = new Tuple<TState, int>[states.Length];
            for (int i = 0; i < states.Length; i++)
            {
                positions[i] = new Tuple<TState, int>(states[i], i);
            }

            return positions.GroupBy(x => x.Item1, x => x.Item2).Select(x => x.ToList()).ToList();
        }

        private int GetGroupMaximum(IList<int> positions)
        {
            if (positions.Count == 1)
            {
                return 0;
            }

            int min = positions.Min();
            int max = positions.Max();
            return max - min;
        }
    }
}
