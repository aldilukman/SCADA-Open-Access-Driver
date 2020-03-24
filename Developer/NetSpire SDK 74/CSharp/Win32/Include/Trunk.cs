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

public class Trunk : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Trunk(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Trunk obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Trunk() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_Trunk(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Trunk(int dstNo, int elemNo, string trunkDevId, TRUNK_TYPE trunkType, string trunkName) : this(netspireSDKPINVOKE.new_Trunk__SWIG_0(dstNo, elemNo, trunkDevId, (int)trunkType, trunkName), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public Trunk(int dstNo, int elemNo, string trunkDevId, TRUNK_TYPE trunkType) : this(netspireSDKPINVOKE.new_Trunk__SWIG_1(dstNo, elemNo, trunkDevId, (int)trunkType), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public Trunk(int dstNo, int elemNo, string trunkDevId) : this(netspireSDKPINVOKE.new_Trunk__SWIG_2(dstNo, elemNo, trunkDevId), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public Trunk(int dstNo, int elemNo) : this(netspireSDKPINVOKE.new_Trunk__SWIG_3(dstNo, elemNo), true) {
  }

  public Trunk(int dstNo) : this(netspireSDKPINVOKE.new_Trunk__SWIG_4(dstNo), true) {
  }

  public Trunk() : this(netspireSDKPINVOKE.new_Trunk__SWIG_5(), true) {
  }

  public void setDstNo(int dstNo) {
    netspireSDKPINVOKE.Trunk_setDstNo(swigCPtr, dstNo);
  }

  public int getDstNo() {
    int ret = netspireSDKPINVOKE.Trunk_getDstNo(swigCPtr);
    return ret;
  }

  public void setElemNo(int elemNo) {
    netspireSDKPINVOKE.Trunk_setElemNo(swigCPtr, elemNo);
  }

  public int getElemNo() {
    int ret = netspireSDKPINVOKE.Trunk_getElemNo(swigCPtr);
    return ret;
  }

  public void setType(TRUNK_TYPE type) {
    netspireSDKPINVOKE.Trunk_setType(swigCPtr, (int)type);
  }

  public int getType() {
    int ret = netspireSDKPINVOKE.Trunk_getType(swigCPtr);
    return ret;
  }

  public void setName(string name) {
    netspireSDKPINVOKE.Trunk_setName(swigCPtr, name);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = netspireSDKPINVOKE.Trunk_getName(swigCPtr);
    return ret;
  }

  public void setDeviceId(string devId) {
    netspireSDKPINVOKE.Trunk_setDeviceId(swigCPtr, devId);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getDeviceId() {
    string ret = netspireSDKPINVOKE.Trunk_getDeviceId(swigCPtr);
    return ret;
  }

}

}
