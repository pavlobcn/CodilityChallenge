function solution(juice, capacity) {
    var itemsByJuice = [];
    for (i = 0; i < juice.length; i++)
    {
        itemsByJuice[i] = {juice : juice[i], capacity: capacity[i], availableCapacity: capacity[i] - juice[i]};
    }
    itemsByJuice.sort((a, b) =>
        a.juice > b.juice
            ? 1
            : (b.juice > a.juice
                ? -1
                : a.availableCapacity - b.availableCapacity));
    for (i = 0; i < juice.length; i++)
    {
        itemsByJuice[i].position = i;
    }
    var itemsByAvailableCapacity = [];
    for (i = 0; i < juice.length; i++)
    {
        itemsByAvailableCapacity[i] = itemsByJuice[i];
    }
    itemsByAvailableCapacity.sort((a, b) => b.availableCapacity - a.availableCapacity);
    var result1 = calculateWhenTargetCupInList(itemsByJuice);
    var result2 = calculateWhenTargetCupOutList(itemsByJuice, itemsByAvailableCapacity);
    return Math.max(result1, result2);
}

function calculateWhenTargetCupInList(itemsByJuice) {
    var totalJuice = 0;
    var maxCapacity = 0;
    var result = 0;
    for (i = 0; i < itemsByJuice.length; i++)
    {
        totalJuice += itemsByJuice[i].juice;
        maxCapacity = Math.max(maxCapacity, itemsByJuice[i].capacity);
        if (maxCapacity >= totalJuice)
        {
            result = Math.max(result, i + 1);
        }
    }
    return result;
}

function calculateWhenTargetCupOutList(itemsByJuice, itemsByAvailableCapacity) {
    var maxAvailableCapacity = 0;
    for (i = itemsByJuice.length - 1; i >=0; i--)
    {
        maxAvailableCapacity = Math.max(maxAvailableCapacity, itemsByJuice[i].availableCapacity);
        itemsByJuice[i].maxAvailableCapacity = maxAvailableCapacity;
    }
    var result = 0;
    var totalJuice = 0;
    for (i = 0; i < itemsByJuice.length - 1; i++)
    {
        totalJuice += itemsByJuice[i].juice;
        if (totalJuice <= itemsByJuice[i + 1].maxAvailableCapacity)
        {
            result = i + 2;
        }
    }
    return result;
}
