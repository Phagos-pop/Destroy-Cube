using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using EcsCore.Components;

namespace EcsCore.Systems
{
    sealed class MoveSystem : IEcsRunSystem , IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            //EcsWorld world = systems.GetWorld();

            //int entity = world.NewEntity();
        }

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<MoveComponent>().Inc<TransformComponent>().End();

            var moves = world.GetPool<MoveComponent>();

            var tranformpool = world.GetPool<TransformComponent>();

            foreach(int entity in filter)
            {
                ref MoveComponent movecomponent = ref moves.Get(entity);
                ref TransformComponent tranformComponent = ref tranformpool.Get(entity);
                movecomponent.speed = 1;
            }
        }
    }
}

