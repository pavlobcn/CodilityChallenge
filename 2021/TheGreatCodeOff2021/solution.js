function solution(N, K, A, B, C) {
    return 0;
}


/* Tests */

let result1 = solution(5, 3, [1, 1, 4, 1, 4], [5, 2, 5, 5, 4], [1, 2, 2, 3, 3]);
let result2 = solution(6, 4, [1, 2, 1, 1], [3, 3, 6, 6], [1, 2, 3, 4]);
let result3 = solution(3, 2, [1, 3, 3, 1, 1], [2, 3, 3, 1, 2], [1, 2, 1, 2, 2]);
let result4 = solution(5, 2, [1, 1, 2], [5, 5, 3], [1, 2, 1]);
console.info("Expected: " + 3 + ". Actual: " + result1);
console.info("Expected: " + 2 + ". Actual: " + result2);
console.info("Expected: " + 1 + ". Actual: " + result3);
console.info("Expected: " + 3 + ". Actual: " + result4);
