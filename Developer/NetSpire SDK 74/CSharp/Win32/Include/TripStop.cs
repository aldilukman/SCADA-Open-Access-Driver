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

public class TripStop : NamedId {
  private HandleRef swigCPtr;

  internal TripStop(IntPtr cPtr, bool cMemoryOwn) : base(netspireSDKPINVOKE.TripStop_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(TripStop obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~TripStop() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_TripStop(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public TripStop() : this(netspireSDKPINVOKE.new_TripStop__SWIG_0(), true) {
  }

  public TripStop(int id) : this(netspireSDKPINVOKE.new_TripStop__SWIG_1(id), true) {
  }

  public TripStop(int id, int platformId, bool isStopping, int arrivalTimeOffset, int departureTimeOffset) : this(netspireSDKPINVOKE.new_TripStop__SWIG_2(id, platformId, isStopping, arrivalTimeOffset, departureTimeOffset), true) {
  }

  public void setTripId(int tripId) {
    netspireSDKPINVOKE.TripStop_setTripId(swigCPtr, tripId);
  }

  public int getTripId() {
    int ret = netspireSDKPINVOKE.TripStop_getTripId(swigCPtr);
    return ret;
  }

  public void setPlatformId(int platformId) {
    netspireSDKPINVOKE.TripStop_setPlatformId(swigCPtr, platformId);
  }

  public int getPlatformId() {
    int ret = netspireSDKPINVOKE.TripStop_getPlatformId(swigCPtr);
    return ret;
  }

  public void setIsStopping(bool isStopping) {
    netspireSDKPINVOKE.TripStop_setIsStopping(swigCPtr, isStopping);
  }

  public bool isStopping() {
    bool ret = netspireSDKPINVOKE.TripStop_isStopping(swigCPtr);
    return ret;
  }

  public void setSchArvOffset(uint schArvOffset) {
    netspireSDKPINVOKE.TripStop_setSchArvOffset(swigCPtr, schArvOffset);
  }

  public uint getSchArvOffset() {
    uint ret = netspireSDKPINVOKE.TripStop_getSchArvOffset(swigCPtr);
    return ret;
  }

  public void setSchDepOffset(uint schDepOffset) {
    netspireSDKPINVOKE.TripStop_setSchDepOffset(swigCPtr, schDepOffset);
  }

  public uint getSchDepOffset() {
    uint ret = netspireSDKPINVOKE.TripStop_getSchDepOffset(swigCPtr);
    return ret;
  }

}

}
