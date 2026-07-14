using System.Text.Json;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>();
        var results = new List<string>();

        foreach (var word in words)
        {
            string reversed = $"{word[1]}{word[0]}";

            if (set.Contains(reversed) && word != reversed)
            {
                results.Add($"{word} & {reversed}");
            }
            else
            {
                set.Add(word);
            }
        }

        return results.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            
            if (fields.Length > 3)
            {
                string degree = fields[3];
                
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        var letterCounts = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (c == ' ') continue;
            
            char lowerC = char.ToLower(c);
            if (letterCounts.ContainsKey(lowerC))
                letterCounts[lowerC]++;
            else
                letterCounts[lowerC] = 1;
        }

        foreach (char c in word2)
        {
            if (c == ' ') continue;

            char lowerC = char.ToLower(c);
            if (letterCounts.ContainsKey(lowerC))
            {
                letterCounts[lowerC]--;
                if (letterCounts[lowerC] == 0)
                {
                    letterCounts.Remove(lowerC);
                }
            }
            else
            {
                return false;
            }
        }

        return letterCounts.Count == 0;
    }

    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                results.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
            }
        }

        return results.ToArray();
    }
}