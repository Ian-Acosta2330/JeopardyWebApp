using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class ScoreService {
    private static string filePath = "scores.json";

    public static void SaveScore(string player, int score) {
        var scores = GetAllScores();
        scores.Add(new Score { Player = player, Value = score, Date = DateTime.Now });
        File.WriteAllText(filePath, JsonConvert.SerializeObject(scores));
    }

    public static List<Score> GetScores() {
        var scores = GetAllScores();
        return scores.OrderByDescending(s => s.Value)
                     .ThenBy(s => s.Date) // Optional: Break ties by date
                     .Take(10)
                     .ToList();
    }

    private static List<Score> GetAllScores() {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Score>>(json) ?? new List<Score>();
        }
        return new List<Score>();
    }
}
