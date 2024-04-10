import csv
import os
import matplotlib.pyplot as plt
import numpy as np
import warnings

from sklearn.linear_model import LogisticRegression

import BatchGradientDescent as bgd

warnings.simplefilter('ignore')

from sklearn.preprocessing import StandardScaler, LabelEncoder
from sklearn import linear_model
from math import sqrt
from mpl_toolkits import mplot3d


def plotDataHistogram(x, variableName):
    n, bins, patches = plt.hist(x, 10)
    plt.title('Histogram of ' + variableName)
    plt.show()


def plot3Ddata(x1Train, x2Train, yTrain, x1Model = None, x2Model = None, yModel = None, x1Test = None, x2Test = None, yTest = None, title = None):
    ax = plt.axes(projection = '3d')
    if x1Train:
        ax.scatter(x1Train, x2Train, yTrain, c = 'r', marker = 'o', label = 'train data')
    if x1Model:
        ax.scatter(x1Model, x2Model, yModel, c = 'b', marker = '_', label = 'learnt model')
    if x1Test:
        ax.scatter(x1Test, x2Test, yTest, c = 'g', marker = '^', label = 'test data')
    ax.set_xlabel("capita")
    ax.set_ylabel("freedom")
    ax.set_zlabel("happiness")
    plt.legend()
    plt.show()


def loadDataMoreInputs(fileName, inputVariabNames, outputVariabName):
    data = []
    dataNames = []
    with open(fileName) as csv_file:
        csv_reader = csv.reader(csv_file, delimiter=',')
        line_count = 0
        for row in csv_reader:
            if line_count == 0:
                dataNames = row
            else:
                data.append(row)
            line_count += 1
    selectedVariable1 = dataNames.index(inputVariabNames[0])
    selectedVariable2 = dataNames.index(inputVariabNames[1])
    inputs = [[float(data[i][selectedVariable1]), float(data[i][selectedVariable2])] for i in range(len(data))]
    selectedOutput = dataNames.index(outputVariabName)
    outputs = [float(data[i][selectedOutput]) for i in range(len(data))]

    return inputs, outputs


def normalisation(trainData, testData):
    scaler = StandardScaler()
    if not isinstance(trainData[0], list):
        # encode each sample into a list
        trainData = [[d] for d in trainData]
        testData = [[d] for d in testData]

        scaler.fit(trainData)  # fit only on training data
        normalisedTrainData = scaler.transform(trainData)  # apply same transformation to train data
        normalisedTestData = scaler.transform(testData)  # apply same transformation to test data

        # decode from list to raw values
        normalisedTrainData = [el[0] for el in normalisedTrainData]
        normalisedTestData = [el[0] for el in normalisedTestData]
    else:
        scaler.fit(trainData)  # fit only on training data
        normalisedTrainData = scaler.transform(trainData)  # apply same transformation to train data
        normalisedTestData = scaler.transform(testData)  # apply same transformation to test data
    return normalisedTrainData, normalisedTestData


def normalize_data(train_data, test_data):
    mean = np.mean(train_data, axis=0)
    std_dev = np.std(train_data, axis=0)

    train_data = (train_data - mean) / std_dev
    test_data = (test_data - mean) / std_dev

    return train_data, test_data


# load data
crtDir = os.getcwd()
filePath = os.path.join(crtDir, 'data', 'wdbc.csv')

inputs, outputs = loadDataMoreInputs(filePath, ['radius1', 'texture1'], 'Diagnosis')

feature1 = [ex[0] for ex in inputs]
feature2 = [ex[1] for ex in inputs]

# convert outputs to numerical values
le = LabelEncoder()
outputs = le.fit_transform(outputs)


# plot the data histograms
plotDataHistogram(feature1, 'radius')
plotDataHistogram(feature2, 'texture')
plotDataHistogram(outputs, 'diagnosis')

# check the liniarity (to check that a linear relationship exists between the dependent variable (y = happiness) and the independent variables (x1 = capita, x2 = freedom).)
plot3Ddata(feature1, feature2, outputs, [], [], [], [], [], [], 'capita vs freedom vs happiness')

