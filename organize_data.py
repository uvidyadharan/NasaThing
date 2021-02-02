import numpy as np
from scipy.interpolate import griddata
import csv

points = []
values = []
total_points = (100, 100)
# Load dataset into memory
with open("LargeAssets/cartesian.csv", "r") as csv_file:
    data = csv.reader(csv_file)
    for row in data:
        try:
            points.append((float(row[0]), float(row[1])))
            values.append(float(row[2]))
        except IndexError:
            pass

points_nd = np.array(points, dtype=np.double)
max = np.amax(points_nd.transpose()[0]), np.amax(points_nd.transpose()[1])
print(max)
min = np.amin(points_nd.transpose()[0]), np.amin(points_nd.transpose()[1])
print(min)
values_nd = np.array(values, dtype=np.double)

xi_x, xi_y = np.mgrid[min[0]:max[0]:total_points[0], min[1]:max[1]:total_points[1]]

print(xi_x)
print(xi_y)

grid = griddata(points_nd, values_nd, (xi_x, xi_y), method="linear", fill_value=0)
print(grid)
np.savetxt("interpolated.txt", grid)

print("done")
