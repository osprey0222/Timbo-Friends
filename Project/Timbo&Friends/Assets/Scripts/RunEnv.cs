using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnv : MonoBehaviour
{
    public SpriteRenderer m_SampleBone;
    public SpriteRenderer m_SampleCookie;
    public SpriteRenderer m_SampleSr;
    public SpriteRenderer m_Home;
    public SpriteRenderer m_SampleCar;
    public GameObject m_VirtualWall;
    public Sprite[] m_ObsSprites;
    public Transform[] m_ObstaclePoses;
    public int m_ToyCountLimit = 10;
    public float m_PlusSpeed = 0f;
    public int m_BoneCountLimit = 3;
    public int m_CookieCountLimit = 1;
    public Transform[] m_BonePoses;
    public Transform[] m_CookiePoses;
    public Transform m_ToyGroup;
    public Transform m_BoneGroup;
    public Transform m_CookieGroup;
    private float[] m_ToySpeeds = new float[3] { 0.1f, 0.2f, 0.3f };
    private float m_BoneSpeed = 1f;
    private float m_CarSpeed = 3f;
    private float m_CookieSpeed = 4f;

    private float EnvWidth
    {
        get
        {
            return GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }
    public float XLimit
    {
        get
        {
            return transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
    }

    public void SetParams(int toyLimit, float plusSpeed)
    {
        m_ToyCountLimit = toyLimit;
        m_PlusSpeed = plusSpeed;
    }

    private void Start()
    {
        float xMin = transform.position.x - EnvWidth / 2;
        float xMax = transform.position.x + EnvWidth / 2;
    }

    public void SpawnSprites()
    {
        SpawnBones();
        SpawnToys();
        SpawnCookies();
        SpawnToyCar();
    }

    public void ShowSuccess()
    {
        m_Home.gameObject.SetActive(true);
    }

    public void HideToys()
    {
        Debug.Log("Hide Toys");
        m_ToyGroup.gameObject.SetActive(false);
        m_VirtualWall.SetActive(false);

    }

    public Vector3 NextRunEnvPos
    {
        get
        {
            return new Vector3(transform.position.x + EnvWidth, transform.position.y, transform.position.z);
        }
    }

    private void SpawnToyCar()
    {
        Vector2 randomPos = Vector2.zero;
        int yRandomIdx = Random.Range(0, m_ObstaclePoses.Length);
        randomPos.y = m_ObstaclePoses[yRandomIdx].position.y;
        randomPos.x = XLimit + GetComponent<SpriteRenderer>().bounds.size.x;
        SpriteRenderer sr = Instantiate(m_SampleCar);
        sr.transform.SetParent(m_ToyGroup);
        sr.transform.localScale = Vector3.one;
        sr.transform.position = randomPos;
        sr.gameObject.AddComponent<MovingObj>().SetMoveInfo(Vector3.left, m_CarSpeed);
    }

    private void SpawnCookies()
    {
        for (int i = 0; i < m_CookieCountLimit; i++)
        {
            int randomIdx = Random.Range(0, m_CookiePoses.Length);

            SpriteRenderer sr = Instantiate(m_SampleCookie);
            sr.transform.SetParent(m_CookieGroup);
            sr.transform.localScale = Vector3.one;
            sr.transform.position = m_CookiePoses[randomIdx].position;
            sr.gameObject.AddComponent<MovingObj>().SetMoveInfo(Vector3.left, m_CookieSpeed);
        }
    }

    private void SpawnToys()
    {
        float xMin = 0f, xMax = 0f;
        GetSpriteXEdgePos(ref xMin, ref xMax);

        for (int i = 0; i < m_ToyCountLimit; i++)
        {
            Vector2 randomPos = Vector2.zero;
            int yRandomIdx = Random.Range(0, m_ObstaclePoses.Length);
            randomPos.y = m_ObstaclePoses[yRandomIdx].position.y;
            randomPos.x = Random.Range(xMin, xMax);

            SpriteRenderer sr = Instantiate(m_SampleSr);
            sr.transform.SetParent(m_ToyGroup);
            sr.sprite = m_ObsSprites[Random.Range(0, m_ObsSprites.Length)];
            sr.transform.localScale = Vector3.one;
            sr.transform.position = randomPos;
            sr.GetComponent<BoxCollider2D>().size = sr.bounds.size;
            int speedRandIdx = Random.Range(0, m_ToySpeeds.Length);
            sr.gameObject.AddComponent<MovingObj>().SetMoveInfo(Vector3.right, m_ToySpeeds[speedRandIdx] + m_PlusSpeed);

        }
    }

    private void SpawnBones()
    {
        float xMin = 0f, xMax = 0f;
        GetSpriteXEdgePos(ref xMin, ref xMax);

        for (int i = 0; i < m_BoneCountLimit; i++)
        {
            Vector2 randomPos = Vector2.zero;
            int yRandomIdx = Random.Range(0, m_BonePoses.Length);
            randomPos.y = m_BonePoses[yRandomIdx].position.y;
            randomPos.x = Random.Range(xMin, xMax);

            SpriteRenderer sr = Instantiate(m_SampleBone);
            sr.transform.SetParent(m_BoneGroup);
            sr.transform.localScale = Vector3.one;
            sr.transform.position = randomPos;
            sr.gameObject.AddComponent<MovingObj>().SetMoveInfo(Vector3.left, m_BoneSpeed);
        }
    }

    private void GetSpriteXEdgePos(ref float xMin, ref float xMax)
    {
        float xSpriteLen = GetComponent<SpriteRenderer>().bounds.size.x;
        xMin = transform.position.x - xSpriteLen / 2;
        xMax = transform.position.x + xSpriteLen / 2;
    }
}

public class MovingObj : MonoBehaviour
{
    private float m_MoveSpeed;
    private Vector3 m_Dir;
    private void Update()
    {
        if (GameData.Singleton.IsPlay)
        {
            transform.Translate(m_Dir * m_MoveSpeed * Time.deltaTime);
        }
    }

    public void SetMoveInfo(Vector3 dir, float speed)
    {
        m_MoveSpeed = speed;
        m_Dir = dir;
    }
}
