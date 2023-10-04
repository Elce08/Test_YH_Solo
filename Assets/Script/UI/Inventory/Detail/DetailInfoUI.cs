using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class DetailInfoUI : MonoBehaviour
{
    /// <summary>
    /// �������� �������� ǥ���� �̹���
    /// </summary>
    Image itemIcon;

    /// <summary>
    /// �������� �̸��� ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI itemName;

    /// <summary>
    /// �������� ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI itemPrice;

    /// <summary>
    /// ��ȭ �� ������ ������Ʈ
    /// </summary>
    GameObject before;

    /// <summary>
    /// ��ȭ �� ������ ������Ʈ
    /// </summary>
    GameObject after;

    /// <summary>
    /// ���ġ�� ������ ������Ʈ
    /// </summary>
    GameObject rising;

    /// <summary>
    /// �������� ���� ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] beforeStat;

    /// <summary>
    /// �������� ������ ������ ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] afterStat;

    /// <summary>
    /// �������� ���� ���ġ�� ����� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI[] risingStat;

    /// <summary>
    /// �ڽ�Ʈ ��ġ�� ���� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI costText;

    /// <summary>
    /// ������â ��ü�� ���ĸ� ������ ĵ���� �׷�
    /// </summary>
    CanvasGroup canvasGroup;

    /// <summary>
    /// ������â�� �Ͻ� ���� ���θ� ǥ���ϴ� ����
    /// </summary>
    bool isPause = false;

    ItemData nowItem;

    GameManager owner;
    /// <summary>
    /// �Ͻ����� ���θ� Ȯ�� �� �����ϴ� ������Ƽ
    /// </summary>
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            if (isPause)
            {
                Close();    // �Ͻ� ������ �Ǹ� ���� �ִ� �͵� �ݴ´�.
            }
        }
    }

    /// <summary>
    /// ������â�� ������ ������ �ӵ�
    /// </summary>
    public float alphaChangeSpeed = 10.0f;

    private void Awake()
    {
        owner = FindObjectOfType<GameManager>();
        nowItem = new ItemData();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
        Transform child = transform.GetChild(0);
        itemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        itemName = child.GetComponent<TextMeshProUGUI>();


        beforeStat = new TextMeshProUGUI[6];
        afterStat = new TextMeshProUGUI[6];
        risingStat = new TextMeshProUGUI[5];


        child = transform.GetChild(3);;
        before = child.gameObject;
        child = transform.GetChild(4);
        after = child.gameObject;
        child = transform.GetChild(5);
        rising = child.gameObject;

        child = transform.GetChild(6);
        costText = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(7);
        itemPrice = child.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        for(int i = 0; i < 5; i++)
        {
            beforeStat[i] = before.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            afterStat[i] = after.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            risingStat[i] = rising.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        beforeStat[5] = before.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

        afterStat[5] = after.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// ������â�� ���� �Լ�
    /// </summary>
    /// <param name="data">������â���� ǥ���� �������� ������</param>
    public void Open(ItemData data)
    {
        if (!IsPause && data != null)    // �Ͻ����� ���°� �ƴϰ�, ������ �����Ͱ� �ְ�, ������ â�� ������������ ����
        {
            data.ItemStatus();
            if (data.upgrade != 5)
            {
                itemIcon.sprite = data.itemIcon;                // ������ ����
                itemName.text = data.itemName;                  // �̸� ����
                itemPrice.text = $"{data.price.ToString("N0")} Gold";     // ���� ����(3�ڸ����� �޸� �߰�)

                beforeStat[0].text = data.beforeStr.ToString();        // ��ȭ �� ����(Str)
                beforeStat[1].text = data.beforeWis.ToString();        // ��ȭ �� ����(Agi)
                beforeStat[2].text = data.beforeDef.ToString();        // ��ȭ �� ����(Int)
                beforeStat[3].text = data.beforeHP.ToString();         // ��ȭ �� ����(HP)
                beforeStat[4].text = data.beforeMP.ToString();         // ��ȭ �� ����(MP)
                beforeStat[5].text = data.beforeValue.ToString();      // ��ȭ �� ��ȭ��ġ

                afterStat[0].text = data.afterStr.ToString();          // ��ȭ �� ����(Str)
                afterStat[1].text = data.afterWis.ToString();          // ��ȭ �� ����(Agi)
                afterStat[2].text = data.afterDef.ToString();          // ��ȭ �� ����(Int)
                afterStat[3].text = data.afterHP.ToString();           // ��ȭ �� ����(HP)
                afterStat[4].text = data.afterMP.ToString();           // ��ȭ �� ����(MP)
                afterStat[5].text = data.afterValue.ToString();        // ��ȭ �� ��ȭ��ġ

                risingStat[0].text = $"+ {data.risingStr.ToString()}";        // ��ȭ �� ��� ����(Str)
                risingStat[1].text = $"+ {data.risingWis.ToString()}";        // ��ȭ �� ��� ����(Agi)
                risingStat[2].text = $"+ {data.risingDef.ToString()}";        // ��ȭ �� ��� ����(Int)
                risingStat[3].text = $"+ {data.risingHP.ToString()}";         // ��ȭ �� ��� ����(HP)
                risingStat[4].text = $"+ {data.risingMP.ToString()}";         // ��ȭ �� ��� ����(MP)

                costText.text = $"{data.cost.ToString("N0")} Gold";                  // ��ȭ �� �Ҹ���

                StopAllCoroutines();
                StartCoroutine(FadeIn());                       // ���ĸ� ���� 1�� �ǵ��� �����ؼ� ���̰� �����

                MovePosition(Mouse.current.position.ReadValue());   // ���� �� ���콺 Ŀ�� ��ġ�� ������
                nowItem = data;
            }
            else
            {
                itemIcon.sprite = data.itemIcon;                // ������ ����
                itemName.text = data.itemName;                  // �̸� ����
                itemPrice.text = $"{data.price.ToString("N0")} Gold";     // ���� ����(3�ڸ����� �޸� �߰�)

                beforeStat[0].text = data.beforeStr.ToString();        // ��ȭ �� ����(Str)
                beforeStat[1].text = data.beforeWis.ToString();        // ��ȭ �� ����(Agi)
                beforeStat[2].text = data.beforeDef.ToString();        // ��ȭ �� ����(Int)
                beforeStat[3].text = data.beforeHP.ToString();         // ��ȭ �� ����(HP)
                beforeStat[4].text = data.beforeMP.ToString();         // ��ȭ �� ����(MP)
                beforeStat[5].text = data.beforeValue.ToString();      // ��ȭ �� ��ȭ��ġ

                afterStat[0].text = "Max";          // ��ȭ �� ����(Str)
                afterStat[1].text = "Max";          // ��ȭ �� ����(Agi)
                afterStat[2].text = "Max";          // ��ȭ �� ����(Int)
                afterStat[3].text = "Max";           // ��ȭ �� ����(HP)
                afterStat[4].text = "Max";           // ��ȭ �� ����(MP)
                afterStat[5].text = "Max";        // ��ȭ �� ��ȭ��ġ

                risingStat[0].text = $"+ 0";        // ��ȭ �� ��� ����(Str)
                risingStat[1].text = $"+ 0";        // ��ȭ �� ��� ����(Agi)
                risingStat[2].text = $"+ 0";        // ��ȭ �� ��� ����(Int)
                risingStat[3].text = $"+ 0";         // ��ȭ �� ��� ����(HP)
                risingStat[4].text = $"+ 0";         // ��ȭ �� ��� ����(MP)

                costText.text = "��ȭ �Ұ�";                  // ��ȭ �� �Ҹ���

                StopAllCoroutines();
                StartCoroutine(FadeIn());                       // ���ĸ� ���� 1�� �ǵ��� �����ؼ� ���̰� �����

                MovePosition(Mouse.current.position.ReadValue());   // ���� �� ���콺 Ŀ�� ��ġ�� ������
                nowItem = data;
            }
        }
    }

    /// <summary>
    /// ������â�� �ݴ� �Լ�
    /// </summary>
    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());  // ���ĸ� ���� 0�� �ǵ��� �����ؼ� �Ⱥ��̰� �����
    }

    /// <summary>
    /// ������â�� �����̴� �Լ�
    /// </summary>
    /// <param name="screenPos">��ġ�� ��ũ�� ��ǥ</param>
    public void MovePosition(Vector2 screenPos)
    {
        screenPos = new Vector2(0, -280);
    }

    public void Upgrade()
    {
        if (nowItem.upgrade <= 4)
        {
            if (nowItem.cost < owner.Money)
            {
                owner.Money -= nowItem.cost;
                float Success = 0.0f;
                switch (nowItem.upgrade)
                {
                    case 0:
                        Success = 7.0f;
                        break;
                    case 1:
                        Success = 6.0f;
                        break;
                    case 2:
                        Success = 5.0f;
                        break;
                    case 3:
                        Success = 4.0f;
                        break;
                    case 4:
                        Success = 3.0f;
                        break;
                    case 5:
                        Success = 0.0f;
                        break;
                }
                float upgrade = Random.Range(0, 10);
                if (upgrade <= Success)
                {
                    nowItem.upgrade++;
                    Close();
                    Open(nowItem);
                    Debug.Log("��ȭ�� �����߽��ϴ�.");
                }
                else
                {
                    Debug.Log("��ȭ�� �����߽��ϴ�.");
                    Open(nowItem);
                }
            }
            else
            {
                Debug.Log("��尡 �����մϴ�");
            }
        }
        else
        {
            Debug.Log("��ȭ�� �ִ�ġ �Դϴ�.");
        }

    }

    public void Sell()
    {
        
    }

    /// <summary>
    /// ������â�� ���� ���̰� ����� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1.0f)    // ���İ� 1�� �� ������ �� �����Ӹ��� ���ݾ� ����
        {
            canvasGroup.alpha += Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 1.0f;
    }

    /// <summary>
    /// ������â�� ���� �Ⱥ��̰� ����� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0.0f)    // ���İ� 0�� �� ������ �������Ӹ��� ���ݾ� ����
        {
            canvasGroup.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
    }
}