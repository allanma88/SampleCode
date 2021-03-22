#include <string.h>
#include <stdio.h>
#include <math.h>
#include <stdlib.h>

unsigned int ctoi(char c)
{
    return (unsigned int)(c - 'a');
}

unsigned int max_prime(unsigned int max)
{
    int *isnotprimes = (int *)calloc(max, sizeof(int));
    isnotprimes[2] = 0;
    isnotprimes[3] = 0;

    for (unsigned int i = 2; i < (unsigned int)sqrt(max); i++)
    {
        for (int j = i; j <= max / i; j++)
        {
            isnotprimes[i * j] = 1;
        }
    }

    for (unsigned int i = max - 1; i > 4; i--)
    {
        if (!isnotprimes[i])
        {
            return i;
        }
    }
    free(isnotprimes);
    return 3;
}

void rabin_karp_match(char *t, char *p, unsigned int d, unsigned int q)
{
    size_t n = strlen(t);
    size_t m = strlen(p);
    unsigned int ps = 0;
    unsigned int ts = 0;
    unsigned int factor = 1;

    for (int i = 0; i < m; i++)
    {
        factor = (factor * d) % q;
    }

    for (size_t s = 0; s < m; s++)
    {
        unsigned int pi = ctoi(p[s]);
        unsigned int ti = ctoi(t[s]);
        ps = (ps * d + pi) % q;
        ts = (ts * d + ti) % q;
    }

    for (size_t s = 0; s <= n - m; s++)
    {
        if (ps == ts)
        {
            if (!strncmp(t + s, p, m))
            {
                printf("match at %ld\n", s);
            }
        }
        if (s < n - m)
        {
            ts = (d * ts - ctoi(t[s]) * factor + ctoi(t[s + m])) % q;
        }
    }
}

void test_rabin_karp_match()
{
    unsigned int d = 26;
    unsigned int max = 10000;
    unsigned int prime = max_prime(max);
    printf("max prime of %u is %u\n", max, prime);
    rabin_karp_match("bacbababacabcbab", "ababaca", d, prime);
    rabin_karp_match("bdababaca", "ababaca", d, prime);
}

void finite_automation_match(char *t, char *p)
{
    size_t n = strlen(t);
    size_t m = strlen(p);

    int transition[m + 1][3];

    char *x = (char *)calloc(m + 2, sizeof(char));
    for (size_t q = 0; q <= m; q++)
    {
        strncpy(x, p, q);
        for (char c = 'a'; c <= 'c'; c++)
        {
            x[q] = c;
            int k = q + 1 < m ? q + 1 : m;
            while (strncmp(x + q + 1 - k, p, k))
            {
                k--;
            }
            transition[q][c - 'a'] = k;
        }
    }
    free(x);

    int q = 0;
    for (size_t i = 0; i < n; i++)
    {
        q = transition[q][t[i] - 'a'];
        if (q == m)
        {
            printf("match at %ld\n", i - m + 1);
        }
    }
}

void test_finite_automation_match()
{
    finite_automation_match("bababacab", "aba");
    finite_automation_match("aaaaa", "aaa");
}

void kmp_match(char *t, char *p)
{
    int n = strlen(t);
    int m = strlen(p);
    int pi[m];
    pi[0] = -1;
    int k = -1;
    for (int q = 1; q < m; q++)
    {
        while (k >= 0 && p[k + 1] != p[q])
        {
            k = pi[k];
        }
        if (p[k + 1] == p[q])
        {
            k++;
        }
        pi[q] = k;
    }

    int q = -1;
    for (int i = 0; i < n; i++)
    {
        while (q >= 0 && t[i] != p[q + 1])
        {
            q = pi[q];
        }

        if (t[i] == p[q + 1])
        {
            q++;
        }
        if (q == m - 1)
        {
            printf("match at %d\n", i - m + 1);
            q = pi[q];
        }
    }
}

void test_kmp_match()
{
    kmp_match("bababaabcb", "ababaca");
    kmp_match("bababacabc", "ababaca");
    kmp_match("aaaaaa", "aaa");
}

int main()
{
    test_rabin_karp_match();
    test_finite_automation_match();
    test_kmp_match();
    return 0;
}