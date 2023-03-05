using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace EcsCore
{
    public sealed class ESCGameStartup : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems systems;

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            AddInjections();
            AddOneFrames();
            AddSystems();

            systems
                .ConvertScene()
                .Init();
        }

        private void AddInjections()
        {

        }

        private void AddOneFrames()
        {

        }

        private void AddSystems()
        {

        }

        private void Update()
        {
            systems?.Run();
        }

        private void OnDestroy()
        {
            if (systems == null) return;

            systems.Destroy();
            systems = null;
            world.Destroy();
            systems = null; 
        }
    }
}
