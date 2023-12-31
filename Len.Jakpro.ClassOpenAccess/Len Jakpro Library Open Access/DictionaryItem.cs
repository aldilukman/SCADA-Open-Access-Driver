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

public class DictionaryItem : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DictionaryItem(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DictionaryItem obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DictionaryItem() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_DictionaryItem(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public int itemNo {
    set {
      netspireSDKPINVOKE.DictionaryItem_itemNo_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryItem_itemNo_get(swigCPtr);
      return ret;
    } 
  }

  public string description {
    set {
      netspireSDKPINVOKE.DictionaryItem_description_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryItem_description_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string category {
    set {
      netspireSDKPINVOKE.DictionaryItem_category_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryItem_category_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string displayText {
    set {
      netspireSDKPINVOKE.DictionaryItem_displayText_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryItem_displayText_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string fileList {
    set {
      netspireSDKPINVOKE.DictionaryItem_fileList_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryItem_fileList_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string metadata {
    set {
      netspireSDKPINVOKE.DictionaryItem_metadata_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryItem_metadata_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public DictionaryItem() : this(netspireSDKPINVOKE.new_DictionaryItem(), true) {
  }

}

}
