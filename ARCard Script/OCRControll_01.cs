using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ImgcodecsModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnityExample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//OCR화면에서 카메라버튼 클릭 시 가이드 영역만큼 잘라내서 OCRControll_02.cs전달한다. 

public class OCRControll_01 : ARcamGetTexture
{
    public Texture2D defaultTexture;
    public ARCardAll_Get arCardAll_get;
    public OCRControll_02 ocrControll02;
    public Canvas ocrCanvas;
    public GameObject ocrImage;

    /// <summary>
    /// AR카메라가 다 켜지고 준비되었는지 체크
    /// </summary>
    public bool isLodingCheck = false; 

    //알림창이 생성될 상위오브젝트
    public GameObject noticeCanvas; 
    //카메라를 받아오기 위한 초기 설정을 한다. 한번실행.
    bool isCheckMat = false;
    //카메라의 원본을 저장할 Mat
    Mat image;
    //Debug를 위한 RawImage
    public RawImage testrawimage;




    protected override void Start()
    {
        base.Start(); //ARcamGetTexture에 Start 선언되어있음.
    }

    protected override bool ProcessTexture(Texture2D input, ref Mat output)
    {
        if (input == null)
        {
            return false;
        }
        else
        {
            if (!isLodingCheck) //실행할때
            {
                arCardAll_get.Load_completed();

                if (!SplayerPrefs.isPlayerPrefs("isARCArdGuideKey")) //처음 실행한것. 제스쳐 가이드가 나오면 하단메뉴를 지워준다.
                {
                    arCardAll_get.hideBottonMenu();
                }
                isLodingCheck = true;
            }
        }

   
        if (!isCheckMat)
        {
            output = new Mat();
            image = new Mat(input.height, input.width, CvType.CV_8UC3, new Scalar(255));
            isCheckMat = true;
            output = image; //원본화면
        }

        //알림창이 비활성화인 경우에만 webcamTexture를 rawImage에 적용해고 알림창이 활성화되어 있을경우 멈준다.
        if (noticeCanvas.transform.childCount == 0) 
        {
            //알림창이 비활성화인 경우.
            Utils.texture2DToMat(input, image);
        }

        //WebcamTexture에서 명함부분만 자르고 회색조 변환하고 세로(명함의 구분자 |가 잘 인식되도록 하기 위함)로 늘린다.
        //**주의할점: Mask_RawImage의 Pivot은 0.5 0.5를 유지해야한다.
        //한번만 실행한다.
        if (ocrControll02.isGetTexture) 
        {
            ocrControll02.textureOCR = defaultTexture; //ocr로 보낼 텍스쳐 초기화 

            Mat ocrMat = new Mat();
            Core.copyTo(image, ocrMat, ocrMat);

            // x는 width값으로 옆으로 늘림/줄임(실제 출력되는 화면은 90도 돌린화면이다. 즉 90도 돌린 화면에서는 위아래로).
            // y는 height값으로 옆으로 늘림/줄임(실제 출력되는 화면은 90도 돌린화면이다. 즉 90도 돌린 화면에서는 좌우로).
            Point p1 = new Point(ocrMat.width() / 2 - 50, ocrMat.height() / 2 - 400); //시작지점. 중심을 기준으로
            Point p2 = new Point(ocrMat.width() / 2 + 450, ocrMat.height() / 2 + 400); //종료지점 중심을 기준으로

            OpenCVForUnity.CoreModule.Rect subRect = new OpenCVForUnity.CoreModule.Rect(0, 0, 100, 400);

            ocrMat = ocrMat.submat((int)p1.y, (int)p2.y, (int)p1.x, (int)p2.x);

            Imgproc.cvtColor(ocrMat, ocrMat, Imgproc.COLOR_BGR2GRAY);
            //Imgproc.threshold(testMat, testMat, 0, 255, Imgproc.THRESH_BINARY | Imgproc.THRESH_OTSU); //실제 명함은 반사때문에 사용 못함.

            //세로를 늘려서. 구분자 |가 잘 인식되도록 한다.
            Imgproc.resize(ocrMat, ocrMat, new Size(ocrMat.width() + 500, ocrMat.height()));
            ocrControll02.textureOCR.Reinitialize(ocrMat.width(), ocrMat.height());

            Utils.matToTexture2D(ocrMat, ocrControll02.textureOCR);

            //Debug용
            //testrawimage.texture = ocrControll02.textureOCR;
            //Debug.Log("p1:" + p1.x + "||" + p1.y + "p2:" + p2.x + "||" + p2.y);
            //Debug.Log("|" + testMat.width() + "|" + testMat.height());

            ocrControll02.isGetTexture = false;


            System.GC.Collect(); //가비지 호출
        }
        return true;
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        isLodingCheck = false;
    }
    /// <summary>
    /// ARcamGetTexture.cs의 SizeCount = 0 초기화
    /// </summary>
    public void InitOCR()
    {
        SizeCount = 0;
    }

}