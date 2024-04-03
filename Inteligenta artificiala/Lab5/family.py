import csv
import os
import matplotlib.pyplot as plt
import numpy as np
from MyRegression import MyLinearUnivariateRegression, My3DRegression
from sklearn import linear_model
from sklearn.metrics import mean_squared_error



def load_data(fileName, inputVariabName, outputVariabName):
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
    selectedVariable = dataNames.index(inputVariabName)
    inputs_local = [float(data[i][selectedVariable]) for i in range(len(data))]
    selectedOutput = dataNames.index(outputVariabName)
    outputs_local = [float(data[i][selectedOutput]) for i in range(len(data))]

    return inputs_local, outputs_local


def plot_data_histogram(x, variable_name):
    n, bins, patches = plt.hist(x, 10)
    plt.title('Histogram of ' + variable_name)
    plt.show()


def plot_data(x1, y1, x2=None, y2=None, x3=None, y3=None, title=None):
    plt.plot(x1, y1, 'ro', label='train data')
    if x2:
        plt.plot(x2, y2, 'b-', label='learnt model')
    if x3:
        plt.plot(x3, y3, 'g^', label='test data')
    plt.title(title)
    plt.legend()
    plt.show()


crtDir = os.getcwd()
filePath = os.path.join(crtDir, 'data', 'v3_world-happiness-report-2017.csv')

inputs, outputs = load_data(filePath, 'Family', 'Happiness.Score')
print('in:  ', inputs[:5])
print('out: ', outputs[:5])

plot_data_histogram(inputs, 'Family')
plot_data_histogram(outputs, 'Happiness score')

inputs, outputs = load_data(filePath, 'Family', 'Happiness.Score')
plot_data(inputs, outputs, [], [], [], [], 'family vs. happiness')

np.random.seed(5)
indexes = [i for i in range(len(inputs))]
trainSample = np.random.choice(indexes, int(0.8 * len(inputs)), replace=False)
validationSample = [i for i in indexes if not i in trainSample]
trainInputs = [inputs[i] for i in trainSample]
trainOutputs = [outputs[i] for i in trainSample]
validationInputs = [inputs[i] for i in validationSample]
validationOutputs = [outputs[i] for i in validationSample]

plot_data(trainInputs, trainOutputs, [], [], validationInputs, validationOutputs, "train and test data")

# training step
# sklearn function
# xx = [[el] for el in trainInputs]
# regressor = linear_model.LinearRegression()
# regressor.fit(xx, trainOutputs)
# regressor = linear_model.SGDRegressor(max_iter =  10000)

# manual regression
regressor = MyLinearUnivariateRegression()
regressor.fit(trainInputs, trainOutputs)

w0, w1 = regressor.intercept_, regressor.coef_

# plot the model
noOfPoints = 1000
xref = []
val = min(trainInputs)
step = (max(trainInputs) - min(trainInputs)) / noOfPoints
for i in range(1, noOfPoints):
    xref.append(val)
    val += step
yref = [w0 + w1 * el for el in xref]
plot_data(trainInputs, trainOutputs, xref, yref, [], [], title="train data and model")

# makes predictions for test data
# computedTestOutputs = [w0 + w1 * el for el in testInputs]
# makes predictions for test data (by tool)
computedValidationOutputs = regressor.predict([[x] for x in validationInputs])
plot_data([], [], validationInputs, computedValidationOutputs, validationInputs, validationOutputs,
         "predictions vs real test data")

# compute the differences between the predictions and real outputs
error = 0.0
for t1, t2 in zip(computedValidationOutputs, validationOutputs):
    error += (t1 - t2) ** 2
error = error / len(validationOutputs)
print("prediction error (manual): ", error)

error = mean_squared_error(validationOutputs, computedValidationOutputs)
print("prediction error (tool): ", error)