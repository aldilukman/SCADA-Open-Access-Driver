#ifndef _SIGNALLING_CONTROLLER_HPP_
#define _SIGNALLING_CONTROLLER_HPP_

#include "StandardIncludes.hpp"
#include "AudioServer.hpp"
#include "zlib.h"
#include "base64.h"

typedef std::string LocationId;

/*******************************************************************************************
********************************* General Information *************************************
*******************************************************************************************
* NamedId is a base class for all the other classes that require a name and abbreviation for their objects.
*******************************************************************************************/
class NamedId
{
public:
	// Need default constructor for Java SWIG to compile java_wrap.cxx
	NamedId() : _id(0), _name(""), _abrv("") { };

	// The constructor which also initialises the numeric ID
	NamedId(int id) : _id(id) { };

	// Get the numeric ID
	int getId() { return this->_id; }

	// set/get functions for the name of the object
	void setName(const std::string &name) { this->_name = name; }
	std::string getName() { return this->_name; }

	// set/get functions for the abbreviation of the object
	void setAbrv(const std::string &abrv) { this->_abrv = abrv; }
	std::string getAbrv() { return this->_abrv; }

protected:
	int _id;
	std::string _name;
	std::string _abrv;
};

/*******************************************************************************************
* Line is only a logical concept which includes a set of stations that are normally bind 
* together to form a trip.
* Line does not have any functional application in Passenger Information interface.
* It includes a name that could be shown as an extra information to passengers.
*******************************************************************************************/
typedef NamedId Line;

/*******************************************************************************************
************************************* TripStop  *******************************************
*******************************************************************************************
* A location where a scheduled trip can stop. Examples are particular platforms of a train 
* station or bus stop.
* Trips define a particular stopping pattern, which is typically repeated over a day to form services.
* When a trip is defined by rail authorities, each of the stops has two scheduled time-offset
* which defines how long after trip start the vehicle is supposed to arrive/depart to/from this stop.
*******************************************************************************************/
// 06112014 - AN and HM communicated and it was resolved that in future class TripStop should be inherited from
// class Identification (instead of class NamedId) which only has a numeral Id.
class TripStop : public NamedId
{
public:
	// Need default constructor for Java SWIG to compile java_wrap.cxx
	TripStop() { };

	// The constructor which initialises the member variables.
	TripStop(int id);

	// The constructor which initialises the member variables.
	TripStop(int id, int platformId, bool isStopping, long arrivalTimeOffset, long departureTimeOffset);

	// set/get functions for the Trip Id
	void setTripId(int tripId) { this->_tripId = tripId; }
	int getTripId() { return this->_tripId; }

	// set/get functions for the platorm Id within the station where vehicle has stopped or passed.
	void setPlatformId(int platformId) { this->_platformId = platformId; }
	int getPlatformId() { return this->_platformId; }

	// set/get functions to indicates if the vehicle stops at the station or passes through
	void setIsStopping(bool isStopping) { this->_isStopping = isStopping; }
	bool isStopping() { return this->_isStopping; }

	// set/get functions for the arrival time offset in seconds from startTime
	void setSchArvOffset(unsigned long schArvOffset) { this->_schArvOffset = schArvOffset; }
	unsigned long getSchArvOffset() { return this->_schArvOffset; }

	// set/get functions for the departure time offset in seconds from startTime
	void setSchDepOffset(unsigned long schDepOffset) { this->_schDepOffset = schDepOffset; }
	unsigned long getSchDepOffset() { return this->_schDepOffset; }

protected:
	// Back pointer to owner Trip - no getters for API.
	int _tripId;

	// The platform within the station where vehicle stops/passes
	int _platformId;

	// specifies whether the vehicle stops in the station or just passes through
	bool _isStopping;

	// Arrival time offset in seconds, from startTime
	unsigned long _schArvOffset;

	// Departure time offset in seconds, from startTime
	unsigned long _schDepOffset;
};

