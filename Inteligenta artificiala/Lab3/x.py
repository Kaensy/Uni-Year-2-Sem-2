from azure.cognitiveservices.vision.computervision import ComputerVisionClient
from azure.cognitiveservices.vision.computervision.models import OperationStatusCodes
from azure.cognitiveservices.vision.computervision.models import VisualFeatureTypes
from msrest.authentication import CognitiveServicesCredentials
from scipy.spatial import distance
from sklearn.feature_extraction.text import CountVectorizer
from scipy.spatial.distance import cityblock
from scipy.spatial.distance import chebyshev



from array import array
import os
from PIL import Image
import sys
import time

'''
Authenticate
Authenticates your credentials and creates a client.
'''
subscription_key = os.environ["VISION_KEY"]
endpoint = os.environ["VISION_ENDPOINT"]

computervision_client = ComputerVisionClient(endpoint, CognitiveServicesCredentials(subscription_key))
'''
END - Authenticate
'''


def euclidean_distance(s1, s2):
    vectorizer = CountVectorizer().fit_transform([s1, s2])
    vectors = vectorizer.toarray()
    return distance.euclidean(vectors[0], vectors[1])


def levenshtein_distance(s1, s2):
    if len(s1) < len(s2):
        return levenshtein_distance(s2, s1)

    if len(s2) == 0:
        return len(s1)

    previous_row = range(len(s2) + 1)
    for i, c1 in enumerate(s1):
        current_row = [i + 1]
        for j, c2 in enumerate(s2):
            insertions = previous_row[j + 1] + 1
            deletions = current_row[j] + 1
            substitutions = previous_row[j] + (c1 != c2)
            current_row.append(min(insertions, deletions, substitutions))
        previous_row = current_row

    return previous_row[-1]


def hamming_distance(s1, s2):
    if len(s1) != len(s2):
        return "Strings must be of the same length"
    return sum(c1 != c2 for c1, c2 in zip(s1, s2))


def jaro_winkler_distance(s1, s2):
    if s1 == s2:
        return 1.0

    len_s1 = len(s1)
    len_s2 = len(s2)

    max_dist = max(len_s1, len_s2) // 2 - 1

    match = 0  # number of matches
    trans = 0  # number of transpositions

    flagged_s1 = []  # characters in s1 that are already matched with s2
    flagged_s2 = []  # characters in s2 that are already matched with s1

    for i in range(len_s1):  # count number of matches and transpositions
        for j in range(max(0, i - max_dist), min(len_s2, i + max_dist + 1)):
            if s1[i] == s2[j] and j not in flagged_s2:
                match += 1
                flagged_s1.append(i)
                flagged_s2.append(j)
                break

    flagged_s2.sort()

    for i, j in zip(flagged_s1, flagged_s2):  # count transpositions
        if s1[i] != s2[j]:
            trans += 1

    if match == 0:
        return 0.0

    trans /= 2.0
    return (match / len_s1 + match / len_s2 + (match - trans) / match) / 3.0


# img = open("test1.png", "rb")
img = open("test2.jpeg", "rb")
# groundTruth = ["Google Cloud", "Platform"]
groundTruth = ["Succes in rezolvarea", "tEMELOR la", "LABORA toarele de", "Inteligenta Artificiala!"]
read_response = computervision_client.read_in_stream(
    image=img,
    mode="Handwritten",
    raw=True
)


operation_id = read_response.headers['Operation-Location'].split('/')[-1]
while True:
    read_result = computervision_client.get_read_result(operation_id)
    if read_result.status not in ['notStarted', 'running']:
        break
    time.sleep(1)

result = []
if read_result.status == OperationStatusCodes.succeeded:
    for text_result in read_result.analyze_result.read_results:
        for line in text_result.lines:
            print(line.text)
            result.append(line.text)


def compute_hamming_distances(result, groundTruth):
    result_words = ' '.join(result).split()
    groundTruth_words = ' '.join(groundTruth).split()
    ham_distances = [hamming_distance(r, g) for r, g in zip(result_words, groundTruth_words)]
    return ham_distances


def compute_levenshtein_distances(result, groundTruth):
    result_words = ' '.join(result).split()
    groundTruth_words = ' '.join(groundTruth).split()
    lev_distances = [levenshtein_distance(r, g) for r, g in zip(result_words, groundTruth_words)]
    return lev_distances


def compute_euclidean_distances(result, groundTruth):
    result_words = ' '.join(result).split()
    groundTruth_words = ' '.join(groundTruth).split()
    euc_distances = [euclidean_distance(r, g) for r, g in zip(result_words, groundTruth_words)]
    return euc_distances


