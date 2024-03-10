# Unity C# styleguide
해당 styleguide는 Unity에서 C# 코드 작성시 일관성과 가독성을 위해 작성함 

[Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/) 와 [C# at Google Style Guide](https://google.github.io/styleguide/csharp-style.html)를 참고해 작성한 [Unity Create a C# Style Guide](https://blog.unity.com/engine-platform/clean-up-your-code-how-to-create-your-own-c-code-style) 책을 기반으로 작성 함

## Naming conventions
식별자(identifier)란 타입(class, interface, struct, delegate, enum), 멤버, 변수 혹은 namespace를 의미. 식별자는 문자로 시작하거나 언더스코어(_)로 시작해야 하며 특수문자들은 쓰지 않아야 함

- camel Case
  - **지역 변수(Local Variables)** , **함수 파라미터(Method Parameters)** camel 형식으로 작성

- _camel Case
  - **Private 멤버 변수(Private Member Variables)** 는 under score(_)를 prefix로 넣은 _camel Case 형식으로 작성 

- Pascal case
  - **클래스(Class)**, **함수(Method)**, **Enums** , **네임스페이스(Namespaces)** , **Public 필드(Public Field)** 는 Pascal 형식으로 작성

- IPascal case
  - **인터페이스(Interface)** 는 'I'를 prefix로 넣은 Pascal 형식으로 작성 

### Fields and Variables
- 변수 이름은 명사를 사용 하지만 bool type의 변수는 명사 이름 앞에 동사형 Prefix(is, has, ..)를 넣어라
  - Ex] bool type : isDead, hasDamage
- 변수 이름을 무의미하게 줄이지 마라
- 루프문에는 Single Letter(i, j, ..) 사용이 허가되나 나머지 변수들엔 사용하지 마라
- 접근자를 사용하지 않으면 default로 private이 되지만 private을 기입하라 
- Public과 Private 멤버 변수는 따로 구분해 그룹화해라
- 한 줄에 하나의 변수만 선언 하라
- var는 사용하지 마라

```
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
```

### Enums
단순 명사를 이름으로 사용하라
```
public enum WeaponType
{
    Knife,
    Gun,
    RocketLauncher
}

public enum FireMode
{
    None = 0,
    Single = 5,
    Burst = 7
}
```

### Classes and interfaces
- Monobehaviour를 가진다면 파일 이름과 클래스 이름은 같아야 한다
- Interface는 형용사 이름으로 만든다
```
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
```

### Methods
- 동사로 시작하라 
- Return 값이 bool 형일시 질문하는 형식으로 이름을 만들어라
  - Ex] IsGameOver, HasStartedTurn

### Events and event handlers
- 동사를 포함하는 이름을 사용하라
- 이벤트가 사건의 전후 언제인지 시점을 나타낼수 있는 이름을 사용하라
  - Ex] OpeningDoor, Door Opened
```
public event Action OpeningDoor;    // event before
public event Action DoorOpened;     // event after
```

- 이벤트를 호출하는 함수에는 **"On"** Prefix를 사용하라
```
// raises the Event if you have subscribers
public void OnDoorOpened()
{
    DoorOpened?.Invoke();
}
```

- 이벤트를 받는 함수의 이름은 **[이벤트를 보내는 클래스 이름]_[이벤트 이름]** 형식으로 작성한다
  - Ex] GameEvents_OpeningDoor, GameEvents_DoorOpened
- Custom EventArgs는 필요한 경우에만 생성한다 
  - custom data를 보내야만 하는 상황이라면 새로운 type의 EventArgs를 만든다
```
// EXAMPLE: read-only, custom struct used to pass an ID and Color
public struct CustomEventArgs
{
    public int ObjectID { get; }
    public Color Color { get; }

    public CustomEventArgs(int objectId, Color color)
    {
        this.ObjectID = objectId;
        this.Color = color;
    }
}
```

### Namespaces
- Pascal형식을 사용하고 특별 기호나 _ 등은 사용하지 않는다


## Formatting

