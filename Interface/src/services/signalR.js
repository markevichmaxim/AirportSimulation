import { HubConnectionBuilder } from '@microsoft/signalr';

const HUB_URL = 'https://localhost:7296/airportHub';
const EVENT = 'ReceiveUpdatedData';

/**
 * A service for managing the SignalR connection to the airportHub.
 */
const signalRService = {
  connection: null,
  startConnection: (onUpdate) => {
    signalRService.connection = new HubConnectionBuilder()
      .withUrl(HUB_URL)
      .build();

    signalRService.connection.on(EVENT, (updatedFlight) => {
      onUpdate(updatedFlight);
    });

    signalRService.connection
      .start()
      .catch((err) => {
        console.error('SignalR connection | ', err);
      });
  },
};

export default signalRService;
