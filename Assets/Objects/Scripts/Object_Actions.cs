using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Object_Actions : MonoBehaviour
{
    [SerializeField] private ToolType requiredToolType;
    [SerializeField] private string wrongToolMessage = "You don't have the right tool for this!";
    [SerializeField] private Transform drop;

    [SerializeField] private int yield = 3;

    [SerializeField] private Transform particle;

    private Object_Health objectHealth;


    private void Awake()
    {
        objectHealth = GetComponent<Object_Health>();
    }

    public void PreformAction(ToolItem tool)
    {
        if (tool.toolType == requiredToolType)
        {
            objectHealth.Damage(tool.harvestPower);
            spawnPartical(particle);
        }
        else
        {
            Debug.Log(wrongToolMessage);
        }
    }

    private float spawnRadius = 1f;
    private float spawnHeight = 2f;
    public void ActionPreformed()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        for (int i = 0; i < yield; i++)
        {
            float spawnUpwardForce = Random.Range(3f, 5f);
            Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            Transform spownedDrop = Instantiate(drop, transform.position + Random.insideUnitSphere * spawnRadius + Vector3.up * spawnHeight, randomRotation);
            spownedDrop.GetComponent<Rigidbody>().AddForce(Vector3.up * spawnUpwardForce, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    void spawnPartical(Transform partical)
    {
        Transform particalInstance = Instantiate(partical, transform.position, Quaternion.identity);
        float particalDuration = particalInstance.GetComponent<ParticleSystem>().main.duration;
        particalInstance.GetComponent<ParticleSystemRenderer>().material = gameObject.GetComponent<Renderer>().material;
        Destroy(particalInstance.gameObject, particalDuration);
    }
}
