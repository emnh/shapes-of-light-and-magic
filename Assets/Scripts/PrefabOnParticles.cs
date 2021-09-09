using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PrefabOnParticles : MonoBehaviour
{
    public GameObject Prefab;

    private List<GameObject> Pool = new List<GameObject>();

    private ParticleSystem m_System;
    private ParticleSystem.Particle[] m_Particles;

    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            //m_Particles[i].velocity += Vector3.up * m_Drift;
            if (!Pool[i].activeSelf)
            {
                Pool[i].SetActive(true);
            }

            var particle = m_Particles[i];
            var obj = Pool[i];
            obj.transform.position = particle.position;
            obj.transform.rotation = Quaternion.Euler(particle.rotation3D);
            obj.transform.localScale = particle.GetCurrentSize3D(m_System);
        }

        for (int i = numParticlesAlive; i < Pool.Count; i++)
        {
            Pool[i].SetActive(false);
        }

        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];

        if (Pool.Count == 0)
        {
            for (var i = 0; i < m_System.main.maxParticles; i++)
            {
                var obj = Instantiate(Prefab, transform);
                Pool.Add(obj);
            }
        }
    }
}