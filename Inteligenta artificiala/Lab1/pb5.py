'''
Pentru un șir cu n elemente care conține valori din mulțimea {1, 2, ..., n - 1}
astfel încât o singură valoare se repetă de două ori, să se identifice acea valoare care se repetă.
De ex. în șirul [1,2,3,4,2] valoarea 2 apare de două ori.
'''

def problema5(arr):
    for i in arr:
        if arr.count(i) == 2:
            return i


def problema5_copilot(arr):
    return sum(arr) - sum(set(arr))

print("Problema 5 :\n",problema5([1,2,3,4,2]),"\nCoPilot - ",problema5_copilot([1,2,3,4,2]))