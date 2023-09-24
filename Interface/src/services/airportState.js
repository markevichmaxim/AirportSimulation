const URL = 'https://localhost:7296/airport/get-state';

/**
 * Retrieves and sorts airport state data from the server.
 */
function getState() {
  return fetch(URL)
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      const sortedData = data.sort((a, b) => b.status - a.status);
      return sortedData;
    })
    .catch((err) => console.error('Airport State | ' + err));
}

/**
 * Updates the airport state data with the provided updated flight.
 */
function updateState(prevData, updatedFlight) {
  const updatedFlightIndex = prevData.findIndex(
    (flight) => flight.id === updatedFlight.id
  );

  if (updatedFlightIndex === -1) {
    return [...prevData, updatedFlight];
  }

  if (updatedFlight.status === 0) {
    return prevData.filter((flight) => flight.id !== updatedFlight.id);
  }

  const newData = [...prevData];
  newData[updatedFlightIndex] = updatedFlight;
  newData.sort((a, b) => b.status - a.status);
  return newData;
}

export { getState, updateState };
