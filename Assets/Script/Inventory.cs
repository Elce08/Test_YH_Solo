using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// ����� �κ��丮(UI ����)
/// </summary>
public class Inventory
{
    /// <summary>
    /// �κ��丮�� ����ִ� �κ� ������ �⺻ ����
    /// </summary>
    public const int Default_Inventory_Size = 73;

    /// <summary>
    /// �ӽý��Կ� �ε���
    /// </summary>
    public const uint TempSlotIndex = 999999999;

    /// <summary>
    /// �� �κ��丮�� ����ִ� ������ �迭
    /// </summary>
    InvenSlot[] slots;

    /// <summary>
    /// �κ��丮 ���Կ� �����ϱ� ���� �ε���
    /// </summary>
    /// <param name="index">������ �ε���</param>
    /// <returns>����</returns>
    public InvenSlot this[uint index] => slots[index];

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public int SlotCount => slots.Length;

    /// <summary>
    /// �ӽ� ����(�巡�׳� ������ �и��۾��� �� �� ���)
    /// </summary>
    InvenSlot tempSlot;
    public InvenSlot TempSlot => tempSlot;

    public static InvenSlot invenSlot;

    /// <summary>
    /// ������ ������ �޴���(������ ������ �����͸� Ȯ���� �� �ִ�.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// �κ��丮 ������
    /// </summary>
    GameManager owner;
    public GameManager Owner => owner;

    /// <summary>
    /// �κ��丮 ������
    /// </summary>
    /// <param name="owner">�κ��丮 ������</param>
    /// <param name="size">�κ��丮�� ũ��</param>
    public Inventory(GameManager owner, uint size = Default_Inventory_Size)
    {
        invenSlot = new InvenSlot(size);
        slots = new InvenSlot[size];

        for (uint i = 0; i < size; i++)
        {
            slots[i] = new InvenSlot(i);                // ���� ���� ����
        }

        tempSlot = new InvenSlot(TempSlotIndex);
        itemDataManager = new ItemDataManager();
        itemDataManager = GameManager.Inst.ItemData;    // ������ ������ �޴��� ĳ��
        this.owner = owner;                             // ������ ���
    }


    /// <summary>
    /// �κ��丮�� �������� �ϳ� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code">�߰��� ������ ����</param>
    /// <returns>true�� �߰� ����, false�� �߰� ����</returns>
    public bool AddItem(PlayerWeapon code)
    {
        bool result = false;
        ItemData data = itemDataManager[code];

        // ���� ������ �������� ����.
        InvenSlot emptySlot = FindEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.AssignSlotItem(data); // �󽽷��� ������ ������ �ϳ� �Ҵ�
            result = true;
        }
        else
        {
            // ����ִ� ������ ����.
            Debug.Log("������ �߰� ���� : �κ��丮�� ���� ���ֽ��ϴ�.");
        }

        return result;
    }

    /// <summary>
    /// �κ��丮�� Ư�� ���Կ� �������� �ϳ� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="code">�߰��� �������� ����</param>
    /// <param name="slotIndex">�������� �߰��� �ε���</param>
    /// <returns></returns>
    public bool AddItem(PlayerWeapon code, uint slotIndex)
    {
        bool result = false;
        ItemData data = itemDataManager[code];  // ������ ������ ��������
        InvenSlot slot = slots[slotIndex];      // �������� �߰��� ���� ��������
        if (IsValidIndex(slotIndex))   // �ε����� �������� Ȯ��
        {
            if (slot.IsEmpty)
            {
                slot.AssignSlotItem(data);          // ������ ������� ������ �Ҵ�
            }
        }
        return result;
    }

    /// <summary>
    /// �κ��丮���� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="slotIndex">�������� ������ ������ �ε���</param>
    public void ClearSlot(uint slotIndex)
    {
        if (IsValidIndex(slotIndex))
        {
            InvenSlot invenSlot = slots[slotIndex];
            invenSlot.ClearSlotItem();
        }
    }

    /// <summary>
    /// �κ��丮�� ���� ���� �Լ�
    /// </summary>
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }


    /// <summary>
    /// �κ��丮�� �������� from��ġ���� to��ġ�� �������� �̵���Ű�� �Լ�
    /// </summary>
    /// <param name="from">��ġ ������ ���۵Ǵ� �ε���</param>
    /// <param name="to">��ġ ������ ������ �ε���</param>
    public void MoveItem(uint from, uint to)
    {
        // from������ to������ �ٸ��� from�� to�� ��� valid�ؾ� �Ѵ�.
        if ((from != to) && IsValidIndex(from) && IsValidIndex(to))
        {
            InvenSlot fromSlot = (from == TempSlotIndex) ? TempSlot : slots[from];  // �ӽ� ������ �����ؼ� ���׿����ڷ� ó��
            if (fromSlot != null)
            {
                if (!fromSlot.IsEmpty)//(equipSlot.IsEmpty)
                {
                    InvenSlot toSlot = (to == TempSlotIndex) ? TempSlot : slots[to];

                    if (toSlot != null)     // toSlot�� Inven�̶��          && EtoSlot == null
                    {
                        // �ٸ� ������ �������̸� ���� ����
                        ItemData tempData = fromSlot.ItemData;
                        fromSlot.AssignSlotItem(toSlot.ItemData);
                        toSlot.AssignSlotItem(tempData);

                        invenSlot = toSlot;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��� ������ ����Ǿ����� �˸��� �Լ�
    /// </summary>
    void RefreshInventory()
    {
        foreach (var slot in slots)
        {
            slot.onSlotItemChange?.Invoke();
        }
    }

    /// <summary>
    /// �κ��丮���� ����ִ� ������ ã�� �Լ�
    /// </summary>
    /// <returns>����ִ� ����(ù��°)</returns>
    InvenSlot FindEmptySlot()
    {
        InvenSlot findSlot = null;
        foreach (var slot in slots)     // ��� ������ �� ���鼭
        {
            if (slot.IsEmpty)            // ����ִ� ������ ������ ã�Ҵ�.
            {
                findSlot = slot;
                break;
            }
        }

        return findSlot;
    }

    /// <summary>
    /// �󽽷��� ��ã���� ���� �ε���
    /// </summary>
    const uint NotFindEmptySlot = uint.MaxValue;

    /// <summary>
    /// ����ִ� ������ �ε����� �����ִ� �Լ�
    /// </summary>
    /// <param name="index">��¿� �Ķ����, �󽽷��� ã���� ��쿡 �ε�����</param>
    /// <returns>true�� �󽽷��� ã�Ҵ�, false�� �󽽷��� ����.</returns>
    public bool FindEmpySlotIndex(out uint index)
    {
        bool result = false;
        index = NotFindEmptySlot;

        InvenSlot slot = FindEmptySlot();   // �󽽷� ã�Ƽ�
        if (slot != null)
        {
            index = slot.Index;             // �󽽷��� ������ �ε��� ����
            result = true;
        }
        return result;
    }

    /// <summary>
    /// ������ �ε������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="index">Ȯ���� �ε���</param>
    /// <returns>true�� ������ �ε���, false�� ���� �ε���</returns>
    bool IsValidIndex(uint index) => (index < SlotCount) || (index == TempSlotIndex) || (index == TempSlotIndex);
}