using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// write event data to separate csv files for each event type. Used in TAUXRDataManager.
public class DataEventWriter
{
    private Dictionary<string, StreamWriter> csvFiles = new Dictionary<string, StreamWriter>();
    private string csvFolder;

    public DataEventWriter()
    {
        // Set the CSV folder path
        csvFolder = Path.Combine(Application.persistentDataPath, "Events_" + TAUXRFunctions.GetFormattedDateTime(true));

        // Create the folder for the CSV files if it doesn't exist
        if (!Directory.Exists(csvFolder))
        {
            Directory.CreateDirectory(csvFolder);
            Debug.Log($"Created a new data folder in: {csvFolder}");
        }

        // Set permissions for the CSV folder
        try
        {
            string filePath = Path.Combine(csvFolder, "permission_test.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("test");
            }
            File.Delete(filePath);
        }
        catch (IOException ex)
        {
            Debug.LogError("Error setting permissions for CSV folder: " + ex.Message);
        }
    }

    public void WriteDataEventToFile(DataEvent dataEvent)
    {
        // Check if a CSV file already exists for this event name
        string eventName = dataEvent.EventName;
        if (!csvFiles.ContainsKey(eventName))
        {
            CreateNewDataFile(dataEvent);
        }

        WriteLineInFile(dataEvent);
    }

    private void CreateNewDataFile(DataEvent dataEvent)
    {
        string eventName = dataEvent.EventName;

        // Create a new CSV file for this event name and add the field keys to the first line
        string csvFilePath = Path.Combine(csvFolder, eventName + $"_{TAUXRFunctions.GetFormattedDateTime(false)}.csv");

        StreamWriter writer = new StreamWriter(csvFilePath, true);
        csvFiles[eventName] = writer;

        string[] fieldKeys = dataEvent.Fields.Keys.ToArray();
        string fieldLine = string.Join(",", fieldKeys);

        writer.WriteLine(fieldLine);
        Debug.Log($"Line Added to {eventName}: {fieldLine}");
    }

    private void WriteLineInFile(DataEvent dataEvent)
    {
        string eventName = dataEvent.EventName;

        // Add the field values to the corresponding CSV file
        string[] fieldValues = dataEvent.Fields.Values.ToArray();
        string fieldLineValues = string.Join(",", fieldValues);
        csvFiles[eventName].WriteLine(fieldLineValues);
        Debug.Log($"Line Added to {eventName}: {fieldLineValues}");

        // flush every time to make sure the data is saved on the file.
        csvFiles[eventName].Flush();
    }

    public void Close()
    {
        // Close all the CSV files
        foreach (StreamWriter writer in csvFiles.Values)
        {
            writer.Close();
        }
    }
}