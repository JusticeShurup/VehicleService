namespace Domain;

public class Vehicle
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public virtual Engine Engine { get; private set; }
    
    private Vehicle(){}

    public Vehicle(string name, Engine engine)
    {
        Name = name;
        Engine = engine;
        engine.Vehicle = this;
    }

    public Vehicle(Guid id, string name, Engine engine)
    {
        Id = id;
        Name = name;
        Engine = engine;
        engine.Vehicle = this;
    }

}