def compute_manhattan_distances(result, groundTruth):
    result_words = ' '.join(result).split()
    groundTruth_words = ' '.join(groundTruth).split()
    vectorizer = CountVectorizer().fit(result_words + groundTruth_words)
    manhattan_distances = [cityblock(vectorizer.transform([r]).toarray()[0], vectorizer.transform([g]).toarray()[0]) for r, g in zip(result_words, groundTruth_words)]
    return manhattan_distances


def compute_chebyshev_distances(result, groundTruth):
    result_words = ' '.join(result).split()
    groundTruth_words = ' '.join(groundTruth).split()
    vectorizer = CountVectorizer().fit(result_words + groundTruth_words)
    chebyshev_distances = [chebyshev(vectorizer.transform([r]).toarray()[0], vectorizer.transform([g]).toarray()[0]) for r, g in zip(result_words, groundTruth_words)]
    return chebyshev_distances


def cer_leven(groundTruth, result):
    return levenshtein_distance(groundTruth, result) / len(groundTruth)


def wer_leven(groundTruth, result):
    return levenshtein_distance(groundTruth.split(), result.split()) / len(groundTruth.split())


def cer_ham(groundTruth, result):
    return hamming_distance(groundTruth, result) / len(groundTruth)


def wer_ham(groundTruth, result):
    return hamming_distance(groundTruth.split(), result.split()) / len(groundTruth.split())


def cer_man(groundTruth, result):
    vectorizer = CountVectorizer().fit([groundTruth, result])
    groundTruth_vector = vectorizer.transform([groundTruth]).toarray()[0]
    result_vector = vectorizer.transform([result]).toarray()[0]
    return cityblock(groundTruth_vector, result_vector) / len(groundTruth)


def wer_man(groundTruth, result):
    vectorizer = CountVectorizer().fit([groundTruth, result])
    groundTruth_vector = vectorizer.transform([groundTruth]).toarray()[0]
    result_vector = vectorizer.transform([result]).toarray()[0]
    return cityblock(groundTruth_vector, result_vector) / len(groundTruth.split())


def cer_cheb(groundTruth, result):
    vectorizer = CountVectorizer().fit([groundTruth, result])
    groundTruth_vector = vectorizer.transform([groundTruth]).toarray()[0]
    result_vector = vectorizer.transform([result]).toarray()[0]
    return chebyshev(groundTruth_vector, result_vector) / len(groundTruth)


def wer_cheb(groundTruth, result):
    vectorizer = CountVectorizer().fit([groundTruth, result])
    groundTruth_vector = vectorizer.transform([groundTruth]).toarray()[0]
    result_vector = vectorizer.transform([result]).toarray()[0]
    return chebyshev(groundTruth_vector, result_vector) / len(groundTruth.split())


def cer_jaro_wrinkler(groundTruth, result):
    return 1-jaro_winkler_distance(groundTruth, result)


def wer_jaro_wrinkler(groundTruth, result):
    return 1-jaro_winkler_distance(groundTruth.split(), result.split())


lev_distances = compute_levenshtein_distances(result, groundTruth)
euc_distances = compute_euclidean_distances(result, groundTruth)
ham_distances = compute_hamming_distances(result, groundTruth)
manhattan_distances = compute_manhattan_distances(result, groundTruth)
chebyshev_distances = compute_chebyshev_distances(result, groundTruth)


leven_cer = cer_leven(' '.join(groundTruth), ' '.join(result))
leven_wer = wer_leven(' '.join(groundTruth), ' '.join(result))

ham_cer = cer_ham(' '.join(groundTruth), ' '.join(result))
ham_wer = wer_ham(' '.join(groundTruth), ' '.join(result))

man_cer = cer_man(' '.join(groundTruth), ' '.join(result))
man_wer = wer_man(' '.join(groundTruth), ' '.join(result))

cheb_cer = cer_cheb(' '.join(groundTruth), ' '.join(result))
cheb_wer = wer_cheb(' '.join(groundTruth), ' '.join(result))

jaro_cer = cer_jaro_wrinkler(' '.join(groundTruth), ' '.join(result))
jaro_wer = wer_jaro_wrinkler(' '.join(groundTruth), ' '.join(result))


print('Levenshtein distances:', lev_distances, '\n','Character Error Rate:', leven_cer, '\n','Word Error Rate:', leven_wer)
print('Euclidean distances:', euc_distances)
print('Hamming distances:', ham_distances, '\n','Character Error Rate:', ham_cer, '\n','Word Error Rate:', ham_wer)
print('Manhattan distances:', manhattan_distances, '\n', 'Character Error Rate:', man_cer, '\n','Word Error Rate:', man_wer)
print('Chebyshev distances:', chebyshev_distances, '\n', 'Character Error Rate:', cheb_cer, '\n','Word Error Rate:', cheb_wer)
print('Jaro-Winkler :', '\n', 'Character Error Rate:', jaro_cer, '\n','Word Error Rate:', jaro_wer)

