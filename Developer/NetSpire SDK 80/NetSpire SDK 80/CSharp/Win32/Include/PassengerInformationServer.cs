/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace netspire {

using System;
using System.Runtime.InteropServices;

public class PassengerInformationServer : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PassengerInformationServer(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PassengerInformationServer obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PassengerInformationServer() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PassengerInformationServer(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public PassengerInformationServer() : this(netspireSDKPINVOKE.new_PassengerInformationServer(), true) {
  }

  public bool getServerStatus() {
    bool ret = netspireSDKPINVOKE.PassengerInformationServer_getServerStatus(swigCPtr);
    return ret;
  }

  public void setLocationRetrievalMethod(PassengerInformationServer.LocationRetrievalMethod method) {
    netspireSDKPINVOKE.PassengerInformationServer_setLocationRetrievalMethod(swigCPtr, (int)method);
  }

  public PassengerInformationServer.LocationRetrievalMethod getLocationRetrievalMethod() {
    PassengerInformationServer.LocationRetrievalMethod ret = (PassengerInformationServer.LocationRetrievalMethod)netspireSDKPINVOKE.PassengerInformationServer_getLocationRetrievalMethod(swigCPtr);
    return ret;
  }

  public bool isAutomaticMessageGenerationEnabled() {
    bool ret = netspireSDKPINVOKE.PassengerInformationServer_isAutomaticMessageGenerationEnabled(swigCPtr);
    return ret;
  }

  public void enableAutomaticMessageGeneration() {
    netspireSDKPINVOKE.PassengerInformationServer_enableAutomaticMessageGeneration(swigCPtr);
  }

  public void disableAutomaticMessageGeneration() {
    netspireSDKPINVOKE.PassengerInformationServer_disableAutomaticMessageGeneration(swigCPtr);
  }

  public void activatePositioningProtocol(SWIGTYPE_p_PositioningProtocol _protocol, int _timeOutSeconds) {
    netspireSDKPINVOKE.PassengerInformationServer_activatePositioningProtocol(swigCPtr, SWIGTYPE_p_PositioningProtocol.getCPtr(_protocol), _timeOutSeconds);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void processPositioningUpdate(string _data) {
    netspireSDKPINVOKE.PassengerInformationServer_processPositioningUpdate(swigCPtr, _data);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setLines(LineList lines) {
    netspireSDKPINVOKE.PassengerInformationServer_setLines(swigCPtr, LineList.getCPtr(lines));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void addLine(NamedId line) {
    netspireSDKPINVOKE.PassengerInformationServer_addLine(swigCPtr, NamedId.getCPtr(line));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteLine(int lineId) {
    netspireSDKPINVOKE.PassengerInformationServer_deleteLine(swigCPtr, lineId);
  }

  public LineList getLines() {
    LineList ret = new LineList(netspireSDKPINVOKE.PassengerInformationServer_getLines(swigCPtr), true);
    return ret;
  }

  public void setVehicles(VehicleList vehicle) {
    netspireSDKPINVOKE.PassengerInformationServer_setVehicles(swigCPtr, VehicleList.getCPtr(vehicle));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void addVehicle(Vehicle vehicle) {
    netspireSDKPINVOKE.PassengerInformationServer_addVehicle(swigCPtr, Vehicle.getCPtr(vehicle));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteVehicle(int vehicleId) {
    netspireSDKPINVOKE.PassengerInformationServer_deleteVehicle(swigCPtr, vehicleId);
  }

  public VehicleList getVehicles() {
    VehicleList ret = new VehicleList(netspireSDKPINVOKE.PassengerInformationServer_getVehicles(swigCPtr), true);
    return ret;
  }

  public void setStations(StationList stations) {
    netspireSDKPINVOKE.PassengerInformationServer_setStations(swigCPtr, StationList.getCPtr(stations));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void addStation(Station station) {
    netspireSDKPINVOKE.PassengerInformationServer_addStation(swigCPtr, Station.getCPtr(station));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteStation(int stationId) {
    netspireSDKPINVOKE.PassengerInformationServer_deleteStation(swigCPtr, stationId);
  }

  public StationList getStations() {
    StationList ret = new StationList(netspireSDKPINVOKE.PassengerInformationServer_getStations(swigCPtr), true);
    return ret;
  }

  public PlatformInfoList getPlatforms() {
    PlatformInfoList ret = new PlatformInfoList(netspireSDKPINVOKE.PassengerInformationServer_getPlatforms(swigCPtr), true);
    return ret;
  }

  public TripList getTrips() {
    TripList ret = new TripList(netspireSDKPINVOKE.PassengerInformationServer_getTrips(swigCPtr), true);
    return ret;
  }

  public TripStopList getTripStops() {
    TripStopList ret = new TripStopList(netspireSDKPINVOKE.PassengerInformationServer_getTripStops(swigCPtr), true);
    return ret;
  }

  public void setServices(ServiceList services) {
    netspireSDKPINVOKE.PassengerInformationServer_setServices(swigCPtr, ServiceList.getCPtr(services));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void addService(Service service) {
    netspireSDKPINVOKE.PassengerInformationServer_addService(swigCPtr, Service.getCPtr(service));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void deleteService(int serviceId) {
    netspireSDKPINVOKE.PassengerInformationServer_deleteService(swigCPtr, serviceId);
  }

  public ServiceList getServices() {
    ServiceList ret = new ServiceList(netspireSDKPINVOKE.PassengerInformationServer_getServices(swigCPtr), true);
    return ret;
  }

  public void registerObserver(PassengerInformationObserver observer) {
    netspireSDKPINVOKE.PassengerInformationServer_registerObserver(swigCPtr, PassengerInformationObserver.getCPtr(observer));
  }

  public void updatePlatformInfo(string msg) {
    netspireSDKPINVOKE.PassengerInformationServer_updatePlatformInfo(swigCPtr, msg);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void updateTrainState(SWIGTYPE_p_int trainId, SWIGTYPE_p_int state) {
    netspireSDKPINVOKE.PassengerInformationServer_updateTrainState(swigCPtr, SWIGTYPE_p_int.getCPtr(trainId), SWIGTYPE_p_int.getCPtr(state));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public enum LocationRetrievalMethod {
    SIGNALLING,
    API_TC,
    API_PLAT,
    API_GPS
  }

}

}
