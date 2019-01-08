using GameLib.Entity.Animation;
using GameLib.System.Controller;
using GameLib.System.Gravity2D;
using System;

namespace GameLib.Entity.Behaviour.State
{
    public class JumpUp : Move
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void init(IEntity entity)
        {
            switchAnimation(AnimationAttributes.ANIMATION_JUMPUP, entity);
            setGravityAndPlaySound(entity);
        }

        protected void setGravityAndPlaySound(IEntity entity)
        {
            entity.getGravity().Jumping = true;
            entity.getGravity()._reset(entity.getGravityClient());
            entity.getGravity().Reset = false;
            entity.playAudio(1);
        }

        public override void update(IEntity entity)
        {
            Gravity gravity = entity.getGravity();
            KeysPressed keysPressed = entity.getKeysPressed();
            IGravityClient gravityClient = entity.getGravityClient();
            
            moveEntity(entity, keysPressed, gravity, entity.getMovementSpeed());

            switchState(entity, keysPressed, gravity, gravityClient);
        }

        protected virtual void switchState(IEntity entity, KeysPressed keysPressed, Gravity gravity, IGravityClient gravityClient)
        { 
            if (!keysPressed.jump)
            {
                if (gravity.Y_velocity >= 0.0f && (gravityClient.getJumpForce() * 0.85f) > gravity.Y_velocity)
                {
                    gravity.Y_velocity = -0.01f;
                }
            }
            if (gravity.isFalling() || gravity.isStanding())
            {
                gravity.Jumping = false;
                IBehaviourStateFactory behaviourStateFactory = entity.getBehaviourStateFactory();
                entity.setState(behaviourStateFactory.getFallState(entity));
            }
        }        
    }
}