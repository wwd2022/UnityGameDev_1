using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";


    private HealthSystem playerHealthSystem;

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpGaugeSlider;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private int currentWaveIndex = 0;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0; // 스폰되는 몹의 개수

    public float spawnInterval = .5f;
    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPostions = new List<Transform>();

    public List<GameObject> rewards = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        // GameObjec 를 통해서 실행하는 find 함수는 하이어라키탭을 전부 탐색하기 때문에 느려서 Awake, Start 같은 함수에서 사용하는게 좋다

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHealthUI;
        playerHealthSystem.OnHeal += UpdateHealthUI;
        playerHealthSystem.OnDeath += GameOver;

        gameOverUI.SetActive(false);

        // 스폰포지션의 위치를 전부 가져와서 저장한다
        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPostions.Add(spawnPositionsRoot.GetChild(i));
        }
    }

    private void Start()
    {
        // UpgradeStatInit(); // 업그레이드
        StartCoroutine("StartNextWave"); // 코루틴 시작
    }


    IEnumerator StartNextWave()
    {
        while (true)
        {
            if (currentSpawnCount == 0)
            {
                UpdateWaveUI(); // Wave 표시 갱신
                yield return new WaitForSeconds(2f); // 2초간 대기

                if (currentWaveIndex % 20 == 0)
                {
                    // RandomUpgrade(); // 랜덤 업그레이드
                }

                if (currentWaveIndex % 10 == 0)
                {
                    waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPostions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
                    waveSpawnCount = 0;
                }

                if (currentWaveIndex % 5 == 0)
                {
                    CreateReward(); // 보상
                }

                if (currentWaveIndex % 3 == 0)
                {
                    waveSpawnCount += 1;
                }


                for (int i = 0; i < waveSpawnPosCount; i++)
                {
                    int posIdx = Random.Range(0, spawnPostions.Count);
                    for (int j = 0; j < waveSpawnCount; j++)
                    {
                        int prefabIdx = Random.Range(0, enemyPrefebs.Count); // 무작위 몹 선택
                        GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPostions[posIdx].position, Quaternion.identity); // 몹 생성 
                        enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath; // 몹사망시 카운트 감소

                        currentSpawnCount++; // 몹 카운트 증가
                        yield return new WaitForSeconds(spawnInterval); // spawnInterval 초간 대기
                    }
                }

                currentWaveIndex++;
            }

            yield return null;
        }
    }

    private void UpdateHealthUI()
    {
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        StopAllCoroutines(); // 오브젝트에 있는 모든 코루틴 멈춤
    }

    private void UpdateWaveUI()
    {
        waveText.text = (currentWaveIndex + 1).ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // 아이템 드랍(보상 생성)
    void CreateReward()
    {
        int idx = Random.Range(0, rewards.Count);
        int posIdx = Random.Range(0, spawnPostions.Count);

        GameObject obj = rewards[idx];
        Instantiate(obj, spawnPostions[posIdx].position, Quaternion.identity);
    }

}
