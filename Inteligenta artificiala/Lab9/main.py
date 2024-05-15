import csv
import os
import numpy as np
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfVectorizer
import gensim

# load some data
crtDir = os.getcwd()
fileName = os.path.join(crtDir, 'data', 'spam.csv')

data = []
with open(fileName) as csv_file:
    csv_reader = csv.reader(csv_file, delimiter=',')
    line_count = 0
    for row in csv_reader:
        if line_count == 0:
            dataNames = row
        else:
            data.append(row)
        line_count += 1

inputs = [data[i][0] for i in range(len(data))][:1000]
outputs = [data[i][1] for i in range(len(data))][:1000]
labelNames = list(set(outputs))

print(inputs[:2])
print(labelNames[:2])

# prepare data for training and testing

np.random.seed(5)
# noSamples = inputs.shape[0]
noSamples = len(inputs)
indexes = [i for i in range(noSamples)]
trainSample = np.random.choice(indexes, int(0.8 * noSamples), replace=False)
testSample = [i for i in indexes if i not in trainSample]

trainInputs = [inputs[i] for i in trainSample]
trainOutputs = [outputs[i] for i in trainSample]
testInputs = [inputs[i] for i in testSample]
testOutputs = [outputs[i] for i in testSample]

print(trainInputs[:3])

# extract some features from the raw text

# # # representation 1: Bag of Words
# vectorizer = CountVectorizer()
#
# trainFeatures = vectorizer.fit_transform(trainInputs)
# testFeatures = vectorizer.transform(testInputs)

# # vocabulary size
# print("vocab size: ", len(vectorizer.vocabulary_),  " words")
# # no of emails (Samples)
# print("traindata size: ", len(trainInputs), " emails")
# # shape of feature matrix
# print("trainFeatures shape: ", trainFeatures.shape)
#
# # vocabbulary from the train data
# print('some words of the vocab: ', vectorizer.get_feature_names_out()[-20:])
# # extracted features
# print('some features: ', trainFeatures.toarray()[:3])

# # representation 2: tf-idf features - word granularity
# vectorizer2 = TfidfVectorizer(max_features=50)
#
# trainFeatures2 = vectorizer2.fit_transform(trainInputs)
# testFeatures2 = vectorizer2.transform(testInputs)
#
# # vocabbulary from the train data
# print('vocab: ', vectorizer2.get_feature_names_out()[:10])
# # extracted features
# print('features: ', trainFeatures2.toarray()[:3])

# representation 3: embedded features extracted by a pre-train model (in fact, word2vec pretrained model)
# Load Google's pre-trained Word2Vec
crtDir = os.getcwd()
modelPath = os.path.join(crtDir, 'models', 'GoogleNews-vectors-negative300.bin')

word2vecModel300 = gensim.models.KeyedVectors.load_word2vec_format(modelPath, binary=True)
print(word2vecModel300.most_similar('support'))
print("vec for house: ", word2vecModel300["house"])

word = "casuta"
if (word in word2vecModel300.index_to_key):
    print("vec for house: ", word2vecModel300[word])
else:
    print("word was not found!")


def featureComputation(model, data):
    features = []
    phrases = [phrase.split() for phrase in data]
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


trainFeatures = featureComputation(word2vecModel300, trainInputs)
testFeatures = featureComputation(word2vecModel300, testInputs)

# unsupervised classification ( = clustering) of data

from sklearn.cluster import KMeans
from kMeans import KMeansz

# unsupervisedClassifier = KMeans(n_clusters=2, random_state=0)
unsupervisedClassifier = KMeansz(n_clusters=2, random_state=0)
unsupervisedClassifier.fit(trainFeatures)

computedTestIndexes = unsupervisedClassifier.predict(testFeatures)
computedTestOutputs = [labelNames[value] for value in computedTestIndexes]
for i in range(0, len(testInputs)):
    print(testInputs[i], " -> ", computedTestOutputs[i])

from sklearn.metrics import accuracy_score

# just supposing that we have the true labels
print("acc: ", accuracy_score(testOutputs, computedTestOutputs))
