﻿using UnityEngine;

namespace GameLib.Entity.Behaviour
{
    public abstract class AbstractState
    {
        public abstract void init(IEntity entity);
        public abstract void update(IEntity entity);
        public abstract void animationMessage(int messageId, IEntity entity);

        public void switchAnimation(int animationState, IEntity entity)
        {
            entity.switchAnimation(animationState);
        }

        public virtual bool onCollisionEnter2D(Collision2D collision, IEntity entity)
        {
            return false;
        }
    }
}