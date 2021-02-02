import math
import csv

# Radius of the moon, in meters
# 1737.4 kilometers
radius = 1000 * 1737.4

with open('LargeAssets/nasa_dataset.csv') as csv_file:
    dataset = csv.reader(csv_file)
    with open('LargeAssets/slope.csv', 'w') as result_file:
        results = csv.writer(result_file)
        for row in dataset:
            lat = row[0]
            long = row[1]
            point_height = float(row[2]) + radius
            x = point_height * math.cos(math.radians((float(lat)))) * math.cos(math.radians((float(long))))
            y = point_height * math.cos(math.radians((float(lat)))) * math.sin(math.radians((float(long))))
            # z = point_height * math.sin(math.radians((float(lat))))
            slope = row[3]

            results.writerow([x, y, slope])
