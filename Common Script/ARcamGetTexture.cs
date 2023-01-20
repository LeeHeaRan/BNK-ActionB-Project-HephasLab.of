using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
using Google.XR.ARCoreExtensions;


public abstract class ARcamGetTexture : MonoBehaviour
{


    Texture2D outputTexture;
 
    Mat output;
    Mat output1;
    Mat output2;
    public Camera ARCam;
    public Camera SubCam;
    public Camera ObjectCam;
    protected int SizeCount = 0;

    int testcount = 0;
    public string ImageInfo;
    bool isStarted = false;
    public Vector2 CamOutputImageSize;
    public Vector2 reSize;
    float changeWaitTime = 0;
    bool isFirst = true;
    bool isConfingChangeSusece = false;
    bool isWait = true;
    [SerializeField]
    ARCameraManager m_CameraManager;

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager
    {
        get => m_CameraManager;
        set => m_CameraManager = value;
    }

    [SerializeField]
    GameObject m_RawCameraImage;



    [SerializeField]
    AROcclusionManager m_OcclusionManager;

    public ARSession arSession;

    public AROcclusionManager occlusionManager
    {
        get => m_OcclusionManager;
        set => m_OcclusionManager = value;
    }
    protected virtual void Start()
    {
        isFirst = true;
        isConfingChangeSusece = false;
    }

    void OnEnable()
    {
        if (m_CameraManager != null)
        {

            m_CameraManager.frameReceived += OnCameraFrameReceived;

        }
    }

    protected virtual void OnDisable()
    {
        if (m_CameraManager != null)
        {
            m_CameraManager.frameReceived -= OnCameraFrameReceived;
            SizeCount = 0;
            changeWaitTime = 0;
            isStarted = false; //?????????? ???????? ?????????? ???? ????????..
        }
    }
    unsafe void UpdateCameraImage()
    {

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {

            if (SizeCount == 1)
            {
                changeWaitTime += Time.deltaTime;
                if (changeWaitTime > 1.5f)
                {
                    SizeCount += 1;
                    InitSizeChagne();
                    arSession.Reset();
                    test.SetLog("time out Start initSizeChange");
                }
            }
            return;
        }
        ImageInfo = string.Format(
            "Image info:\n\twidth: {0}\n\theight: {1}\n\tplaneCount: {2}\n\ttimestamp: {3}\n\tformat: {4}",
            image.width, image.height, image.planeCount, image.timestamp, image.format);

        var format = TextureFormat.RGBA32;

        if (!isStarted)
        {
            arSession.enabled = false;
        }
       

        if (m_CameraTexture == null || m_CameraTexture.width != image.width || m_CameraTexture.height != image.height)
        {

            m_CameraTexture = new Texture2D(image.width, image.height, format, false);
            outputTexture = new Texture2D(image.width, image.height, format, false);

        }



        // Convert the image to format, flipping the image across the Y axis.
        // We can also get a sub rectangle, but we'll get the full image here.
        var conversionParams = new XRCpuImage.ConversionParams(image, format, XRCpuImage.Transformation.MirrorY);


        // Texture2D allows us write directly to the raw texture data
        // This allows us to do the conversion in-place without making any copies.
        var rawTextureData = m_CameraTexture.GetRawTextureData<byte>();
        try
        {
            image.Convert(conversionParams, new IntPtr(rawTextureData.GetUnsafePtr()), rawTextureData.Length);
        }
        finally
        {
            // We must dispose of the XRCpuImage after we're finished
            // with it to avoid leaking native resources.
            image.Dispose();
        }

        // Apply the updated texture data to our texture
        m_CameraTexture.Apply();

        // Set the RawImage's texture so we can visualize it.

        m_RawCameraImage.GetComponent<RawImage>().texture = m_CameraTexture;


        isStarted = true;
        //test.text += "\n update!!" +"\n";

    }
    void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {

        if (SizeCount == 0)
        {
            if (SubCam != null)
            {
                //test.SetLog("\n\n\n\n");
                //test.SetLog("SubCam.fieldOfView " + SubCam.fieldOfView + "\n");

                ARCam.fieldOfView = Mathf.Atan(1 / eventArgs.projectionMatrix.Value[5]) * 2f * Mathf.Rad2Deg;
                // SubCam.projectionMatrix = eventArgs.projectionMatrix.Value;
                SubCam.fieldOfView = ARCam.fieldOfView;
                ObjectCam.fieldOfView = ARCam.fieldOfView;
                //test.SetLog(SizeCount + "SubCam.fieldOfView " + SubCam.fieldOfView + "\n");

            }
            //test.SetLog("isConfingChangeSusece " + isConfingChangeSusece + "\n");

            if (isFirst)
            {
                SizeChange();
                isFirst = false;

            }
            else
            {
                if (isConfingChangeSusece)
                {
                    SizeChange();

                }
                else
                {
                    InitSizeChagne();

                }

            }

            SizeCount += 1;

        }
        UpdateCameraImage();


    }

