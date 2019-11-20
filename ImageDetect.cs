using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baidu.Aip.ImageClassify;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System;

public class ImageDetect : MonoBehaviour
{
    public string app_id;
    public string api_key;
    public string secret_key;

    private string deviceName;
    private WebCamTexture webTex;

    //AI返回的结果数据
    public Text resultMsg;

    void Awake()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
               delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                           System.Security.Cryptography.X509Certificates.X509Chain chain,
                           System.Net.Security.SslPolicyErrors sslPolicyErrors)
               {
                   return true; // **** Always accept
               };
    }

    // Use this for initialization
    void Start()
    {
        api_key = "kkt8bqP70q8K1ftYVGq5fsXo";
        secret_key = "4O0kk0wFi4GoqGaaYZoK3Ez0D4mLqlvO";
        StartCoroutine(CallCamera());
        var client = new ImageClassify(api_key, secret_key);
        //client = new Body(api_key, secret_key);
        client.Timeout = 60000;
    }

    // Update is called once per frame
    void Update()
    {
        //CaptureScreen();
    }

    IEnumerator CallCamera()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            //设置摄像机摄像的区域    
            webTex = new WebCamTexture(deviceName, 1024, 768, 20);
            webTex.Play();//开始摄像    
            transform.GetComponent<RawImage>().texture = webTex;
        }
    }


    public float timer = 0;
    //截屏
    public void CaptureScreen()
    {
        File.Delete(Application.streamingAssetsPath + "/Imagecapture.jpg");
        CapturePhoto();
        //timer += Time.deltaTime;
        //每隔两秒检测一次
        //if (timer > 2)
        //if(Input.GetMouseButtonDown(0))
        //{
        //    //删除上一次检测的图片
        //    File.Delete(Application.streamingAssetsPath + "/capture.jpg");
        //    CapturePhoto();
        //    //timer = 0;
        //}
    }
    public int width;
    public int height;
    //截图摄像头
    public Camera cameras;
    public string fileName;

    public void CapturePhoto()
    {
        Texture2D screenShot;
        RenderTexture rt = new RenderTexture(width, height, 1);
        cameras.targetTexture = rt;
        cameras.Render();
        RenderTexture.active = rt;
        screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        //在Asset路径下新建一个StreamingAsset文件夹
        fileName = Application.streamingAssetsPath + "/Imagecapture.jpg";
        //byte[] bytes = screenShot.EncodeToJPG();

        ScaleTextureCutOut(screenShot, 0, 0, 1024, 768);
        Debug.Log(string.Format("截屏一张照片: {0}", fileName));
        var image = File.ReadAllBytes(fileName);

        var client = new ImageClassify(api_key, secret_key);
        //client = new Body(api_key, secret_key);
        client.Timeout = 60000;  // 修改超时时间

        var result = client.AdvancedGeneral(image);
        var result_num = result["result_num"];
        int result_number = int.Parse(result_num.ToString());

        for(int i = 0; i <= result_number - 1; i++)
        {
            var result_key = result["result"][i]["keyword"];
            resultMsg.text = resultMsg.text + result_key.ToString() + "\n";
        }
        //var a = result["result"][0]["keyword"];
        //resultMsg.text = result.ToString();
        //Debug.Log(resultMsg.text);
        Thread.Sleep(5000);
    }

    //切图

    byte[] ScaleTextureCutOut(Texture2D originalTexture, int pos_x, int pos_y, float originalWidth, float originalHeight)
    {
        //Ocr client;
        Color[] pixels = new Color[(int)(originalWidth * originalHeight)];
        //要返回的新图
        Texture2D newTexture = new Texture2D(Mathf.CeilToInt(originalWidth), Mathf.CeilToInt(originalHeight));
        //批量获取点像素
        pixels = originalTexture.GetPixels(pos_x, pos_y, (int)originalWidth, (int)originalHeight);
        newTexture.SetPixels(pixels);
        newTexture.anisoLevel = 2;
        newTexture.Apply();
        //这一步把裁剪的新图片存下来
        byte[] jpgData = newTexture.EncodeToJPG();
        System.IO.File.WriteAllBytes(fileName, jpgData);
        //GestureDemo(fileName);
        return jpgData;
    }

    private void OnGUI()
    {
        //string chinese = null;
        //Regex reg = new Regex("[\u4e00-\u9fa5]");
 
        //for (int i = 0; i < resultMsg.text.Length; i++)
        //{
        //    if (resultMsg.text[i].ToString() == ",")
        //    {
        //        chinese += ",";
        //    }
        //    else if (reg.IsMatch(resultMsg.text[i].ToString()))
        //    {
        //        chinese += resultMsg.text[i].ToString();
        //    }
        //}
        //chinese = Regex.Replace(chinese, ",{2}", "\n");

        GUI.TextArea(new Rect(3, 75, 200, 100), resultMsg.text);
        //Debug.Log(resultMsg.text);
    }
}