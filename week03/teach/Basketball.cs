/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
                players[playerId] += points;
            else
                players[playerId] = points;
        }

        // Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");

        var top10Heap = new PriorityQueue<string, int>();

        foreach (var player in players)
        {
            top10Heap.Enqueue(player.Key, player.Value);

            if (top10Heap.Count > 10)
            {
                top10Heap.Dequeue();
            }
        }

        var reverseOrder = new Stack<(string Key, int Value)>();

        while (top10Heap.Count > 0)
        {
            top10Heap.TryDequeue(out string key, out int value);
            reverseOrder.Push((key, value));
        }

        foreach (var (key, value) in reverseOrder)
        {
            Console.WriteLine($"{key}: {value}");
        }
    }
}