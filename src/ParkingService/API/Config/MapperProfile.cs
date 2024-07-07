using AutoMapper;
using Domain;
using Google.Protobuf.WellKnownTypes;
using ProtosContract;

namespace API.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(x => x.ParkingStartDate, s => s.MapFrom(x => Timestamp.FromDateTime(x.ParkingStartDate)))
                .ForMember(x => x.ParkingEndDate, s => s.MapFrom(x => Timestamp.FromDateTime(x.ParkingEndDate)))
                .ReverseMap();
            CreateMap<ParkingPlace, ParkingPlaceDto>().ReverseMap();
            CreateMap<Domain.Parking, ParkingDto>().ReverseMap();
        }
    }
}
