function solution(A) {
    let result = 0;
    const bitCount = getBitCount(A.length);
    const array = [A.length];
    for (let i = 0; i < A.length; i++) {
        array[i] = {height: A[i], bits: getBits(bitCount, A[i])}
    }

    for (let start = 0; start < array.length; start++)
    {
        let heightInfo = {};
        let finish = start + 1;
        if (array[start].height < result) {
            continue;
        }
        add(heightInfo, start, array[start]);
        while (true) {
            if (array.length - start < result) {
                return result
            }
            let length = finish - start;
            let finishHeight = finish === array.length ? 0 : array[finish].height;
            if (length >= heightInfo.minHeight)
            {
                let newResult = heightInfo.minHeight;
                result = Math.max(result, newResult);
                let newStart = getNextAfterLastMinHeight(heightInfo);
                if (newStart - start < finish - newStart) {
                    for (let i = start; i < newStart; i++) {
                        remove(heightInfo, i, array[i]);
                    }
                } else {
                    heightInfo = {};
                    for (let i = newStart; i < finish; i++) {
                        add(heightInfo, i, array[i]);
                    }
                }
                start = newStart;
                if (start >= array.length)
                {
                    break;
                }
                if (start >= finish) {
                    finish = start + 1;
                    heightInfo = {};
                    if (array[start].height < result) {
                        start = finish;
                        break;
                    }
                    add(heightInfo, start, array[start]);
                }
            }
            else if (finishHeight < length)
            {
                let newResult = finish - start;
                result = Math.max(result, newResult);
                start = finish - 1;
                break;
            }
            else
            {
                if (array[finish].height < result) {
                    start = finish;
                    break;
                }
                add(heightInfo, finish, array[finish]);
                finish++;
            }
        }
    }
    return result;

    function getNextAfterLastMinHeight(heightInfo) {
        const bits = getBits(bitCount, heightInfo.minHeight);
        return findNextAfterLastMinHeight(heightInfo, bits, 0);
    }

    function findNextAfterLastMinHeight(heightInfo, bits, bitPosition) {
        if (bitPosition === bitCount) {
            return heightInfo.lastPosition + 1;
        }
        return findNextAfterLastMinHeight(heightInfo.children[bits[bitPosition]], bits, bitPosition + 1);
    }

    function add(heightInfo, position, point) {
        include(heightInfo, position, point.height, point.bits, 0);
    }

    function remove(heightInfo, position, point) {
        exclude(heightInfo, position, point.bits, 0);
    }

    function include(heightInfo, position, height, bits, bitPosition) {
        if (bitPosition === bitCount) {
            heightInfo.minHeight = height;
            heightInfo.lastPosition = position;
            return;
        }
        if (heightInfo.children === undefined) {
            heightInfo.children = new Array(10);
        }
        if (heightInfo.children[bits[bitPosition]] === undefined) {
            heightInfo.children[bits[bitPosition]] = {};
        }
        include(heightInfo.children[bits[bitPosition]], position, height, bits, bitPosition + 1);
        heightInfo.minHeight = Math.min(heightInfo.children[bits[bitPosition]].minHeight, heightInfo.minHeight === undefined ? 1000000 : heightInfo.minHeight);
    }

    function exclude(heightInfo, position, bits, bitPosition) {
        if (bitPosition === bitCount) {
            if (heightInfo.lastPosition === position) {
                heightInfo.lastPosition = undefined;
                heightInfo.minHeight = 1000000;
            }
            return;
        }
        exclude(heightInfo.children[bits[bitPosition]], position, bits, bitPosition + 1);
        if (heightInfo.children[bits[bitPosition]].minHeight === 1000000) {
            heightInfo.children[bits[bitPosition]] = undefined;
        }
        heightInfo.minHeight = 1000000;
        for (let i = 0; i < heightInfo.children.length; i++) {
            if (heightInfo.children[i] !== undefined) {
                heightInfo.minHeight = heightInfo.children[i].minHeight;
                break;
            }
        }
    }

    function getBitCount(length) {
        let count = 1;
        while (Math.pow(10, count) <= length) {
            count++;
        }
        return count;
    }

    function getBits(length, value) {
        let bits = [];
        for (let i = 0; i < length; i++) {
            bits[length - i - 1] = value % 10;
            value = (value - value % 10) / 10;
        }
        return bits;
    }
}

/* Tests */

let result1 = solution([1, 2, 5, 3, 1, 3]);
let result2 = solution([3, 3, 3, 5, 4]);
let result3 = solution([6, 5, 5, 6, 2, 2]);
let result4 = solution([1, 6, 3, 10, 1, 2, 1, 2, 2, 2]);
let result5 = solution([1, 2, 3, 4, 5]);
console.info("Expected: " + 2 + ". Actual: " + result1);
console.info("Expected: " + 3 + ". Actual: " + result2);
console.info("Expected: " + 4 + ". Actual: " + result3);
console.info("Expected: " + 3 + ". Actual: " + result4);
console.info("Expected: " + 3 + ". Actual: " + result5);

testPerformance();
function testPerformance()
{
    let length = 100000;
    let a = [];
    for (let i = 0; i < length; i++)
    {
        a[i] = i + 1;
    }
    let result = solution(a);
    console.info("Expected: " + length / 2 + ". Actual: " + result);
}