import math

'''
Să se determine distanța Euclideană între două locații identificate prin perechi de numere. 
De ex. distanța între (1,5) și (4,1) este 5.0
'''

def problema2(a, b):
    return math.sqrt((a[0] - b[0]) ** 2 + (a[1] - b[1]) ** 2)


def euclidean_distance(loc1, loc2):
    # Extrage coordonatele pentru fiecare locație
    x1, y1 = loc1
    x2, y2 = loc2

    # Calculează diferența pe fiecare axă
    diff_x = x2 - x1
    diff_y = y2 - y1

    # Calculează distanța Euclidiană
    distance = math.sqrt(diff_x ** 2 + diff_y ** 2)

    return distance

print("Problema 2 :\n",problema2((1, 5), (4, 1)), "\nChatGPT - ",euclidean_distance((1, 5), (4, 1)))

