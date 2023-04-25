using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAUXRDataManager : TAUXRSingleton<TAUXRDataManager>
{
    // updated from TAUXRPlayer
    bool ExportEyeTracking = false;
    bool ExportFaceTracking = false;

    // automatically switched to true if not in editor.
    public bool ShouldExport = false;


    DataEventWriter eventWriter;
    DataContinuousWriter continuousWriter;
    DataExporterFaceExpression faceExpressionWriter;


    #region Events
    // declare pointers for all experience-specific event classes
    public WriteNoteDataEvent WriteNoteDataEvent;
    // write additional events here..


    #endregion

    void Start()
    {
        Init();
    }

    private void Init()
    {
        ShouldExport = ShouldExportData();
        if (!ShouldExport) return;

        ExportEyeTracking = TAUXRPlayer.Instance.IsEyeTrackingEnabled;
        ExportFaceTracking = TAUXRPlayer.Instance.IsFaceTrackingEnabled;

        eventWriter = new DataEventWriter();
        ConstructEvents();

        // for now, instead of making the whole interface in the datamanager, it will split between the different scripts.
        continuousWriter = GetComponent<DataContinuousWriter>();
        continuousWriter.Init(ExportEyeTracking);

        if (ExportFaceTracking)
        {
            faceExpressionWriter = GetComponent<DataExporterFaceExpression>();
            faceExpressionWriter.Init();
        }
    }

    // default data export on false in editor. always export on build.
    private bool ShouldExportData()
    {
        if (!ShouldExport)
        {
            // if app runs on build- always export.
            if (!Application.isEditor)
            {
                return true;
            }
            else
            {
                Debug.Log("Data Manager won't export data because it is running in editor. To export, manually enable ShouldExport");
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    // build instances for all experience-specific event classes
    private void ConstructEvents()
    {
        WriteNoteDataEvent = new WriteNoteDataEvent();
    }

    void FixedUpdate()
    {
        if (!ShouldExport) return;

        continuousWriter.RecordContinuousData();

        if (ExportFaceTracking)
        {
            faceExpressionWriter.CollectWriteDataToFile();
        }

    }


    // called from specific events classes when triggered from everywhere in codei.e:
    // TAUXRDataManager.Instance.WriteNoteDataEvent.ReportNote("participant touched button") -> will call this function from the WriteNoteDataEvent class. 
    public void SendEvent(DataEvent dataEvent)
    {
        if (!ShouldExport) return;

        eventWriter.WriteDataEventToFile(dataEvent);
    }

    private void OnApplicationQuit()
    {
        if (!ShouldExport) return;

        eventWriter.Close();
        continuousWriter.Close();
        faceExpressionWriter.Close();
    }

}
