import re

import nltk
import spacy as spacy
from nltk.corpus import wordnet
from nltk.tokenize import sent_tokenize
from nltk.tokenize import word_tokenize
from rowordnet import RoWordNet
from unidecode import unidecode



def visualize_sentence_count(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()
        sentences = sent_tokenize(text)
        print("Number of sentences: ", len(sentences))


def visualize_word_count(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()

    words = word_tokenize(text)
    print("Number of words: ", len(words))


def visualize_unique_word_count(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()

    words = word_tokenize(text)
    unique_words = set(words)
    print("Number of unique words: ", len(unique_words))


def visualize_word_lengths(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()

    cleaned_text = re.sub(r'[.,:;!?]', '', text)
    words = word_tokenize(cleaned_text)
    shortest_word = min(words, key=len)
    longest_word = max(words, key=len)
    print("Shortest word: ", shortest_word)
    print("Longest word: ", longest_word)


def remove_diacritics(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()

    text_without_diacritics = unidecode(text)
    print("Text without diacritics: ", text_without_diacritics)


def synonyms_longest_word(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        text = file.read()

    cleaned_text = re.sub(r'[.,:;!?]', '', text)
    words = word_tokenize(cleaned_text)
    longest_word = str(max(words, key=len))
    #"ro_core_news_sm"
    nlp = spacy.load("ro_core_news_sm")
    doc = nlp(longest_word)
    singular_form = ""
    for token in doc:
        singular_form = token.lemma_ + " "
    word = singular_form.strip()
    rown = RoWordNet()
    synset_ids = rown.synsets(literal=word)
    for synset_id in synset_ids:
        synset = rown.synset(synset_id)
        synonyms = [literal for literal in synset.literals]
        print(synonyms)

visualize_sentence_count('texts.txt')
visualize_word_count('texts.txt')
visualize_unique_word_count('texts.txt')
remove_diacritics('texts.txt')
synonyms_longest_word('texts.txt')






