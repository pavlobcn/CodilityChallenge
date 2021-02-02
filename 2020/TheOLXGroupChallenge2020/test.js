import { describe } from 'jest';

describe("solution", () => {
    beforeAll(() => {
    });

    beforeEach(() => {
    });

    afterAll(() => {
    });

    test("Test", () => {
        expect(solution([10, 2, 1, 1], [10, 3, 2, 2])).toBe(2);
    });
});

var result1 = solution([10, 2, 1, 1], [10, 3, 2, 2]);
var result2 = solution([1, 2, 3, 4], [3, 6, 4, 4]);
var result3 = solution([2, 3], [3, 4]);
var result4 = solution([1, 1, 5], [6, 5, 8]);
console.info("Expected: " + 2 + ". Actual: " + result1);
console.info("Expected: " + 3 + ". Actual: " + result2);
console.info("Expected: " + 1 + ". Actual: " + result3);
console.info("Expected: " + 3 + ". Actual: " + result4);