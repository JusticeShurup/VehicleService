namespace Domain;

public class Vehicle
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int MaxSpeed { get; private set; }
    public ParkingPlace? ParkingPlace { get; internal set; }

    public Vehicle()
    {

    }

    public Vehicle(string name, int maxSpeed, ParkingPlace? parkingPlace = null)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        if (MaxSpeed <= 0)
        {
            throw new ArgumentException("Max speed can't be lower or equal zero");
        }

        ParkingPlace = parkingPlace;
    }

    public Vehicle(Guid id, string name, int maxSpeed, ParkingPlace? parkingPlace = null)
    {
        Id = id;
        Name = name;
        MaxSpeed = maxSpeed;
        if (MaxSpeed <= 0)
        {
            throw new ArgumentException("Max speed can't be lower or equal zero");
        }

        ParkingPlace = parkingPlace;
    }
}
