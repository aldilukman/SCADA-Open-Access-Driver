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

public class CallControllerObserver : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal CallControllerObserver(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(CallControllerObserver obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~CallControllerObserver() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_CallControllerObserver(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public CallControllerObserver() : this(netspireSDKPINVOKE.new_CallControllerObserver(), true) {
    SwigDirectorConnect();
  }

  public virtual void onCallUpdate(CallInfo callInfo) {
    if (SwigDerivedClassHasMethod("onCallUpdate", swigMethodTypes0)) netspireSDKPINVOKE.CallControllerObserver_onCallUpdateSwigExplicitCallControllerObserver(swigCPtr, CallInfo.getCPtr(callInfo)); else netspireSDKPINVOKE.CallControllerObserver_onCallUpdate(swigCPtr, CallInfo.getCPtr(callInfo));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onCallDelete(int callId) {
    if (SwigDerivedClassHasMethod("onCallDelete", swigMethodTypes1)) netspireSDKPINVOKE.CallControllerObserver_onCallDeleteSwigExplicitCallControllerObserver(swigCPtr, callId); else netspireSDKPINVOKE.CallControllerObserver_onCallDelete(swigCPtr, callId);
  }

  public virtual void onCDRMessageUpdate(CDRInfo cdrInfo) {
    if (SwigDerivedClassHasMethod("onCDRMessageUpdate", swigMethodTypes2)) netspireSDKPINVOKE.CallControllerObserver_onCDRMessageUpdateSwigExplicitCallControllerObserver(swigCPtr, CDRInfo.getCPtr(cdrInfo)); else netspireSDKPINVOKE.CallControllerObserver_onCDRMessageUpdate(swigCPtr, CDRInfo.getCPtr(cdrInfo));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onCDRMessageDelete(int cdrId) {
    if (SwigDerivedClassHasMethod("onCDRMessageDelete", swigMethodTypes3)) netspireSDKPINVOKE.CallControllerObserver_onCDRMessageDeleteSwigExplicitCallControllerObserver(swigCPtr, cdrId); else netspireSDKPINVOKE.CallControllerObserver_onCDRMessageDelete(swigCPtr, cdrId);
  }

  public virtual void onTerminalUpdate(TerminalInfo termInfo) {
    if (SwigDerivedClassHasMethod("onTerminalUpdate", swigMethodTypes4)) netspireSDKPINVOKE.CallControllerObserver_onTerminalUpdateSwigExplicitCallControllerObserver(swigCPtr, TerminalInfo.getCPtr(termInfo)); else netspireSDKPINVOKE.CallControllerObserver_onTerminalUpdate(swigCPtr, TerminalInfo.getCPtr(termInfo));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onTerminalDelete(int termId) {
    if (SwigDerivedClassHasMethod("onTerminalDelete", swigMethodTypes5)) netspireSDKPINVOKE.CallControllerObserver_onTerminalDeleteSwigExplicitCallControllerObserver(swigCPtr, termId); else netspireSDKPINVOKE.CallControllerObserver_onTerminalDelete(swigCPtr, termId);
  }

  public virtual void onSIPTrunkUpdate(SIPTrunk sipTrunk) {
    if (SwigDerivedClassHasMethod("onSIPTrunkUpdate", swigMethodTypes6)) netspireSDKPINVOKE.CallControllerObserver_onSIPTrunkUpdateSwigExplicitCallControllerObserver(swigCPtr, SIPTrunk.getCPtr(sipTrunk)); else netspireSDKPINVOKE.CallControllerObserver_onSIPTrunkUpdate(swigCPtr, SIPTrunk.getCPtr(sipTrunk));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onSIPTrunkDelete(SIPTrunk sipTrunk) {
    if (SwigDerivedClassHasMethod("onSIPTrunkDelete", swigMethodTypes7)) netspireSDKPINVOKE.CallControllerObserver_onSIPTrunkDeleteSwigExplicitCallControllerObserver(swigCPtr, SIPTrunk.getCPtr(sipTrunk)); else netspireSDKPINVOKE.CallControllerObserver_onSIPTrunkDelete(swigCPtr, SIPTrunk.getCPtr(sipTrunk));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onISDNTrunkUpdate(ISDNTrunk isdnTrunk) {
    if (SwigDerivedClassHasMethod("onISDNTrunkUpdate", swigMethodTypes8)) netspireSDKPINVOKE.CallControllerObserver_onISDNTrunkUpdateSwigExplicitCallControllerObserver(swigCPtr, ISDNTrunk.getCPtr(isdnTrunk)); else netspireSDKPINVOKE.CallControllerObserver_onISDNTrunkUpdate(swigCPtr, ISDNTrunk.getCPtr(isdnTrunk));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void onISDNTrunkDelete(ISDNTrunk isdnTrunk) {
    if (SwigDerivedClassHasMethod("onISDNTrunkDelete", swigMethodTypes9)) netspireSDKPINVOKE.CallControllerObserver_onISDNTrunkDeleteSwigExplicitCallControllerObserver(swigCPtr, ISDNTrunk.getCPtr(isdnTrunk)); else netspireSDKPINVOKE.CallControllerObserver_onISDNTrunkDelete(swigCPtr, ISDNTrunk.getCPtr(isdnTrunk));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("onCallUpdate", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateCallControllerObserver_0(SwigDirectoronCallUpdate);
    if (SwigDerivedClassHasMethod("onCallDelete", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateCallControllerObserver_1(SwigDirectoronCallDelete);
    if (SwigDerivedClassHasMethod("onCDRMessageUpdate", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateCallControllerObserver_2(SwigDirectoronCDRMessageUpdate);
    if (SwigDerivedClassHasMethod("onCDRMessageDelete", swigMethodTypes3))
      swigDelegate3 = new SwigDelegateCallControllerObserver_3(SwigDirectoronCDRMessageDelete);
    if (SwigDerivedClassHasMethod("onTerminalUpdate", swigMethodTypes4))
      swigDelegate4 = new SwigDelegateCallControllerObserver_4(SwigDirectoronTerminalUpdate);
    if (SwigDerivedClassHasMethod("onTerminalDelete", swigMethodTypes5))
      swigDelegate5 = new SwigDelegateCallControllerObserver_5(SwigDirectoronTerminalDelete);
    if (SwigDerivedClassHasMethod("onSIPTrunkUpdate", swigMethodTypes6))
      swigDelegate6 = new SwigDelegateCallControllerObserver_6(SwigDirectoronSIPTrunkUpdate);
    if (SwigDerivedClassHasMethod("onSIPTrunkDelete", swigMethodTypes7))
      swigDelegate7 = new SwigDelegateCallControllerObserver_7(SwigDirectoronSIPTrunkDelete);
    if (SwigDerivedClassHasMethod("onISDNTrunkUpdate", swigMethodTypes8))
      swigDelegate8 = new SwigDelegateCallControllerObserver_8(SwigDirectoronISDNTrunkUpdate);
    if (SwigDerivedClassHasMethod("onISDNTrunkDelete", swigMethodTypes9))
      swigDelegate9 = new SwigDelegateCallControllerObserver_9(SwigDirectoronISDNTrunkDelete);
    netspireSDKPINVOKE.CallControllerObserver_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3, swigDelegate4, swigDelegate5, swigDelegate6, swigDelegate7, swigDelegate8, swigDelegate9);
  }

  private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes) {
    System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(CallControllerObserver));
    return hasDerivedMethod;
  }

  private void SwigDirectoronCallUpdate(IntPtr callInfo) {
    onCallUpdate(new CallInfo(callInfo, false));
  }

  private void SwigDirectoronCallDelete(int callId) {
    onCallDelete(callId);
  }

  private void SwigDirectoronCDRMessageUpdate(IntPtr cdrInfo) {
    onCDRMessageUpdate(new CDRInfo(cdrInfo, false));
  }

  private void SwigDirectoronCDRMessageDelete(int cdrId) {
    onCDRMessageDelete(cdrId);
  }

  private void SwigDirectoronTerminalUpdate(IntPtr termInfo) {
    onTerminalUpdate(new TerminalInfo(termInfo, false));
  }

  private void SwigDirectoronTerminalDelete(int termId) {
    onTerminalDelete(termId);
  }

  private void SwigDirectoronSIPTrunkUpdate(IntPtr sipTrunk) {
    onSIPTrunkUpdate(new SIPTrunk(sipTrunk, false));
  }

  private void SwigDirectoronSIPTrunkDelete(IntPtr sipTrunk) {
    onSIPTrunkDelete(new SIPTrunk(sipTrunk, false));
  }

  private void SwigDirectoronISDNTrunkUpdate(IntPtr isdnTrunk) {
    onISDNTrunkUpdate(new ISDNTrunk(isdnTrunk, false));
  }

  private void SwigDirectoronISDNTrunkDelete(IntPtr isdnTrunk) {
    onISDNTrunkDelete(new ISDNTrunk(isdnTrunk, false));
  }

  public delegate void SwigDelegateCallControllerObserver_0(IntPtr callInfo);
  public delegate void SwigDelegateCallControllerObserver_1(int callId);
  public delegate void SwigDelegateCallControllerObserver_2(IntPtr cdrInfo);
  public delegate void SwigDelegateCallControllerObserver_3(int cdrId);
  public delegate void SwigDelegateCallControllerObserver_4(IntPtr termInfo);
  public delegate void SwigDelegateCallControllerObserver_5(int termId);
  public delegate void SwigDelegateCallControllerObserver_6(IntPtr sipTrunk);
  public delegate void SwigDelegateCallControllerObserver_7(IntPtr sipTrunk);
  public delegate void SwigDelegateCallControllerObserver_8(IntPtr isdnTrunk);
  public delegate void SwigDelegateCallControllerObserver_9(IntPtr isdnTrunk);

  private SwigDelegateCallControllerObserver_0 swigDelegate0;
  private SwigDelegateCallControllerObserver_1 swigDelegate1;
  private SwigDelegateCallControllerObserver_2 swigDelegate2;
  private SwigDelegateCallControllerObserver_3 swigDelegate3;
  private SwigDelegateCallControllerObserver_4 swigDelegate4;
  private SwigDelegateCallControllerObserver_5 swigDelegate5;
  private SwigDelegateCallControllerObserver_6 swigDelegate6;
  private SwigDelegateCallControllerObserver_7 swigDelegate7;
  private SwigDelegateCallControllerObserver_8 swigDelegate8;
  private SwigDelegateCallControllerObserver_9 swigDelegate9;

  private static Type[] swigMethodTypes0 = new Type[] { typeof(CallInfo) };
  private static Type[] swigMethodTypes1 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes2 = new Type[] { typeof(CDRInfo) };
  private static Type[] swigMethodTypes3 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes4 = new Type[] { typeof(TerminalInfo) };
  private static Type[] swigMethodTypes5 = new Type[] { typeof(int) };
  private static Type[] swigMethodTypes6 = new Type[] { typeof(SIPTrunk) };
  private static Type[] swigMethodTypes7 = new Type[] { typeof(SIPTrunk) };
  private static Type[] swigMethodTypes8 = new Type[] { typeof(ISDNTrunk) };
  private static Type[] swigMethodTypes9 = new Type[] { typeof(ISDNTrunk) };
}

}
