using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    float radius = 15f;
    float dotProduct;
    float dotProductAngle;

    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Player;
    public CharacterController CharacterController;
    public float SpawnRate = 1.0f;

    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private float Tiempo;

    [SerializeField] private TextMeshProUGUI StartText;

    private bool GameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemiesAround());
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterController.HP <= 0)
        {
            GameOver = true;
        }

        Tiempo += Time.deltaTime;
        TimerText.text = Mathf.FloorToInt(Tiempo / 60).ToString() + ":" + Mathf.FloorToInt(Tiempo % 60).ToString();
    }

    IEnumerator SpawnEnemiesAround()
    {
        
        yield return new WaitForSeconds(1.0f);

        StartText.text = "Get Ready";

        yield return new WaitForSeconds(1.0f);

        StartText.text = "Set";

        yield return new WaitForSeconds(1.0f);

        StartText.text = "Go!";

        yield return new WaitForSeconds(0.5f);

        StartText.text = "";
        
        while (!GameOver)
        {
            yield return new WaitForSeconds(SpawnRate);
            Vector3 randomPos = Random.insideUnitSphere * radius;
            randomPos += Player.transform.position;
            randomPos.y = 0f;

            Vector3 direction = randomPos - transform.position;
            direction.Normalize();

            dotProduct = Vector3.Dot(transform.forward, direction);
            dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

            randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
            randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
            randomPos.z = transform.position.z;
        
            Instantiate(Enemy, randomPos, Quaternion.identity);
        }
    }
}
