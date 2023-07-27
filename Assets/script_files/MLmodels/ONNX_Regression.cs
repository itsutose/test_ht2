using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System;


public class ONNX_Regression : MonoBehaviour
{

    [SerializeField] TextAsset CSV_sd_mean;// CSV�t�@�C��
    [SerializeField] TextAsset CSV_sd_scale;// CSV�t�@�C��
    [SerializeField] TextAsset CSV_mm_data_min;// CSV�t�@�C��
    [SerializeField] TextAsset CSV_mm_scale;// CSV�t�@�C��
    [SerializeField] TextAsset CSV_mmsd_mean;// CSV�t�@�C��
    [SerializeField] TextAsset CSV_mmsd_scale;// CSV�t�@�C��

    public NNModel DLRegression;

    public string PreProccessing = "n";
    public Boolean TwoInput = false;
    public int norm;

    private Model model;

    private Model m_RuntimeModel;
    private IWorker worker;
    private int max_i = -1;
    private float max = -1;

    private Tensor input;

    private float[] sd_mean;
    private float[] sd_scale;
    private float[] minmax_data_min;
    private float[] minmax_scale;
    private float[] mmsd_mean;
    private float[] mmsd_scale;


    //public RenderTexture targetTexture;
    // Start is called before the first frame update
    void Start()
    {

        string[] lines1 = CSV_sd_mean.text.Split('\n');
        string[] lines2 = CSV_sd_scale.text.Split('\n');
        string[] lines3 = CSV_mm_data_min.text.Split('\n');
        string[] lines4 = CSV_mm_scale.text.Split('\n');
        string[] lines5 = CSV_mmsd_mean.text.Split('\n');
        string[] lines6 = CSV_mmsd_scale.text.Split('\n');

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

        mmsd_mean = new float[12]{
            float.Parse(lines5[0]),
            float.Parse(lines5[1]),
            float.Parse(lines5[2]),
            float.Parse(lines5[3]),
            float.Parse(lines5[4]),
            float.Parse(lines5[5]),
            float.Parse(lines5[6]),
            float.Parse(lines5[7]),
            float.Parse(lines5[8]),
            float.Parse(lines5[9]),
            float.Parse(lines5[10]),
            float.Parse(lines5[11])
        };

        mmsd_scale = new float[12]{
            float.Parse(lines6[0]),
            float.Parse(lines6[1]),
            float.Parse(lines6[2]),
            float.Parse(lines6[3]),
            float.Parse(lines6[4]),
            float.Parse(lines6[5]),
            float.Parse(lines6[6]),
            float.Parse(lines6[7]),
            float.Parse(lines6[8]),
            float.Parse(lines6[9]),
            float.Parse(lines6[10]),
            float.Parse(lines6[11])
        };


        model = ModelLoader.Load(DLRegression);

        // �Q�l
        // GPU�ݒ�ACPU���łł���
        var workerType = WorkerFactory.Type.Compute;
        // ���f�������s���郏�[�J�[���쐬
        worker = WorkerFactory.CreateWorker(workerType, model);
    }

    void OnDestroy()
    {
        input.Dispose();
        worker.Dispose();
    }

