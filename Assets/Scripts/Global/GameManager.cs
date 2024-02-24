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
        // GameObjec 를 통해서 실행하는 find 함수는 하이어라키탭을 전부 탐색하기 때문에 느려서 Awake, Start 같은 함수에서 사용하는게 좋다
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

    }
}
