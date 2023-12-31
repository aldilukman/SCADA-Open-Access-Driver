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

public class ISDNTrunk : Trunk {
  private HandleRef swigCPtr;

  internal ISDNTrunk(IntPtr cPtr, bool cMemoryOwn) : base(netspireSDKPINVOKE.ISDNTrunk_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(ISDNTrunk obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~ISDNTrunk() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_ISDNTrunk(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public ISDNTrunk() : this(netspireSDKPINVOKE.new_ISDNTrunk__SWIG_0(), true) {
  }

  public ISDNTrunk(int dstNo, int elemNo, string trunkDevId, string trunkName) : this(netspireSDKPINVOKE.new_ISDNTrunk__SWIG_1(dstNo, elemNo, trunkDevId, trunkName), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void setLayer1Status(TRUNK_STATUS status) {
    netspireSDKPINVOKE.ISDNTrunk_setLayer1Status(swigCPtr, (int)status);
  }

  public int getLayer1Status() {
    int ret = netspireSDKPINVOKE.ISDNTrunk_getLayer1Status(swigCPtr);
    return ret;
  }

  public void setLayer2_3Status(TRUNK_STATUS status) {
    netspireSDKPINVOKE.ISDNTrunk_setLayer2_3Status(swigCPtr, (int)status);
  }

  public int getLayer2_3Status() {
    int ret = netspireSDKPINVOKE.ISDNTrunk_getLayer2_3Status(swigCPtr);
    return ret;
  }

}

}
