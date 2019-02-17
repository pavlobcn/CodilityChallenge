def solution(S, K):
	if K == 0:
		return len(S)
	if IsNoAnswer(S, K):
		return -1
	if IsZero(S, K):
		return 0
	result = GetResult(S, K)
	return result

def GetResult(s, k):
	left = GetInfos(list(s))
	right = GetInfos(list(s[::-1]))
	result = len(s)
	i = 0
	while i <= k:
		if i == 0:
			rightChars = right[0:k]
			leftChars = TakeWithCondition(left, 1, rightChars)
			del leftChars[-1]
		else:
			leftChars = left[0:i]
			rightChars = TakeWithCondition(right, k - i + 1, leftChars)
			del rightChars[-1]
		newResult = len(s) - sum([c.Length for c in leftChars]) - sum([c.Length for c in rightChars])
		result = min(result, newResult)
		if result == 1:
			return result
		i += 1
	return result

def TakeWithCondition(source, count, otherChars):
	result = []
	added = 0
	i = 0
	while i < len(source):
		if not any(source[i].C == x.C for x in otherChars):
			added += 1
		result.append(source[i])
		if added == count:
			break
		i += 1
	return result


def GetInfos(s):
	infos = []
	chars = [False] * 26
	length = 0
	c = ' '
	i = 0
	while i <= len(s):
		if i == 0:
			length = 1
			c = s[0]
			chars[ord(c) - ord('a')] = True
			i += 1
			continue
		if i == len(s):
			current = ' '
			isNewChar = True
		else:
			current = s[i]
			isNewChar = not chars[ord(current) - ord('a')]
		if isNewChar:
			infos.append(Info(c, length))
			length = 1
			c = current
			if i != len(s):
				chars[ord(current) - ord('a')] = True
		else:
			length += 1
		i += 1
	return infos

def IsNoAnswer(s, k):
	if k == 0:
		return True
	chars = [False] * 26
	for c in s:
		chars[ord(c) - ord('a')] = True
	isNoAnswer = sum(1 for x in chars if x) < k;
	return isNoAnswer

def IsZero(s, k):
	chars = [False] * 26
	for c in s:
		chars[ord(c) - ord('a')] = True
	isZero = sum(1 for x in chars if x) == k;
	return isZero

class Info(object):
	def get_C(self):

		C = property(fget=get_C)

	def get_Length(self):

		Length = property(fget=get_Length)

	def __init__(self, c, length):
		self.C = c
		self.Length = length
