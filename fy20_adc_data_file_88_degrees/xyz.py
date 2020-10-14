import csv
from math import cos
from math import sin
result = []
with open('fy20_adc_data_file_88_degrees.csv',"r", newline='') as csvfile:
    spamreader = csv.reader(csvfile, delimiter=',', quotechar='|')
    rad = 1737.4
    
    for row in spamreader:
        
        lat = float(row[0])
        lon = float(row[1])
        
        x = rad*(cos(lat)*cos(lon))
        y = rad*(cos(lat)*sin(lon))
        z = rad*sin(lat)

        result.append([x, y, z])
        

        

with open("out.csv", "w", newline='') as f:
    writer = csv.writer(f)
    writer.writerows(result)