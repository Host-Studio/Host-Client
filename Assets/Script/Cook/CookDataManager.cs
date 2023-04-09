using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CookDataManager : MonoBehaviour
{
    // History Object Class
    public class CookObject {
        public string id;
        public string itemInfo = null;
    }
    public List<CookObject> curCook = new List<CookObject>();

        

    // 드래그 중인 아이템 정보
    public string draggingItem;

    [SerializeField] private Inventory Inventory;
    [SerializeField] private CookUI cookUI;
    [SerializeField] private InventoryUI invUI;
    [SerializeField] private FlavorUI flavorUI;
    [SerializeField] ResultUI resultUI;
    [SerializeField] private NoelleUI noelleUI;

    private Dictionary<string, int> inventoryDict = new Dictionary<string, int>();


    // Noelle Dialog Data
    private List<Dictionary<string, object>> dialogData;
    // Flavor Data
    private List<Dictionary<string, object>> flavorData;
    // Recipe Data
    private List<Dictionary<string, object>> recipeData;
    private List<Dictionary<string, object>> findRecipe;

    public static CookDataManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        // Singleton
        Instance = this;

        // Read Noelle Dialog Database
        dialogData = CSVReader.Read("NoelleDialog");
        flavorData = CSVReader.Read("Flavor");
        recipeData = CSVReader.Read("Recipe");
        findRecipe = CSVReader.Read("FindRecipe");

        // test
        inventoryDict["food0001"] = 4;
        inventoryDict["food0003"] = 6;
        inventoryDict["food0015"] = 3;
        inventoryDict["food0016"] = 12;
        inventoryDict["food0002"] = 2;
        inventoryDict["food0017"] = 2;
        inventoryDict["food0018"] = 2;
        SetInventory();
    }

    /*****************************************************
        # NoelleUI -> # CookDataManager -> # NoelleUI
        노엘 대사 DB를 전달
    ******************************************************/
    public string DialogNoelle()
    {
        int random = Random.Range(0, dialogData.Count);
        
        return dialogData[random]["대사"].ToString();
    }

    /*****************************************************
        # NoelleUI -> # CookDataManager -> # CookUI
        노엘의 Clean/Delete 버튼 누르면 조리 프로세스 삭제
    ******************************************************/
    public void CleanHistory(int cleanAll)
    {
        
        // clean
        if(cleanAll == 1)
        {
            // inventory update
            foreach(CookObject obj in curCook)
            {
                // item 아니면 skip
                if(obj.itemInfo == null)
                    continue;

                print("Cleaning ... In Cur Cook : "+ obj.id);
                // update InventoryDict
                ItemUnselect(obj.id);
            }
            
            // history 전부 지우기
            cookUI.cleanHistory();
            curCook.Clear();
            numOfObj=0;
            hasHistory = false;
            order = Order.Food;
        }
        else    // delete
        {
            if(curCook.Count != 0)
            {
                ItemUnselect(curCook[curCook.Count - 2].itemInfo);
                cookUI.deleteHistory(curCook[curCook.Count - 1].id, curCook.Count-1);
                cookUI.deleteHistory(curCook[curCook.Count - 2].id, curCook.Count-2);
                curCook.RemoveAt(curCook.Count - 1);
                curCook.RemoveAt(curCook.Count - 1);
                numOfObj = numOfObj - 2;
                foreach(CookObject obj in curCook)
                {
                    print("In Cur Cook : "+ obj.id);
                }
            }
            else
            {
                cookUI.cleanHistory();
                numOfObj=0;
                hasHistory = false;
            }
        }
    }

    // 조리프로세스에서 아이템 인벤토리로 다시 옮기기
    private void ItemUnselect(string item)
    {
        int itemCount;
        // 해당 아이템 슬롯 찾기
        Slot slot;

        // 현재 inven에 없는 아이템인 경우
        if(!inventoryDict.ContainsKey(item))    
        {
            inventoryDict.Add(item, 1);
            // 새 슬롯에 해당 아이템 추가
        }
        else
        {
            inventoryDict.TryGetValue(item, out itemCount);
            inventoryDict[item] = ++itemCount;
            // 해당 슬롯에 SetSlotCount(1)
        }
        
    }

    // 인벤토리 초기화
    private void SetInventory()
    {
        foreach (KeyValuePair<string, int> slot in inventoryDict)
        {
            Item item = Resources.Load<Item>("Item/" + slot.Key);
            Inventory.AcquireItem(item, slot.Value);
        }
    }

    /*****************************************************
        # Inventory -> # CookDataManager -> # CookUI
        아이템 선택 시 조리 프로세스에 띄우기
    ******************************************************/
    public int numOfObj = 0;
    public bool hasHistory = false;

    // 조리 차례, 아이템 차례
    public enum Order {Food, Operation};
    public Order order = Order.Food;
    public void ItemSelected(string item, CookObject operation)
    {
        if(order != Order.Food || numOfObj == 6)
            return;

        // 아이템 차례이고, 조리 기회가 아직 남아있을 때
        // update history, 차례, 조리 수
        hasHistory = true;
        order = Order.Operation;


        CookObject itemObj = new CookObject();
        itemObj.id = item;
        itemObj.itemInfo = item;
        
        
        noelleUI.DeleteDialog();
        curCook.Add(itemObj);
        cookUI.ShowObject(itemObj, curCook.Count-1);
        numOfObj++;


        // update InventoryDict
        int itemCount;
        inventoryDict.TryGetValue(item, out itemCount);
        inventoryDict[item] = --itemCount;
        
        if(itemCount==0)
            inventoryDict.Remove(item);

        OperSelected(operation);

    }
    // 여기까지 수정함
    /*****************************************************
        # CookUI -> # CookDataManager -> # CookUI
        조리 과정 선택 시 조리 프로세스 띄우기
    ******************************************************/
    public void OperSelected(CookObject oper)
    {
        if(order != Order.Operation || numOfObj == 6)
            return;

        // 아이템 차례이고, 조리 기회가 아직 남아있을 때
        // update history, 차례, 조리 수
        hasHistory = true;
        order = Order.Food;
        
        noelleUI.DeleteDialog();
        curCook.Add(oper);
        cookUI.ShowObject(oper, curCook.Count-1);
        numOfObj++;
    }

    /*****************************************************
        # CookUI -> # CookDataManager -> # ResultUI
        콜벨 누르면 조리 계산해서 결과 띄우기
    ******************************************************/
    private string resultfood, foodstring;
    public void MakeResult()
    {
        string recipe = "";

        for (int i = 1 ; i <= 3 ; i++)
        {
            // "레시피N"
            string column = "레시피";
            bool isRecipeCor = true;
            column += i.ToString();
            recipe = findRecipe[int.Parse(Regex.Replace(curCook[0].id, @"\D", "")) - 1][column].ToString();
            
            // 레시피 찾았을 때
            if(recipe != "")
            {
                // 중간 과정 저장 배열
                string [] processes = new string[3];
                // 해당 레시피 행
                int row = int.Parse(Regex.Replace(recipe, @"\D", "")) - 1 ;
                // 해당 레시피 과정 수
                int count = int.Parse(recipeData[row]["과정_Count"].ToString());
                if(count * 2 != curCook.Count || recipeData[row]["과정1_ID"].ToString() != curCook[1].id)
                    continue;  // 과정 수 틀리거나, 첫번째 과정 틀렸을 때 다음 레시피 탐색
                processes[0] = curCook[1].id;

                // 과정 수 맞으면 나머지 모든 재료와 과정 확인하기
                for(int j = 2 ; j <= count && isRecipeCor==true ; j=j+2)
                {
                    if(curCook[j].id != recipeData[row]["재료"+(j/2+1).ToString()+"_ID"].ToString()
                    || curCook[j+1].id != recipeData[row]["과정"+(j/2+1).ToString()+"_ID"].ToString())
                    isRecipeCor = false;
                    if(isRecipeCor)
                        processes[j/2] = curCook[j+1].id;
                }
                print("processes[0]: "+processes[0]);
                // 레시피 내용 중 틀린 게 있을 때
                if(!isRecipeCor)
                    continue;   // 다음 레시피 탐색
                // 레시피 모든 내용 맞았을 때
                else
                {
                    resultfood = recipeData[row]["성공"].ToString();
                    foodstring = recipeData[row]["요리_이름"].ToString();
                    print("resultFood : "+resultfood);
                    resultUI.ShowResult("Success", processes, count, resultfood, foodstring);
                    break;
                }
                
            }

            // 레시피 없을 때
            else
            {
                print("Fail to find recipe...");
            }
        }
        
        // Clean History
        CleanHistory(1);
    }

    
    /*****************************************************
        # Inventory -> # CookDataManager -> # FlavorUI
        아이템 누르면 설명 띄우기
    ******************************************************/
    public void SendFlavorData(int itemId)
    {
        flavorUI.PrintFlavor(flavorData[itemId - 1]["플레이버_텍스트"].ToString(), flavorData[itemId - 1]["재료_이름"].ToString());
    }
    public void DelFlavorData()
    {
        flavorUI.ExitFlavor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
