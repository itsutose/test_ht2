using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System.IO;
using UnityEditor;
using System.Linq;


public class ONNX_multi_classification : MonoBehaviour
{

    [SerializeField] TextAsset CSVsd_mean;// CSVファイル
    [SerializeField] TextAsset CSVsd_scale;// CSVファイル
    [SerializeField] TextAsset CSVminmax_data_min;// CSVファイル
    [SerializeField] TextAsset CSVminmax_scale;// CSVファイル

    public NNModel DLClass;
    public string PreProccessing = "n";

    //public ONNXModel model; これはエラー
    private Model model;

    //private ComputeBuffer computeBuffer;
    private Model m_RuntimeModel;
    private IWorker worker;
    private int max_i=-1;
    private float max = -1;

    private Tensor input;

    private float[] sd_mean;
    private float[] sd_scale;
    private float[] minmax_data_min;
    private float[] minmax_scale;

    //public RenderTexture targetTexture;
    // Start is called before the first frame update
    void Start()
    {

        string[] lines1 = CSVsd_mean.text.Split('\n');
        string[] lines2 = CSVsd_scale.text.Split('\n');
        string[] lines3 = CSVminmax_data_min.text.Split('\n');
        string[] lines4 = CSVminmax_scale.text.Split('\n');

        //string sd_mean_path = "Assets/script_files/MLmodels/multi_classification/sd_mean.csv";
        //string[] lines1 = File.ReadAllLines(sd_mean_path);

        //Debug.Log("onnx multi classification  line1  " + string.Join(",", lines));

        sd_mean = new float[12]{ 
            float.Parse(lines1[0]),
            float.Parse(lines1[1]),
            float.Parse(lines1[2]),
            float.Parse(lines1[3]),
            float.Parse(lines1[4]),
            float.Parse(lines1[5]),
            float.Parse(lines1[6]),
            float.Parse(lines1[7]),
            float.Parse(lines1[8]),
            float.Parse(lines1[9]),
            float.Parse(lines1[10]),
            float.Parse(lines1[11])
        };

        //string sd_scale_path = "Assets/script_files/MLmodels/multi_classification/sd_scale.csv";
        //string[] lines2 = File.ReadAllLines(sd_scale_path);

        sd_scale = new float[12]{
            float.Parse(lines2[0]),
            float.Parse(lines2[1]),
            float.Parse(lines2[2]),
            float.Parse(lines2[3]),
            float.Parse(lines2[4]),
            float.Parse(lines2[5]),
            float.Parse(lines2[6]),
            float.Parse(lines2[7]),
            float.Parse(lines2[8]),
            float.Parse(lines2[9]),
            float.Parse(lines2[10]),
            float.Parse(lines2[11])
        };


        minmax_data_min = new float[12]{
            float.Parse(lines3[0]),
            float.Parse(lines3[1]),
            float.Parse(lines3[2]),
            float.Parse(lines3[3]),
            float.Parse(lines3[4]),
            float.Parse(lines3[5]),
            float.Parse(lines3[6]),
            float.Parse(lines3[7]),
            float.Parse(lines3[8]),
            float.Parse(lines3[9]),
            float.Parse(lines3[10]),
            float.Parse(lines3[11])
        };

        minmax_scale = new float[12]{
            float.Parse(lines4[0]),
            float.Parse(lines4[1]),
            float.Parse(lines4[2]),
            float.Parse(lines4[3]),
            float.Parse(lines4[4]),
            float.Parse(lines4[5]),
            float.Parse(lines4[6]),
            float.Parse(lines4[7]),
            float.Parse(lines4[8]),
            float.Parse(lines4[9]),
            float.Parse(lines4[10]),
            float.Parse(lines4[11])
        };



        model = ModelLoader.Load(DLClass);

        var workerType = WorkerFactory.Type.Compute;
        // モデルを実行するワーカーを作成
        worker = WorkerFactory.CreateWorker(workerType, model);
    }

    void OnDestroy()
    {
        input.Dispose();
        worker.Dispose();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public int ModelPredict(float ux, float ux1, float ux2, float ux3, float ux4, float ux5,
        float uy, float uy1,float uy2, float uy3, float uy4, float uy5)
    {

        float[] us = new float[12] { ux, ux1, ux2, ux3, ux4, ux5, uy, uy1, uy2, uy3, uy4, uy5 };

        input = new Tensor(n: 1, h: 1, w: 1, c: 12);

        for (int i = 0; i < 12; i++)
        {
            if (PreProccessing == "sd")
            {
                input[0, 0, 0, i] = (us[i] - sd_mean[i]) / sd_scale[i];

            }
            else if (PreProccessing == "mm")
            {
                input[0, 0, 0, i] = (us[i] - minmax_data_min[i]) / minmax_scale[i];
            }
            else if (PreProccessing == "mmsd")
            {
                input[0, 0, 0, i] = ((us[i] - minmax_data_min[i]) / minmax_scale[i]) - sd_mean[i] / sd_scale[i];
            }
            else
            {
                input[0, 0, 0, i] = us[i];
            }
        }
        
        //Debug.Log(string.Format("ONNX model   ux  {0}, uy  {1}, ux1  {2}, uy1  {3}, ux2  {4}, uy2  {5}, ux3  {6}, uy3  {7}, ux4  {8}, uy4  {9}, ux5  {10}, uy5  {11}", ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5));


        // input the uxuys to model
        worker.Execute(input);

        Tensor output2 = worker.PeekOutput();

        max = -1;
        max_i = -1;

        // use output here
        for (int i = 0; i < 15; i++)
        {
            if (max < output2[0, 0, 0, i])
            {
                max = output2[0, 0, 0, i];
                max_i = i;
            }
        }

        //Debug.Log(string.Format("ONNX model  {0}   {1}   {2}",output2[0,0,0,0] , output2[0, 0, 0, 1], output2[0, 0, 0, 2]));

        input.Dispose();
        output2.Dispose();


        return max_i;
    }

}