/*******************************************************************************************
********************************* ServiceStop *********************************************
*******************************************************************************************
* A stop with dynamic arrival and departure time information.
* Each service is an instance of a trip with a specific start time, and arrival/departure times defined for each stop.
* The delays for each stop will be separately settable
*******************************************************************************************/
class ServiceStop : public TripStop
{
public:
	// Need default constructor for Java SWIG to compile java_wrap.cxx
	ServiceStop() { };

	// The constructor requires the station and platform IDs to be initialised from the very beginning
	ServiceStop(int id);

	ServiceStop(int id, int serviceId, int tripStopId);

	// set/get functions for the service Id
	void setServiceId(int serviceId) { this->_serviceId = serviceId; }
	int getServiceId() { return this->_serviceId; }

	// set/get functions for the trip stop Id
	void setTripStopId(int tripStopId) { this->_tripStopId = tripStopId; }
	int getTripStopId() { return this->_tripStopId; }

	// set/get functions for the time when the vehicle started the trip in the first station
	void setServiceStartTime(unsigned long serviceStartTime) { this->_serviceStartTime = serviceStartTime; }
	unsigned long getServiceStartTime() { return this->_serviceStartTime; }

	// set/get functions for the amount of delay expected for the vehicle to arrive the station
	void setArvDelay(unsigned long arvDelay) { this->_arvDelay = arvDelay; }
	unsigned long getArvDelay() { return this->_arvDelay; }

	// set/get functions for the amount of delay expected for the vehicle to depart the station
	void setDepDelay(unsigned long depDelay) { this->_depDelay = depDelay; }
	unsigned long getDepDelay() { return this->_depDelay; }

	// set/get functions for the Return the estimated arrival time. This is calculated as startTime + arvOffset + arrival_delay
	void setArvTime(unsigned long arvTime) { setArvDelay(arvTime - this->_serviceStartTime - this->_schArvOffset); }
	unsigned long getArvTime() { return (this->_serviceStartTime + this->_schArvOffset + this->_arvDelay); }

	// set/get functions for the estimated departure time. This is calculated as startTime + depOffset + departure_delay
	void setDepTime(unsigned long depTime) { setDepDelay(depTime - this->_serviceStartTime - this->_schDepOffset); }
	unsigned long getDepTime() { return (this->_serviceStartTime + this->_schDepOffset + this->_depDelay); }

	// This function sets both arrival and departure delay to same value.
	void setDelay(unsigned long delay) { setArvDelay(delay); setDepDelay(delay); }

protected:
	// Back pointer to owner Service. Used internally; no need for getters in API.
	int _serviceId;

	// Used internally; no need for getters in API.
	int _tripStopId;

	// startTime will be identical in all ServiceStops in a service and indicates the time when the vehicle started the trip in the first station
	unsigned long _serviceStartTime;

	// the amount of delay expected for the vehicle to arrive the station
	unsigned long _arvDelay;

	// the amount of delay expected for the vehicle to depart the station
	unsigned long _depDelay;
};

/*******************************************************************************************
****************************************** Trip *******************************************
*******************************************************************************************
* Trip is a stopping pattern, which is typically repeated over a day to form services.
* This is normally defined by rail authorities for long term.
*******************************************************************************************/
class Trip : public NamedId
{
public:
	// Datatype describing the direction of the vehicle
	enum Direction
	{
		DESCENDING,
		ASCENDING
	};

	// A datatype which shows what is the stopping pattern for the trip over a line
	enum Priority
	{
		ALL_STOPS,      // vehicle stops in all the stations
		LIMITED_STOPS,  // vehicle will not stop in a few stations in the line
		EXPRESS         // vehicle only stops at a few stations in the line
	};

	// Need default constructor for Java SWIG to compile java_wrap.cxx
	Trip() { };

	// Constructor. the numeric Trip ID should be defined here and it won't be editable afterwards
	Trip(int id);

	Trip(int id, Direction direction, Priority priority);

	// set/get functions for the line to which the trip belongs
	void setLineId(int lineId) { this->_lineId = lineId; }
	int getLineId() { return this->_lineId; }

