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

public class Vehicle : NamedId {
  private HandleRef swigCPtr;

  internal Vehicle(IntPtr cPtr, bool cMemoryOwn) : base(netspireSDKPINVOKE.Vehicle_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Vehicle obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Vehicle() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_Vehicle(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public Vehicle() : this(netspireSDKPINVOKE.new_Vehicle__SWIG_0(), true) {
  }

  public Vehicle(int id) : this(netspireSDKPINVOKE.new_Vehicle__SWIG_1(id), true) {
  }

  public void setNumCars(int numCars) {
    netspireSDKPINVOKE.Vehicle_setNumCars(swigCPtr, numCars);
  }

  public int getNumCars() {
    int ret = netspireSDKPINVOKE.Vehicle_getNumCars(swigCPtr);
    return ret;
  }

  public void setCurrentLocation(string currentLocation) {
    netspireSDKPINVOKE.Vehicle_setCurrentLocation(swigCPtr, currentLocation);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getCurrentLocation() {
    string ret = netspireSDKPINVOKE.Vehicle_getCurrentLocation(swigCPtr);
    return ret;
  }

  public void setDirection(Vehicle.Direction direction) {
    netspireSDKPINVOKE.Vehicle_setDirection(swigCPtr, (int)direction);
  }

  public Vehicle.Direction getDirection() {
    Vehicle.Direction ret = (Vehicle.Direction)netspireSDKPINVOKE.Vehicle_getDirection(swigCPtr);
    return ret;
  }

  public void setState(Vehicle.State state) {
    netspireSDKPINVOKE.Vehicle_setState(swigCPtr, (int)state);
  }

  public Vehicle.State getState() {
    Vehicle.State ret = (Vehicle.State)netspireSDKPINVOKE.Vehicle_getState(swigCPtr);
    return ret;
  }

  public void setStateText(string stateText) {
    netspireSDKPINVOKE.Vehicle_setStateText(swigCPtr, stateText);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getStateText() {
    string ret = netspireSDKPINVOKE.Vehicle_getStateText(swigCPtr);
    return ret;
  }

  public void setCurrentServiceId(int currentServiceId) {
    netspireSDKPINVOKE.Vehicle_setCurrentServiceId(swigCPtr, currentServiceId);
  }

  public int getCurrentServiceId() {
    int ret = netspireSDKPINVOKE.Vehicle_getCurrentServiceId(swigCPtr);
    return ret;
  }

  public Service getCurrentService() {
    Service ret = new Service(netspireSDKPINVOKE.Vehicle_getCurrentService(swigCPtr), true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setOpeningDoors(Vehicle.DoorSide openingDoors) {
    netspireSDKPINVOKE.Vehicle_setOpeningDoors(swigCPtr, (int)openingDoors);
  }

  public Vehicle.DoorSide getOpeningDoors() {
    Vehicle.DoorSide ret = (Vehicle.DoorSide)netspireSDKPINVOKE.Vehicle_getOpeningDoors(swigCPtr);
    return ret;
  }

  public void setOpeningDoorsText(string openingDoorsText) {
    netspireSDKPINVOKE.Vehicle_setOpeningDoorsText(swigCPtr, openingDoorsText);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getOpeningDoorsText() {
    string ret = netspireSDKPINVOKE.Vehicle_getOpeningDoorsText(swigCPtr);
    return ret;
  }

  public void setOpenedDoors(Vehicle.DoorSide openedDoors) {
    netspireSDKPINVOKE.Vehicle_setOpenedDoors(swigCPtr, (int)openedDoors);
  }

  public Vehicle.DoorSide getOpenedDoors() {
    Vehicle.DoorSide ret = (Vehicle.DoorSide)netspireSDKPINVOKE.Vehicle_getOpenedDoors(swigCPtr);
    return ret;
  }

  public void setOpenedDoorsText(string openedDoorsText) {
    netspireSDKPINVOKE.Vehicle_setOpenedDoorsText(swigCPtr, openedDoorsText);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getOpenedDoorsText() {
    string ret = netspireSDKPINVOKE.Vehicle_getOpenedDoorsText(swigCPtr);
    return ret;
  }

  public void setServicesListAsString(string servicesListAsString) {
    netspireSDKPINVOKE.Vehicle_setServicesListAsString(swigCPtr, servicesListAsString);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setServices(ServiceList serviceList) {
    netspireSDKPINVOKE.Vehicle_setServices(swigCPtr, ServiceList.getCPtr(serviceList));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getServicesListAsString() {
    string ret = netspireSDKPINVOKE.Vehicle_getServicesListAsString(swigCPtr);
    return ret;
  }

  public ServiceList getServices() {
    ServiceList ret = new ServiceList(netspireSDKPINVOKE.Vehicle_getServices(swigCPtr), true);
    return ret;
  }

  public void createService(uint tripId, uint startTime) {
    netspireSDKPINVOKE.Vehicle_createService(swigCPtr, tripId, startTime);
  }

  public void deleteService(int serviceId) {
    netspireSDKPINVOKE.Vehicle_deleteService(swigCPtr, serviceId);
  }

  public void clearServices() {
    netspireSDKPINVOKE.Vehicle_clearServices(swigCPtr);
  }

  public void replaceService(uint tripId, uint startTime) {
    netspireSDKPINVOKE.Vehicle_replaceService(swigCPtr, tripId, startTime);
  }

  public enum Direction {
    DESCENDING,
    ASCENDING
  }

  public enum State {
    OUT_OF_SERVICE,
    IN_SERVICE,
    SWITCHING_SERVICE
  }

  public enum DoorSide {
    NONE,
    SIDE_1,
    SIDE_2,
    BOTH
  }

}

}
