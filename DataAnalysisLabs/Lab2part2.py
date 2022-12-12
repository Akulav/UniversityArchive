from sklearn.datasets import load_digits
import matplotlib.pyplot as plt
import numpy as np
from sklearn import preprocessing as pr
from matplotlib import offsetbox
from sklearn.decomposition import TruncatedSVD
from sklearn.discriminant_analysis import LinearDiscriminantAnalysis
from sklearn.ensemble import RandomTreesEmbedding
from sklearn.manifold import Isomap
from sklearn.manifold import LocallyLinearEmbedding
from sklearn.manifold import MDS
from sklearn.manifold import SpectralEmbedding
from sklearn.manifold import TSNE
from sklearn.neighbors import NeighborhoodComponentsAnalysis
from sklearn.pipeline import make_pipeline
from sklearn.random_projection import SparseRandomProjection
from time import time
from sklearn.tree import DecisionTreeClassifier
from sklearn import tree
from sklearn import manifold


digits = load_digits(n_class=3)
X, y = digits.data, digits.target
n_samples, n_features = X.shape
n_neighbors = 30


swiss_roll_p_lle, swiss_roll_lle_error = manifold.locally_linear_embedding(X, n_neighbors=5, n_components=2)
print("error: " + str(swiss_roll_lle_error))
arr32lle_in = list()
arr32lle_err = list()
fig = plt.figure()
for i in range(2, 16):
    try:
        swiss_roll_p_lle, err = manifold.locally_linear_embedding(X, n_neighbors=i, n_components=2)
        arr32lle_err.append(err)
        arr32lle_in.append(i)
    except:
        pass
plt_after = fig.add_subplot(111)
plt_after.plot(arr32lle_in, arr32lle_err, c='blue')
plt_after.set_title('II.2 before x: number of neighbours   y: error')
plt.show()

clf = DecisionTreeClassifier()
clf = clf.fit(X=X, y=y)
tree.plot_tree(clf)
plt.show()


fig, axs = plt.subplots(nrows=10, ncols=10, figsize=(6, 6))
for idx, ax in enumerate(axs.ravel()):
    ax.imshow(X[idx].reshape((8, 8)), cmap=plt.cm.binary)
    ax.axis("off")
_ = fig.suptitle("A selection from the 64-dimensional digits dataset", fontsize=16)
plt.show()


def plot_embedding(X, title):
    _, ax = plt.subplots()
    X = pr.MinMaxScaler().fit_transform(X)

    for digit in digits.target_names:
        ax.scatter(
            *X[y == digit].T,
            marker=f"${digit}$",
            s=60,
            color=plt.cm.Dark2(digit),
            alpha=0.425,
            zorder=2,
        )
    shown_images = np.array([[1.0, 1.0]])
    for i in range(X.shape[0]):
        dist = np.sum((X[i] - shown_images) ** 2, 1)
        if np.min(dist) < 4e-3:
            continue
        shown_images = np.concatenate([shown_images, [X[i]]], axis=0)
        imagebox = offsetbox.AnnotationBbox(
            offsetbox.OffsetImage(digits.images[i], cmap=plt.cm.gray_r), X[i]
        )
        imagebox.set(zorder=1)
        ax.add_artist(imagebox)

    ax.set_title(title)
    ax.axis("off")


embeddings = {
    "Random projection embedding": SparseRandomProjection(
        n_components=2, random_state=42
    ),
    "Truncated SVD embedding": TruncatedSVD(n_components=2),
    "Linear Discriminant Analysis embedding": LinearDiscriminantAnalysis(
        n_components=2
    ),
    "Isomap embedding": Isomap(n_neighbors=n_neighbors, n_components=2),
    "Standard LLE embedding": LocallyLinearEmbedding(
        n_neighbors=n_neighbors, n_components=2, method="standard"
    ),
    "Modified LLE embedding": LocallyLinearEmbedding(
        n_neighbors=n_neighbors, n_components=2, method="modified"
    ),
    "Hessian LLE embedding": LocallyLinearEmbedding(
        n_neighbors=n_neighbors, n_components=2, method="hessian"
    ),
    "LTSA LLE embedding": LocallyLinearEmbedding(
        n_neighbors=n_neighbors, n_components=2, method="ltsa"
    ),
    "MDS embedding": MDS(n_components=2, n_init=1, max_iter=120, n_jobs=2),
    "Random Trees embedding": make_pipeline(
        RandomTreesEmbedding(n_estimators=200, max_depth=5, random_state=0),
        TruncatedSVD(n_components=2),
    ),
    "Spectral embedding": SpectralEmbedding(
        n_components=2, random_state=0, eigen_solver="arpack"
    ),
    "t-SNE embeedding": TSNE(
        n_components=2,
        n_iter=500,
        n_iter_without_progress=150,
        n_jobs=2,
        random_state=0,
    ),
    "NCA embedding": NeighborhoodComponentsAnalysis(
        n_components=2, init="pca", random_state=0
    ),
}


projections, timing = {}, {}
for name, transformer in embeddings.items():
    if name.startswith("Linear Discriminant Analysis"):
        data = X.copy()
        data.flat[:: X.shape[1] + 1] += 0.01  # Make X invertible
    else:
        data = X

    print(f"Computing {name}...")
    start_time = time()
    projections[name] = transformer.fit_transform(data, y)
    timing[name] = time() - start_time


for name in timing:
    lst = []
    title = f"{name} (time {timing[name]:.3f}s)"
    plot_embedding(projections[name], title)

plt.show()


# 2
lst  = zip(projections.keys(), projections.values())
lst  = list(lst)
lst2 = [[], [], []]

for i in range(0, len(lst)):
    for j in range(0, len(lst[i][1])):
        lst2[0].append(lst[i][0])
        lst2[1].append(lst[i][1][j][0])
        lst2[2].append(lst[i][1][j][1])

X = lst2[1:3]
y = lst2[0]

arrX = np.ndarray(shape=(len(X[0]), 2))

for i in range(0, 1):
    for j in range(0, len(X[0]) - 1):
        arrX[j, i] = X[i][j]


swiss_roll_p_lle, swiss_roll_lle_error = manifold.locally_linear_embedding(arrX, n_neighbors=5, n_components=2)
print("error: " + str(swiss_roll_lle_error))
arr32lle_in = list()
arr32lle_err = list()
fig = plt.figure()
for i in range(2, 16):
    try:
        swiss_roll_p_lle, err = manifold.locally_linear_embedding(arrX, n_neighbors=i, n_components=2)
        arr32lle_err.append(err)
        arr32lle_in.append(i)
    except:
        pass
plt_after = fig.add_subplot(111)
plt_after.plot(arr32lle_in, arr32lle_err, c='blue')
plt_after.set_title('II.2 before x: number of neighbours   y: error')
plt.show()


clf = DecisionTreeClassifier()
clf = clf.fit(X=arrX, y=y)
tree.plot_tree(clf)
plt.show()