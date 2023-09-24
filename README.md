<h4 align="center">Project Overview</h4>
<p align="center">This project serves as a demonstration of skills in architecture development, asynchronous and parallel programming, event-driven programming, OOP, integration of SQL database, and SignalR.</p>

## Description

**Airport Simulation**

The essence of the project is to simulate the operation of an airport. The airport consists of six key stages that an incoming flight from the outside must go through: entry into the airport zone, landing on the runway, terminal waiting, passenger boarding from the terminal, departure waiting, takeoff from the runway, and exiting the airport zone.

**Key Points**

- **Runway:** Only one flight can be on the runway at any given time, regardless of whether it is for takeoff or landing.
- **Terminal:** The terminal can accommodate up to two flights simultaneously.

**Technical Details**

The project consists of three parts:

1. **Interface Folder**: This folder contains a React application that serves as the user interface.

2. **Server Folder**: This folder contains the server-side code implemented using .NET technologies.

3. **Simulator Folder**: This folder contains a console .NET application that simulates the creation of incoming flights to the airport.

## Launch Instructions

#### Quick Launch In-Memory Mode (No Database):

1. Navigate to the **`"Server" folder`** and open the **`"Airport.sln" file`** using Microsoft Visual Studio, Visual Studio Code, or other .NET development environments.

2. Set up the **`"API"`** and **`"Simulator" projects`** as **`"Multiple startup projects"`** (For *Visual Studio:* right-click on the solution in Solution Explorer -> go to the **`"Configure Startup Project"`** tab -> select **`"Multiple startup projects"`** and set **`"Action"`** to **`"Start"`** for both **`"API" and "Simulator"`**).

- Alternatively, you can run the projects individually. Start the "**`API" project`** in the **`"Airport.sln" solution`** from the **`"Server" folder`**, and then start the **`"Simulator" project`** in the **`"Simulator.sln" solution`** from the **`"Simulator" folder`**.

3. Click the **`"Start" button`** and wait for two console windows to open. Additionally, a Swagger tab will open in your web browser or open it by [https://localhost:7296/swagger](https://localhost:7296/swagger) . One console window periodically sends flights to the server, while the other console window is the server. You can also choose to disable the simulator and manually send requests to the server via Swagger.

4. For visual representation, navigate to the **"Interface" folder**, open it in the **terminal (PowerShell/cmd)**, and run the command **"npm i"** to install the required dependencies. After that, you can run **"npm start"**. This will open a browser tab displaying the visual representation of what is happening on the server, or open it by [http://localhost:3000](http://localhost:3000).


#### Launch with Database (Add Database Connection):

Follow the same steps as above, but **before executing step 2**, go to the **`"appsettings.json" file`** in the **`"API" project`** (which is part of the **`"Airport.sln" solution`**) and uncomment the line in the **`"ConnectionStrings" section`** (remove the **`"//"`** at the beginning of the line). This will make the server work with a real database instead of an in-memory database.

