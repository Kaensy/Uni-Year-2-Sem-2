import random
from collections import deque, defaultdict
def markov_chain(text):
    words = text.split(' ')
    m_dict = {}
    for i in range(1, len(words)):
        if words[i-1] not in m_dict:
            m_dict[words[i-1]] = [words[i]]
        else:
            m_dict[words[i-1]].append(words[i])
    return m_dict


def markov_chain2(text, state_size):
    words = text.split(' ')
    m_dict = defaultdict(list)
    words = deque(words)
    for i in range(len(words) - state_size):
        history = tuple(list(words)[i:i+state_size])  # convert deque to list before slicing
        next_word = list(words)[i+state_size]  # convert deque to list before accessing element
        m_dict[history].append(next_word)
    return m_dict


def generate_sentence(chain, count=15):
    word1 = random.choice(list(chain.keys()))
    sentence = word1.capitalize()

    for i in range(count-1):
        word2 = random.choice(chain[word1])
        word1 = word2
        sentence += ' ' + word2

    sentence += '.'
    return sentence


def generate_sentence2(chain, state_size, count=15):
    history = random.choice(list(chain.keys()))
    sentence = ' '.join(history).capitalize()

    for i in range(count-state_size):
        next_word = random.choice(chain[history])
        history = tuple((list(history)[1:] + [next_word]))
        sentence += ' ' + next_word

    sentence += '.'
    return sentence


state_size = 10  # Change this to the number of states you want

with open('data/proverbe.txt', 'r', encoding='utf-8') as f:
    text = f.read().replace('\n', ' ')

chain = markov_chain(text)
sentence = generate_sentence(chain)
print(sentence)
chain2 = markov_chain2(text, state_size)
sentence2 = generate_sentence(chain,state_size)
print(sentence2)