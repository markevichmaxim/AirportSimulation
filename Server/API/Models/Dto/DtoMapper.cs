using Core.Models;

namespace API.Models.Dto
{
    public class DtoMapper
    {
        public Flight DtoToModel(FlightDto dto)
        {
            return new Flight()
            {
                SerialNumber = dto.SerialNumber,
                ArrivalTime = dto.ArrivalTime,
                DepartureTime = dto.DepartureTime,
                Id = 0,
                Status = 0,
                Facility = 0,
            };
        }
    }
}
