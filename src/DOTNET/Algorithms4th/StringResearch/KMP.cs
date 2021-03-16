using System;

namespace StringResearch
{
    public class KMP
    {
        private string _pat;
        private int[,] _dfa;

        public KMP(string pat, int R)
        {
            _pat = pat;
            int M = pat.Length;
            _dfa = new int[R, M];
            _dfa[0, 0] = 1;
            for (int X = 0, j = 1; j < M; j++)
            {
                for (int c = 0; c < R; c++)
                {
                    _dfa[c, j] = _dfa[c, X];
                }
                int index = pat[j] - 'A';
                _dfa[index, j] = j + 1;
                X = _dfa[index, X];
            }
        }

        public int Search(string txt)
        {
            int i, j, N = txt.Length, M = _pat.Length;
            for (i = 0, j = 0; i < N && j < M; i++)
            {
                int index = txt[i] - 'A';
                j = _dfa[index, j];
            }
            if (j == M)
            {
                return i - M;
            }
            else
            {
                return N;
            }
        }

        public int BruteForceSearch(string pat, string txt)
        {
            int j, M = pat.Length;
            int i, N = txt.Length;
            for (i = 0, j = 0; i < N && j < M; i++)
            {
                if (txt[i] == pat[j])
                {
                    j++;
                }
                else
                {
                    i -= j;
                    j = 0;
                }
            }
            if (j == M)
            {
                return i - M;
            }
            else
            {
                return N;
            }
        }
    }
}