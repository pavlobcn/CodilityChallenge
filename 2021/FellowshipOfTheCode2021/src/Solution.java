import java.util.ArrayList;
import java.util.LinkedList;

public class Solution {
    private static final int CHAR_COUNT = 26;
    private static final char MIN_CHAR = 'a';

    public String solution(String S, int K) {
        CharInfo[] chars = getChars(S);
        CharGroup[] charGroups = getCharGroups(chars);
        ArrayList<Character> minimumString = getMinimumString(S.length(), charGroups, K);
        return getString(minimumString);
    }

    private CharInfo[] getChars(String s) {
        var chars =  new CharInfo[s.length()];
        for (int i = 0; i < s.length(); i++) {
            chars[i] = new CharInfo(s.charAt(i), i);
        }
        return chars;
    }

    private CharGroup[] getCharGroups(CharInfo[] chars) {
        CharGroup[] charGroups = new CharGroup[CHAR_COUNT];
        for (int i = 0; i < charGroups.length; i++) {
            charGroups[i] = new CharGroup();
        }
        for (CharInfo charInfo : chars) {
            charGroups[charInfo.c - MIN_CHAR].positions.add(charInfo.position);
        }
        for (CharGroup charGroup : charGroups) {
            charGroup.initRequiredMovementCount();
        }
        return charGroups;
    }

    private ArrayList<Character> getMinimumString(int length, CharGroup[] charGroups, int k) {
        var charList = new ArrayList<Character>();
        int movementCount = 0;
        var usedPositions = new boolean[length];
        while (isNotEmpty(charGroups))
        {
            MinimumCharInfo minimumCharInfo = getMinimumCharInfo(charGroups, k - movementCount);
            movementCount += minimumCharInfo.requiredMovementCount;
            charList.add(minimumCharInfo.c);
            usedPositions[minimumCharInfo.position] = true;
            for (int i = 0; i < charGroups.length; i++) {
                if (i == minimumCharInfo.c - MIN_CHAR) {
                    charGroups[i].removeChar(usedPositions);
                }
                else {
                    charGroups[i].updateRequirementMovementCount(minimumCharInfo);
                }
            }
        }
        return charList;
    }

    private MinimumCharInfo getMinimumCharInfo(CharGroup[] charGroups, int allowedMovementCount) {
        for (int i = 0; i < charGroups.length; i++) {
            if (charGroups[i].positions.size() == 0) {
                continue;
            }
            CharGroup group = charGroups[i];
            int position = group.positions.getFirst();
            if (allowedMovementCount >= group.requiredMovementCount) {
                return new MinimumCharInfo((char) (MIN_CHAR + i), position, group.requiredMovementCount);
            }
        }
        throw new RuntimeException("Error in getMinimumCharInfo");
    }

    private boolean isNotEmpty(CharGroup[] charGroups) {
        for (CharGroup charGroup : charGroups) {
            if (charGroup.positions.size() > 0) {
                return true;
            }
        }
        return false;
    }

    private String getString(ArrayList<Character> charList)
    {
        var chars = new char[charList.size()];
        for (int i = 0; i < charList.size(); i++) {
            chars[i] = charList.get(i);
        }
        return new String(chars);
    }
}

class CharInfo {
    public char c;
    public int position;

    public CharInfo(char c, int position) {
        this.c = c;
        this.position = position;
    }
}

class CharGroup {
    public LinkedList<Integer> positions = new LinkedList<>();
    public int requiredMovementCount;

    public void initRequiredMovementCount()
    {
        if (positions.size() == 0) {
            return;
        }

        requiredMovementCount = positions.getFirst();
    }

    public void removeChar(boolean[] usedPositions) {
        int previousPosition = positions.removeFirst();
        if (positions.size() == 0) {
            return;
        }

        int position = positions.getFirst();
        for (int i = previousPosition; i < position; i++) {
            if (usedPositions[i]) {
                requiredMovementCount--;
            }
        }

        requiredMovementCount += position - previousPosition;
    }

    public void updateRequirementMovementCount(MinimumCharInfo minimumCharInfo) {
        if (positions.size() == 0) {
            return;
        }

        if (minimumCharInfo.position < positions.getFirst())
        {
            requiredMovementCount--;
        }
    }
}

class MinimumCharInfo {
    public char c;
    public int position;
    public int requiredMovementCount;

    public MinimumCharInfo(char c, int position, int requiredMovementCount) {
        this.c = c;
        this.position = position;
        this.requiredMovementCount = requiredMovementCount;
    }
}
