import numpy as np
import pandas as pd
import csv
import matplotlib
import time

import matplotlib.pyplot as plt

plt.style.use('seaborn-white')
start = time.time()
x = []
y = []
z = []
with open("C:\\Users\\ja766\\AppData\\Local\\Programs\\Python\\Python37\\NASAADC\\Regional_Data_File.csv", newline='') as File:
    reader = csv.reader(File)
    for row in reader:
        x.append(float(row[0]))
        y.append(float(row[1]))
        z.append(float(row[2]))
print(f"X: {x[0:10]}")
print(f"Y: {y[0:10]}")
print(f"Z: {z[0:10]}")
print(f"len(x) = {len(x)}\nlen(y) = {len(y)}\nlen(z) = {len(z)}")
print(f"read time: {time.time() - start}")

start = time.time()
"""
use either one depending on what u want

plt.contourf(x, y, z)
plt.scatter(x,y,c=z, s=0.01)
"""
print(f"heatmap generation time: {time.time() - start}")
plt.show()
