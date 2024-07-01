namespace Domain;

public class Parking
{
    public Guid Id { get; private set; }
    public int FloorCount { get; private set; }
    public int PlacesPerFloor { get; private set; }
    public string Address { get; private set; }
    public Dictionary<int, List<ParkingPlace>> Floors { get; } = new();
    public ICollection<ParkingPlace> ParkingPlaces { get; private set; } = new List<ParkingPlace>();

    private Parking()
    {

    }

    public Parking(int maxFloor, int placesPerFloor, string address)
    {
        FloorCount = maxFloor;
        if (FloorCount < 0)
        {
            throw new ArgumentException("Floor count can't be lower than zero");
        }

        PlacesPerFloor = placesPerFloor;
        if (PlacesPerFloor < 0)
        {
            throw new ArgumentException("Places per floor can't be lower than zero");
        }


        Address = address;
        Floors = new();
        ParkingPlaces = new List<ParkingPlace>();

        for (int floor = 0; floor < FloorCount; floor++)
        {
            List<ParkingPlace> places = new(PlacesPerFloor);
            for (int i = 0; i < PlacesPerFloor; i++)
            {
                var parkingPlace = new ParkingPlace(floor, this);
                places.Add(parkingPlace);
                ParkingPlaces.Add(parkingPlace);
            }
            Floors.Add(floor, places);
        }
    }

    public void AddVehicle(Guid? vehicleId)
    {
        var parkingPlace = ParkingPlaces.Where(p => p.VehicleId == null).FirstOrDefault();
        if (parkingPlace == null)
        {
            throw new Exception("There are no free places");
        }
        parkingPlace.VehicleId = vehicleId;
    }
}
