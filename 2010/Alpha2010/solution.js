function solution(A) {
    let map = new Map();
    for (let i = 0; i < A.length; i++) {
        if (map.get(A[i]) === undefined) {
            map.set(A[i], i);
        }
    }
    let result = 0;
    map.forEach(x => result = Math.max(result, x));
    return result;
}