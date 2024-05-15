import csv
import os
import numpy as np
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfVectorizer
import gensim
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.cluster import KMeans
from sklearn.metrics import accuracy_score
from kMeans import KMeansz

def featureComputation(model, data):
    features = []
    phrases = [ phrase.split() for phrase in data]
    for phrase in phrases:
        # compute the embeddings of all the words from a phrase (words of more than 2 characters) known by the model
        # vectors = [model[word] for word in phrase if (len(word) > 2) and (word in model.vocab.keys())]
        vectors = [model[word] for word in phrase if (len(word) > 2) and (word in model.index_to_key)]
        if len(vectors) == 0:
            result = [0.0] * model.vector_size
        else:
            result = np.sum(vectors, axis=0) / len(vectors)
        features.append(result)
    return features


# load some data
df = pd.read_csv('data/text.csv')

df = df.sample(n=10000, random_state=1)

# Split the dataset into training and testing sets
trainInputs, testInputs, trainOutputs, testOutputs = train_test_split(df['text'], df['label'], test_size=0.2, random_state=42)

# Load Google's pre-trained Word2Vec
crtDir = os.getcwd()
modelPath = os.path.join(crtDir, 'models', 'GoogleNews-vectors-negative300.bin')
word2vecModel300 = gensim.models.KeyedVectors.load_word2vec_format(modelPath, binary=True)


trainFeatures = featureComputation(word2vecModel300, trainInputs)
testFeatures = featureComputation(word2vecModel300, testInputs)


# unsupervised classification ( = clustering) of data
# Six categories: sadness (0), joy (1), love (2), anger (3), fear (4), and surprise (5).
# unsupervisedClassifier = KMeans(n_clusters=6, random_state=0)
unsupervisedClassifier = KMeansz(n_clusters=6, random_state=0)

unsupervisedClassifier.fit(trainFeatures)

computedTestIndexes = unsupervisedClassifier.predict(testFeatures)
labelNames = ['sadness', 'joy', 'love', 'anger', 'fear', 'surprise']
computedTestOutputs = [labelNames[value] for value in computedTestIndexes]

# Create a dictionary that maps the numerical labels to the corresponding emotion names
label_dict = {0: 'sadness', 1: 'joy', 2: 'love', 3: 'anger', 4: 'fear', 5: 'surprise'}

# Map the numerical labels in y_test to the corresponding emotion names
y_test_mapped = testOutputs.map(label_dict)


# just supposing that we have the true labels
print("acc: ", accuracy_score(y_test_mapped, computedTestOutputs))

message = "By choosing a bike over a car, I’m reducing my environmental footprint. Cycling promotes eco-friendly transportation, and I’m proud to be part of that movement."

# Preprocess the message and convert it into a feature vector
messageFeatures = featureComputation(word2vecModel300, [message])

# Use the KMeans model to predict the cluster of the feature vector
predictedIndex = unsupervisedClassifier.predict(messageFeatures)[0]
predictedSentiment = labelNames[predictedIndex]

print("The sentiment of the message is: ", predictedSentiment)