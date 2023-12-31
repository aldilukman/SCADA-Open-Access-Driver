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

public class EventItem : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal EventItem(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(EventItem obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~EventItem() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_EventItem(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public EventItem() : this(netspireSDKPINVOKE.new_EventItem(), true) {
  }

  public string dateTime {
    set {
      netspireSDKPINVOKE.EventItem_dateTime_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.EventItem_dateTime_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public EventItem.EVENT_TYPE type {
    set {
      netspireSDKPINVOKE.EventItem_type_set(swigCPtr, (int)value);
    } 
    get {
      EventItem.EVENT_TYPE ret = (EventItem.EVENT_TYPE)netspireSDKPINVOKE.EventItem_type_get(swigCPtr);
      return ret;
    } 
  }

  public int id {
    set {
      netspireSDKPINVOKE.EventItem_id_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.EventItem_id_get(swigCPtr);
      return ret;
    } 
  }

  public Device device {
    set {
      netspireSDKPINVOKE.EventItem_device_set(swigCPtr, Device.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_device_get(swigCPtr);
      Device ret = (cPtr == IntPtr.Zero) ? null : new Device(cPtr, false);
      return ret;
    } 
  }

  public PaSource paSource {
    set {
      netspireSDKPINVOKE.EventItem_paSource_set(swigCPtr, PaSource.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_paSource_get(swigCPtr);
      PaSource ret = (cPtr == IntPtr.Zero) ? null : new PaSource(cPtr, false);
      return ret;
    } 
  }

  public PaSink paSink {
    set {
      netspireSDKPINVOKE.EventItem_paSink_set(swigCPtr, PaSink.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_paSink_get(swigCPtr);
      PaSink ret = (cPtr == IntPtr.Zero) ? null : new PaSink(cPtr, false);
      return ret;
    } 
  }

  public PaTrigger paTrigger {
    set {
      netspireSDKPINVOKE.EventItem_paTrigger_set(swigCPtr, PaTrigger.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_paTrigger_get(swigCPtr);
      PaTrigger ret = (cPtr == IntPtr.Zero) ? null : new PaTrigger(cPtr, false);
      return ret;
    } 
  }

  public PaSelector paSelector {
    set {
      netspireSDKPINVOKE.EventItem_paSelector_set(swigCPtr, PaSelector.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_paSelector_get(swigCPtr);
      PaSelector ret = (cPtr == IntPtr.Zero) ? null : new PaSelector(cPtr, false);
      return ret;
    } 
  }

  public CallInfo callInfo {
    set {
      netspireSDKPINVOKE.EventItem_callInfo_set(swigCPtr, CallInfo.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_callInfo_get(swigCPtr);
      CallInfo ret = (cPtr == IntPtr.Zero) ? null : new CallInfo(cPtr, false);
      return ret;
    } 
  }

  public CDRInfo cdrInfo {
    set {
      netspireSDKPINVOKE.EventItem_cdrInfo_set(swigCPtr, CDRInfo.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_cdrInfo_get(swigCPtr);
      CDRInfo ret = (cPtr == IntPtr.Zero) ? null : new CDRInfo(cPtr, false);
      return ret;
    } 
  }

  public TerminalInfo termInfo {
    set {
      netspireSDKPINVOKE.EventItem_termInfo_set(swigCPtr, TerminalInfo.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_termInfo_get(swigCPtr);
      TerminalInfo ret = (cPtr == IntPtr.Zero) ? null : new TerminalInfo(cPtr, false);
      return ret;
    } 
  }

  public SIPTrunk sipTrunk {
    set {
      netspireSDKPINVOKE.EventItem_sipTrunk_set(swigCPtr, SIPTrunk.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_sipTrunk_get(swigCPtr);
      SIPTrunk ret = (cPtr == IntPtr.Zero) ? null : new SIPTrunk(cPtr, false);
      return ret;
    } 
  }

  public ISDNTrunk isdnTrunk {
    set {
      netspireSDKPINVOKE.EventItem_isdnTrunk_set(swigCPtr, ISDNTrunk.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_isdnTrunk_get(swigCPtr);
      ISDNTrunk ret = (cPtr == IntPtr.Zero) ? null : new ISDNTrunk(cPtr, false);
      return ret;
    } 
  }

  public NamedId pisLine {
    set {
      netspireSDKPINVOKE.EventItem_pisLine_set(swigCPtr, NamedId.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisLine_get(swigCPtr);
      NamedId ret = (cPtr == IntPtr.Zero) ? null : new NamedId(cPtr, false);
      return ret;
    } 
  }

  public Vehicle pisVehicle {
    set {
      netspireSDKPINVOKE.EventItem_pisVehicle_set(swigCPtr, Vehicle.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisVehicle_get(swigCPtr);
      Vehicle ret = (cPtr == IntPtr.Zero) ? null : new Vehicle(cPtr, false);
      return ret;
    } 
  }

  public Station pisStation {
    set {
      netspireSDKPINVOKE.EventItem_pisStation_set(swigCPtr, Station.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisStation_get(swigCPtr);
      Station ret = (cPtr == IntPtr.Zero) ? null : new Station(cPtr, false);
      return ret;
    } 
  }

  public PlatformInfo pisPlatform {
    set {
      netspireSDKPINVOKE.EventItem_pisPlatform_set(swigCPtr, PlatformInfo.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisPlatform_get(swigCPtr);
      PlatformInfo ret = (cPtr == IntPtr.Zero) ? null : new PlatformInfo(cPtr, false);
      return ret;
    } 
  }

  public Service pisServices {
    set {
      netspireSDKPINVOKE.EventItem_pisServices_set(swigCPtr, Service.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisServices_get(swigCPtr);
      Service ret = (cPtr == IntPtr.Zero) ? null : new Service(cPtr, false);
      return ret;
    } 
  }

  public Trip pisTrip {
    set {
      netspireSDKPINVOKE.EventItem_pisTrip_set(swigCPtr, Trip.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_pisTrip_get(swigCPtr);
      Trip ret = (cPtr == IntPtr.Zero) ? null : new Trip(cPtr, false);
      return ret;
    } 
  }

  public ScheduleDefinition schedule {
    set {
      netspireSDKPINVOKE.EventItem_schedule_set(swigCPtr, ScheduleDefinition.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_schedule_get(swigCPtr);
      ScheduleDefinition ret = (cPtr == IntPtr.Zero) ? null : new ScheduleDefinition(cPtr, false);
      return ret;
    } 
  }

  public PaZone paZone {
    set {
      netspireSDKPINVOKE.EventItem_paZone_set(swigCPtr, PaZone.getCPtr(value));
    } 
    get {
      IntPtr cPtr = netspireSDKPINVOKE.EventItem_paZone_get(swigCPtr);
      PaZone ret = (cPtr == IntPtr.Zero) ? null : new PaZone(cPtr, false);
      return ret;
    } 
  }

  public enum EVENT_TYPE {
    COMMS_LINK_UP = 0,
    COMMS_LINK_DOWN,
    AUDIO_SERVER_CONNECTED,
    AUDIO_SERVER_DISCONNECTED,
    DEVICE_STATE_CHANGED,
    DEVICE_DELETED,
    PA_SOURCE_UPDATED,
    PA_SOURCE_DELETED,
    PA_SINK_UPDATED,
    PA_SINK_DELETED,
    PA_TRIGGER_UPDATED,
    PA_TRIGGER_DELETED,
    PA_SELECTOR_UPDATED,
    PA_SELECTOR_DELETED,
    CALL_UPDATED,
    CALL_DELETED,
    CDR_MESSAGE_UPDATED,
    CDR_MESSAGE_DELETED,
    TERMINAL_UPDATED,
    TERMINAL_DELETED,
    SIPTRUNK_UPDATED,
    SIPTRUNK_DELETED,
    ISDNTRUNK_UPDATED,
    ISDNTRUNK_DELETED,
    PIS_SERVER_ONLINE,
    PIS_SERVER_OFFLINE,
    PIS_LINE_UPDATED,
    PIS_LINE_DELETED,
    PIS_VEHICLE_UPDATED,
    PIS_VEHICLE_DELETED,
    PIS_STATION_UPDATED,
    PIS_STATION_DELETED,
    PIS_PLATFORM_UPDATED,
    PIS_PLATFORM_DELETED,
    PIS_SERVICE_UPDATED,
    PIS_SERVICE_DELETED,
    PIS_TRIP_UPDATED,
    PIS_TRIP_DELETED,
    SCHEDULE_UPDATED,
    SCHEDULE_DELETED,
    PA_ZONE_UPDATED,
    PA_ZONE_DELETED
  }

}

}
