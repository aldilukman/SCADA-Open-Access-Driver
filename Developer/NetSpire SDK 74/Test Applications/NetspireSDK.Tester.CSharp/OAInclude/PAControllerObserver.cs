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

public class PAControllerObserver : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PAControllerObserver(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PAControllerObserver obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PAControllerObserver() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PAControllerObserver(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public PAControllerObserver() : this(netspireSDKPINVOKE.new_PAControllerObserver(), true) {
    SwigDirectorConnect();
  }

  public virtual void onPaSourceUpdate(PaSource source) {
    if (SwigDerivedClassHasMethod("onPaSourceUpdate", swigMethodTypes0)) netspireSDKPINVOKE.PAControllerObserver_onPaSourceUpdateSwigExplicitPAControllerObserver(swigCPtr, PaSource.getCPtr(source)); else netspireSDKPINVOKE.PAControllerObserver_onPaSourceUpdate(swigCPtr, PaSource.getCPtr(source));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaSourceDelete(int sourceId) {
    if (SwigDerivedClassHasMethod("onPaSourceDelete", swigMethodTypes1)) netspireSDKPINVOKE.PAControllerObserver_onPaSourceDeleteSwigExplicitPAControllerObserver(swigCPtr, sourceId); else netspireSDKPINVOKE.PAControllerObserver_onPaSourceDelete(swigCPtr, sourceId);
  }

  public virtual void onPaSinkUpdate(PaSink sink) {
    if (SwigDerivedClassHasMethod("onPaSinkUpdate", swigMethodTypes2)) netspireSDKPINVOKE.PAControllerObserver_onPaSinkUpdateSwigExplicitPAControllerObserver(swigCPtr, PaSink.getCPtr(sink)); else netspireSDKPINVOKE.PAControllerObserver_onPaSinkUpdate(swigCPtr, PaSink.getCPtr(sink));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaSinkDelete(int sinkId) {
    if (SwigDerivedClassHasMethod("onPaSinkDelete", swigMethodTypes3)) netspireSDKPINVOKE.PAControllerObserver_onPaSinkDeleteSwigExplicitPAControllerObserver(swigCPtr, sinkId); else netspireSDKPINVOKE.PAControllerObserver_onPaSinkDelete(swigCPtr, sinkId);
  }

  public virtual void onPaTriggerUpdate(PaTrigger trigger) {
    if (SwigDerivedClassHasMethod("onPaTriggerUpdate", swigMethodTypes4)) netspireSDKPINVOKE.PAControllerObserver_onPaTriggerUpdateSwigExplicitPAControllerObserver(swigCPtr, PaTrigger.getCPtr(trigger)); else netspireSDKPINVOKE.PAControllerObserver_onPaTriggerUpdate(swigCPtr, PaTrigger.getCPtr(trigger));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaTriggerDelete(int triggerId) {
    if (SwigDerivedClassHasMethod("onPaTriggerDelete", swigMethodTypes5)) netspireSDKPINVOKE.PAControllerObserver_onPaTriggerDeleteSwigExplicitPAControllerObserver(swigCPtr, triggerId); else netspireSDKPINVOKE.PAControllerObserver_onPaTriggerDelete(swigCPtr, triggerId);
  }

  public virtual void onPaSelectorUpdate(PaSelector selector) {
    if (SwigDerivedClassHasMethod("onPaSelectorUpdate", swigMethodTypes6)) netspireSDKPINVOKE.PAControllerObserver_onPaSelectorUpdateSwigExplicitPAControllerObserver(swigCPtr, PaSelector.getCPtr(selector)); else netspireSDKPINVOKE.PAControllerObserver_onPaSelectorUpdate(swigCPtr, PaSelector.getCPtr(selector));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaSelectorDelete(int selectorId) {
    if (SwigDerivedClassHasMethod("onPaSelectorDelete", swigMethodTypes7)) netspireSDKPINVOKE.PAControllerObserver_onPaSelectorDeleteSwigExplicitPAControllerObserver(swigCPtr, selectorId); else netspireSDKPINVOKE.PAControllerObserver_onPaSelectorDelete(swigCPtr, selectorId);
  }

  public virtual void onScheduleUpdate(ScheduleDefinition schedule) {
    if (SwigDerivedClassHasMethod("onScheduleUpdate", swigMethodTypes8)) netspireSDKPINVOKE.PAControllerObserver_onScheduleUpdateSwigExplicitPAControllerObserver(swigCPtr, ScheduleDefinition.getCPtr(schedule)); else netspireSDKPINVOKE.PAControllerObserver_onScheduleUpdate(swigCPtr, ScheduleDefinition.getCPtr(schedule));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onScheduleDelete(ScheduleDefinition schedule) {
    if (SwigDerivedClassHasMethod("onScheduleDelete", swigMethodTypes9)) netspireSDKPINVOKE.PAControllerObserver_onScheduleDeleteSwigExplicitPAControllerObserver(swigCPtr, ScheduleDefinition.getCPtr(schedule)); else netspireSDKPINVOKE.PAControllerObserver_onScheduleDelete(swigCPtr, ScheduleDefinition.getCPtr(schedule));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaZoneUpdate(PaZone paZone) {
    if (SwigDerivedClassHasMethod("onPaZoneUpdate", swigMethodTypes10)) netspireSDKPINVOKE.PAControllerObserver_onPaZoneUpdateSwigExplicitPAControllerObserver(swigCPtr, PaZone.getCPtr(paZone)); else netspireSDKPINVOKE.PAControllerObserver_onPaZoneUpdate(swigCPtr, PaZone.getCPtr(paZone));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onPaZoneDelete(string zoneId) {
    if (SwigDerivedClassHasMethod("onPaZoneDelete", swigMethodTypes11)) netspireSDKPINVOKE.PAControllerObserver_onPaZoneDeleteSwigExplicitPAControllerObserver(swigCPtr, zoneId); else netspireSDKPINVOKE.PAControllerObserver_onPaZoneDelete(swigCPtr, zoneId);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onAudioMessageRetrievalComplete(string uuid, bool success, PAControllerObserver.MessageRetrievalError errorCode, string uri) {
    if (SwigDerivedClassHasMethod("onAudioMessageRetrievalComplete", swigMethodTypes12)) netspireSDKPINVOKE.PAControllerObserver_onAudioMessageRetrievalCompleteSwigExplicitPAControllerObserver(swigCPtr, uuid, success, (int)errorCode, uri); else netspireSDKPINVOKE.PAControllerObserver_onAudioMessageRetrievalComplete(swigCPtr, uuid, success, (int)errorCode, uri);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onMessageUpdate(ANNOUNCEMENT_REQUEST req) {
    if (SwigDerivedClassHasMethod("onMessageUpdate", swigMethodTypes13)) netspireSDKPINVOKE.PAControllerObserver_onMessageUpdateSwigExplicitPAControllerObserver(swigCPtr, ANNOUNCEMENT_REQUEST.getCPtr(req)); else netspireSDKPINVOKE.PAControllerObserver_onMessageUpdate(swigCPtr, ANNOUNCEMENT_REQUEST.getCPtr(req));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onMessageDelete(ANNOUNCEMENT_REQUEST req) {
    if (SwigDerivedClassHasMethod("onMessageDelete", swigMethodTypes14)) netspireSDKPINVOKE.PAControllerObserver_onMessageDeleteSwigExplicitPAControllerObserver(swigCPtr, ANNOUNCEMENT_REQUEST.getCPtr(req)); else netspireSDKPINVOKE.PAControllerObserver_onMessageDelete(swigCPtr, ANNOUNCEMENT_REQUEST.getCPtr(req));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("onPaSourceUpdate", swigMethodTypes0))
      swigDelegate0 = new SwigDelegatePAControllerObserver_0(SwigDirectoronPaSourceUpdate);
    if (SwigDerivedClassHasMethod("onPaSourceDelete", swigMethodTypes1))
      swigDelegate1 = new SwigDelegatePAControllerObserver_1(SwigDirectoronPaSourceDelete);
    if (SwigDerivedClassHasMethod("onPaSinkUpdate", swigMethodTypes2))
      swigDelegate2 = new SwigDelegatePAControllerObserver_2(SwigDirectoronPaSinkUpdate);
    if (SwigDerivedClassHasMethod("onPaSinkDelete", swigMethodTypes3))
      swigDelegate3 = new SwigDelegatePAControllerObserver_3(SwigDirectoronPaSinkDelete);
    if (SwigDerivedClassHasMethod("onPaTriggerUpdate", swigMethodTypes4))
      swigDelegate4 = new SwigDelegatePAControllerObserver_4(SwigDirectoronPaTriggerUpdate);
    if (SwigDerivedClassHasMethod("onPaTriggerDelete", swigMethodTypes5))
      swigDelegate5 = new SwigDelegatePAControllerObserver_5(SwigDirectoronPaTriggerDelete);
    if (SwigDerivedClassHasMethod("onPaSelectorUpdate", swigMethodTypes6))
      swigDelegate6 = new SwigDelegatePAControllerObserver_6(SwigDirectoronPaSelectorUpdate);
    if (SwigDerivedClassHasMethod("onPaSelectorDelete", swigMethodTypes7))
      swigDelegate7 = new SwigDelegatePAControllerObserver_7(SwigDirectoronPaSelectorDelete);
    if (SwigDerivedClassHasMethod("onScheduleUpdate", swigMethodTypes8))
      swigDelegate8 = new SwigDelegatePAControllerObserver_8(SwigDirectoronScheduleUpdate);
    if (SwigDerivedClassHasMethod("onScheduleDelete", swigMethodTypes9))
      swigDelegate9 = new SwigDelegatePAControllerObserver_9(SwigDirectoronScheduleDelete);
    if (SwigDerivedClassHasMethod("onPaZoneUpdate", swigMethodTypes10))
      swigDelegate10 = new SwigDelegatePAControllerObserver_10(SwigDirectoronPaZoneUpdate);
    if (SwigDerivedClassHasMethod("onPaZoneDelete", swigMethodTypes11))
      swigDelegate11 = new SwigDelegatePAControllerObserver_11(SwigDirectoronPaZoneDelete);
    if (SwigDerivedClassHasMethod("onAudioMessageRetrievalComplete", swigMethodTypes12))
      swigDelegate12 = new SwigDelegatePAControllerObserver_12(SwigDirectoronAudioMessageRetrievalComplete);
    if (SwigDerivedClassHasMethod("onMessageUpdate", swigMethodTypes13))
      swigDelegate13 = new SwigDelegatePAControllerObserver_13(SwigDirectoronMessageUpdate);
    if (SwigDerivedClassHasMethod("onMessageDelete", swigMethodTypes14))
      swigDelegate14 = new SwigDelegatePAControllerObserver_14(SwigDirectoronMessageDelete);
    netspireSDKPINVOKE.PAControllerObserver_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3, swigDelegate4, swigDelegate5, swigDelegate6, swigDelegate7, swigDelegate8, swigDelegate9, swigDelegate10, swigDelegate11, swigDelegate12, swigDelegate13, swigDelegate14);
  }

  private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes) {
    System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(PAControllerObserver));
    return hasDerivedMethod;
  }

  private void SwigDirectoronPaSourceUpdate(IntPtr source) {
    onPaSourceUpdate(new PaSource(source, false));
  }

  private void SwigDirectoronPaSourceDelete(int sourceId) {
    onPaSourceDelete(sourceId);
  }

  private void SwigDirectoronPaSinkUpdate(IntPtr sink) {
    onPaSinkUpdate(new PaSink(sink, false));
  }

  private void SwigDirectoronPaSinkDelete(int sinkId) {
    onPaSinkDelete(sinkId);
  }

  private void SwigDirectoronPaTriggerUpdate(IntPtr trigger) {
    onPaTriggerUpdate(new PaTrigger(trigger, false));
  }

  private void SwigDirectoronPaTriggerDelete(int triggerId) {
    onPaTriggerDelete(triggerId);
  }

  private void SwigDirectoronPaSelectorUpdate(IntPtr selector) {
    onPaSelectorUpdate(new PaSelector(selector, false));
  }

  private void SwigDirectoronPaSelectorDelete(int selectorId) {
    onPaSelectorDelete(selectorId);
  }

  private void SwigDirectoronScheduleUpdate(IntPtr schedule) {
    onScheduleUpdate(new ScheduleDefinition(schedule, false));
  }

  private void SwigDirectoronScheduleDelete(IntPtr schedule) {
    onScheduleDelete(new ScheduleDefinition(schedule, false));
  }

  private void SwigDirectoronPaZoneUpdate(IntPtr paZone) {
    onPaZoneUpdate(new PaZone(paZone, false));
  }

  private void SwigDirectoronPaZoneDelete(string zoneId) {
    onPaZoneDelete(zoneId);
  }

  private void SwigDirectoronAudioMessageRetrievalComplete(string uuid, bool success, int errorCode, string uri) {
    onAudioMessageRetrievalComplete(uuid, success, (PAControllerObserver.MessageRetrievalError)errorCode, uri);
  }

  private void SwigDirectoronMessageUpdate(IntPtr req) {
    onMessageUpdate(new ANNOUNCEMENT_REQUEST(req, false));
  }

  private void SwigDirectoronMessageDelete(IntPtr req) {
    onMessageDelete(new ANNOUNCEMENT_REQUEST(req, false));
  }

  public delegate void SwigDelegatePAControllerObserver_0(IntPtr source);
  public delegate void SwigDelegatePAControllerObserver_1(int sourceId);
  public delegate void SwigDelegatePAControllerObserver_2(IntPtr sink);
  public delegate void SwigDelegatePAControllerObserver_3(int sinkId);
  public delegate void SwigDelegatePAControllerObserver_4(IntPtr trigger);
  public delegate void SwigDelegatePAControllerObserver_5(int triggerId);
  public delegate void SwigDelegatePAControllerObserver_6(IntPtr selector);
  public delegate void SwigDelegatePAControllerObserver_7(int selectorId);
  public delegate void SwigDelegatePAControllerObserver_8(IntPtr schedule);
  public delegate void SwigDelegatePAControllerObserver_9(IntPtr schedule);
  public delegate void SwigDelegatePAControllerObserver_10(IntPtr paZone);
  public delegate void SwigDelegatePAControllerObserver_11(string zoneId);
  public delegate void SwigDelegatePAControllerObserver_12(string uuid, bool success, int errorCode, string uri);
  public delegate void SwigDelegatePAControllerObserver_13(IntPtr req);
  public delegate void SwigDelegatePAControllerObserver_14(IntPtr req);

  private SwigDelegatePAControllerObserver_0 swigDelegate0;
  private SwigDelegatePAControllerObserver_1 swigDelegate1;
  private SwigDelegatePAControllerObserver_2 swigDelegate2;
  private SwigDelegatePAControllerObserver_3 swigDelegate3;
  private SwigDelegatePAControllerObserver_4 swigDelegate4;
  private SwigDelegatePAControllerObserver_5 swigDelegate5;
  private SwigDelegatePAControllerObserver_6 swigDelegate6;
  private SwigDelegatePAControllerObserver_7 swigDelegate7;
  private SwigDelegatePAControllerObserver_8 swigDelegate8;
  private SwigDelegatePAControllerObserver_9 swigDelegate9;
  private SwigDelegatePAControllerObserver_10 swigDelegate10;
  private SwigDelegatePAControllerObserver_11 swigDelegate11;
  private SwigDelegatePAControllerObserver_12 swigDelegate12;
  private SwigDelegatePAControllerObserver_13 swigDelegate13;
  private SwigDelegatePAControllerObserver_14 swigDelegate14;

  private static Type[] swigMethodTypes0 = new Type[] { typeof(PaSource) };
  private static Type[] swigMethodTypes1 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes2 = new Type[] { typeof(PaSink) };
  private static Type[] swigMethodTypes3 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes4 = new Type[] { typeof(PaTrigger) };
  private static Type[] swigMethodTypes5 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes6 = new Type[] { typeof(PaSelector) };
  private static Type[] swigMethodTypes7 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes8 = new Type[] { typeof(ScheduleDefinition) };
  private static Type[] swigMethodTypes9 = new Type[] { typeof(ScheduleDefinition) };
  private static Type[] swigMethodTypes10 = new Type[] { typeof(PaZone) };
  private static Type[] swigMethodTypes11 = new Type[] { typeof(string) };
  private static Type[] swigMethodTypes12 = new Type[] { typeof(string), typeof(bool), typeof(PAControllerObserver.MessageRetrievalError), typeof(string) };
  private static Type[] swigMethodTypes13 = new Type[] { typeof(ANNOUNCEMENT_REQUEST) };
  private static Type[] swigMethodTypes14 = new Type[] { typeof(ANNOUNCEMENT_REQUEST) };
  public enum MessageRetrievalError {
    MRE_NO_ERROR = 0,
    MRE_TIMED_OUT,
    MRE_INVALID_PARAMS
  }

}

}
