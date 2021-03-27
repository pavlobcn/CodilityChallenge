function solution(P, T, A, B) {
    const connections = getConnections(P.length, A, B);
    const groups = getGroups(connections);
    const allGroupsAreValid = validateGroups(groups);
    return allGroupsAreValid;

    function getConnections(size) {
        const connections = [];
        for (let i = 0; i < size; i++) {
            connections[i] = [];
        }
        for (let i = 0; i < A.length; i++) {
            connections[A[i]].push(B[i]);
            connections[B[i]].push(A[i]);
        }
        return connections;
    }

    function getGroups(connections) {
        const groups = [];
        const nodeTypes = [];
        const visitedNodeType = 1;
        const connectedNodeType = 2;
        const notConnectedNodeType = 3;
        const notConnectedNodes = new Set();
        for (let i = 0; i < P.length; i++) {
            nodeTypes[i] = notConnectedNodeType;
            notConnectedNodes.add(i);
        }
        while (notConnectedNodes.size > 0) {
            const group = [];
            const notConnectedNode = notConnectedNodes.values().next().value;
            const connectedNodes = [notConnectedNode];
            nodeTypes[notConnectedNode] = connectedNodeType;
            notConnectedNodes.delete(notConnectedNode);

            for (let i = 0; i < connectedNodes.length; i++) {
                const connectedNode = connectedNodes[i];
                const nextConnections = connections[connectedNode];
                for (let j = 0; j < nextConnections.length; j++) {
                    const nextConnection = nextConnections[j];
                    if (nodeTypes[nextConnection] === notConnectedNodeType) {
                        connectedNodes.push(nextConnection);
                        nodeTypes[nextConnection] = connectedNodeType;
                        notConnectedNodes.delete(nextConnection);
                    }
                }
                nodeTypes[connectedNode] = visitedNodeType;
                group.push(connectedNode)
            }

            groups.push(group);
        }
        return groups;
    }

    function validateGroups(groups) {
        for (let i = 0; i < groups.length; i++) {
            const isGroupValid = validateGroup(groups[i]);
            if (!isGroupValid) {
                return false;
            }
        }
        return true;
    }

    function validateGroup(group) {
        let petCount = 0;
        let toyCount = 0;
        const dog = 1;
        for (let i = 0; i < group.length; i++) {
            if (P[group[i]] === dog) {
                petCount++;
            }
            if (T[group[i]] === dog) {
                toyCount++;
            }
        }
        return petCount === toyCount;
    }
}

/* Tests */

let result1 = solution([1, 1, 2], [2, 1, 1], [0, 2], [1, 1]);
let result2 = solution([2, 2, 1, 1, 1], [1, 1, 1, 2, 2], [0, 1, 2, 3], [1, 2, 0, 4]);
let result3 = solution([1, 1, 2, 2, 1, 1, 2, 2], [1, 1, 1, 1, 2, 2, 2, 2], [0, 2, 4, 6], [1, 3, 5, 7]);
let result4 = solution([2, 2, 2, 2, 1, 1, 1, 1], [1, 1, 1, 1, 2, 2, 2, 2], [0, 1, 2, 3, 4, 5, 6], [1, 2, 3, 4, 5, 6, 7]);
console.info("Expected: " + true + ". Actual: " + result1);
console.info("Expected: " + false + ". Actual: " + result2);
console.info("Expected: " + false + ". Actual: " + result3);
console.info("Expected: " + true + ". Actual: " + result4);
