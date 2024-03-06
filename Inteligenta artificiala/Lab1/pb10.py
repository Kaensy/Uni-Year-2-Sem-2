'''
Considerându-se o matrice cu n x m elemente binare (0 sau 1) sortate crescător pe linii, să se identifice indexul liniei care conține cele mai multe elemente de 1.

De ex. în matricea
[[0,0,0,1,1],
[0,1,1,1,1],
[0,0,1,1,1]]
a doua linie conține cele mai multe elemente 1.
'''

def problema10(matrix):
    minim = float('inf')
    rez = 0
    for linie,row in enumerate(matrix):
        i=0
        for nr_0, elem in enumerate(row):
            if elem == 1:
                break;
        if nr_0 < minim:
            minim = nr_0
            rez = linie
    return rez+1

def problema10_copilot(matrix):
    return max(range(len(matrix)), key=lambda i: matrix[i].count(1))

matrix = [
[0,0,0,1,1],
[0,1,1,1,1],
[0,0,1,1,1]]
print("Problema 10 :\nLinia",problema10(matrix),"contine cei mai multi de 1\nCoPilot - ",problema10_copilot(matrix))