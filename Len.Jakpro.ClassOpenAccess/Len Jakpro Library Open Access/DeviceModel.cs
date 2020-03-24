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

public class DeviceModel : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DeviceModel(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DeviceModel obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DeviceModel() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_DeviceModel(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public DeviceModel(string modelName, string modelRevision, int digitalInputCnt, int digitalOutputCnt, int audioOutputChannels) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_0(modelName, modelRevision, digitalInputCnt, digitalOutputCnt, audioOutputChannels), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DeviceModel(string modelName, string modelRevision, int digitalInputCnt, int digitalOutputCnt) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_1(modelName, modelRevision, digitalInputCnt, digitalOutputCnt), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DeviceModel(string modelName, string modelRevision, int digitalInputCnt) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_2(modelName, modelRevision, digitalInputCnt), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DeviceModel(string modelName, string modelRevision) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_3(modelName, modelRevision), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DeviceModel(string modelName) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_4(modelName), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public DeviceModel() : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_5(), true) {
  }

  public DeviceModel(DeviceModel deviceModelAnother) : this(netspireSDKPINVOKE.new_DeviceModel__SWIG_6(DeviceModel.getCPtr(deviceModelAnother)), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public string getName() {
    string ret = netspireSDKPINVOKE.DeviceModel_getName(swigCPtr);
    return ret;
  }

  public string getRevision() {
    string ret = netspireSDKPINVOKE.DeviceModel_getRevision(swigCPtr);
    return ret;
  }

  public int getDigitalInputCount() {
    int ret = netspireSDKPINVOKE.DeviceModel_getDigitalInputCount(swigCPtr);
    return ret;
  }

  public int getDigitalOutputCount() {
    int ret = netspireSDKPINVOKE.DeviceModel_getDigitalOutputCount(swigCPtr);
    return ret;
  }

  public int getAudioOutputChannels() {
    int ret = netspireSDKPINVOKE.DeviceModel_getAudioOutputChannels(swigCPtr);
    return ret;
  }

}

}
