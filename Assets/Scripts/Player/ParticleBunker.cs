using UnityEngine;

public class ParticleBunker : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    private bool[] particleAlive;

    void Start()
    {
        // Configurar ParticleSystem para que genere una forma de bunker
        var emission = particleSystem.emission;
        emission.rateOverTime = 0; // estático

        var shape = particleSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Sprite;

        // Emitir partículas en forma de bunker
        int totalParticles = 500;
        particles = new ParticleSystem.Particle[totalParticles];
        particleAlive = new bool[totalParticles];

        for (int i = 0; i < totalParticles; i++)
        {
            float x = Random.Range(-2f, 2f);
            float y = Random.Range(0f, 1.5f - Mathf.Abs(x) * 0.5f);
            particles[i].position = new Vector3(x, y, 0);
            particles[i].startSize = 0.1f;
            particles[i].startLifetime = float.MaxValue;
            particleAlive[i] = true;
        }

        particleSystem.SetParticles(particles, totalParticles);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            Vector2 hitPoint = transform.InverseTransformPoint(other.transform.position);

            // Eliminar partículas cerca del impacto
            int total = particleSystem.particleCount;
            particleSystem.GetParticles(particles);
            int removed = 0;

            for (int i = 0; i < total; i++)
            {
                float dist = Vector2.Distance(particles[i].position, hitPoint);
                if (dist < 0.3f && particleAlive[i])
                {
                    particleAlive[i] = false;
                    particles[i].remainingLifetime = 0;
                    removed++;
                }
            }

            particleSystem.SetParticles(particles, total);
            Destroy(other.gameObject);

            if (removed > 50) // destrucción casi total
                Destroy(gameObject);
        }
    }
}