using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookUI : MonoBehaviour
{
    /********************
         # 3 -> # 7
    ********************/
    [Header("Cooking Buttons")]
    private bool actionBtn_NPC = false;
    public GameObject boilingBtn, steamingBtn, boildownBtn, roastingBtn, fryingBtn, stirfryingBtn, inventory, flavorTxt;
    public void ClickedNPC()
    {
        // 요리 버튼 ON/OFF
        actionBtn_NPC = !actionBtn_NPC;
        boilingBtn.SetActive(actionBtn_NPC); steamingBtn.SetActive(actionBtn_NPC); boildownBtn.SetActive(actionBtn_NPC); 
        roastingBtn.SetActive(actionBtn_NPC); fryingBtn.SetActive(actionBtn_NPC); stirfryingBtn.SetActive(actionBtn_NPC);
        inventory.SetActive(actionBtn_NPC); //flavorTxt.SetActive(actionBtn_NPC);
    }

    /********************
         # 4 -> # 6
    ********************/
    [Header("History")]
    public Transform history;
    public void cleanHistory()
    {
        Transform[] childList = history.GetComponentsInChildren<Transform>();
        if(childList != null)
        {
            foreach(RectTransform child in childList)
            {
                if(child != history)
                    Destroy(child.gameObject);
            }
            // remove history data
            CookDataManager.Instance.curCook.Clear();
        }
    }

    public void deleteHistory(string lastId, int orderNum)
    {
        print("Order+orderNum+lastId : "+"Order"+orderNum+lastId);
        Transform lastChild = history.Find("Order"+orderNum+lastId);
        Destroy(lastChild.gameObject);
        
        // remove history data
        //CookDataManager.Instance.curCook.RemoveAt(CookDataManager.Instance.curCook.Count - 1);
    }

    
    /********************
    # 7, 8 -> CDM -> # 6
    ********************/
    public Object objImg;
    public void ShowObject(CookDataManager.CookObject obj, int orderNum)
    {
        GameObject order = Instantiate(objImg, history) as GameObject;
        Image orderImg = order.GetComponent<Image>();
        orderImg.name = "Order"+orderNum.ToString()+obj.id;
        orderImg.sprite = Resources.Load("Cook/Items/" + obj.id, typeof(Sprite)) as Sprite;
        orderImg.color = new Color(orderImg.color.r, orderImg.color.g, orderImg.color.b, 1);
    }

    /********************
         # 7 -> # 6
    ********************/
    public void ClickedCook(GameObject category)
    {
        CookDataManager.CookObject operation = new CookDataManager.CookObject();
        operation.id = category.name;
        CookDataManager.Instance.OperSelected(operation);
    }

    /********************
         # 5 -> # CDM
    ********************/
    public void ClickedBell()
    {
        CookDataManager.Instance.MakeResult();
    }

}
