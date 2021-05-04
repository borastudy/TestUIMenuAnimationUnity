using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestScript : MonoBehaviour
{
    [SerializeField] GameObject panel;
    
    [SerializeField] Vector3 posShow;
    [SerializeField] Vector3 posHide;
    [SerializeField] Dropdown dropdownPluginType;
    [SerializeField] Dropdown dropdownEaseTypeMoveUp;
    [SerializeField] Dropdown dropdownEaseTypeMoveDown;
    [SerializeField] Text textTime;
    [SerializeField] Slider sliderTime;
    [SerializeField] Text textAlert;

    private bool isShowed;

    enum PluginType
    {
        LeanTween,
        iTween,
        DOTween
    }

    void Start()
    {
        MoveDown();
        isShowed = false;

        RefreshDropDownOptionByPluginType(PluginType.LeanTween);
        dropdownEaseTypeMoveUp.SetValueWithoutNotify(1);
        dropdownEaseTypeMoveDown.SetValueWithoutNotify(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RefreshDropDownOptionByPluginType (PluginType type)
    {
        Array values ;
        switch (type)
        {
            case PluginType.LeanTween:
                values = Enum.GetValues(typeof(LeanTweenType));
                break;

            case PluginType.iTween:
                values = Enum.GetValues(typeof(iTween.EaseType));
                break;

            case PluginType.DOTween:
                values = Enum.GetValues(typeof(Ease));
                break;

            default:
                values = Enum.GetValues(typeof(LeanTweenType));
                break;
        }

        var list = new List<string>();
        foreach (var v in values)
        {
            list.Add(v.ToString());
        }

        dropdownEaseTypeMoveUp.ClearOptions();
        dropdownEaseTypeMoveUp.AddOptions(list);

        dropdownEaseTypeMoveDown.ClearOptions();
        dropdownEaseTypeMoveDown.AddOptions(list);
    }

    public void OnPluginValueChanged (Int32 value)
    {
        Debug.Log("Plugin changed : " + value);
        RefreshDropDownOptionByPluginType((PluginType)(value));
    }
    public void OnEaseMoveUpValueChanged(Int32 value)
    {
        Debug.Log("Ease Move Up changed : " + value);
        if ((iTween.EaseType)value == iTween.EaseType.punch)
        {
            textAlert.gameObject.SetActive(true);
            dropdownEaseTypeMoveUp.SetValueWithoutNotify((int)iTween.EaseType.linear);
        }
    }
    public void OnEaseMoveDownValueChanged(Int32 value)
    {
        Debug.Log("Ease Move Down changed : " + value);
        if ((iTween.EaseType)value == iTween.EaseType.punch)
        {
            textAlert.gameObject.SetActive(true);
            dropdownEaseTypeMoveDown.SetValueWithoutNotify((int)iTween.EaseType.linear);
        }
    }

    public void SliderTimeValueChanged (float time)
    {
        textTime.text = "Time: " + time;
    }

    public void OnClick_Button ()
    {
        if (isShowed)
        {
            //to hide
            MoveDown();
        }
        else
        {
            //to show
            MoveUp();
        }
        isShowed = !isShowed;
    }

    void MoveUp ()
    {
        try
        {
            var selected = (PluginType)dropdownPluginType.value;
            var easeType = dropdownEaseTypeMoveUp.value;
            var time = sliderTime.value;
            if (selected == PluginType.LeanTween)
            {
                LeanTween.move(panel.GetComponent<RectTransform>(), posShow, time)
                    .setIgnoreTimeScale(true)
                    .setEase((LeanTweenType)easeType);
            }
            else if (selected == PluginType.iTween)
            {
                iTween.MoveTo(panel, iTween.Hash("y", posShow.y, "time", time, "easetype", (iTween.EaseType)easeType));
            }
            else
            {
                panel.transform.DOMoveY(posShow.y, time).SetEase((Ease)easeType);
            }
        }
        catch
        {
            textAlert.gameObject.SetActive(true);
        }

    }
    void MoveDown ()
    {
        try
        {
            var selected = (PluginType)dropdownPluginType.value;
            var easeType = dropdownEaseTypeMoveDown.value;
            var time = sliderTime.value;
            if (selected == PluginType.LeanTween)
            {
                LeanTween.move(panel.GetComponent<RectTransform>(), posHide, time)
                    .setIgnoreTimeScale(true)
                    .setEase((LeanTweenType)easeType);
            }
            else if (selected == PluginType.iTween)
            {
                iTween.MoveTo(panel, iTween.Hash("y", posHide.y, "time", time, "easetype", (iTween.EaseType)easeType));
            }
            else
            {
                panel.transform.DOMoveY(posHide.y, time).SetEase((Ease)easeType);
            }
        } catch
        {
            textAlert.gameObject.SetActive(true);
        }
    }

}
