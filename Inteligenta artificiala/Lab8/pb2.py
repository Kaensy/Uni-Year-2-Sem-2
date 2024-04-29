import markovify
import nltk
from nltk.sentiment import SentimentIntensityAnalyzer
from textblob import TextBlob
from nltk.corpus import sentiwordnet as swn
from nltk.corpus import wordnet
from nltk.tokenize import word_tokenize

from nltk.translate.bleu_score import sentence_bleu
from nltk.translate.bleu_score import SmoothingFunction

# Get raw text as string.
with open("data/english.txt") as f:
    text = f.read()

# Build the model.
text_model = markovify.NewlineText(text)



sentence = ""
new_sentences = ""
for i in range(5):
    generated_sentence = text_model.make_sentence()
    if generated_sentence is not None:
        sentence += generated_sentence + "\n"

        words = word_tokenize(generated_sentence)
        new_words = []
        for word in words:
            # Get synonyms for the word
            synonyms = wordnet.synsets(word)
            synonyms = [lem.name() for syn in synonyms for lem in syn.lemmas() if
                        lem.name() != word]  # Exclude the original word

            # If synonyms were found, replace the word with the closest synonym
            if synonyms:
                new_word = synonyms[0]  # Select the first synonym
                new_words.append(new_word)
            else:
                new_words.append(word)

        # Create the new sentence
        new_sentence = ' '.join(new_words)
        new_sentences += new_sentence + "\n"

print(sentence)

sia = SentimentIntensityAnalyzer()
sentiment_scores = sia.polarity_scores(sentence)
print("nltk ", sentiment_scores)

blob = TextBlob(sentence)
print("TextBlob", blob.sentiment, "\n")

print(new_sentences)



