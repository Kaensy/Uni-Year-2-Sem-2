import numpy as np


class BatchGradientDescent:
    def __init__(self, learning_rate=0.01, epochs=1000):
        self.learning_rate = learning_rate
        self.epochs = epochs
        self.coef_ = None
        self.intercept_ = None

    def fit(self, X, y):
        num_samples, num_features = X.shape

        # initialize weights and bias
        self.coef_ = np.zeros(num_features)
        self.intercept_ = 0

        # gradient descent
        for _ in range(self.epochs):
            y_predicted = np.dot(X, self.coef_) + self.intercept_

            # compute gradients
            dw = (1 / num_samples) * np.dot(X.T, (y_predicted - y))
            db = (1 / num_samples) * np.sum(y_predicted - y)

            # update parameters
            self.coef_ -= self.learning_rate * dw
            self.intercept_ -= self.learning_rate * db

    def predict(self, X):
        return np.dot(X, self.coef_) + self.intercept_

