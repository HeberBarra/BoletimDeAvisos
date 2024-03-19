using System.Text.Json;

List<DayOfWeek> allowed_days_of_week = new List<DayOfWeek>(){DayOfWeek.Monday, DayOfWeek.Friday};
List<Reminder> reminders = new List<Reminder>();

bool ShouldSendReminders() 
{
    return allowed_days_of_week.Contains(DateTime.Today.DayOfWeek);
}

void SaveToJsonFile(List<Reminder> reminders)
{
    string jsonData = JsonSerializer.Serialize(reminders);
    File.WriteAllText("./reminders.json", jsonData);
}

string TreatedStringInput(string message, bool checkIfRight=true)
{
    while(true)
    {
        string? isRight = null;
        Console.Write(message);
        string? input = Console.ReadLine();

        Console.Write($"Is {input} right? [Y/n]\n>");

        if (checkIfRight)
        {
            isRight = Console.ReadLine();
        }

        if (isRight != null && isRight.ToLower().Contains("n"))
        {
            continue;
        }

        if (input == null) {
            return "";
        }

        return input;
    }
}

DateTime TreatedDateInput()
{
    while (true) 
    {
        int year = Int32.Parse(TreatedStringInput("What's the year? \n>"));
        int month = Int32.Parse(TreatedStringInput("What's the month(number)?\n>"));
        int day = Int32.Parse(TreatedStringInput("What's the day?\n>"));

        try 
        {
            return new DateTime(year, month, day);
        } 
        catch (ArgumentOutOfRangeException) {
            Console.WriteLine("Invalid date! Please try again... ");
            continue;
        }
    }
}

List<Reminder> CreateNewReminders()
{
    List<Reminder> reminders = new List<Reminder>();
    
    string? addNext;
    while(true)
    {
        string name = TreatedStringInput("What's the reminder's name?\n>");
        string message = TreatedStringInput("What's the reminder's message?\n>");
        string type = TreatedStringInput("What's the reminder's type?\n>");
        DateTime date = TreatedDateInput();

        reminders.Add(new Reminder(name, message, type, date));

        addNext = TreatedStringInput("Add another reminder? [y/N]\n>", false);

        if (!addNext.ToLower().Contains("y"))
        {
            break;
        }

    }
    

    return reminders;
}

//Console.WriteLine(CreateNewReminders()[0]);
Console.WriteLine(ReadJson.ReadFile("test.json", new List<Reminder>()));

