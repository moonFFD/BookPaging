using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paging : MonoBehaviour {
    public Button rtl_btn;
    public Button ltr_btn;
    public Image page0;
    public Text page0Txt;
    public Image page2Mask_left;
    public Image page2_left;
    public Text page2LeftTxt;
    public Image page1Mask_left;
    public Image page1_left;
    public Text page1LeftTxt;
    //public Image leftPageShadow;

    public Image page3;
    public Text page3Txt;
    public Image page1Mask_right;
    public Image page1_right;
    public Text page1RightTxt;
    public Image page2Mask_right;
    public Image page2_right;
    public Text page2RightTxt;
    //public Image rightPageShadow;

    public float turningTime;
    public int pageCount;
    //左边的页数，默认是0
    int curPageCount = 0; 
    public float deltaDegree;
    private bool isTurning;

    //每页的颜色
    private Color[] pagesColor = { Color.black, Color.blue, Color.magenta, Color.cyan, Color.green, Color.grey, Color.red, Color.yellow};

    public IEnumerator turnPageFromRTL;

	// Use this for initialization
	void Start () {
        rtl_btn.onClick.AddListener(OnClickRTL);
        ltr_btn.onClick.AddListener(OnClickLTR);
        //开始的时候显示第0页与第1页以及第3页
        page0.transform.gameObject.SetActive(true);
        page0.color = pagesColor[0];
        page0Txt.text = (curPageCount).ToString();

        //page2Mask_left.gameObject.SetActive(false);
        //page2LeftTxt.text = (curPageCount - ).ToString();

        //page1Mask_left.gameObject.SetActive(false);
        //page1LeftTxt.text = (curPageCount - 2).ToString();

        page3.gameObject.SetActive(true);
        page3Txt.text = (curPageCount + 3).ToString();

        page1Mask_right.gameObject.SetActive(true);
        page1_right.color = pagesColor[curPageCount + 1];
        page1RightTxt.text = (curPageCount + 1).ToString();

        //page2Mask_right.gameObject.SetActive(true);
        //page2RightTxt.text = (curPageCount + 2).ToString();

        page0.transform.SetSiblingIndex(0);
        //page2Mask_left.transform.SetSiblingIndex(1);
        //page1Mask_left.transform.SetSiblingIndex(2);
        page3.transform.SetSiblingIndex(3);
        page1Mask_right.transform.SetSiblingIndex(4);
        page2Mask_right.transform.SetSiblingIndex(5);
        InitBtnsState();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickRTL()
    {
        if(!isTurning)
        {
            StartCoroutine("TurnPageRTL");
        }
    }
    public void OnClickLTR()
    {
        if(!isTurning)
        {
            StartCoroutine("TurnPageLTR");
        }
    }

    public IEnumerator TurnPageRTL()
    {
        InitPagesState(true);
        float curTime = 0f;
        isTurning = true;
        Vector3 deltaDegree = new Vector3(0f, 0f, -90f / 100f);
        while (true)
        {
            curTime += 0.01f;
            yield return new WaitForSeconds(0.01f);
            if (curTime < turningTime)
            {
                page1Mask_right.transform.eulerAngles -= deltaDegree;
                page1_right.transform.eulerAngles += deltaDegree;
                page2Mask_right.transform.eulerAngles -= deltaDegree;
                page2_right.transform.localEulerAngles -= deltaDegree;
            }
            else
            {
                break;
            }
        }
        isTurning = false;
        curPageCount = curPageCount + 2;
        ResetPagesState();
    }

    public IEnumerator TurnPageLTR()
    {
        InitPagesState(false);
        float curTime = 0.0f;
        isTurning = true;
        Vector3 deltaDegree = new Vector3(0, 0, -90f / 100);
        while (true)
        {
            curTime += 0.01f;
            yield return new WaitForSeconds(0.01f);
            if (curTime < turningTime)
            {
                page1Mask_right.transform.eulerAngles += deltaDegree;
                page1_right.transform.eulerAngles -= deltaDegree;
                page2Mask_right.transform.eulerAngles += deltaDegree;
                page2_right.transform.eulerAngles += deltaDegree;
            }
            else
            {
                break;
            }
        }
        isTurning = false;
        curPageCount = curPageCount - 2;
        ResetPagesState(false);
    }

    /// <summary>
    /// 初始化各个页面
    /// 
    /// </summary>
    public void InitPagesState(bool isRTL = true)
    {
        Debug.Log(curPageCount);
        InitPagesPositionAndPivot(isRTL);
        if (isRTL)
        {
            page1Mask_right.transform.eulerAngles = Vector3.zero;
            page1_right.transform.eulerAngles = Vector3.zero;
            page1_right.color = pagesColor[curPageCount + 1];
            page1RightTxt.text = (curPageCount + 1).ToString();

            page2Mask_right.transform.eulerAngles = new Vector3(0, 0, -90);
            page2_right.transform.localEulerAngles = new Vector3(0, 0, -90);
            page2_right.color = pagesColor[curPageCount + 2];
            page2RightTxt.text = (curPageCount + 2).ToString();

            page3.color = pagesColor[curPageCount + 3];
            page3Txt.text = (curPageCount + 3).ToString();
        }
        else
        {
            page1Mask_right.transform.eulerAngles = Vector3.zero;
            page1_right.transform.eulerAngles = Vector3.zero;
            page1_right.color = pagesColor[curPageCount];
            page1RightTxt.text = (curPageCount).ToString();

            page2Mask_right.transform.eulerAngles = new Vector3(0, 0, 90);
            page2_right.transform.localEulerAngles = new Vector3(0, 0, 90);
            page2_right.color = pagesColor[curPageCount - 1];
            page2RightTxt.text = (curPageCount - 1).ToString();

            page0.color = pagesColor[curPageCount - 2];
            page0Txt.text = (curPageCount - 2).ToString();
        }
    }

    void InitPagesPositionAndPivot(bool isRTL = true)
    {
        if(isRTL)
        {
            page1Mask_right.rectTransform.pivot = Vector2.zero;
            page1Mask_right.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            page1Mask_right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            page1Mask_right.rectTransform.anchoredPosition = new Vector2(0, -page1Mask_right.rectTransform.rect.height / 2);
            page1_right.rectTransform.pivot = Vector2.zero;
            page1_right.rectTransform.anchorMax = Vector2.zero;
            page1_right.rectTransform.anchorMin = Vector2.zero;
            page1_right.rectTransform.anchoredPosition = Vector2.zero;

            page2Mask_right.rectTransform.pivot = Vector2.right;
            page2Mask_right.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            page2Mask_right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            page2Mask_right.rectTransform.anchoredPosition = new Vector2(0f, -page2_right.rectTransform.rect.width);
            page2_right.rectTransform.pivot = Vector2.right;
            page2_right.rectTransform.anchorMax = Vector2.right;
            page2_right.rectTransform.anchorMin = Vector2.right;
            page2_right.rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            page1Mask_right.rectTransform.pivot = Vector2.right;
            page1Mask_right.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            page1Mask_right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            page1Mask_right.rectTransform.anchoredPosition = new Vector2(0, -page1Mask_right.rectTransform.rect.height / 2);
            page1_right.rectTransform.pivot = Vector2.right;
            page1_right.rectTransform.anchorMax = Vector2.right;
            page1_right.rectTransform.anchorMin = Vector2.right;
            page1_right.rectTransform.anchoredPosition = Vector2.zero;

            page2Mask_right.rectTransform.pivot = Vector2.zero;
            page2Mask_right.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            page2Mask_right.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            page2Mask_right.rectTransform.anchoredPosition = new Vector2(0f, -page2_right.rectTransform.rect.width);
            page2_right.rectTransform.pivot = Vector2.zero;
            page2_right.rectTransform.anchorMax = Vector2.zero;
            page2_right.rectTransform.anchorMin = Vector2.zero;
            page2_right.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    ///重置各个页面状态
    ///page2Mask_Right复位且如果 有下一页的话颜色值为下一页
    public void ResetPagesState(bool isRTL = true)
    {
        if(isRTL)
        {
            page0.color = page2_right.color;
            page0Txt.text = page2RightTxt.text;
        }
        else
        {
            page3.color = page2_right.color;
            page3Txt.text = page2RightTxt.text;
        }
        InitBtnsState();
    }

    public void InitBtnsState()
    {
        if (curPageCount >= pageCount - 2)
        {
            rtl_btn.interactable = false;
        }
        else
        {
            rtl_btn.interactable = true;
        }

        if (curPageCount <= 0)
        {
            ltr_btn.interactable = false;
        }
        else
        {
            ltr_btn.interactable = true;
        }
    }
}
