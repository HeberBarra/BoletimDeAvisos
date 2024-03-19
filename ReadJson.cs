using System.Text.Json;

public class ReadJson
{

    public static List<T> ReadFile<T>(string filepath, List<T> informationList)
    {
        List<T>? data = new List<T>();
        
        using (StreamReader streamReader = new StreamReader(filepath))
        {
            string jsonData = streamReader.ReadToEnd();
            data = JsonSerializer.Deserialize<List<T>>(jsonData);
        }

        if (data == null)
        {
            return informationList;
        }

        informationList.AddRange(data);
        return informationList;
    }

}
