class Reminder
{
    private string name;
    private string message;
    private string type;
    private DateTime date;

    public string Name 
    { 
        get { return name; } 
        set { name = value; }
    }

    public string Message 
    { 
        get { return message; } 
        set { message = value; } 
    }

    public String Type
    {
        get { return type; }
        set { type = value; }
    }

    public DateTime Date 
    { 
        get { return date; } 
        set { date = value; } 
    }

    public Reminder(string name, string message, string type, DateTime date) 
    {
       this.name = name;
       this.message = message;
       this.date = date;
       this.type = type;
    }

    public string FormatReminder()
    {
        return $"{name} - {date.ToShortDateString()}\n{message}";
    }

}

