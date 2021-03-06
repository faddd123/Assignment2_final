using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class PlayerFire : MonoBehaviour
{
    SpriteRenderer kirbySprite;
    AudioSource kirbyAudio;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePrefab;
    public AudioClip fireSFX;

    // Start is called before the first frame update
    void Start()
    {
        kirbySprite = GetComponent<SpriteRenderer>();
      

        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Unity inspector values not set");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
            kirbyAudio.clip = fireSFX;
            kirbyAudio = GetComponent<AudioSource>();
        }

        void FireProjectile()
        {
            if (kirbySprite.flipX)
            {
                Projectile projectileInstance =  Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
                projectileInstance.speed = -projectileSpeed;
            }
            else
            {
                Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
                projectileInstance.speed = projectileSpeed;
            }
        }
    }
}
