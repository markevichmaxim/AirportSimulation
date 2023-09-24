import facilityMap from "../../enums/facility";
import statusMap from "../../enums/status";

function Flight({ flight }) {
  const arrivalTime = new Date(flight.arrivalTime);
  const departureTime = new Date(flight.departureTime);

  const formatDate = (date) => {
    const options = { month: 'short', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    return date.toLocaleDateString('en-En', options);
  };

  const status = statusMap[flight.status];
  const facility = facilityMap[flight.facility];

    return (
      <>
        <td>{flight.id}</td>
        <td>{flight.serialNumber}</td>
        <td>{formatDate(arrivalTime)}</td>
        <td>{formatDate(departureTime)}</td>
        <td>{status}</td>
        <td>{facility}</td>
      </>
    );
  }
  
  export default Flight;
  