    bool isSizeChangeCompleted = false;
    void SizeChange()
    {

        /*        var configurations = cameraManager.GetConfigurations(Allocator.Temp);
                SizeCount = configurations.Length;

                int count = 0;
                foreach (var config in configurations)
                {
                    if (config.width >= 1920)
                    {
                        Vector2 reSize = new Vector2(config.width, config.height);
                        if (Screen.height > reSize.x)
                        {
                            reSize = new Vector2(Screen.height, (Screen.height / reSize.x) * reSize.y);
                        }

                        if (Screen.width > reSize.y)
                        {
                            reSize = new Vector2((Screen.width / reSize.y) * reSize.x, Screen.width);
                        }
                        m_RawCameraImage.GetComponent<RectTransform>().sizeDelta = reSize;
                        m_SubRawCameraImage.GetComponent<RectTransform>().sizeDelta = reSize; //????????.

                        test.text += Screen.width + " " + Screen.height + " " + count + "\n";
                        test.text += reSize.x + " " + reSize.y + " " + count + "\n";
                        test.text += config.width + " " + config.height + " " + count + "\n";
                        cameraManager.currentConfiguration = configurations[count];
                        break;
                    }
                    count++;
                }*/

        isSizeChangeCompleted = false;
        using (NativeArray<XRCameraConfiguration> configurations = cameraManager.GetConfigurations(Allocator.Temp))
        {
            if (!configurations.IsCreated || (configurations.Length <= 0))
            {
                return;
            }

            // Iterate through the list of returned configs to locate the config you want.
            var desiredConfig = configurations[0];
            for (int i = 0; i < configurations.Length; ++i)
            {
                // Choose a config for a given camera that uses the maximum
                // target FPS and texture dimension. If supported, this config also enables
                // the depth sensor.
                if (configurations[i].width >= 1920) //ar ī�޶󿡼� �޾ƿ� ���ΰ� 1920�� ���.
                {

                    reSize = new Vector2(configurations[i].width, configurations[i].height); //1920*1080
                    CamOutputImageSize = reSize;

                    //test.SetLog("ScreenSize:" + Screen.height + "(H)" + Screen.width + "(W)");
                    //test.SetLog("reSize:" + reSize.x + "(X)" + reSize.y + "(Y)");
                    //Debug.Log("ScreenSize:" + Screen.height + "(H)" + Screen.width + "(W)");
                    //Debug.Log("reSize:" + reSize.x + "(X)" + reSize.y + "(Y)");

                    //���α���
                    if (Screen.height > reSize.x && !isSizeChangeCompleted) //?????? > ARConfig?????? ??????.
                    {
                        if(Screen.height % 16 != 0 || Screen.width % 9 != 0) //16:9�� �ƴ� ���.
                        {
                            //
                            if (Screen.width > reSize.y && !isSizeChangeCompleted)
                            {
                                reSize = new Vector2((Screen.width / reSize.y) * reSize.x, Screen.width);
                                Debug.Log("3");
                                isSizeChangeCompleted = true;

                            }

                            if (Screen.width < reSize.y && !isSizeChangeCompleted)
                            {
                                reSize = new Vector2((Screen.width / reSize.y) * reSize.x, Screen.width);
                                Debug.Log("4");
                                isSizeChangeCompleted = true;

                            }
                        }
                        else
                        {
                            reSize = new Vector2(Screen.height, (Screen.height / reSize.x) * reSize.y); //????*???????? ????
                            Debug.Log("1");
                            isSizeChangeCompleted = true;
                        }
                        
                    }

                    //������ ���� �ػ󵵰� ���� �͵�
                    if (Screen.height < reSize.x && !isSizeChangeCompleted)
                    {
                        reSize = new Vector2(Screen.height, (Screen.height / reSize.x) * reSize.y);
                        Debug.Log("2");
                        isSizeChangeCompleted = true;

                    }

                    m_RawCameraImage.GetComponent<RectTransform>().sizeDelta = reSize;
                    Debug.Log("���� �� reSize:" + reSize.x + "(X)" + reSize.y + "(Y)");

                    desiredConfig = configurations[i];
                    /* test.text += "\nconfigurations[i].GetFPSRange().y " + configurations[i].GetFPSRange().y + "\n";
                     test.text += "\ndesiredConfig.GetFPSRange().y  " + desiredConfig.GetFPSRange().y + "\n";
                     test.text += "\nconfigurations[i].GetTextureDimensions().x  " + configurations[i].GetTextureDimensions().x + "\n";
                     test.text += "\nconfigurations[i].GetTextureDimensions().y   " + configurations[i].GetTextureDimensions().y + "\n";
                     test.text += "\nconfigurations[i].GetDepthSensorUsage()   " + configurations[i].GetDepthSensorUsage() + " == " + CameraConfigDepthSensorUsage.DoNotUse + "\n";*/
                    break;
                }
                else
                {
                    //test.SetLog("\nConfig_Select\n");

                }

            }
            if (desiredConfig != cameraManager.currentConfiguration)
            {
                isConfingChangeSusece = true;

                cameraManager.currentConfiguration = desiredConfig;
                //test.SetLog("\ndesiredConfig " + desiredConfig.width + "\n");

            }
        }


    }
    void InitSizeChagne()
    {
        //test.SetLog("Start_initSzieChange");

        isConfingChangeSusece = false;

        // Use ARCameraManager to obtain the camera configurations.
        using (NativeArray<XRCameraConfiguration> configurations = cameraManager.GetConfigurations(Allocator.Temp))
        {
            var desiredConfig = configurations[0];

            reSize = new Vector2(desiredConfig.width, desiredConfig.height);
            CamOutputImageSize = reSize;
           

            if (Screen.height > reSize.x)
            {
                reSize = new Vector2(Screen.height, (Screen.height / reSize.x) * reSize.y);
            }

            if (Screen.width > reSize.y)
            {
                reSize = new Vector2((Screen.width / reSize.y) * reSize.x, Screen.width);
            }
            // Iterate through the list of returned configs to locate the config you want.
            m_RawCameraImage.GetComponent<RectTransform>().sizeDelta = reSize;
        
            //test.SetLog("initSzieChange");

            // Set the configuration you want. If it succeeds, the session
            // automatically pauses and resumes to apply the new configuration.
            // If it fails, cameraManager.currentConfiguration throws an exception.

            //     cameraManager.currentConfiguration = desiredConfig;

        }
    }
    protected abstract bool ProcessTexture(Texture2D input, ref Mat output);


