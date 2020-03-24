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

public class DictionaryChangesetItem : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DictionaryChangesetItem(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DictionaryChangesetItem obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DictionaryChangesetItem() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_DictionaryChangesetItem(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public int key {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_key_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryChangesetItem_key_get(swigCPtr);
      return ret;
    } 
  }

  public int versionSeq {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_versionSeq_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryChangesetItem_versionSeq_get(swigCPtr);
      return ret;
    } 
  }

  public string operationId {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_operationId_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_operationId_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int newVersion {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_newVersion_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryChangesetItem_newVersion_get(swigCPtr);
      return ret;
    } 
  }

  public int itemNo {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_itemNo_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryChangesetItem_itemNo_get(swigCPtr);
      return ret;
    } 
  }

  public string deviceType {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_deviceType_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_deviceType_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string description {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_description_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_description_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string category {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_category_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_category_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string audioSegment {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_audioSegment_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_audioSegment_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string image {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_image_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_image_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string video {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_video_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_video_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string displayText {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_displayText_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_displayText_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public string metadata {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_metadata_set(swigCPtr, value);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = netspireSDKPINVOKE.DictionaryChangesetItem_metadata_get(swigCPtr);
      if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public int lastUpdate {
    set {
      netspireSDKPINVOKE.DictionaryChangesetItem_lastUpdate_set(swigCPtr, value);
    } 
    get {
      int ret = netspireSDKPINVOKE.DictionaryChangesetItem_lastUpdate_get(swigCPtr);
      return ret;
    } 
  }

  public DictionaryChangesetItem() : this(netspireSDKPINVOKE.new_DictionaryChangesetItem(), true) {
  }

}

}
