import numpy as np

class KMeansz:
    def __init__(self, n_clusters=2, max_iter=300, random_state=0):
        self.n_clusters = n_clusters
        self.max_iter = max_iter
        self.random_state = random_state

    def fit(self, data):
        np.random.seed(self.random_state)
        # 1. Initialize centroids randomly from the data points
        initial_centroids = np.random.choice(len(data), self.n_clusters, replace=False)
        self.centroids = [data[i] for i in initial_centroids]

        for _ in range(self.max_iter):
            self.labels_ = [self._closest_centroid(datum) for datum in data]
            new_centroids = []
            for i in range(self.n_clusters):
                cluster_data = [data[j] for j in range(len(data)) if self.labels_[j] == i]
                new_centroid = np.mean(cluster_data, axis=0)
                new_centroids.append(new_centroid)
            if np.allclose(self.centroids, new_centroids):
                break

            self.centroids = new_centroids

    def predict(self, data):
        return [self._closest_centroid(datum) for datum in data]

    def _closest_centroid(self, datum):
        distances = [np.linalg.norm(datum - centroid) for centroid in self.centroids]
        return np.argmin(distances)