	// set/get functions for direction of the vehicle
	void setDirection(Direction direction) { this->_direction = direction; }
	Direction getDirection() { return this->_direction; }

	// set/get functions for priority of the vehicle on the trip
	void setPriority(Priority priority) { this->_priority = priority; }
	Priority getPriority() { return this->_priority; }

	// set/get functions for priority text of the vehicle on the trip
	void setPriorityText(const std::string &priorityText) { this->_priorityText = priorityText; }
	std::string getPriorityText() { return this->_priorityText; }

	// set/get functions for the list of all the stops
	void setStopsListAsString(const std::string &stopsListAsString) { this->_stopsListAsString = stopsListAsString; }
	std::string getStopsListAsString() { return this->_stopsListAsString; }
	void setStops(std::vector<int> tripStopIds);

protected:
	// The line Id to which the trip belongs
	int _lineId;

	// Indicates the direction of the vehicle
	Direction _direction;

	// Indicates the priority of the vehicle
	Priority _priority;

	// Indicates the priority of the vehicle in text representation
	std::string _priorityText;

	// The stopping pattern of the vehicle on the trip
	std::string _stopsListAsString;
};

/*******************************************************************************************
**************************************** Service ******************************************
*******************************************************************************************
* A service is a train servicing a specific trip.
* A service is created for each train on a scheduled or dynamically created trip.
* The service start time includes the date and time the service is starting/has started its run.
*******************************************************************************************/
class Service : public Trip
{
public:
	// A data type definig the state of the service
	enum State
	{
		NOT_STARTED,            // The service has not started yet
		RUNNING,
		APPROACHING_STATION,    // The train in this service is approachin a station
		IN_STATION,             // The train in this service is in a station
		BOARDING,               // The train in this service is boarding passengers - Doors are open
		DEPARTING_STATION,      // The train in this service is about to leave the station
		DEPARTED_STATION,       // The train in this service has left the station
		TERMINATED,             // The service has has reached the destination and terminated
		CANCELLED               // The service has been cancelled
	};

	// Need default constructor for Java SWIG to compile java_wrap.cxx
	Service() { };

	// Constructor: Specifying a numeric service ID is mandatory
	Service(int id);

	Service(int id, int tripId, long startTime);

	// set/get functions for the Trip to which the Serivce belongs
	void setTripId(int tripId) { this->_tripId = tripId; }
	int getTripId() { return this->_tripId; }

	// set/get functions for the arrival time offset in seconds from startTime
	void setStartTime(unsigned long startTime) { this->_startTime = startTime; }
	unsigned long getStartTime() { return this->_startTime; }

	// Returns the identification of the vehicle performing the service (numeral ID of the vehicle running the service)
	void setVehicleId(int vehicleId) { this->_vehicleId = vehicleId; }
	int getVehicleId() { return this->_vehicleId; }

	// set/get functions for the list of all the stops
	void setFollowingStopsListAsString(const std::string &followingStopsListAsString) { this->_followingStopsListAsString = followingStopsListAsString; }
	std::string getFollowingStopsListAsString() { return this->_followingStopsListAsString; }

	// set/get functions for the state of the service (enumerator object indicating the Service Stop)
	void setState(State state) { this->_state = state; }
	State getState() { return this->_state; }

	// set/get functions for the state text of the service
	void setStateText(const std::string &stateText) { this->_stateText = stateText; }
	std::string getStateText() { return this->_stateText; }

	// set/get functions for loading
	void setIsLoading(bool isLoading) { this->_isLoading = isLoading; }
	bool getIsLoading() { return this->_isLoading; }

	// set/get functions for destinationStationId
	void setDestinationStationId(const int &destinationStationId) { this->_destinationSationId = destinationStationId; }
	int getDestinationStationId() { return this->_destinationSationId; }

	// set/get functions for isLastTrain
	void setIsLastTrain(bool isLastTrain) { this->_isLastTrain = isLastTrain; }
	bool getIsLastTrain() { return this->_isLastTrain; }

	// set/get functions for LineId
	void setLineId(const int &lineId) { this->_lineId = lineId; }
	int getLineId() { return this->_lineId; }

