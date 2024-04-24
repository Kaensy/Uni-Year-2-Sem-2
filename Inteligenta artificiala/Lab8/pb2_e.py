import sacrebleu
import nltk
import markovify

# Load the text data
with open("data/english.txt", "r") as f:
    text = f.read()

# Build the Markov chain model
text_model = markovify.Text(text, state_size=2)

# Generate a 4-line poem
poem = []
for _ in range(4):
    line = text_model.make_short_sentence(300, tries=100)   # Adjust the length as needed
    poem.append(line + '\n')

# Join the lines into a single string
generated_poem = ' '.join(poem)

# Keep the generated poem as a single string
candidate = [generated_poem.lower()]

# Your 4-line reference poem
reference_poem = """
For having traffic with thy hours my love's fair brow,
For having traffic with thy self were happier than thou art,
Then being asked, where all thy beauty do I question make,
Or ten times refigured thee:
"""

# Use your reference poem for the BLEU score calculation
references = [[reference_poem]]

# Calculate the BLEU score
bleu = sacrebleu.raw_corpus_bleu(candidate, references, .01).score
print(f"Generated Poem: \n{generated_poem}")
print(f"BLEU Score: {bleu}")
