using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Baidu.Aip.Ocr;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using System.Text.RegularExpressions;
using System;

public class TextDetect : MonoBehaviour
{
    public string app_id;
    public string api_key;
    public string secret_key;

    private string deviceName;
    private WebCamTexture webTex;

    //AI返回的结果数据
    public Text resultMsg;
    //提取其中的文字
    //public Text detectedTextMsg;

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
        api_key = "QusDMkImWclCNsweUR7KXjvm";
        secret_key = "vFxAWvY7yjDc7GcYCDQFL2p3B00Tofja";
        StartCoroutine(CallCamera());
        var client = new Ocr(api_key, secret_key);
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
            //摄像机摄像的区域    
            webTex = new WebCamTexture(deviceName, 1024, 768, 20);
            webTex.Play();//开始摄像    
            transform.GetComponent<RawImage>().texture = webTex;
        }
    }


    public float timer = 0;
    //截屏
   public void CaptureScreen()
    {
        File.Delete(Application.streamingAssetsPath + "/capture.jpg");
        CapturePhoto();
        //timer += Time.deltaTime;
        ////每隔2秒检测一次
        //if (timer > 5)
        //{
        //    //删除上一次检测的图片
        //    File.Delete(Application.streamingAssetsPath + "/capture.jpg");
        //    CapturePhoto();
        //    timer = 0;
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
        fileName = Application.streamingAssetsPath + "/capture.jpg";
        //byte[] bytes = screenShot.EncodeToJPG();

        ScaleTextureCutOut(screenShot, 0, 0, 1024, 768);
        Debug.Log(string.Format("截屏一张照片: {0}", fileName));
        var image = File.ReadAllBytes(fileName);

        var client = new Ocr(api_key, secret_key);
        //client = new Body(api_key, secret_key);
        client.Timeout = 60000;  // 修超时时间

        var result = client.GeneralBasic(image);
        resultMsg.text = result.ToString();
        Debug.Log(resultMsg.text);
        Thread.Sleep(5000);
    }

    //切图
    byte[] ScaleTextureCutOut(Texture2D originalTexture, int pos_x, int pos_y, float originalWidth, float originalHeight)
    {
        Color[] pixels = new Color[(int)(originalWidth * originalHeight)];
        //要返回的新图
        Texture2D newTexture = new Texture2D(Mathf.CeilToInt(originalWidth), Mathf.CeilToInt(originalHeight));
        //批量获取点像素
        pixels = originalTexture.GetPixels(pos_x, pos_y, (int)originalWidth, (int)originalHeight);
        newTexture.SetPixels(pixels);
        newTexture.anisoLevel = 2;
        newTexture.Apply();
        //存裁剪的新图片
        byte[] jpgData = newTexture.EncodeToJPG();
        System.IO.File.WriteAllBytes(fileName, jpgData);

        return jpgData;
    }

    private void OnGUI()
    {
        string chinese = null;
        //string chinese = Regex.Replace(resultMsg.text, "[a-z]", "", RegexOptions.IgnoreCase);
        //chinese = Regex.Replace(chinese, "[0-9]", "", RegexOptions.IgnoreCase);
        //string chinese = Regex.Match(resultMsg.text,"[\u4e00-\u9fa5]");

        Regex reg = new Regex("[\u4e00-\u9fa5]");
        Regex RegNumberSign = new Regex(@"^[+-]?[0-9]+$");

        //foreach (Match v in reg.Matches(resultMsg.text))
        //    chinese = v.ToString();
        //for (int i = 2; i < chinese.Length;i++)
        //{
        //    string c = resultMsg.text.Substring(i-1, i);
        //    if (Regex.IsMatch(c, "[\u4e00-\u9fbb]"))
        //    {
        //        chinese = chinese + c;
        //    }
        //}

        int j = 0;
        for (int i = 0; i < resultMsg.text.Length; i++)
        {
            if (resultMsg.text[i].ToString() == " ")
            {
                j = j + 1;
                if (j < 2)
                {
                    chinese += " ";
                }
            }
            else if (reg.IsMatch(resultMsg.text[i].ToString()) || RegNumberSign.IsMatch(resultMsg.text[i].ToString()))
            {
                chinese += resultMsg.text[i].ToString();
            }
        }
    //chinese = chinese.Trim();
    chinese = chinese.Remove(0, 21);
        //chinese = Regex.Replace(chinese.Trim(), " {2}", "\n");
        GUI.TextArea(new Rect(3, 75, 200, 100), chinese);
    }
}

