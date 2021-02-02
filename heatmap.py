import numpy as np
import pandas as pd
import csv
import matplotlib
import time
import seaborn as sns
from image_utils import crop_transparent

import matplotlib.pyplot as plt

start = time.time()
x = []
y = []
z = []
# with open("interpolated.txt", newline='') as File:
#     reader = csv.reader(File, delimiter=" ")
#     for row in reader:
#         x.append(float(row[0]))
#         y.append(float(row[1]))
#         z.append(float(row[2]))
# print(x)
# print(y)
# print(f"X: {x[0:10]}")
# print(f"Y: {y[0:10]}")
# print(f"Z: {z[0:10]}")
# print(f"len(x) = {len(x)}\nlen(y) = {len(y)}\nlen(z) = {len(z)}")
# print(f"read time: {time.time() - start}")

arr = np.loadtxt("interpolated.txt")
start = time.time()
mask = arr == 0

ax = sns.heatmap(arr, square=True, cbar=False, xticklabels=False, yticklabels=False, mask=mask, cmap="gist_rainbow")
print(f"heatmap generation time: {time.time() - start}")
plt.savefig("height.png", dpi=4800, transparent=True)
crop_transparent("height.png", "height_cropped.png")
plt.show()
