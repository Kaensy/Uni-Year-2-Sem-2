"""
Să se determine cuvintele unui text care apar exact o singură dată în acel text.
De ex. cuvintele care apar o singură dată în ”ana are ana are mere rosii ana" sunt: 'mere' și 'rosii'.
"""

def pb4(text):
    words = text.split()
    frequency = {}
    for word in words:
        frequency[word] = frequency.get(word,0) + 1

    return [word for word in frequency if frequency[word] == 1]

def pb4_copilot(text):
    words = text.split()
    unique_words = []
    for word in words:
        if words.count(word) == 1:
            unique_words.append(word)
    return unique_words


print("Problema 2 :\n",pb4("ana are ana are mere rosii ana"),"\nCoPilot - ",pb4_copilot("ana are ana are mere rosii ana"))
