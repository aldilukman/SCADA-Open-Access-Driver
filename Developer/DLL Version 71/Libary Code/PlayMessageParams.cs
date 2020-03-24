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

public class PlayMessageParams : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PlayMessageParams(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PlayMessageParams obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PlayMessageParams() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PlayMessageParams(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public bool resumeOnInterruptFlag {
    set {
      netspireSDKPINVOKE.PlayMessageParams_resumeOnInterruptFlag_set(swigCPtr, value);
    } 
    get {
      bool ret = netspireSDKPINVOKE.PlayMessageParams_resumeOnInterruptFlag_get(swigCPtr);
      return ret;
    } 
  }

  public bool overrideExisting {
    set {
      netspireSDKPINVOKE.PlayMessageParams_overrideExisting_set(swigCPtr, value);
    } 
    get {
      bool ret = netspireSDKPINVOKE.PlayMessageParams_overrideExisting_get(swigCPtr);
      return ret;
    } 
  }

  public uint validityPeriod {
    set {
      netspireSDKPINVOKE.PlayMessageParams_validityPeriod_set(swigCPtr, value);
    } 
    get {
      uint ret = netspireSDKPINVOKE.PlayMessageParams_validityPeriod_get(swigCPtr);
      return ret;
    } 
  }

  public uint priority {
    set {
      netspireSDKPINVOKE.PlayMessageParams_priority_set(swigCPtr, value);
    } 
    get {
      uint ret = netspireSDKPINVOKE.PlayMessageParams_priority_get(swigCPtr);
      return ret;
    } 
  }

  public uint mode {
    set {
      netspireSDKPINVOKE.PlayMessageParams_mode_set(swigCPtr, value);
    } 
    get {
      uint ret = netspireSDKPINVOKE.PlayMessageParams_mode_get(swigCPtr);
      return ret;
    } 
  }

  public PlayMessageParams() : this(netspireSDKPINVOKE.new_PlayMessageParams(), true) {
  }

}

}
