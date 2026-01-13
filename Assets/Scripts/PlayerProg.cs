using UnityEngine;

public class PlayerProg : MonoBehaviour
{
    public EggsNum eggnum;
    public HealthController healthController;
    public AudioSource nice;
    public AudioSource bad;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Fox")
        {
            healthController.TakeDamage();
            bad.Play();
        }

        if (collision.gameObject.tag == "Egg")
        {
            nice.Play();
            eggnum.Add(1);
            Destroy(collision.gameObject);
        }
    }
}
