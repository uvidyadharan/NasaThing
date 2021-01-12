import math


class Point(object):
    """
    Example:
    p1 = Point(1,1,1)
    p2 = Point(2,2,2)
    p1.distance(p2)
    >>> 1.73205...
    p1.move(0, 1, 0)
    print(p1)
    >>> Point(1, 2, 1)
    p1.get_x()
    >>> 1
    """
    def __init__(self, x, y, z):
        """
        :param x: float
        :param y: float
        :param z: float
        """
        self.x = x
        self.y = y
        self.z = z

    def move(self, dx=0, dy=0, dz=0):
        """
        :param dx: float
        :param dy: float
        :param dz: float
        :return: None
        """
        self.x += dx
        self.y += dy
        self.z += dz

    def __str__(self):
        return f"Point({self.x}, {self.y}, {self.z})"

    def get_x(self):
        return self.x

    def get_y(self):
        return self.y

    def get_z(self):
        return self.z

    def distance(self, p):
        dx = self.x - p.x
        dy = self.y - p.y
        dz = self.z - p.z
        dp = math.sqrt(dx**2 + dy**2 + dz**2)
        return dp