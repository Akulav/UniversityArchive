import matplotlib.pyplot as plt
from sklearn.decomposition import PCA
from sklearn import manifold, datasets
from sklearn.manifold import MDS as mds
from sklearn.manifold import TSNE as tsne
import sklearn.datasets

n = 2000


# I.1
swiss_roll_p, swiss_roll_c = sklearn.datasets.make_swiss_roll(n_samples=n)
fig = plt.figure()
plt_before = fig.add_subplot(151, projection="3d")
plt_before.scatter(swiss_roll_p[:, 0], swiss_roll_p[:, 1], swiss_roll_p[:, 2], c=swiss_roll_c)
plt_before.set_title('I.1 swiss_roll_dataset before PCA')


# I.2
srPCA = PCA(n_components=2)
swiss_roll_PCA = srPCA.fit(swiss_roll_p, swiss_roll_c).transform(swiss_roll_p)
plt_after = fig.add_subplot(152)
plt_after.scatter(swiss_roll_PCA[:, 0], swiss_roll_PCA[:, 1], c=swiss_roll_c)
plt_after.set_title('I.2 swiss_roll_dataset after PCA')


# I.3
swiss_roll_p_lle, swiss_roll_lle_error = manifold.locally_linear_embedding(swiss_roll_p, n_neighbors=5, n_components=2)
print("error: " + str(swiss_roll_lle_error))
arr32lle_in = list()
arr32lle_err = list()
for i in range(2, 16):
    try:
        swiss_roll_p_lle, err = manifold.locally_linear_embedding(swiss_roll_p, n_neighbors=i, n_components=2)
        arr32lle_err.append(err)
        arr32lle_in.append(i)
    except:
        pass
plt_after = fig.add_subplot(153)
plt_after.plot(arr32lle_in, arr32lle_err, c='blue')
plt_after.set_title('I.3 x: number of neighbours   y: error')


# I.4
swiss_roll_p_mds = mds(n_components=2).fit_transform(swiss_roll_p)
print(swiss_roll_p_mds)
plt_after_mds = fig.add_subplot(154)
plt_after_mds.scatter(swiss_roll_p_mds[:, 0], swiss_roll_p_mds[:, 1], c=swiss_roll_c)
plt_after_mds.set_title('I.4 swiss_roll_dataset after MDS')


# I.5
swiss_roll_p_tsne = tsne(n_components=2).fit_transform(swiss_roll_p)
print(swiss_roll_p_tsne)
plt_after_tsne = fig.add_subplot(155)
plt_after_tsne.scatter(swiss_roll_p_tsne[:, 0], swiss_roll_p_tsne[:, 1], c=swiss_roll_c)
plt_after_tsne.set_title('I.5 swiss_roll_dataset after TSNE')


plt.show()