	// set/get functions for list of stations with their timing information as ServiceStop objects
	void setServiceStops(const std::vector<ServiceStop>& serviceStops);
	void addServiceStop(const ServiceStop& serviceStop);
	void deleteAllServiceStops();
	void deleteServiceStop(int serviceStopId);
	std::vector<ServiceStop> getServiceStops();

	void updateServiceState(State serviceState);

protected:
	// the Trip Id that the Service belongs to
	int _tripId;

	unsigned long _startTime;

	// the identification of the vehicle performing the service
	int _vehicleId;

	// The stopping pattern of the vehicle on the trip
	std::string _followingStopsListAsString;

	// state of the service
	State _state;

	// textual representation of the state of the service
	std::string _stateText;

	// list of all ServiceStops of the service
	std::vector<ServiceStop> _serviceStops;

	bool _isLoading;

	int _destinationSationId;

	bool _isLastTrain;

	int _lineId;
};

/*******************************************************************************************
********************************* TimeTable Entry *****************************************
*******************************************************************************************
* Timetable entry - A trip which is scheduled to start at specific times during weekdays/weekends.
* This class will not be used in KL where we don't have any time-table information.
* This is the result of seasonal plan provided by rail authorities
*******************************************************************************************/
//class TimetableEntry
//{
//    public:
//        // The data type used to indicates which days of week the service is scheduled for
//        enum DayMask
//        {
//            MON = 0x01,
//            TUE = 0x02,
//            WED = 0x04,
//            THU = 0x08,
//            FRI = 0x10,
//            SAT = 0x20,
//            SUN = 0x40,
//            PUB = 0x40,     // public holiday - The same as SUN
//            SPE = 0x80      // special for certain events
//        };
//
//        // Constructor
//        TimetableEntry(const Trip &trip) : _trip(trip) { }
//
//    protected:
//        // Hours/mins offset from 00:00
//        long _startTime;
//
//        // which days is this trip scheduled.
//        DayMask _dayMask;
//
//        // The base trip which is scheduled to start at the startTime
//        const Trip &_trip;
//};

/*******************************************************************************************
*************************************** Vehicle *******************************************
*******************************************************************************************
* Class representing vehicles in the system. It could be trains, buses or ferries
*******************************************************************************************/
class Vehicle : public NamedId
{
public:
	// Datatype describing the direction of the vehicle
	enum Direction
	{
		DESCENDING,
		ASCENDING
	};

	// Datatype defining the state of the vehicle
	enum State
	{
		OUT_OF_SERVICE,     // The vehicle is out of service
		IN_SERVICE,         // The vehicle is currently running a service
		SWITCHING_SERVICE   // The vehicle has finished a service and is being assigned to another
	};

	// Datatype defining which door will be open in the next station
	enum DoorSide
	{
		NONE,               // The doors are not supposed to open
		SIDE_1,             // The doors on side 1 are expected to be opened (Left/Right depends on vehicle's moving direction) - Left
		SIDE_2,             // The doors on side 2 are expected to be opened (Left/Right depends on vehicle's moving direction) - Right
		BOTH                // Both of the doors are expected to be opened
	};

	// Need default constructor for Java SWIG to compile java_wrap.cxx
	Vehicle() { };

	// The constructor which requires a numeric ID to be assigned to the vehicle
	Vehicle(int id);

	// set/get functions for number of cars in the vehicle
	void setNumCars(int numCars) { this->_numCars = numCars; }
	int getNumCars() { return this->_numCars; }

	// set/get functions for current location of the vehicle
	void setCurrentLocation(const LocationId &currentLocation) { this->_currentLocation = currentLocation; }
	LocationId getCurrentLocation() { return this->_currentLocation; }

	// set/get functions for direction of the vehicle
	void setDirection(Direction direction) { this->_direction = direction; }
	Direction getDirection() { return this->_direction; }

	// set/get functions for state of the vehicle
	void setState(State state) { this->_state = state; }
	State getState() { return this->_state; }

