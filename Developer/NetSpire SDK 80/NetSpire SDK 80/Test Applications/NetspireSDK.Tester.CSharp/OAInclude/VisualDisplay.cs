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

public class VisualDisplay : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal VisualDisplay(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(VisualDisplay obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~VisualDisplay() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_VisualDisplay(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public void setName(string name) {
    netspireSDKPINVOKE.VisualDisplay_setName(swigCPtr, name);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = netspireSDKPINVOKE.VisualDisplay_getName(swigCPtr);
    return ret;
  }

  public void setCommsType(string commsType) {
    netspireSDKPINVOKE.VisualDisplay_setCommsType(swigCPtr, commsType);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getCommsType() {
    string ret = netspireSDKPINVOKE.VisualDisplay_getCommsType(swigCPtr);
    return ret;
  }

  public void setAddress(string address) {
    netspireSDKPINVOKE.VisualDisplay_setAddress(swigCPtr, address);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getAddress() {
    string ret = netspireSDKPINVOKE.VisualDisplay_getAddress(swigCPtr);
    return ret;
  }

  public void setHealthState(string state) {
    netspireSDKPINVOKE.VisualDisplay_setHealthState(swigCPtr, state);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getHealthState() {
    string ret = netspireSDKPINVOKE.VisualDisplay_getHealthState(swigCPtr);
    return ret;
  }

  public VisualDisplay() : this(netspireSDKPINVOKE.new_VisualDisplay(), true) {
  }

}

}
