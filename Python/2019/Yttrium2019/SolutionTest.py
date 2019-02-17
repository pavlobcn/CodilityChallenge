import unittest
from Solution import solution

class Test_SolutionTest(unittest.TestCase):
    def test_1(self):
        self.execute("abaacbca", 2, 3)
    def test_2(self):
        self.execute("aabcabc", 1, 5)
    def test_3(self):
        self.execute("zaaaa", 1, 1)
    def test_4(self):
        self.execute("aaaa", 2, -1)
    def test_5(self):
        self.execute("aaaa", 0, 4)
    def execute(self, s, k, expectedResult):
        actualResult = solution(s, k)
        self.assertEqual(expectedResult, actualResult)

if __name__ == '__main__':
    unittest.main()