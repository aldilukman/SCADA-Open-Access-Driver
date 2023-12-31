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

public class AudioServerObserver : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AudioServerObserver(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(AudioServerObserver obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~AudioServerObserver() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_AudioServerObserver(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public AudioServerObserver() : this(netspireSDKPINVOKE.new_AudioServerObserver(), true) {
    SwigDirectorConnect();
  }

  public virtual void onCommsLinkUp() {
    if (SwigDerivedClassHasMethod("onCommsLinkUp", swigMethodTypes0)) netspireSDKPINVOKE.AudioServerObserver_onCommsLinkUpSwigExplicitAudioServerObserver(swigCPtr); else netspireSDKPINVOKE.AudioServerObserver_onCommsLinkUp(swigCPtr);
  }

  public virtual void onCommsLinkDown() {
    if (SwigDerivedClassHasMethod("onCommsLinkDown", swigMethodTypes1)) netspireSDKPINVOKE.AudioServerObserver_onCommsLinkDownSwigExplicitAudioServerObserver(swigCPtr); else netspireSDKPINVOKE.AudioServerObserver_onCommsLinkDown(swigCPtr);
  }

  public virtual void onAudioConnected() {
    if (SwigDerivedClassHasMethod("onAudioConnected", swigMethodTypes2)) netspireSDKPINVOKE.AudioServerObserver_onAudioConnectedSwigExplicitAudioServerObserver(swigCPtr); else netspireSDKPINVOKE.AudioServerObserver_onAudioConnected(swigCPtr);
  }

  public virtual void onAudioDisconnected() {
    if (SwigDerivedClassHasMethod("onAudioDisconnected", swigMethodTypes3)) netspireSDKPINVOKE.AudioServerObserver_onAudioDisconnectedSwigExplicitAudioServerObserver(swigCPtr); else netspireSDKPINVOKE.AudioServerObserver_onAudioDisconnected(swigCPtr);
  }

  public virtual void onDeviceStateChange(Device device) {
    if (SwigDerivedClassHasMethod("onDeviceStateChange", swigMethodTypes4)) netspireSDKPINVOKE.AudioServerObserver_onDeviceStateChangeSwigExplicitAudioServerObserver(swigCPtr, Device.getCPtr(device)); else netspireSDKPINVOKE.AudioServerObserver_onDeviceStateChange(swigCPtr, Device.getCPtr(device));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onDeviceDelete(Device device) {
    if (SwigDerivedClassHasMethod("onDeviceDelete", swigMethodTypes5)) netspireSDKPINVOKE.AudioServerObserver_onDeviceDeleteSwigExplicitAudioServerObserver(swigCPtr, Device.getCPtr(device)); else netspireSDKPINVOKE.AudioServerObserver_onDeviceDelete(swigCPtr, Device.getCPtr(device));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onVUMeterUpdate(int level) {
    if (SwigDerivedClassHasMethod("onVUMeterUpdate", swigMethodTypes6)) netspireSDKPINVOKE.AudioServerObserver_onVUMeterUpdateSwigExplicitAudioServerObserver(swigCPtr, level); else netspireSDKPINVOKE.AudioServerObserver_onVUMeterUpdate(swigCPtr, level);
  }

  public virtual void onConfigUpdate(string deviceId, string resId, int instanceId, string progress, string key, string value) {
    if (SwigDerivedClassHasMethod("onConfigUpdate", swigMethodTypes7)) netspireSDKPINVOKE.AudioServerObserver_onConfigUpdateSwigExplicitAudioServerObserver(swigCPtr, deviceId, resId, instanceId, progress, key, value); else netspireSDKPINVOKE.AudioServerObserver_onConfigUpdate(swigCPtr, deviceId, resId, instanceId, progress, key, value);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onStateUpdate(bool isImportData, int key, string value) {
    if (SwigDerivedClassHasMethod("onStateUpdate", swigMethodTypes8)) netspireSDKPINVOKE.AudioServerObserver_onStateUpdateSwigExplicitAudioServerObserver(swigCPtr, isImportData, key, value); else netspireSDKPINVOKE.AudioServerObserver_onStateUpdate(swigCPtr, isImportData, key, value);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onUserDataUpdate(int elemId, int key, string value) {
    if (SwigDerivedClassHasMethod("onUserDataUpdate", swigMethodTypes9)) netspireSDKPINVOKE.AudioServerObserver_onUserDataUpdateSwigExplicitAudioServerObserver(swigCPtr, elemId, key, value); else netspireSDKPINVOKE.AudioServerObserver_onUserDataUpdate(swigCPtr, elemId, key, value);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onUserDataDelete(int elemId, int key) {
    if (SwigDerivedClassHasMethod("onUserDataDelete", swigMethodTypes10)) netspireSDKPINVOKE.AudioServerObserver_onUserDataDeleteSwigExplicitAudioServerObserver(swigCPtr, elemId, key); else netspireSDKPINVOKE.AudioServerObserver_onUserDataDelete(swigCPtr, elemId, key);
  }

  public virtual void onDebugMessage(string msg) {
    if (SwigDerivedClassHasMethod("onDebugMessage", swigMethodTypes11)) netspireSDKPINVOKE.AudioServerObserver_onDebugMessageSwigExplicitAudioServerObserver(swigCPtr, msg); else netspireSDKPINVOKE.AudioServerObserver_onDebugMessage(swigCPtr, msg);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void updateIPPAOnDictionaryUpdate(string dictionaryStateStr) {
    if (SwigDerivedClassHasMethod("updateIPPAOnDictionaryUpdate", swigMethodTypes12)) netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnDictionaryUpdateSwigExplicitAudioServerObserver(swigCPtr, dictionaryStateStr); else netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnDictionaryUpdate(swigCPtr, dictionaryStateStr);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void updateIPPAOnICIDigitalInputStateChange(int inputNo, bool active) {
    if (SwigDerivedClassHasMethod("updateIPPAOnICIDigitalInputStateChange", swigMethodTypes13)) netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnICIDigitalInputStateChangeSwigExplicitAudioServerObserver(swigCPtr, inputNo, active); else netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnICIDigitalInputStateChange(swigCPtr, inputNo, active);
  }

  public virtual void updateIPPAOnPreviewSpeakerStateChange(bool active) {
    if (SwigDerivedClassHasMethod("updateIPPAOnPreviewSpeakerStateChange", swigMethodTypes14)) netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnPreviewSpeakerStateChangeSwigExplicitAudioServerObserver(swigCPtr, active); else netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnPreviewSpeakerStateChange(swigCPtr, active);
  }

  public virtual void updateIPPAOnStreamingServerStateChange(bool streaming) {
    if (SwigDerivedClassHasMethod("updateIPPAOnStreamingServerStateChange", swigMethodTypes15)) netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnStreamingServerStateChangeSwigExplicitAudioServerObserver(swigCPtr, streaming); else netspireSDKPINVOKE.AudioServerObserver_updateIPPAOnStreamingServerStateChange(swigCPtr, streaming);
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("onCommsLinkUp", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateAudioServerObserver_0(SwigDirectoronCommsLinkUp);
    if (SwigDerivedClassHasMethod("onCommsLinkDown", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateAudioServerObserver_1(SwigDirectoronCommsLinkDown);
    if (SwigDerivedClassHasMethod("onAudioConnected", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateAudioServerObserver_2(SwigDirectoronAudioConnected);
    if (SwigDerivedClassHasMethod("onAudioDisconnected", swigMethodTypes3))
      swigDelegate3 = new SwigDelegateAudioServerObserver_3(SwigDirectoronAudioDisconnected);
    if (SwigDerivedClassHasMethod("onDeviceStateChange", swigMethodTypes4))
      swigDelegate4 = new SwigDelegateAudioServerObserver_4(SwigDirectoronDeviceStateChange);
    if (SwigDerivedClassHasMethod("onDeviceDelete", swigMethodTypes5))
      swigDelegate5 = new SwigDelegateAudioServerObserver_5(SwigDirectoronDeviceDelete);
    if (SwigDerivedClassHasMethod("onVUMeterUpdate", swigMethodTypes6))
      swigDelegate6 = new SwigDelegateAudioServerObserver_6(SwigDirectoronVUMeterUpdate);
    if (SwigDerivedClassHasMethod("onConfigUpdate", swigMethodTypes7))
      swigDelegate7 = new SwigDelegateAudioServerObserver_7(SwigDirectoronConfigUpdate);
    if (SwigDerivedClassHasMethod("onStateUpdate", swigMethodTypes8))
      swigDelegate8 = new SwigDelegateAudioServerObserver_8(SwigDirectoronStateUpdate);
    if (SwigDerivedClassHasMethod("onUserDataUpdate", swigMethodTypes9))
      swigDelegate9 = new SwigDelegateAudioServerObserver_9(SwigDirectoronUserDataUpdate);
    if (SwigDerivedClassHasMethod("onUserDataDelete", swigMethodTypes10))
      swigDelegate10 = new SwigDelegateAudioServerObserver_10(SwigDirectoronUserDataDelete);
    if (SwigDerivedClassHasMethod("onDebugMessage", swigMethodTypes11))
      swigDelegate11 = new SwigDelegateAudioServerObserver_11(SwigDirectoronDebugMessage);
    if (SwigDerivedClassHasMethod("updateIPPAOnDictionaryUpdate", swigMethodTypes12))
      swigDelegate12 = new SwigDelegateAudioServerObserver_12(SwigDirectorupdateIPPAOnDictionaryUpdate);
    if (SwigDerivedClassHasMethod("updateIPPAOnICIDigitalInputStateChange", swigMethodTypes13))
      swigDelegate13 = new SwigDelegateAudioServerObserver_13(SwigDirectorupdateIPPAOnICIDigitalInputStateChange);
    if (SwigDerivedClassHasMethod("updateIPPAOnPreviewSpeakerStateChange", swigMethodTypes14))
      swigDelegate14 = new SwigDelegateAudioServerObserver_14(SwigDirectorupdateIPPAOnPreviewSpeakerStateChange);
    if (SwigDerivedClassHasMethod("updateIPPAOnStreamingServerStateChange", swigMethodTypes15))
      swigDelegate15 = new SwigDelegateAudioServerObserver_15(SwigDirectorupdateIPPAOnStreamingServerStateChange);
    netspireSDKPINVOKE.AudioServerObserver_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3, swigDelegate4, swigDelegate5, swigDelegate6, swigDelegate7, swigDelegate8, swigDelegate9, swigDelegate10, swigDelegate11, swigDelegate12, swigDelegate13, swigDelegate14, swigDelegate15);
  }

  private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes) {
    System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(AudioServerObserver));
    return hasDerivedMethod;
  }

  private void SwigDirectoronCommsLinkUp() {
    onCommsLinkUp();
  }

  private void SwigDirectoronCommsLinkDown() {
    onCommsLinkDown();
  }

  private void SwigDirectoronAudioConnected() {
    onAudioConnected();
  }

  private void SwigDirectoronAudioDisconnected() {
    onAudioDisconnected();
  }

  private void SwigDirectoronDeviceStateChange(IntPtr device) {
    onDeviceStateChange(new Device(device, false));
  }

  private void SwigDirectoronDeviceDelete(IntPtr device) {
    onDeviceDelete(new Device(device, false));
  }

  private void SwigDirectoronVUMeterUpdate(int level) {
    onVUMeterUpdate(level);
  }

  private void SwigDirectoronConfigUpdate(string deviceId, string resId, int instanceId, string progress, string key, string value) {
    onConfigUpdate(deviceId, resId, instanceId, progress, key, value);
  }

  private void SwigDirectoronStateUpdate(bool isImportData, int key, string value) {
    onStateUpdate(isImportData, key, value);
  }

  private void SwigDirectoronUserDataUpdate(int elemId, int key, string value) {
    onUserDataUpdate(elemId, key, value);
  }

  private void SwigDirectoronUserDataDelete(int elemId, int key) {
    onUserDataDelete(elemId, key);
  }

  private void SwigDirectoronDebugMessage(string msg) {
    onDebugMessage(msg);
  }

  private void SwigDirectorupdateIPPAOnDictionaryUpdate(string dictionaryStateStr) {
    updateIPPAOnDictionaryUpdate(dictionaryStateStr);
  }

  private void SwigDirectorupdateIPPAOnICIDigitalInputStateChange(int inputNo, bool active) {
    updateIPPAOnICIDigitalInputStateChange(inputNo, active);
  }

  private void SwigDirectorupdateIPPAOnPreviewSpeakerStateChange(bool active) {
    updateIPPAOnPreviewSpeakerStateChange(active);
  }

  private void SwigDirectorupdateIPPAOnStreamingServerStateChange(bool streaming) {
    updateIPPAOnStreamingServerStateChange(streaming);
  }

  public delegate void SwigDelegateAudioServerObserver_0();
  public delegate void SwigDelegateAudioServerObserver_1();
  public delegate void SwigDelegateAudioServerObserver_2();
  public delegate void SwigDelegateAudioServerObserver_3();
  public delegate void SwigDelegateAudioServerObserver_4(IntPtr device);
  public delegate void SwigDelegateAudioServerObserver_5(IntPtr device);
  public delegate void SwigDelegateAudioServerObserver_6(int level);
  public delegate void SwigDelegateAudioServerObserver_7(string deviceId, string resId, int instanceId, string progress, string key, string value);
  public delegate void SwigDelegateAudioServerObserver_8(bool isImportData, int key, string value);
  public delegate void SwigDelegateAudioServerObserver_9(int elemId, int key, string value);
  public delegate void SwigDelegateAudioServerObserver_10(int elemId, int key);
  public delegate void SwigDelegateAudioServerObserver_11(string msg);
  public delegate void SwigDelegateAudioServerObserver_12(string dictionaryStateStr);
  public delegate void SwigDelegateAudioServerObserver_13(int inputNo, bool active);
  public delegate void SwigDelegateAudioServerObserver_14(bool active);
  public delegate void SwigDelegateAudioServerObserver_15(bool streaming);

  private SwigDelegateAudioServerObserver_0 swigDelegate0;
  private SwigDelegateAudioServerObserver_1 swigDelegate1;
  private SwigDelegateAudioServerObserver_2 swigDelegate2;
  private SwigDelegateAudioServerObserver_3 swigDelegate3;
  private SwigDelegateAudioServerObserver_4 swigDelegate4;
  private SwigDelegateAudioServerObserver_5 swigDelegate5;
  private SwigDelegateAudioServerObserver_6 swigDelegate6;
  private SwigDelegateAudioServerObserver_7 swigDelegate7;
  private SwigDelegateAudioServerObserver_8 swigDelegate8;
  private SwigDelegateAudioServerObserver_9 swigDelegate9;
  private SwigDelegateAudioServerObserver_10 swigDelegate10;
  private SwigDelegateAudioServerObserver_11 swigDelegate11;
  private SwigDelegateAudioServerObserver_12 swigDelegate12;
  private SwigDelegateAudioServerObserver_13 swigDelegate13;
  private SwigDelegateAudioServerObserver_14 swigDelegate14;
  private SwigDelegateAudioServerObserver_15 swigDelegate15;

  private static Type[] swigMethodTypes0 = new Type[] {  };
  private static Type[] swigMethodTypes1 = new Type[] {  };
  private static Type[] swigMethodTypes2 = new Type[] {  };
  private static Type[] swigMethodTypes3 = new Type[] {  };
  private static Type[] swigMethodTypes4 = new Type[] { typeof(Device) };
  private static Type[] swigMethodTypes5 = new Type[] { typeof(Device) };
  private static Type[] swigMethodTypes6 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes7 = new Type[] { typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(string) };
  private static Type[] swigMethodTypes8 = new Type[] { typeof(bool), typeof(int), typeof(string) };
  private static Type[] swigMethodTypes9 = new Type[] { typeof(int), typeof(int), typeof(string) };
  private static Type[] swigMethodTypes10 = new Type[] { typeof(int), typeof(int) };
  private static Type[] swigMethodTypes11 = new Type[] { typeof(string) };
  private static Type[] swigMethodTypes12 = new Type[] { typeof(string) };
  private static Type[] swigMethodTypes13 = new Type[] { typeof(int), typeof(bool) };
  private static Type[] swigMethodTypes14 = new Type[] { typeof(bool) };
  private static Type[] swigMethodTypes15 = new Type[] { typeof(bool) };
}

}
