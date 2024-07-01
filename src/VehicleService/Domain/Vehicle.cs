namespace Domain;

public class Vehicle
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public virtual Engine Engine { get; private set; }
    public Guid? ParkingPlaceId { get; internal set; }

    private Vehicle(){}

    public Vehicle(string name, Engine engine, Guid? parkingPlaceId = null)
    {
        Name = name;
        Engine = engine;
        ParkingPlaceId = parkingPlaceId;
    }

    public Vehicle(Guid id, string name, Engine engine, Guid? parkingPlaceId = null)
    {
        Id = id;
        Name = name;
        Engine = engine;
        ParkingPlaceId = parkingPlaceId;
    }

}
