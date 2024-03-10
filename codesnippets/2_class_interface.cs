// EXAMPLE: Class formatting
public class ExampleClass : MonoBehaviour
{
    public int PublicField;
    public static int MyStaticField;

    private int _packagePrivate;
    private static int _myPrivate;

    protected int _myProtected;

    public void DoSomething()
    {

    }
}

// EXAMPLE: Interfaces
public interface IKillable
{
    void Kill();
}

public interface IDamageable<T>
{
    void Damage(T damageTaken)
}