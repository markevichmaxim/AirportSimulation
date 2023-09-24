import { useState, useEffect } from 'react';
import Flight from './Flight';
import styles from './Table.module.css';
import { getState, updateState } from '../../services/airportState';
import signalRService from '../../services/signalR';

function Table() {
  const [flightBoard, setFlightBoard] = useState([]);
  const [highlightedRows, setHighlightedRows] = useState([]);

  useEffect(() => {
    getState().then((data) => setFlightBoard(data));

    signalRService.startConnection((updatedFlight) => {
      setFlightBoard((prevData) => {
        const updatedData = updateState(prevData, updatedFlight);

        highlightRows(updatedFlight);

        return updatedData;
      });
    });
  }, []);

  const highlightRows = (updatedFlight) => {
    setHighlightedRows((prevHighlightedRows) => [
      ...prevHighlightedRows,
      updatedFlight.id,
    ]);

    setTimeout(() => {
      setHighlightedRows((prevHighlightedRows) =>
        prevHighlightedRows.filter((id) => id !== updatedFlight.id)
      );
    }, 1600);
  };

  return (
    <table className={styles.table}>
      <thead className={styles.tableHeading}>
        <tr>
          <th>Id</th>
          <th>Serial Number</th>
          <th>Arrival Time</th>
          <th>Departure Time</th>
          <th>Status</th>
          <th>Facility</th>
        </tr>
      </thead>
      <tbody className={styles.tableBody}>
        {flightBoard &&
          flightBoard.map((flight) => (
            <tr
              key={flight.id}
              className={
                highlightedRows.includes(flight.id)
                  ? styles.highlightedRow
                  : styles.rowReset
              }
            >
              <Flight flight={flight} />
            </tr>
          ))}
      </tbody>
    </table>
  );
}

export default Table;
