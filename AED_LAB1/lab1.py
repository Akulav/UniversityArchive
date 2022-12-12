import numpy as np
import pandas as pd
import pickle
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score
from sklearn.model_selection import train_test_split


dataset = pd.read_csv('HousingData.csv')
get_rows = dataset.head(20)

print (get_rows)

##

#median = np.median(dataset['MEDV'])
#print(median)

print('----MEDIAN----')
print(dataset.median())
print('----MIN----')
print(dataset.min())
print('----MAX----')
print(dataset.max())
print('----VARIABILITY----')
print(dataset.max()-dataset.min())
print('----SKEW----')
print(dataset.skew())


data_types_dict = {'CHAS': bool}
dataset = dataset.astype(data_types_dict)
print(dataset.dtypes)

	

dataset= dataset.fillna(dataset.median())	
#dataset['LSTAT'] = dataset['LSTAT'].fillna(dataset['LSTAT'].mode()[0])
get_rows = dataset.head(20)

print (get_rows)


river_dataset = dataset.loc[dataset['CHAS'] == 1]
non_river_dataset = dataset.loc[dataset['CHAS'] == 0]

for x in river_dataset.columns:
    if (x == 'CHAS'):
        continue
    plt.boxplot(dataset[x])
    plt.title("NEAR RIVER | " + x)
    plt.show()

for x in non_river_dataset.columns:
    if (x == 'CHAS'):
        continue
    plt.boxplot(dataset[x])
    plt.title("NOT NEAR THE RIVER" + x)
    plt.show()


for x in dataset.columns:
    if (x == 'CHAS'):
        continue
    plt.hist(dataset[x], color = 'blue', edgecolor = 'black',
         bins = int(35000/1000))
    plt.axvline(dataset[x].mean(), color='red', linestyle='dashed', linewidth=1)
    plt.axvline(dataset[x].median(), color='green', linestyle='dashed', linewidth=1)
    plt.title(x)
    plt.show()

for x in dataset.columns:
    if (x == 'CHAS'):
        continue
    plt.boxplot(dataset[x])
    plt.title(x)
    plt.show()



#task 8

plt.scatter(dataset['CRIM'], dataset['CHAS'])
plt.title("CRIM and CHAS")
plt.show()

#task 9

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


#task 10
	
X = dataset[['LSTAT','RM']]
y = dataset[['MEDV']]
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.25, random_state=0)
linreg = LinearRegression()
linreg = linreg.fit(X_train, y_train)
y_pred = linreg.predict(X_test)

lin_model = pd.DataFrame(y_pred, columns=['Predicted_MEDV'])
lin_model['Actual_MEDV'] = y_test.to_numpy()
print('\n[7] Model')
print(lin_model.head(200))