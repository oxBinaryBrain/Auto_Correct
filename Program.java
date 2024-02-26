
import java.util.HashMap;
import java.util.Map;

public class Autocorrect {

    private interface Dictionary {
        String getClosestMatch(String input);
    }

    private static class LevenshteinDictionary implements Dictionary {
        private final Map<String, String> dictionary;

        public LevenshteinDictionary(Map<String, String> dictionary) {
            this.dictionary = dictionary;
        }

        @Override
        public String getClosestMatch(String input) {
            String closestMatch = "";
            int minDistance = Integer.MAX_VALUE;

            for (String word : dictionary.keySet()) {
                int distance = calculateLevenshteinDistance(input.toLowerCase(), word);
                if (distance < minDistance) {
                    minDistance = distance;
                    closestMatch = word;
                }
            }
            return closestMatch;
        }

        private int calculateLevenshteinDistance(String s1, String s2) {
            int[][] dp = new int[s1.length() + 1][s2.length() + 1];

            for (int i = 0; i <= s1.length(); i++) {
                dp[i][0] = i;
            }

            for (int j = 0; j <= s2.length(); j++) {
                dp[0][j] = j;
            }

            for (int i = 1; i <= s1.length(); i++) {
                for (int j = 1; j <= s2.length(); j++) {
                    int cost = (s1.charAt(i - 1) == s2.charAt(j - 1)) ? 0 : 1;
                    dp[i][j] = Math.min(Math.min(dp[i - 1][j] + 1, dp[i][j - 1] + 1), dp[i - 1][j - 1] + cost);
                }
            }

            return dp[s1.length()][s2.length()];
        }
    }
