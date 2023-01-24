using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Unity.Barracuda;


// Define a class to hold the data to be transformed
public class InputData
{
    [ColumnName("Feature1")]
    public float Feature1 { get; set; }
    [ColumnName("Feature2")]
    public float Feature2 { get; set; }
    [ColumnName("Feature3")]
    public float Feature3 { get; set; }
    [ColumnName("Feature4")]
    public float Feature4 { get; set; }
    [ColumnName("Feature5")]
    public float Feature5 { get; set; }
    [ColumnName("Feature6")]
    public float Feature6 { get; set; }
    [ColumnName("Feature7")]
    public float Feature7 { get; set; }
    [ColumnName("Feature8")]
    public float Feature8 { get; set; }
    [ColumnName("Feature9")]
    public float Feature9 { get; set; }
    [ColumnName("Feature10")]
    public float Feature10 { get; set; }
    [ColumnName("Feature11")]
    public float Feature11 { get; set; }
    [ColumnName("Feature12")]
    public float Feature12 { get; set; }
}

// Define a class to hold the transformed data
public class OutputData
{
    public float Feature1 { get; set; }
    public float Feature2 { get; set; }
    public float Feature3 { get; set; }
    public float Feature4 { get; set; }
    public float Feature5 { get; set; }
    public float Feature6 { get; set; }
    public float Feature7 { get; set; }
    public float Feature8 { get; set; }
    public float Feature9 { get; set; }
    public float Feature10 { get; set; }
    public float Feature11 { get; set; }
    public float Feature12 { get; set; }
}

public class PKLloader : MonoBehaviour
{
    private ITransformer _model;
    private MLContext _context;

    public PKLloader()
    {
        // Load the saved model
        _model = _context.Model.Load("path/to/model.pkl", out _);

        // Create a new MLContext
        _context = new MLContext();
    }

    //public OutputData Transform(InputData inputData)
    //{
    //    // Convert the InputData instance to IDataView
    //    var input = _context.Data.LoadFromEnumerable(new List<InputData>() { inputData });

    //    // Use the model to transform new data
    //    var transformedData = _model.Transform(input);

    //    // Extract the transformed data as an OutputData instance
    //    var output = _context.Data.CreateEnumerable<OutputData>(transformedData, reuseRowObject: false).First();

    //    return output;
    //}
}

//void Start()
//{
//    // Load the saved model
//    //_model = TransformerChain.Load("path/to/model.pkl");
//    _model = _context.Model.Load("Assets/script_files/MLmodels/standard_scaler.pkl", out _);

//    // Create a new MLContext
//    _context = new MLContext();

//}

//    public float[] Transform(float[] inputData)
//    {
//        // Create an instance of InputData
//        InputData inputRow = new InputData();
//        inputRow.Feature1 = inputData[0];
//        inputRow.Feature2 = inputData[1];
//        inputRow.Feature3 = inputData[2];
//        inputRow.Feature4 = inputData[3];
//        inputRow.Feature5 = inputData[4];
//        inputRow.Feature6 = inputData[5];
//        inputRow.Feature7 = inputData[6];
//        inputRow.Feature8 = inputData[7];
//        inputRow.Feature9 = inputData[8];
//        inputRow.Feature10 = inputData[9];
//        inputRow.Feature11 = inputData[10];
//        inputRow.Feature12 = inputData[11];

//        // Convert the InputData instance to IDataView
//        var input = _context.Data.LoadFromEnumerable(new List<InputData>() { inputRow });

//        // Use the model to transform new data
//        var transformedData = _model.Transform(input);

//        // Extract the transformed data as an array of floats
//        //var output = _context.Data.CreateEnumerable<float>(transformedData, reuseRowObject: false).ToArray();
//        var output = _context.Data.CreateEnumerable<float?>(transformedData, reuseRowObject: false).Select(x => (float)x.Value).ToArray();

//        return output;
//    }

//}
