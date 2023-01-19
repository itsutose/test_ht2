using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;


public class ONNXLoader : MonoBehaviour
{

    public NNModel modelAssets;

    //public ONNXModel model; これはエラー
    private Model model;

    private Model m_RuntimeModel;
    private IWorker worker;

    //public RenderTexture targetTexture;
    // Start is called before the first frame update
    void Start()
    {

        model = ModelLoader.Load(modelAssets);


        // Workerを作成する
        //worker = WorkerFactory.CreateWorker(model);

        // 参考

        //// モデルをロード
        //runtimeModel = ModelLoader.Load(modelAsset);
        // GPU設定、CPUもでできる
        var workerType = WorkerFactory.Type.Compute;
        // モデルを実行するワーカーを作成
        worker = WorkerFactory.CreateWorker(workerType, model);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float[] ModelPredict(float ux, float uy, float ux1, float uy1,
        float ux2, float uy2, float ux3, float uy3,
        float ux4, float uy4, float ux5, float uy5)
    {

        float[] us = new float[12] { ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5 };

        Tensor input = new Tensor(n: 1, h: 1, w: 1, c:12);

        var inputTensor = new float[12]{ ux, uy, ux1, uy1, ux2, uy2, ux3, uy3, ux4, uy4, ux5, uy5 };


        for(int i=0;i<12;i++)
        {
            input[0, 0, 0, i] = us[i] * 100;
        }

        //var input = new Tensor(inputTensor, 1,12);
        //var output = model.Execute(input);

        var output1 = worker.Execute(input);

        Debug.Log(string.Format("1 {0}", output1));

        //var output1 = worker.PeekOutput();

        Tensor output2 = worker.PeekOutput();

        Debug.Log(string.Format("1.5 {0}", input));

        Debug.Log(string.Format("2 {0}", output2));

        //var input = Tensor.New(new[] { 1, 12 }, Tensor.DataType.Float);
        //input.SetData(inputTensor);
        //var output = model.Execute(input);
        //float[] outputData = output.ToReadOnlyArray();
        //Debug.Log(output.shape);
        //Debug.Log(string.Format("Predict ux {0}, uy {1}",))

        Debug.Log(string.Format("3 {0}  {1}", output2[0, 0, 0, 0], output2[0, 0, 0, 1]));

        return new float[] { output2[0,0,0,0]/100 , output2[0,0,0,1]/100 };
    }
}