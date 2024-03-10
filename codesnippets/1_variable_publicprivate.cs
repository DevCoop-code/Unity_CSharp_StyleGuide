// EXAMPLE: public and private variables

// public member variables
public float DamageMultiplier = 1.5f;
public float MaxHealth;
public bool IsInvincible;

// private member variables
private bool _isDead;
private float _currentHealth;

public void InflictDamage(float damage, bool isSpecialDamage)
{
    // local Variable
    int totalDamage = damage;

    // local variable versus public member variable
    if (isSpecialDamage)
    {
        totalDamage *= DamageMultiplier;
    }

    // local variable versus private member variable
    if (totalDamage > _currentHealth)
    {

    }
}

