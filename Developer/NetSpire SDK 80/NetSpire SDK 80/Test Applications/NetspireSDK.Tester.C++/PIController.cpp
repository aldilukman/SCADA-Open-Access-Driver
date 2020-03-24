// NetSpire SDK Sample Program. 
// Name: 	Update Service Information.
// Description: 
// Language	C++

// This sample application demonstrates how the automatic message
// generation functionality of the PassengerInformationController can be enabled
// using only limited prediction information, without creating a comprehensive
// and consistent Trip, TripStop and Service data structure.

// In the example below, only the predicted arrival and departure times for
// each station, the destination stop and the approaching/on platform state of 
// services are provided to the PassengerInformationController.
// Providing additional information including delays, cancellation, and complete
// stopping pattern allows the system to display and announce messages that include
// the additional information.

// Standard Includes
#include <stdio.h>
#include <string>
#include <stdlib.h>
#include <iostream>
#include <ctime>

// Open Access Includes
#include "StandardIncludes.hpp"
#include "AudioServer.hpp"
#include "AudioServerObserver.hpp"

/***************************************************************************/
void CreateLines(PassengerInformationServer* piServer)
{
	// Create sample Lines - this is an optional step and line information
	// does not need to be set unless line specific behaviour is required.
	Line line1(5);
	line1.setName("Gawler");
	piServer->addLine(line1);

	Line line2(12);
	line2.setName("Outer Harbor");
	piServer->addLine(line2);
}

/***************************************************************************/
void CreateVehicles(PassengerInformationServer* piServer)
{
	// Create vehicles - this is an optional step and vehicle information does
	// not need to be set unless real-time vehicle state is available.

	int vehicleIDs[] = { 101, 102, 103, 104, 105, 106, 107, 108, 109, 110 };
	for (int i = 0; i < sizeof(vehicleIDs) / sizeof (int); ++i)	// Assume 10 sample trains
	{
		int vehicleId = vehicleIDs[i];	// The vehicle ID would typically be 
										// looked up from a list of available vehicles
		Vehicle vehicle(vehicleId);
		piServer->addVehicle(vehicle);
	}
}

/***************************************************************************/
void CreateStationsAndPlatforms(PassengerInformationServer* piServer)
{
	// Create stations and platforms
	char* stationNames[] = { (char *)"Station1", (char *)"Station2", (char *)"Station3", (char *)"Station4" };
	int numStations = sizeof(stationNames) / sizeof(char*);

	for (int i = 0; i < numStations; ++i)
	{
		std::string name = stationNames[i];

		Station station(i, name, true, false);

		int platformNumbers[] = { 1, 2, 3, 4 };
		char* platformLocations[] = { (char *)"P1", (char *)"P2", (char *)"P3", (char *)"P4" };
		for (int j = 0; j < sizeof(platformNumbers) / sizeof(int); ++j)
		{
			int platformId = i * 10 + platformNumbers[j];	// A unique number for each platform in the system
			PlatformInfo plat(platformId);
			std::string platLocation = name + platformLocations[j];	// Typically looked up from a data store
			plat.setLocation(platLocation);

			station.setPlatform(platformId, plat);
		}

		piServer->addStation(station);
	}
}

/***************************************************************************/
// Generates sample service information that cna be used without creating a
// comprehensive Trip, TripStop and Service data structure.
// The following function demonstrates how per platform prediction information
// can be sent to the PassengerInformationController to allow the system show
// and announce information on the next <numServices> services.
// In the example below, only the predicted arrival and departure times for
// each station, as well as the destination stop is provided to the
// PassengerInformationController. Providing additional information including 
// delays, cancellation, and stopping pattern allows the system to display and
// announce messages that include this information.
std::list<Service> GenerateServices(int platformId, int numServices)
{
	static int lastService = 0;
	static int lastServiceStop = 0;
	int terminatingStops[] = { 4, 14, 24, 34 };
	time_t now = time(0);

	std::list<Service> nextServices;

	for (int i = 0; i < numServices; ++i)
	{
		Service service(lastService++);

		service.setLineId(12);	// optional
		service.setDirection(Trip::ASCENDING);	// optional
		service.setVehicleId(101 + i);	// optional
		service.setState(Service::RUNNING);

		if (i == 0)
		{
			// In this example, the first service is marked as "APPROACHING_STATION".
			service.setState(Service::APPROACHING_STATION);
		}

		// Set the current and following ServiceStops - the last stop is used as the destination station name
		std::list<ServiceStop> nextStops;
		
		// Current stop
		ServiceStop currentStop(lastServiceStop++);
		currentStop.setPlatformId(platformId);
		currentStop.setIsStopping(true);
		currentStop.setArvTime(now + (i * 240) + 60);
		currentStop.setDepTime(now + (i * 240) + 120);
		nextStops.push_back(currentStop);

		// Current stop is followe dby other stops in the stopping pattern.
		// This can be empty if stopping pattern display is not required.

		// Destination stop is the last entry of the stopping pattern. If the stopping pattern contains the current 
		// stop only, the current stop will be shown as the destination.
		ServiceStop destination(lastServiceStop++);
		destination.setPlatformId(terminatingStops[i]);
		destination.setIsStopping(true);
		// Arrival and departure time at destination are only required if display of arrival/departure time at destination
		// is required.
		nextStops.push_back(destination);

		//service.setStops(nextStops);
		nextServices.push_back(service);
	}

	return nextServices;
}

/***************************************************************************/
void UpdateServiceLocations(PassengerInformationServer* piServer)
{
	std::vector<Station> stations = piServer->getStations();
	for (std::vector<Station>::iterator stationIT = stations.begin(); stationIT != stations.end(); stationIT++)
	{
		std::vector<PlatformInfo> platforms = stationIT->getListOfPlatforms();
		for (std::vector<PlatformInfo>::iterator platformsIT = platforms.begin(); platformsIT != platforms.end(); platformsIT++)
		{
			//std::list<Service> nextServices = GenerateServices(platformsIT->getId(), 3);
			//platformsIT->setPassers(nextServices);
		}
	}
}

/***************************************************************************/
void UpdatePIController(PassengerInformationServer* piServer)
{
	// Create Lines, Vehicles, Stations and Platforms. 
	// These are typically static information and changes rarely.
	CreateLines(piServer);
	CreateVehicles(piServer);
	CreateStationsAndPlatforms(piServer);

	// Start automatic message updates
	piServer->enableAutomaticMessageGeneration();

	for (int count = 0; count < 100; ++ count)
	{
		// Send service arrival/departure prediction per platform
		std::cout << "count: " << count << " sending service arrival/departure prediction per platform" << std::endl;
		UpdateServiceLocations(piServer);
#ifdef WIN32
		Sleep(60000);
#else
		sleep(60);
#endif
	}
}

