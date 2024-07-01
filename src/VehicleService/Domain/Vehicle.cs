namespace Domain;

public class Vehicle
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int MaxSpeed { get; private set; }
    public Engine Engine { get; private set; }
    public Guid? ParkingPlaceId { get; internal set; }

    private Vehicle(){}

    public Vehicle(string name, int maxSpeed, Engine engine, Guid? parkingPlaceId = null)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        if (MaxSpeed <= 0)
        {
            throw new ArgumentException("Max speed can't be lower or equal zero");
        }
        Engine = engine;
        ParkingPlaceId = parkingPlaceId;
    }

    public Vehicle(Guid id, string name, int maxSpeed, Engine engine, Guid? parkingPlaceId = null)
    {
        Id = id;
        Name = name;
        MaxSpeed = maxSpeed;
        if (MaxSpeed <= 0)
        {
            throw new ArgumentException("Max speed can't be lower or equal zero");
        }
        Engine = engine;
        ParkingPlaceId = parkingPlaceId;
    }
}