	// set/get functions for state text
	void setStateText(const std::string &stateText) { this->_stateText = stateText; }
	std::string getStateText() { return this->_stateText; }

	// set/get functions for current service
	void setCurrentServiceId(int currentServiceId) { this->_currentServiceId = currentServiceId; }
	int getCurrentServiceId() { return this->_currentServiceId; }

	// Returns the service that the vehicle is currently serving
	Service getCurrentService();

	// set/get functions for DoorSide that will open at next/current stop
	void setOpeningDoors(DoorSide openingDoors) { this->_openingDoors = openingDoors; }
	DoorSide getOpeningDoors() { return this->_openingDoors; }

	// set/get functions for DoorSide Text that will open at next/current stop
	void setOpeningDoorsText(const std::string &openingDoorsText) { this->_openingDoorsText = openingDoorsText; }
	std::string getOpeningDoorsText() { return this->_openingDoorsText; }

	// set/get functions for DoorSide that are opened
	void setOpenedDoors(DoorSide openedDoors) { this->_openedDoors = openedDoors; }
	DoorSide getOpenedDoors() { return this->_openedDoors; }

	// set/get functions for DoorSide Text that are opened
	void setOpenedDoorsText(const std::string &openedDoorsText) { this->_openedDoorsText = openedDoorsText; }
	std::string getOpenedDoorsText() { return this->_openedDoorsText; }

	// Returns the list of all the services scheduled to be served by this vehicle
	void setServicesListAsString(const std::string &servicesListAsString) { this->_servicesListAsString = servicesListAsString; }
	std::string getServicesListAsString() { return this->_servicesListAsString; }
	std::vector<Service> getServices();

private:
	// Number of cars in the train
	int _numCars;

	// The current location of the vehicle whithin the network position system
	// e.g in KL it will be a comma separated list of all the track circuits this train occupied
	LocationId _currentLocation;

	// Indicates the direction of the vehicle
	Direction _direction;

	// The state of the vehicle
	State _state;

	// Text describing the state of the vehicle
	std::string _stateText;

	// Indicates the current service Id that the vehicle is running
	int _currentServiceId;

	// The DoorSide that will open at next/current stop
	DoorSide _openingDoors;

	// Text describing the DoorSide that will open at next/current stop
	std::string _openingDoorsText;

	// The DoorSide that is open
	DoorSide _openedDoors;

	// Text describing the DoorSide that is open
	std::string _openedDoorsText;

	// A list of all the services scheduled to be run by this vehicle
	std::string _servicesListAsString;

	// List of all Services for this vehicle
	std::vector<Service> _services;
};

/*******************************************************************************************
********************************** PlatformInfo *******************************************
*******************************************************************************************
* The class which represents a platform in a station or a stand in a bus stop.
*******************************************************************************************/
class PlatformInfo : public NamedId
{
public:
	// The datatype defining the state of the platform
	enum State
	{
		NO_VEHICLE_SCHEDULED,           // No Vehicle is scheduled to pass this platform
		VEHICLE_APPROACHING,            // Next vehicle is approaching the platform
		VEHICLE_ON_PLATFORM,            // The vehicle is on platform
		VEHICLE_DOES_NOT_STOP,          // When the train doesn't stop the state transitions to DOES_NOT_STOP instead of ON_PLATFORM and then to DEPARTED.
		VEHICLE_TERMINATED,             // It is the last stop in the serivce and train terminates here
		VEHICLE_BOARDING,               // The vehicle is boarding passengers - Doors are open
		VEHICLE_DEPARTING,              // The vehicle is about to depart - Doors closed
		VEHICLE_DEPARTED                // The vehicle departed the platform
	};

	// Need default constructor for Java SWIG to compile java_wrap.cxx
	PlatformInfo() {};

	// PlatformInfo constructor - requires a numberic Id which will be used for the platform
	PlatformInfo(int id);

	// set/get functions for the numerical ID of the station this platform reside in
	void setStationId(int stationId) { this->_stationId = stationId; }
	int getStationId() { return this->_stationId; }

