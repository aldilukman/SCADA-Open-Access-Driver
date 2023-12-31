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

public class SIPTrunk : Trunk {
  private HandleRef swigCPtr;

  internal SIPTrunk(IntPtr cPtr, bool cMemoryOwn) : base(netspireSDKPINVOKE.SIPTrunk_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(SIPTrunk obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~SIPTrunk() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_SIPTrunk(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public SIPTrunk() : this(netspireSDKPINVOKE.new_SIPTrunk__SWIG_0(), true) {
  }

  public SIPTrunk(int dstNo, int elemNo, string trunkDevId, string trunkName) : this(netspireSDKPINVOKE.new_SIPTrunk__SWIG_1(dstNo, elemNo, trunkDevId, trunkName), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setStatus(TRUNK_STATUS status) {
    netspireSDKPINVOKE.SIPTrunk_setStatus(swigCPtr, (int)status);
  }

  public int getStatus() {
    int ret = netspireSDKPINVOKE.SIPTrunk_getStatus(swigCPtr);
    return ret;
  }

}

}
