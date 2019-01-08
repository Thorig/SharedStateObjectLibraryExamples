using GameLib.Entity.Animation;
namespace GameLib.Entity.Behaviour.State
{
    public class Attack : AbstractState
    {
        public override void animationMessage(int messageId, IEntity entity)
        {
            entity.getTransform().gameObject.GetComponent<Player>().AnimationAttributes.setCooldown();
            entity.setState(entity.getBehaviourStateFactory().getIdleState(entity));
        }

        public override void init(IEntity entity)
        {
            entity.setMoving(false);
            switchAnimation(AnimationAttributes.ANIMATION_ATTACK, entity);
        }

        public override void update(IEntity entity)
        {
            
        }
    }
}