	// set/get functions for the location which represent where the platform is in the network position system
	void setLocation(const std::string &location) { this->_location = location; }
	LocationId getLocation() { return this->_location; }

	// set/get functions for the state of the platform
	void setState(State state) { this->_state = state; }
	State getState() { return this->_state; }

	// set/get functions for the state text of the platform
	void setStateText(const std::string &stateText) { this->_stateText = stateText; }
	std::string getStateText() { return this->_stateText; }

	// set/get functions for list of all the services scheduled to pass this platform
	void setPassersListAsString(const std::string &passersListAsString) { this->_passersListAsString = passersListAsString; }
	void setPassers(std::vector<ServiceStop> serviceStops);
	void setPassers(std::vector<Service> services);
	std::string getPassersListAsString() { return this->_passersListAsString; }
	std::vector<ServiceStop> getPassers();

protected:
	// The station that this platform reside in
	int _stationId;

	// The location which represent where the platform is in the network position system
	// e.g. in KL it will be the track-circuit which corresponds to this platform
	LocationId _location;

	// The state of the platform
	State _state;

	// Text representing the state of the platform
	std::string _stateText;

	// A list of all the services scheduled to pass this platform.
	std::string _passersListAsString;
};

/*******************************************************************************************
************************************** Station ********************************************
*******************************************************************************************
* Class representing a train station, bus stop, ferry wharf or etc
*******************************************************************************************/
class Station : public NamedId
{
public:
	// Need default constructor for Java SWIG to compile java_wrap.cxx
	Station() {};

	// The constructor which requires a numeric ID to be used for this station
	Station(int id);

	Station(int id, const std::string &name, bool isMajor, bool isClosed);

	// set/get functions of whether this station is a major station
	// This information is used in some systems where all the stations in a trip are not announced and only the major ones are announced
	void setIsMajor(bool isMajor)   { this->_isMajor = isMajor; }
	bool isMajor() { return this->_isMajor; }

	// set/get functions of whether this station is closed station
	void setIsClosed(bool isClosed) { this->_isClosed = isClosed; }
	bool isClosed() { return this->_isClosed; }

	// set/get list of all the platforms exist whithin this station
	void setPlatformsListAsString(const std::string &platformsListAsString) { this->_platformsListAsString = platformsListAsString; }
	void setListOfPlatforms(std::map<int, PlatformInfo> platforms);
	void setPlatform(int platformId, const PlatformInfo& platform);
	std::string getPlatformsListAsString() { return this->_platformsListAsString; }
	std::vector<PlatformInfo> getListOfPlatforms();

	// Returns the platform information of a specific platform
	// throw std::out_of_range If a platform with that numeric id doesn't exist in this station
	PlatformInfo getPlatform(int platformId);

private:
	// indicates that this station is a major station. This information is used in some systems where all the stations in a trip is not announced and only the major ones are announced
	bool _isMajor;

	// indicates that this station is closed.
	bool _isClosed;

	// List of all the platforms exist within this station
	std::string _platformsListAsString;
};

/*******************************************************************************************
**************************** Passenger Information Observe ********************************
*******************************************************************************************
* The PassengerInformationObserver provides an interface to observe changes
* in the passenger information system including object status changes.
*******************************************************************************************/
class PassengerInformationObserver
{
public:
	// This function is called when the status of the signalling server is updated.
	virtual void onServerStatusUpdated(bool status)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onServerStatusUpdated()");
#else
		std::cout << "PassengerInformationObserver::onServerStatusUpdated()" << std::endl;
#endif
	}

	// This function is called when a new line defined in the system
	virtual void onLineUpdated(Line line)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onLineUpdated()");
#else
		std::cout << "PassengerInformationObserver::onLineUpdated()" << std::endl;
#endif
	}

	// This function is called when a line is removed from the system
	virtual void onLineRemoved(Line line)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengersInformationObserver::onLineRemoved()");
#else
		std::cout << "PassengerInformationObserver::onLineRemoved()" << std::endl;
