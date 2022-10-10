using TMPro;
using UnityEngine;

public class billboard : MonoBehaviour
{
    [SerializeField] int TargetKarma;
    [SerializeField] Squid Player;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        text.text = TargetKarma.ToString();
    }

    private void Update()
    {
        text.transform.rotation = new Quaternion(text.transform.rotation.x, -Camera.main.transform.rotation.y, text.transform.rotation.z, text.transform.rotation.w);

        if (Player.GetKarma() >= TargetKarma)
        {
            text.color = Color.clear;
            GetComponent<Destructable>().enabled = true;
        }


        if(transform.position.y < -20)
        {
            gameObject.SetActive(false);
        }
    }
}