### Properties
Property들은 public member variable처럼 행동하나 그들은 [accessor](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties)라 불리는 특별한 함수이다
각각의 property는 get과 set이라는 private field([backing field](https://learn.microsoft.com/en-us/ef/core/modeling/backing-field?tabs=data-annotations))에 접근할수 있는 함수를 가진다

- read-only property인 경우 expression-bodied(=>)를 사용해 한줄을 만든다 
```
public class PlayerHealth
{
    // the private backing field
    private int _maxHealth;

    // read-only, returns backing field
    public int MaxHealth => _maxHealth;
}
```

- 나머지는 { get; set; } syntax를 사용
```
public class PlayerHealth
{
    // backing field
    private int _maxHealth;

    // explicitly implementing getter and setter
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    // write-only (not using backing field)
    public int Health { private get; set; }

    // write-only, without an explicit setter
    public SetMaxHealth(int newMaxValue) => _maxHealth = newMaxValue;
}
```

### Serialization
Serialization은 데이터 구조나 Object를 Unity가 저장하고 재구성할 수 있는 형태로 바꾸는 것. Performance의 이유로 Unity는 Serialization을 다른 것들과는 다르게 다룸

- Inspector에 변수를 드러내기만을 위한 것이라면 [SerializeField] Attribute를 사용하라
- 최솟값과 최대값의 범위가 있는 값의 경우 [Range(min, max)] attribute를 사용해서 값의 범위에 limit을 둬라 
- public class or struct를 Inspector에 나타내야하는 경우엔 해당 class 혹은 struct를 [Serializable] 해라
```
public class Player : MonoBehaviour
{
    [Serializable]
    public struct PlayerStats
    {
        public int MovementSpeed;
        public int HitPoints;
    }

    [SerializeField]
    private PlayerStats _stats;
}
```

### 괄호(Brace) 와 들여쓰기(indentation) style
- 괄호의 경우 [Allman style](https://en.wikipedia.org/wiki/Indentation_style#Allman_style)을 사용한다
- 한줄의 코드라 할지라도 괄호를 무조건 넣어야 한다
```
for (int i = 0; i < 100; i++) { DoSomething(i); }
```
- 들여쓰기시 Space가 아닌 Tab을 사용한다 

### switch 문
switch문의 경우 아래와 같은 Format으로 작성한다
default문을 항상 넣어준다
```
switch (someExpression)
{
    case 0:
        DoSomething();
        break;
    case 1:
        DoSomethingElse();
        break;
    default:

        break;
}
```

### Horizontal spacing
code 밀도를 낮추기 위해 space를 넣어라
```
for (int i = 0; i < 100; i++) { DoSomething(i); }
```

- 콤마(,) 다음은 스페이스를 넣어라
```
CollectionItem(myObject, 0, 1);
```

- 함수 파라미터 작성 전/후에는 스페이스를 넣지 마라
```
// GOOD
DropPowerUp(myPrefab, 0, 1);

// BAD
DropPowerUp( myPrefab, 0, 1 );
```

- 함수 이름과 괄호() 사이에 스페이스를 넣지 마라
- 대괄호 안에 전/후 스페이스 넣지 마라
```
// BAD
x = dataArray[ index ];
```

- if문과 switch문과 같은 코드의 flow를 컨트롤하는 코드의 조건문 에는 연산자 전/후에 스페이스를 넣어라
```
// GOOD
if (x == y)

// GOOD
switch (x > y)
```
- 한줄은 짧은것이 좋음 한줄에는 최대 80~120 개 정도의 글자만 존재하게 한다

- 변수의 타입과 이름 사이에는 하나의 스페이스만 넣는다 column alignment는 피한다
```
// GOOD
public float Speed = 12f;
public float Gravity = -10f;
public LayerMask GroundMask;

// BAD
public float        Speed = 12f;
public float        Gravity = -10f;
public LayerMask    GroundMask;
```
### Vertical spacing
- 유사한 함수들 이거나 서로 Dependent가 있는 함수는 가까이 위치시킨다
- 아래와 같은 상황일때는 2개의 blank를 사이에 둔다
  - 변수 선언 부분과 함수 사이 
  - Class와 Interface 사이 
- Region을 써서 코드를 숨기기 보다는 Class를 잘게 쪼개라


## Classes
class는 되도록이면 작게 만들어야 함 <br>
역피라미드 방식으로 top-down형식의 구조를 가져야 함

### Class organization
- Class
  - Fields
  - Properties
  - Events / Delegates
  - Monobehaviour Methods (Awake, Start, OnEnable, ..)
  - Public Methods
  - Private Methods


## Methods
함수도 클래스처럼 single responsibility를 가지고 작게 유지하는것이 좋다<br>
각각의 함수들은 하나의 기능 혹은 하나의 질문에 대한 해답이어야 함<br>
함수이름은 그 함수가 무슨 일을 하는지 잘 나타내는 것이 좋음

- 적은 전달인자(Argument)를 가지는 함수가 좋다. 
  - 전달인자가 많으면 복잡도를 높인다
- flag 전달인자를 쓰지 마라
  - boolean 형식의 flag 전달인자는 Single-Responsibility를 해치니 가급적 사용하지 말고 flag 인자를 사용하기보다는 새로운 함수를 만들어라 


## Comments
코드가 쉽게 소화시킬 정도로 작게 쪼개져있고 잘 지은 이름들로 이뤄져 있다면 주석은 필요하지 않다<br>
주석을 달때는 "무엇(what)"에 대한 것인지 보다는 "왜(why)" 인지에 대한 주석이 유용하다

- 주석을 달때는 새로운 줄에 작성해라.
  - 코드 옆에 달지 마라
- 되도록이면 더블 슬래쉬(//)의 한줄 주석을 달아라. 
  - 여러 줄의 주석은 지양하고 주석은 되도록 code와 가깝게 둬라
- serialized field엔 주석보단 tooltip을 사용해라
  - Inspector에 있는 Field가 설명이 필요할 때 tooltip을 사용
- public 함수에는 XML Tag 형식의 주석을 다는것도 괜찮다
- 더블 슬러쉬(//)와 주석 사이에는 스페이스 하나를 넣어라
- 주석 작성시 첫글자는 대문자를 사용하고 마지막엔 마침표(.)를 넣어라
- 별표(*)와 같은 특수문자를 주석에 쓰지 마라
- Code제거시 그 코드와 관련된 주석이 있다면 같이 지워라
- 주석은 일기가 아니다 Log와 같은 주석을 쓰지마라

## Appendix
### Camel Case
첫 단어는 소문자로 시작해 다른 단어가 시작될 때는 대문자로 작성하는 형식 
<br>
Example] examplePlayerController, maxHealthPoints

### Pascal Case
첫 단어를 대문자로 시작해 다른 단어가 시작될 때마다 대문자로 작성하는 형식 
<br>
Example] ExamplePlayerController, MaxHealthPoints

### Field
class나 struct type의 변수(variable)를 field라 부름 

