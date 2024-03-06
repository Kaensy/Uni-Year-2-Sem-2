'''
Considerându-se o matrice cu n x m elemente întregi și o listă cu perechi formate din coordonatele a 2 căsuțe din matrice
((p,q) și (r,s)), să se calculeze suma elementelor din sub-matricile identificate de fieare pereche.

De ex, pt matricea
[[0, 2, 5, 4, 1],
[4, 8, 2, 3, 7],
[6, 3, 4, 6, 2],
[7, 3, 1, 8, 3],
[1, 5, 7, 9, 4]]
și lista de perechi ((1, 1) și (3, 3)), ((2, 2) și (4, 4)), suma elementelor din prima sub-matrice este 38, iar suma elementelor din a 2-a sub-matrice este 44.
'''

def problema9(matrix, pairs):
    sums = []
    for pair in pairs:
        p, q = pair[0]
        r, s = pair[1]
        sum=0
        for i in range(p, r+1):
            for j in range(q, s+1):
                sum+=matrix[i][j]
        sums.append(sum)
    return sums

def problema9_copilot(matrix, pairs):
    sums = []
    for pair in pairs:
        p, q = pair[0]
        r, s = pair[1]
        submatrix = [row[q:s+1] for row in matrix[p:r+1]]
        sums.append(sum([sum(row) for row in submatrix]))
    return sums

matrix = [
[0, 2, 5, 4, 1],
[4, 8, 2, 3, 7],
[6, 3, 4, 6, 2],
[7, 3, 1, 8, 3],
[1, 5, 7, 9, 4]]
print("Problema 9 :\n",problema9(matrix, [((1, 1), (3, 3)), ((2, 2), (4, 4))]),"\nCoPilot - ",problema9_copilot(matrix, [((1, 1), (3, 3)), ((2, 2), (4, 4))]))