np.random.seed(5)
indexes = [i for i in range(len(inputs))]
trainSample = np.random.choice(indexes, int(0.8 * len(inputs)), replace=False)
testSample = [i for i in indexes if not i in trainSample]

trainInputs = [inputs[i] for i in trainSample]
trainOutputs = [outputs[i] for i in trainSample]
testInputs = [inputs[i] for i in testSample]
testOutputs = [outputs[i] for i in testSample]

# trainInputs, testInputs = normalisation(trainInputs, testInputs)
# trainOutputs, testOutputs = normalisation(trainOutputs, testOutputs)
trainInputs, testInputs = normalize_data(np.array(trainInputs), np.array(testInputs))
trainOutputs, testOutputs = normalize_data(np.array(trainOutputs), np.array(testOutputs))

# create a logistic regression model
classifier = LogisticRegression(multi_class='ovr', solver='liblinear')
# train the model
classifier.fit(trainInputs, trainOutputs)
# predict the class of a new sample
new_sample = [[18, 10]]  # new sample with radius=18 and texture=10
prediction = classifier.predict(new_sample)

# convert numerical prediction back to original class
prediction = le.inverse_transform(prediction)

print("The predicted class of the new sample is:", prediction)

feature1train = [ex[0] for ex in trainInputs]
feature2train = [ex[1] for ex in trainInputs]

feature1test = [ex[0] for ex in testInputs]
feature2test = [ex[1] for ex in testInputs]

plot3Ddata(feature1train, feature2train, trainOutputs, [], [], [], feature1test, feature2test, testOutputs,"train and test data (after normalisation)")

# identify (by training) the regressor

# # use sklearn regressor


# regressor = linear_model.SGDRegressor(learning_rate='constant', eta0=0.01, max_iter=1)
# # Batch GD
# no_epochs = 1000
# for _ in range(no_epochs):
#     regressor.partial_fit(trainInputs, trainOutputs)

# using developed code
# from SGD import MySGDRegression

# model initialisation
# regressor = MySGDRegression()

regressor = bgd.BatchGradientDescent(learning_rate=0.01, epochs=1000)
regressor.fit(np.array(trainInputs), np.array(trainOutputs))


print(regressor.coef_)
print(regressor.intercept_)

# parameters of the liniar regressor
w0, w1, w2 = regressor.intercept_, regressor.coef_[0], regressor.coef_[1]
print('the learnt model: f(x) = ', w0, ' + ', w1, ' * x1 + ', w2, ' * x2')

# numerical representation of the regressor model
noOfPoints = 50
xref1 = []
val = min(feature1)
step1 = (max(feature1) - min(feature1)) / noOfPoints
for _ in range(1, noOfPoints):
    for _ in range(1, noOfPoints):
        xref1.append(val)
    val += step1

xref2 = []
val = min(feature2)
step2 = (max(feature2) - min(feature2)) / noOfPoints
for _ in range(1, noOfPoints):
    aux = val
    for _ in range(1, noOfPoints):
        xref2.append(aux)
        aux += step2
yref = [w0 + w1 * el1 + w2 * el2 for el1, el2 in zip(xref1, xref2)]
plot3Ddata(feature1train, feature2train, trainOutputs, xref1, xref2, yref, [], [], [],
           'train data and the learnt model')

# use the trained model to predict new inputs

# makes predictions for test data
# computedTestOutputs = [w0 + w1 * el[0] + w2 * el[1] for el in testInputs]
# makes predictions for test data (by tool)
computedTestOutputs = regressor.predict(testInputs)

plot3Ddata([], [], [], feature1test, feature2test, computedTestOutputs, feature1test, feature2test, testOutputs,
           'predictions vs real test data')

# compute the differences between the predictions and real outputs
error = 0.0
for t1, t2 in zip(computedTestOutputs, testOutputs):
    error += (t1 - t2) ** 2
error = error / len(testOutputs)
print('prediction error (manual): ', error)

from sklearn.metrics import mean_squared_error

error = mean_squared_error(testOutputs, computedTestOutputs)
print('prediction error (tool):   ', error)
