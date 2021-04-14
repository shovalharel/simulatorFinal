# simulatorFinal

WPF Flight Gear Simulator App  
link to github –  צריך להכניס כאן קישור  
link to an explanatory video - https://youtu.be/c75q5-h0EP0  
# Summary:
An Interpreter project for a FlightGear flight simulator. The program connects to the simulator and allows us to view flight data on a dedicated simulator and explore it.
In this project we built an application that allows us to view flight data on a dedicated simulator and explore them. Our users are flight researchers or pilots who want to view data, sampled at a certain rate during any flight.
The flight data includes the rudder mode, speed, altitude direction, etc, and are recorded into a text file, which can be loaded in our app.
The app will play the data like a movie from the beginning of the recording to the end, it will graphically display the plane in relation to the earth, the rudder status, and additional flight data in a number of different views, including a view designed to find anomalies in the data.
# Collaborators
This program was developed by four student, Hila Shechter, Shoval Harel, Liron Weizman and Sapir Vaisman, CS students in Bar-Ilan university, Israel.
# Prerequisites
Windows environment to run the code
# Installing
Download and install the simulator on your computer https://www.flightgear.org/download/ 
Config the following settings in the 'Settings' tab in the simulator:
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null

This will open communication socket - 'in' where you send commands to the simulator.
Running
a.	Exceute the code using the terminal or any c# work environment. The program will wait for a connection from the simulator.
b.	Click the ‘CSV’ button, add anomaly_flight.csv file with all flight data.
c.	Click the ‘CSV Normal’ add the reg_flight.csv file.
d.	Click the ‘XML’ button, add playback_small.xml file with your desired features of the flight.
e.	Click the ‘Start’ button. 
f.	If pleased to show the graph of one of the features with his correlative feature graph and their line regression, choose one of the features from the list box in the left side of the window.
g.	If pleased to add an anomaly detection algorithm, click the 'Csv Normal' button and add a csv flight data with expected flight data. Then, click 'Open Dll' and add the algorithm dll file.
h.	If you want to add other algorithm dll, you need to implement the interface “Dll” – with the function “create” and “update”, you can take a look in our dll algorithm to see more details.

Documentation and general explanation of the structure of the folders and main files in the project:
The project designed by “MVVM” architect.
The View is the ‘MainWindow’ and he contain some of user controls:
•	‘JoyStick’ - Showing the main controls of the plane.
•	‘Graph’ - show the graph of chosen features with his correlative feature graph and their line regression.
•	‘Player’ – Scroll bar to move whenever you want, along with additional buttons to control the flight display.
•	‘DataFlay’ - show data of the aircraft.
•	‘RunControl’ – the buttons that load the files.
The main ViewModel is the ‘FGViewModel’.  In addition, any user control have his view model.
The view model is the  connecting layer between the model and the view.
We have one  model,  “FGmodel”,  that responsible for all the logic of the application.

structure project:
There are a few folders:
1. simulator -  the main folder with the solution project.
2. packages  - there is the Oxplot libary - you should have it in the project.
3. filesAndDiagrams -  the files that needed to upload, and the UML diagrams.
4. plugins - there is the dll library.





