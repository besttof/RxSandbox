public interface ICoffee
{
	float Price { get; }
}

public struct Espresso : ICoffee
{
	public float Price => 2.5f;
}

public struct Americano : ICoffee
{
	public float Price => 2.75f;
}

public struct Latte : ICoffee
{
	public float Price => 4.5f;
}

public struct Cortado : ICoffee
{
	public float Price => 3.5f;
}

public struct Cappuccino : ICoffee
{
	public float Price => 3.75f;
}