using System.Collections;
using UnityEngine;
using System.Threading;
using Baidu.Aip.Ocr;
using System.IO;
using Baidu.Aip.ImageClassify;
using UnityEngine.UI;

public class TextDetectV3 : MonoBehaviour
{
    //Ocr client;
    public string api_key = "QusDMkImWclCNsweUR7KXjvm";
    public string secret_key = "vFxAWvY7yjDc7GcYCDQFL2p3B00Tofja";
    public string result_string;
    private string deviceName;
    public GUISkin MyGUISkin2;
    private WebCamTexture webTex;

    public static string result1;
    public static string result2;
    public static string result3;
    public static string result_final;


    void Start()
    {
        var client = new Ocr(api_key, secret_key);
        client.Timeout = 60000;
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
    public void CaptureScreen()
    {
        File.Delete(Application.streamingAssetsPath + "/Textcapture.jpg");
        CapturePhoto();
        Find_data();
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
        fileName = Application.streamingAssetsPath + "/Textcapture.jpg";
        //byte[] bytes = screenShot.EncodeToJPG();

        ScaleTextureCutOut(screenShot, 0, 0, 1024, 768);
        Debug.Log(string.Format("截屏一张照片: {0}", fileName));
        var image = File.ReadAllBytes(fileName);
        //        var client = new Ocr(api_key, secret_key);
        var client = new Ocr(api_key, secret_key);
        //client = new Body(api_key, secret_key);
        client.Timeout = 60000;  // 修改超时时间

        //var result = client.AdvancedGeneral(image);
        //resultMsg.text = result.ToString();
        //Debug.Log(resultMsg.text);
        //var image = File.ReadAllBytes(fileName);
        var result = client.GeneralBasic(image);
        var a = result["words_result"][0]["words"];
        result_string = a.ToString();
        //Console.WriteLine(a);
        //Console.ReadLine();
        Thread.Sleep(5000);
    }

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

    void Find_data()
    {
        if (string.Equals(result_string, "00709"))
        {
            result1 = "车型：棚车";
            result2 = "始发站：北京";
            result3 = "终点站：杭州";
        }

        if (string.Equals(result_string, "4926858"))
        {
            result1 = "车型：棚车";
            result2 = "始发站：北京";
            result3 = "终点站：杭州";
        }

        if (string.Equals(result_string, "0872857"))
        {
            result1 = "车型：罐车";
            result2 = "始发站：北京";
            result3 = "终点站：新疆";
        }
    }

    void OnGUI()
    {
        GUI.skin = MyGUISkin2;
        result_final = result_string + "\n" + result1 + "\n" + result2 + "\n" + result3;
        GUI.TextArea(new Rect(3, 80, 150, 80), result_final,"TextArea");
    }

}