#endif
	}

	// This function is called when a new vehicle defined in the system
	virtual void onVehicleUpdated(Vehicle vehicle)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onVehicleUpdated()");
#else
		std::cout << "PassengerInformationObserver::onVehicleUpdated()" << std::endl;
#endif
	}

	// This function is called when a vehicle is removed from the system
	virtual void onVehicleRemoved(Vehicle vehicle)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onVehicleRemoved()");
#else
		std::cout << "PassengerInformationObserver::onVehicleRemoved()" << std::endl;
#endif
	}

	// This function is called when a new station defined in the system
	virtual void onStationUpdated(Station station)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onStationUpdated()");
#else
		std::cout << "PassengerInformationObserver::onStationUpdated()" << std::endl;
#endif
	}

	// This function is called when a station dis removed from the system
	virtual void onStationRemoved(Station station)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onStationRemoved()");
#else
		std::cout << "PassengerInformationObserver::onStationRemoved()" << std::endl;
#endif
	}

	// This function is called when a new platform defined in the system
	virtual void onPlatformInfoUpdated(PlatformInfo platforminfo)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onPlatformInfoUpdated()");
#else
		std::cout << "PassengerInformationObserver::onPlatformInfoUpdated()" << std::endl;
#endif
	}

	// This function is called when a platform is removed from the system
	virtual void onPlatformInfoRemoved(PlatformInfo platforminfo)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onPlatformInfoRemoved()");
#else
		std::cout << "PassengerInformationObserver::onPlatformInfoRemoved()" << std::endl;
#endif
	}

	// This function is called when a new service defined in the system
	virtual void onServiceUpdated(Service service)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onServiceUpdated()");
#else
		std::cout << "PassengerInformationObserver::onServiceUpdated()" << std::endl;
#endif
	}

	// This function is called when a service is removed from the system
	virtual void onServiceRemoved(Service service)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onServiceRemoved()");
#else
		std::cout << "PassengerInformationObserver::onServiceRemoved()" << std::endl;
#endif
	}

	// This function is called when a trip is updated in the system
	virtual void onTripUpdated(Trip trip)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onTripUpdated()");
#else
		std::cout << "PassengerInformationObserver::onTripUpdated()" << std::endl;
#endif
	}

	// This function is called when a trip is removed from the system
	virtual void onTripRemoved(Trip trip)
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::onTripRemoved()");
#else
		std::cout << "PassengerInformationObserver::onTripRemoved()" << std::endl;
#endif
	}

	/// Callback function - called when deleting the observer object
	virtual ~PassengerInformationObserver()
	{
#ifdef OA_DEBUG
		OA_DEBUG(2, "PassengerInformationObserver::~PassengerInformationObserver()");
#else
		std::cout << "PassengerInformationObserver::~PassengerInformationObserver()" << std::endl;
#endif
	}
};

/*******************************************************************************************
************************* Passenger Information Server ************************************
*******************************************************************************************
* The interface to the Passenger Information Server is encapsulated by this
* PassengerInformationServer class.
*******************************************************************************************/
class PassengerInformationServer
{
public:
	/*******************************************************************************************
	* LocationId is a generic type that is going to be used differently in various projects.
	* e.g. a list of one or more track names separated by comma ie. "UA,UC"
	*******************************************************************************************/
	typedef std::string LocationId;

	// Set Location retrieval method
	enum LocationRetrievalMethod
	{
		SIGNALLING,  /// Location and positioning information is received using a
					 /// Signalling system interface in the NetSpire system.
					 /// Only the "getter" API methods of the PassengerInformationServer are enabled.
		API_TC,		 /// Location and positioning information is received from the API. LocationId contains Track Circuit IDs.
		API_PLAT,	 /// Location and positioning information is received from the API. LocationId contains Station and Platform IDs.
		API_GPS 	 /// Location and positioning information is received from the API. LocationId contains GPS coordinates.
	};

	PassengerInformationServer();

	~PassengerInformationServer();

	// Returns the current status of the signalling server (online/offline)
	bool getServerStatus();

