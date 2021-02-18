using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField]
    GameObject electron;

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    SkinnedMeshRenderer jpsi;

    private bool isJpsiEnabled;

    private void Start()    {
        isJpsiEnabled = false;
    }

    public void ToggleParticles()
    {
        electron.GetComponent<Renderer>().enabled = isJpsiEnabled;
        gameObject.GetComponent<Renderer>().enabled = isJpsiEnabled;
        jpsi.enabled = !isJpsiEnabled;
        isJpsiEnabled = !isJpsiEnabled;
    }

    public void EnableExplosion()
    {
        explosion.SetActive(true);
    }
}
