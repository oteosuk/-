using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance = null;
    
    public static ShopManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private User sUser;
    List<Character> chrListtmp = new List<Character>();


    // Fadein, FadeOut in Config
    private float alpha100 = 1.0f;
    private float alpha000 = 0.0f;

    // AlertCanvas
    public Canvas AlertCanvas;
    public CanvasGroup AlertCanvasGroup;



    public Text errorMsg;
    private void Awake()
    {

        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        sUser = DataBaseManager.Instance.getUserData();
        StartCoroutine(DataBaseManager.Instance.GetInventoryData((List<Character> inven_chr_data) =>
        {
            for (int i = 0; i < inven_chr_data.Count; i++)
            {
                chrListtmp.Add(inven_chr_data[i]);
            }
        }));

        // 갖고 있는 아이템인지 체크



        AlertCanvasGroup.gameObject.SetActive(false);
        AlertCanvasGroup.alpha = alpha000;
        AlertCanvas.enabled = false;
    }

    private void Start()
    {
       
    }

    public void OnClickShopCancelBtn()
    {
        SceneLoader.Instance.LoadScene("MainScene");
    }

   
    public void CheckSumBuyRule(int itemId)
    {
        sUser = DataBaseManager.Instance.getUserData();
        Character chr = new Character();

        int money = sUser.money;
        string userEmail = sUser.userEmail;
        int itemkey = itemId;
        string itemName = chr.getChrName(itemkey);
        int itemMoney = chr.getChrMoney(itemkey);
        bool checksum = true;

        chr = new Character(itemName, itemkey, itemMoney, userEmail);

       
        if (money >= chr.money)
        {
            for (int i = 0; i < chrListtmp.Count; i++)
            {
                if (chrListtmp[i].userEmail == userEmail)
                {
                    if (chrListtmp[i].chrCode == itemkey)
                    {
                        checksum = false;
                    }
                }
            }
            if (checksum)
            {
                money -= chr.money;
                DataBaseManager.Instance.UpdateInventory(chr);
                DataBaseManager.Instance.UpdateMoney(sUser.userEmail, money);

            }
            else
            {
                BuyErrorMessage("이미 보유중입니다");
            }
        }
        else
        {
            BuyErrorMessage("돈이 부족합니다");
        }
     
        
    }

    public void BuyErrorMessage(string msg)
    {
        Time.timeScale = 0; // 화면 정지
        AlertCanvasGroup.gameObject.SetActive(true);
        AlertCanvasGroup.alpha = alpha100;
        AlertCanvas.enabled = true;
        errorMsg.text = msg;
    }

    public void OnClickBuyErrorMessageCancelBtn()
    {
        Time.timeScale = 1;
        AlertCanvasGroup.gameObject.SetActive(false);
        AlertCanvasGroup.alpha = alpha000;
        AlertCanvas.enabled = false;
    }
}