    protected virtual void Update()
    {
        if (isStarted)
        {
             if(!arSession.enabled)
            {
                arSession.enabled = true;
            }
            if (m_CameraTexture != null)
            {
                try
                {
                    if (ProcessTexture(m_CameraTexture, ref output))
                    {
                        if (testcount == 0)
                        {

                            //test.SetLog("");
                            //test.SetLog("Start Run\n");
                            //test.SetLog(output.width() + " " + output.height());

                        }
                        try
                        {

                            if (!output.empty() && output != null)
                            {
                                outputTexture.Reinitialize(output.width(), output.height());
                                Utils.matToTexture2D(output, outputTexture, new Color32[output.width() * output.height()]);
                                m_RawCameraImage.GetComponent<RawImage>().texture = outputTexture;
                                //
                                //test.text += "output\n";
                                //
                                //
                                //.text = "\noutput01 null!!\n";

                            }

                         

                        }
                        catch (Exception e)
                        {
                            //  test.text += e + "\n";
                        }
                    }
                    else
                    {
                        //test.SetLog("null point\n");
                    }

                }
                catch (Exception e)
                {

                    //  test.text = e.ToString() + "    " + testcount1;
                    testcount1++;
                }
            }
            else
            {
                //  test.text += "m_CameraTexture null\n";
            }
        }
        else
        {

        }
    }
    Texture2D m_CameraTexture;


    int testcount1 = 0;
    public TestviewLog test;
}