	void setLocationRetrievalMethod(LocationRetrievalMethod method);
	LocationRetrievalMethod getLocationRetrievalMethod();
	
	bool isAutomaticMessageGenerationEnabled();
	void enableAutomaticMessageGeneration();
	void disableAutomaticMessageGeneration();
	void activatePositioningProtocol(PositioningProtocol _protocol, int _timeOutSeconds);
	void processPositioningUpdate(const std::string& _data);

	// Functions related to Lines
	void setLines(const std::vector<Line>& lines);
	void addLine(const Line& line);
	void deleteLine(int lineId);
	std::vector<Line> getLines();
	
	// Functions related to Vehicles
	void setVehicles(const std::vector<Vehicle>& vehicle);
	void addVehicle(const Vehicle& vehicle);
	void deleteVehicle(int vehicleId);
	std::vector<Vehicle> getVehicles();

	// Functions related to Stations
	void setStations(const std::vector<Station>& stations);
	void addStation(const Station& station);
	void deleteStation(int stationId);
	std::vector<Station> getStations();

	// Returns list of all Platforms defined in the system
	std::vector<PlatformInfo> getPlatforms();

	// Returns list of all Trips defined in the system
	std::vector<Trip> getTrips();

	// Returns list of all TripStops defined in the system
	std::vector<TripStop> getTripStops();

	// Functions related to Services
	void setServices(const std::vector<Service>& services);
	void addService(const Service& service);
	void deleteService(int serviceId);
	std::vector<Service> getServices();

	// Registers an observer with the PassengerInformationServer instance.
	// Callback methods on the observer will be called on state changes.
	void registerObserver(PassengerInformationObserver* observer);

	// Following 2 functions do not follow the SignallingController modal.
	// These have been implemented to cater Palemband and Ottawa project where full hierarchy is not required
	void updatePlatformInfo(const std::string &msg);
	void updateTrainState(int &trainId, int &state);

	// Helper Functions
	void updatePassengerInformation(int dstNo, int elemId, int elemNo, DVAlist *elemArgList);
	void deletePassengerInformation(int elemId, int elemNo);

private:
	// Indicates the status of signalling servers
	bool _signallingServerOnline;

	// Indicates if the automatic message generation system (enabled/disabled)
	bool _automaticMessageGenerationEnabled;

	// List of all Lines defined in the system
	std::vector<Line> _lines;

	// List of all Vehicles defined in the system
	std::vector<Vehicle> _vehicles;

	// List of all Stations defined in the system
	std::vector<Station> _stations;

	// List of all Platforms defined in the system
	std::vector<PlatformInfo> _platforms;

	// List of all Trips defined in the system
	std::vector<Trip> _trips;

	// List of all Trip Stops defined in the system
	std::vector<TripStop> _tripStops;

	// List of all Services defined in the system
	std::vector<Service> _services;

	// List of all ServiceStops defined in the system
	std::vector<ServiceStop> _serviceStops;

	oaLocalMutex pisMtx;

	// The registered event observer object
	std::vector<PassengerInformationObserver *> _pisObserverList;

	// Helper Functions
	void updateSignallingServerStatus(int elemNo, DVAlist *elemArgList);
	void updateLine(int elemNo, DVAlist *elemArgList);
	void updateVehicle(int elemNo, DVAlist *elemArgList);
	void updateVehicleState(int elemNo, DVAlist *elemArgList);
	void updateStation(int elemNo, DVAlist *elemArgList);
	void updatePlatform(int elemNo, DVAlist *elemArgList);
	void updatePlatformState(int elemNo, DVAlist *elemArgList);
	void updateTrip(int elemNo, DVAlist *elemArgList);
	void updateService(int elemNo, DVAlist *elemArgList);
	void updateServiceState(int elemNo, DVAlist *elemArgList);
	void updateTripStop(int elemNo, DVAlist *elemArgList);
	void updateServiceStop(int elemNo, DVAlist *elemArgList);
	void updateAutoMessageState(int elemNo, DVAlist *elemArgList);
};

#endif // _SIGNALLING_CONTROLLER_HPP_
