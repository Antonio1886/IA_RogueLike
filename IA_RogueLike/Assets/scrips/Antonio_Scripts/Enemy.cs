public class Enemy
{
    public int Health { get; private set; }
    public int AttackDamage { get; private set; }
    public float AttackRange { get; private set; }
    public bool CanAttack { get; private set; }

    private float attackCooldown;
    private float lastAttackTime;

    public Enemy(int health, int attackDamage, float attackRange, float attackCooldown)
    {
        Health = health;
        AttackDamage = attackDamage;
        AttackRange = attackRange;
        this.attackCooldown = attackCooldown;
        lastAttackTime = -attackCooldown; // Listo para atacar de inmediato
        CanAttack = true;
    }

    public void ReceiveDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public bool IsTargetInRange(float distanceToTarget)
    {
        return distanceToTarget <= AttackRange;
    }

    public bool TryAttack(float currentTime)
    {
        if (!CanAttack || currentTime - lastAttackTime < attackCooldown)
            return false;

        lastAttackTime = currentTime;
        return true;
    }

    public void SetCanAttack(bool value)
    {
        CanAttack = value;
    }
}
