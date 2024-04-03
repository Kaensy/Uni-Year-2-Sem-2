import csv
import os
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from sklearn import linear_model
from sklearn.metrics import mean_squared_error


def load_data(fileName, inputVariabName1, inputVariabName2, outputVariabName):
    df = pd.read_csv(fileName)
    df = df.fillna(df.min())

    inputs_local1 = df[inputVariabName1].tolist()
    inputs_local2 = df[inputVariabName2].tolist()
    outputs_local = df[outputVariabName].tolist()

    return inputs_local1, inputs_local2, outputs_local


def plot_data_histogram(x1, variable_name1, x2=None, variable_name2=None):
    if x2 is None:
        n, bins, patches = plt.hist(x1, 10)
        plt.title('Histogram of ' + variable_name1)
        plt.show()
    else:
        plt.hist(x1, 10, alpha=0.5, label=variable_name1)
        plt.hist(x2, 10, alpha=0.5, label=variable_name2)
        plt.legend(loc='upper right')
        plt.title('Histogram of ' + variable_name1 + ' and ' + variable_name2)
        plt.show()


def plot_data(x1, y1, x2=None, y2=None, x3=None, y3=None, title=None):
    plt.plot(x1, y1, 'ro', label='GDP')
    if x2:
        plt.plot(x2, y2, 'bo', label='Freedom')
    if x3:
        plt.plot(x3, y3, 'g', label='test data')
    plt.title(title)
    plt.legend()
    plt.show()


def plot_data_3d(x1, x12, y1, x2=None, y2=None, x3=None, x32=None, y3=None, x4=None, y4=None, title=None):
    plt.plot(x1, y1, 'ro', label='train data GDP')
    plt.plot(x12, y1, 'bo', label='train data Freedom')

    if x2:
        plt.plot(x2, y2, 'c-', label='learnt model')

    if x3:
        plt.plot(x3, y3, 'g^', label='test data GDP')
        plt.plot(x32, y3, 'y^', label='test data Freedom')

    if x4:
        plt.plot(x4, y4, 'mo', label='learnt model')

    plt.title(title)
    plt.legend()
    plt.show()


if __name__ == '__main__':
    crtDir = os.getcwd()
    filePath = os.path.join(crtDir, 'data', 'v1_world-happiness-report-2017.csv')

    inputs1, inputs2, outputs = load_data(filePath, 'Economy..GDP.per.Capita.', 'Freedom', 'Happiness.Score')
    print('in:', inputs1[:5])
    print('in:', inputs2[:5])
    print('out:', outputs[:5])

    plot_data_histogram(inputs1, 'GDP', inputs2, 'Freedom')
    plot_data_histogram(outputs, 'Happiness Score')

    plot_data(inputs1, outputs, inputs2, outputs, [], [], 'Happiness Score vs GDP and Freedom')

    # split the data into training and validation data
    np.random.seed(5)
    indexes = [i for i in range(len(inputs1))]
    trainSample = np.random.choice(indexes, int(0.8 * len(indexes)), replace=False)
    validationSample = [i for i in indexes if i not in trainSample]
    trainInputs1 = [inputs1[i] for i in trainSample]
    trainInputs2 = [inputs2[i] for i in trainSample]
    trainOutputs = [outputs[i] for i in trainSample]
    validationInputs1 = [inputs1[i] for i in validationSample]
    validationInputs2 = [inputs2[i] for i in validationSample]
    validationOutputs = [outputs[i] for i in validationSample]

    plot_data_3d(trainInputs1, trainInputs2, trainOutputs, [], [], validationInputs1, validationInputs2,
                 validationOutputs, title="Train and Test data")

    # training
    xx = [el for el in zip(trainInputs1, trainInputs2)]

    # sklearn function
    regressor = linear_model.LinearRegression()
    regressor.fit(xx, trainOutputs)

    w0, w1 = regressor.intercept_, regressor.coef_

    # plot the model
    noOfPoints = 1000
    xref = []
    val = min(min(xx))
    step = (max(max(xx)) - min(min(xx))) / noOfPoints
    for i in range(1, noOfPoints):
        xref.append(val)
        val += step
    vref = [w0 + w1[0] * el + w1[1] * el for el in xref]
    plot_data_3d(trainInputs1, trainInputs2, trainOutputs, xref, vref, title="Train and Test Data")

    # predict the validation data
    validationInputs = [x for x in zip(validationInputs1, validationInputs2)]
    computedValidationOutputs = regressor.predict(validationInputs)
    plot_data_3d([], [], [], [], [], validationInputs1, validationInputs2, validationOutputs, validationInputs,
                 computedValidationOutputs, title="Predictions vs Real Test data")

    #compute the differences between the predictions and real outputs
    error = 0.0
    for t1,t2 in zip(computedValidationOutputs, validationOutputs):
        error += (t1-t2)**2
    error = error/len(validationOutputs)
    print("Prediction Error [manual]: ", error)

    error = mean_squared_error(validationOutputs, computedValidationOutputs)
    print("Prediction Error [sklearn]: ", error)
