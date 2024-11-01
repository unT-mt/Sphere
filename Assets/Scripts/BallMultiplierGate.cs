using UnityEngine;

public class BallMultiplierGate : MonoBehaviour
{
    public int multiplier = 2;             // x倍にする倍率（2なら2倍、3なら3倍など）
    public GameObject ballPrefab;          // 複製するボールのPrefab

    private void OnTriggerEnter(Collider other)
    {
        // 接触したオブジェクトがボールであることを確認
        if (other.CompareTag("Sphere"))
        {
            // 元のボールを含めてx倍にしたいので、(multiplier - 1)個分のボールを生成
            for (int i = 0; i < multiplier - 1; i++)
            {
                // ゲートの上にボールを生成
                Instantiate(ballPrefab, other.transform.position + Vector3.up * (i + 1), Quaternion.identity);
            }
            Destroy(this);
        }
    }
}
