import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn import preprocessing
import statsmodels.api as sm
from statsmodels.discrete.discrete_model import Probit
#--------------
dataset = pd.read_csv('Lab_2_data.csv')
print (dataset.head(5))
#--------------
dataset = dataset.drop(dataset.columns[0], axis = 1)
dataset["lfp"] = dataset["lfp"] == "yes"
dataset["wc"] = dataset["wc"] == "yes"
dataset["hc"] = dataset["hc"] == "yes"

print(dataset.describe())

#--------------
print(dataset.isnull().sum())

#--------------
for x in dataset.columns:
    if (x == 'wc' or x == 'hc'):
        continue
    plt.hist(dataset[x], color = 'gray', edgecolor = 'black',
         bins = int(35000/1000))
    plt.axvline(dataset[x].mean(), color='blue', linestyle='dashed', linewidth=1)
    plt.axvline(dataset[x].median(), color='green', linestyle='dashed', linewidth=1)
    plt.title(x)
    plt.show()

#--------------
corr = dataset.corr()
ax = sns.heatmap(
    corr, 
    vmin=-1, vmax=1, center=0,
    cmap=sns.diverging_palette(20, 220, n=200),
    square=True
)
ax.set_xticklabels(
    ax.get_xticklabels(),
    rotation=45,
    horizontalalignment='right'
)
plt.show()

#--------------
print("\nProbit model:")
X = dataset[['k5', 'k618', 'age', 'wc', 'hc', 'lwg', 'inc']]
Y = dataset['lfp']
X = sm.add_constant(X)
model = Probit(Y, X.astype(float))
probit_model = model.fit()
print(probit_model.summary())