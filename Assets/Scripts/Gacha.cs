using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct GachaItem
{
    public int Amount;
    public GameObject Instance;
}

public class Gacha : MonoBehaviour
{
    [SerializeField]
    private List<GachaItem> GachaItemList;

    [SerializeField]
    private TextMeshProUGUI ResultText;

    [SerializeField]
    private GameObject GachaItemListScrollViewContent;

    [SerializeField]
    private GameObject GachaItemListRecordPrefab;

    private int GachaItemCount;

    void UpdateGachaItemInformation()
    {
        this.GachaItemCount = 0;
        for(int i=0; i<this.GachaItemList.Count; i++)
        {
            GachaItemCount += this.GachaItemList[i].Amount;
            GameObject gachaItemListRecord = Instantiate(
                this.GachaItemListRecordPrefab,
                this.GachaItemListScrollViewContent.transform
            );
            TextMeshProUGUI gachaItemListRecordText = gachaItemListRecord.GetComponent<TextMeshProUGUI>();
            gachaItemListRecordText.SetText(string.Format("{0}. {1} ({2}x)", i+1, this.GachaItemList[i].Instance.name, this.GachaItemList[i].Amount));
        }
    }

    void HideAllGachaItemInstance()
    {
        foreach(GachaItem gachaItem in this.GachaItemList)
        {
            gachaItem.Instance.SetActive(false);
        }
    }

    void ShowGachaItemInstance(int index)
    {
        this.GachaItemList[index].Instance.SetActive(true);
        this.ResultText.text = string.Format("Result: {0}",  this.GachaItemList[index].Instance.name);
    }

    int GetRandomGachaItemIndex()
    {
        int rndItemIndex = Random.Range(0, this.GachaItemCount);
        for(int i=0; i<this.GachaItemList.Count; i++)
        {
            if(rndItemIndex < this.GachaItemList[i].Amount)
            {
                return i;
            }
            rndItemIndex -= this.GachaItemList[i].Amount;
        }

        throw new System.ArgumentException("The gacha item count does not match the sum of gacha item amount in the list.");
    }

    public void DoRandomGachaItem()
    {
        this.HideAllGachaItemInstance();
        int gachaItemIndex = this.GetRandomGachaItemIndex();
        this.ShowGachaItemInstance(gachaItemIndex);
    }

    void Start()
    {
        this.UpdateGachaItemInformation();
        this.HideAllGachaItemInstance();
    }
}