    public float[] ModelPredict(float ux, float ux1, float ux2, float ux3, float ux4, float ux5,
        float uy, float uy1, float uy2, float uy3, float uy4, float uy5)
    {
        float[] us = new float[12] { ux, ux1, ux2, ux3, ux4, ux5, uy, uy1, uy2, uy3, uy4, uy5 };

        if (TwoInput == false)
        {
            Tensor input = new Tensor(n: 1, h: 1, w: 1, c: 12);

            for (int i = 0; i < 12; i++)
            {
                if (PreProccessing == "sd")
                {
                    input[0, 0, 0, i] = ((us[i] - sd_mean[i]) / sd_scale[i]) * norm;

                }
                else if (PreProccessing == "mm")
                {
                    input[0, 0, 0, i] = ((us[i] - minmax_data_min[i]) * minmax_scale[i]) * norm;
                }
                else if (PreProccessing == "mmsd")
                {
                    input[0, 0, 0, i] = (us[i] - mmsd_mean[i]) * mmsd_scale[i];
                }
                else
                {
                    input[0, 0, 0, i] = us[i];
                }
            }

            Debug.Log(string.Format("ONNX model   ux  {0}, uy  {1}, ux1  {2}, uy1  {3}, ux2  {4}, uy2  {5}, ux3  {6}, uy3  {7}, ux4  {8}, uy4  {9}, ux5  {10}, uy5  {11}", ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5));
            Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", ux, ux1, ux2, ux3, ux4, ux5, uy, uy1, uy2, uy3, uy4, uy5));
            Debug.Log(string.Format("ONNX model input 0:{0}  1:{1}  2:{2}  3:{3}  4:{4}  5:{5}" +
           "��:{6}  ��:{7}  ��:{8}  ��:{9}  point:{10}  space:{11}",
           input[0, 0, 0, 0], input[0, 0, 0, 1], input[0, 0, 0, 2],
           input[0, 0, 0, 3], input[0, 0, 0, 4], input[0, 0, 0, 5],
           input[0, 0, 0, 6], input[0, 0, 0, 7], input[0, 0, 0, 8],
           input[0, 0, 0, 9], input[0, 0, 0, 10], input[0, 0, 0, 11]));

            //var input = new Tensor(inputTensor, 1,12);
            //var output = model.Execute(input);

            var output1 = worker.Execute(input);

            //Debug.Log(string.Format("1 {0}", output1));

            //var output1 = worker.PeekOutput();

            Tensor output2 = worker.PeekOutput();

            float[] a = new float[2] { output2[0, 0, 0, 0], output2[0, 0, 0, 1] };
            //Debug.Log(string.Format("1.5 {0}", input));

            //Debug.Log(string.Format("2 {0}", output2));

            ////var input = Tensor.New(new[] { 1, 12 }, Tensor.DataType.Float);
            ////input.SetData(inputTensor);
            ////var output = model.Execute(input);
            ////float[] outputData = output.ToReadOnlyArray();
            ////Debug.Log(output.shape);
            ////Debug.Log(string.Format("Predict ux {0}, uy {1}",))

            //Debug.Log(string.Format("3 {0}  {1}", output2[0, 0, 0, 0], output2[0, 0, 0, 1]));


            input.Dispose();
            output2.Dispose();


            return new float[] { a[0] / (float)norm, a[1] / (float)norm };
        }
        else
        {
            input = new Tensor(n: 1, h: 1, w: 1, c: 2);

            for (int i = 0; i < 2; i++)
            {
                //if(i != 0 || i != 6)
                //{
                //    continue;
                //}

                if (PreProccessing == "sd")
                {
                    input[0, 0, 0, i] = ((us[i * 6] - sd_mean[i * 6]) / sd_scale[i * 6]) * norm;

                }
                else if (PreProccessing == "mm")
                {
                    input[0, 0, 0, i] = ((us[i * 6] - minmax_data_min[i * 6]) * minmax_scale[i * 6]) * norm;
                }
                else if (PreProccessing == "mmsd")
                {
                    input[0, 0, 0, i] = ((us[i * 6] - minmax_data_min[i * 6]) * minmax_scale[i * 6] - sd_mean[i * 6]) / sd_scale[i * 6];

                }
                else
                {
                    input[0, 0, 0, i] = us[i * 6];
                }
            }

            //// input the uxuys to model
            //worker.Execute(input);

            //Tensor output2 = worker.PeekOutput();


            var output1 = worker.Execute(input);

            Tensor output2 = worker.PeekOutput();

            float[] a = new float[2] { output2[0, 0, 0, 0]/1000f, output2[0, 0, 0, 1]/1000f };

            input.Dispose();
            output2.Dispose();


            return new float[] { a[0], a[1] };
        }
    }
}