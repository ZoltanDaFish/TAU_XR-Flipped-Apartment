using System.Collections.Generic;
using UnityEngine;

public class ExampleEvent : IEventReporter
{
    TAUXRDataManager dataManager = TAUXRDataManager.Instance;
    private DataEvent dataEvent;

    // declare event dependencies here.
    // i.e GameManager gameManager;

    // get all needed dependencies in the constructor, then assign them in the method
    public ExampleEvent()
    {
        // i.e this.gameManager = gameManager;

        SetupDataEvent();
    }

    // add fields for the dataEvent class for every information you want your event to report.
    public void SetupDataEvent()
    {
        dataEvent = new DataEvent("ExampleEvent");

        // add every desired field here: (the empty string value is because it has no importance now. We'll update it in the UpdateDataEvent function"
        //dataEvent.Fields.Add("PlayerPosition_X", "");
        dataEvent.Fields.Add("Time_Trial", "");
        dataEvent.Fields.Add("Pos_X", "");
        dataEvent.Fields.Add("Pos_Y", "");

    }

    // update every field value by getting information from the actual variable
    public void UpdateDataEvent()
    {
        //i.e: dataEvent.Fields["PlayerPosition_X"] = TAUXRPlayer.Instance.PlayerHead.position.x.ToString();
        dataEvent.Fields["TimeSinceLaunch"] = Time.time.ToString();
        dataEvent.Fields["Time_Trial"] = "7.475";
        dataEvent.Fields["Pos_X"] = "1.23";
        dataEvent.Fields["Pos_Y"] = "4.23";
    }

    // update data and send to TAUXRDataManager
    public void Report()
    {
        UpdateDataEvent();
        dataManager.SendEvent(dataEvent);
    }
}


public class WriteNoteDataEvent : IEventReporter
{
    TAUXRDataManager dataManager = TAUXRDataManager.Instance;
    private DataEvent dataEvent;

    public WriteNoteDataEvent()
    {
        SetupDataEvent();
    }

    public void SetupDataEvent()
    {
        dataEvent = new DataEvent("Notes");
        
        dataEvent.Fields.Add("Note", "");
    }

    public void UpdateDataEvent()
    {
        // no need for that because we have ReportNote.
    }

    public void ReportNote(string note)
    {
        dataEvent.Fields["TimeSinceLaunch"] = Time.time.ToString();
        dataEvent.Fields["Note"] = note;
        Report();
    }

    public void Report()
    {
        dataManager.SendEvent(dataEvent);
    }

}

public class DataEvent
{
    public string EventName => eventName;
    private readonly string eventName;

    public Dictionary<string, string> Fields => fields;
    private readonly Dictionary<string, string> fields;

    public DataEvent(string eventName)
    {
        this.eventName = eventName;
        fields = new Dictionary<string, string>();
        fields.Add("TimeSinceLaunch", Time.time.ToString());
    }
}
public interface IEventReporter
{
    // add all relevant fields to dataEvent.
    void SetupDataEvent();

    // update dataEvent to reflect current variables values serialized to strings.
    void UpdateDataEvent();

    // tell TAUXRDataManager to write our dataEvent to file.
    void Report();
}
