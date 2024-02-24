using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        instance = this;
        // GameObjec �� ���ؼ� �����ϴ� find �Լ��� ���̾��Ű���� ���� Ž���ϱ� ������ ������ Awake, Start ���� �Լ����� ����ϴ°� ����
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

    }
}
