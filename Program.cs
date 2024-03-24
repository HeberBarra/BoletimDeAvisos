using System.Text.Json;

const string REMINDERS_FILEPATH = "reminders.json";
List<DayOfWeek> allowed_days_of_week = new List<DayOfWeek>(){DayOfWeek.Monday, DayOfWeek.Friday};

bool ShouldSendReminders() 
{
    return allowed_days_of_week.Contains(DateTime.Today.DayOfWeek);
}

void SaveToJsonFile(List<Reminder> reminders, string filepath)
{
    string jsonData = JsonSerializer.Serialize(reminders);
    File.WriteAllText($"./{filepath}", jsonData);
}

string TreatedStringInput(string message, bool checkIfRight=true)
{
    while(true)
    {
        string? isRight = null;
        Console.Write(message);
        string? input = Console.ReadLine();

        if (checkIfRight)
        {
            Console.Write($"Is {input} right? [Y/n]\n>");
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

int TreatedIntInput(string message, bool checkIfRight=true)
{
    while(true) {
        try 
        {
            return Int32.Parse(TreatedStringInput(message, checkIfRight));
        } 
        catch (System.FormatException)
        {
            Console.WriteLine("Invalid number");
        }
    }
}

DateTime TreatedDateInput()
{
    while (true) 
    {
        int year = TreatedIntInput("What's the year\n>");
        int month = TreatedIntInput("What's the month(number)?\n>");
        int day = TreatedIntInput("What's the day?\n>");

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

void Main()
{
    while (true)
    {
        List<Reminder> reminders = ReadJson.ReadFile<Reminder>(REMINDERS_FILEPATH);
        string opcoes = "[ 0 ] Show reminders \n[ 1 ] Create new reminder(s) \n[ 2 ] Delete reminder \n[ 3 ] Edit reminder \n[ 4 ] Save reminders \n[5] Send reminders \n[ 6 ] Exit";
        Console.WriteLine(opcoes);
        int chosenOption = TreatedIntInput("> ", false);

        switch (chosenOption)
        {
            case 0:
                foreach (Reminder reminder in reminders)
                {
                    Console.WriteLine(reminder.FormatReminder());
                }
                break;

            case 1:
                reminders.AddRange(CreateNewReminders());
                break;

            case 2:
                // TODO: functionality to Delete
                break;

            case 3:
                // TODO: functionality to Edit
                break;

            case 4:
                SaveToJsonFile(reminders, REMINDERS_FILEPATH);
                break;

            case 5:
                // TODO: functionality send through email
                break;

            case 6:
                Console.WriteLine("Terminating the program.");
                return;

            default:
                Console.WriteLine("Invalid option!");
                break;
        }
    }
}
 
Main();

