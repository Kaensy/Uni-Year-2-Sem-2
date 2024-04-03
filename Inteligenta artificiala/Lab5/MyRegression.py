class MyLinearUnivariateRegression:
    def __init__(self):
        self.intercept_ = 0.0
        self.coef_ = 0.0

    # learn a linear univariate regression model by using training inputs (x) and outputs (y)
    def fit(self, x, y):
        sx = sum(x)
        sy = sum(y)
        sx2 = sum(i * i for i in x)
        sxy = sum(i * j for (i, j) in zip(x, y))
        w1 = (len(x) * sxy - sx * sy) / (len(x) * sx2 - sx * sx)
        w0 = (sy - w1 * sx) / len(x)
        self.intercept_, self.coef_ = w0, w1

    # predict the outputs for some new inputs (by using the learnt model)
    def predict(self, x):
        if (isinstance(x[0], list)):
            return [self.intercept_ + self.coef_ * val[0] for val in x]
        else:
            return [self.intercept_ + self.coef_ * val for val in x]

class My3DRegression:
    def __init__(self):
        self.intercept_ = 0.0
        self.coef_ = 0.0

    def fit(self, x, y):
        sx1 = sum(val[0] for val in x)
        sx2 = sum(val[1] for val in x)
        sy = sum(y)
        sx1_2 = sum(val[0]**2 for val in x)
        sx2_2 = sum(val[1]**2 for val in x)
        sxy1 = sum(val[0]*y[i] for i, val in enumerate(x))
        sxy2 = sum(val[1]*y[i] for i, val in enumerate(x))
        w1 = (len(x)*sxy1 - sx1*sy) / (len(x)*sx1_2 - sx1**2)
        w2 = (len(x)*sxy2 - sx2*sy) / (len(x)*sx2_2 - sx2**2)
        w0 = (sy - w1*sx1 - w2*sx2) / len(x)
        self.intercept_, self.coef_ = w0, (w1, w2)

    def predict(self, x):
        x1, x2 = zip(*x)
        return [self.intercept_ + self.coef_[0]*x1[i] + self.coef_[1]*x2[i] for i in range(len(x))]