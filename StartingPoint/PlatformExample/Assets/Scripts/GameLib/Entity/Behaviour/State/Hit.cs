using GameLib.Entity.Animation;

namespace GameLib.Entity.Behaviour.State
{
    public class Hit : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
          
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_HITTED, entity);
        }

        public override void update(IEntity entity)
        {
            
        }
    }
}