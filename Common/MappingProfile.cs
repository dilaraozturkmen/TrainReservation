using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using TrainBooking.Entiities;
using static TrainBooking.Application.BookingOperations.CreateBooking.CreateBookingCommand;
using static TrainBooking.Application.BookingOperations.GetBooking.GetBookingQuery;
using static TrainBooking.Application.BookingWagonOperationsü.GetBookinWagon.GetBookingWagonQuery;
using static TrainBooking.Application.TrainOperations.GetTrains.GetTrainsQuery;
using static TrainBooking.Application.WagonOperations.GetWagon.GetWagonQuery;

namespace TrainBooking.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookingModel, Booking>();
            CreateMap<WagonsViewModel , Wagon >();
            CreateMap<Booking, BookingsViewModel>().ForMember(dest => dest.Train, opt => opt.MapFrom(src => src.train.Name));
            CreateMap<Train, TrainsViewModel>();
            CreateMap<Wagon, WagonsViewModel>();
            CreateMap<BookingWagon, BookingWagonViewModel>().ForMember(dest => dest.Wagon, opt => opt.MapFrom(src => src.Wagon.Name));
     
             

        }
    }
}
