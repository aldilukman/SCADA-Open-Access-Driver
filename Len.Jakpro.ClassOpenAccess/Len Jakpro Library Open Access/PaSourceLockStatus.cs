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

public class PaSourceLockStatus : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PaSourceLockStatus(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PaSourceLockStatus obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PaSourceLockStatus() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PaSourceLockStatus(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public int sourceId {
    set {
      netspireSDKPINVOKE.PaSourceLockStatus_sourceId_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.PaSourceLockStatus_sourceId_get(swigCPtr);
      return ret;
    } 
  }

  public string lockedUUID {
    set {
      netspireSDKPINVOKE.PaSourceLockStatus_lockedUUID_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.PaSourceLockStatus_lockedUUID_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public PaSourceLockStatus() : this(netspireSDKPINVOKE.new_PaSourceLockStatus(), true) {
  }

}

}
