using Core.Models;
using Core.Models.Enums.Airport;
using Core.Models.Enums.Flight;

namespace Service.Airport.Processing
{
    public class FlightManager
    {
        public void SwitchStage(Flight flight)
        {
            switch (flight.Status)
            {
                case Status.WaitingToLand:
                    flight.Facility = Facility.Runway;
                    flight.Status = Status.Landing;
                    break;
                case Status.Landing:
                    flight.Facility = Facility.BoardingQueue;
                    flight.Status = Status.WaitingToBoard;
                    break;
                case Status.WaitingToBoard:
                    flight.Facility = Facility.Terminal;
                    flight.Status = Status.Boadring;
                    break;
                case Status.Boadring:
                    flight.Facility = Facility.TakingoffQueue;
                    flight.Status = Status.WaitingToTakeoff;
                    break;
                case Status.WaitingToTakeoff:
                    flight.Facility = Facility.Runway;
                    flight.Status = Status.Takingoff;
                    break;
                case Status.Takingoff:
                    flight.Facility = Facility.DepartingQueue;
                    flight.Status = Status.WaitingToDeparture;
                    break;
                case Status.WaitingToDeparture:
                    break;
                default:
                    flight.Facility = Facility.LandingQueue;
                    flight.Status = Status.WaitingToLand;
                    break;
            }
        }
    }
}
