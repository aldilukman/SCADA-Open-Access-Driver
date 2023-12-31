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

public class PaSinkArray : IDisposable, System.Collections.IEnumerable
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerable<PaSink>
#endif
 {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PaSinkArray(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PaSinkArray obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PaSinkArray() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PaSinkArray(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public PaSinkArray(System.Collections.ICollection c) : this() {
    if (c == null)
      throw new ArgumentNullException("c");
    foreach (PaSink element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public PaSink this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < size())
        throw new ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array)
#else
  public void CopyTo(PaSink[] array)
#endif
  {
    CopyTo(0, array, 0, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array, int arrayIndex)
#else
  public void CopyTo(PaSink[] array, int arrayIndex)
#endif
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(int index, System.Array array, int arrayIndex, int count)
#else
  public void CopyTo(int index, PaSink[] array, int arrayIndex, int count)
#endif
  {
    if (array == null)
      throw new ArgumentNullException("array");
    if (index < 0)
      throw new ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

#if !SWIG_DOTNET_1
  System.Collections.Generic.IEnumerator<PaSink> System.Collections.Generic.IEnumerable<PaSink>.GetEnumerator() {
    return new PaSinkArrayEnumerator(this);
  }
#endif

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    return new PaSinkArrayEnumerator(this);
  }

  public PaSinkArrayEnumerator GetEnumerator() {
    return new PaSinkArrayEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class PaSinkArrayEnumerator : System.Collections.IEnumerator
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerator<PaSink>
#endif
  {
    private PaSinkArray collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public PaSinkArrayEnumerator(PaSinkArray collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public PaSink Current {
      get {
        if (currentIndex == -1)
          throw new InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new InvalidOperationException("Collection modified.");
        return (PaSink)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new InvalidOperationException("Collection modified.");
      }
    }

#if !SWIG_DOTNET_1
    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
#endif
  }

  public void Clear() {
    netspireSDKPINVOKE.PaSinkArray_Clear(swigCPtr);
  }

  public void Add(PaSink x) {
    netspireSDKPINVOKE.PaSinkArray_Add(swigCPtr, PaSink.getCPtr(x));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = netspireSDKPINVOKE.PaSinkArray_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = netspireSDKPINVOKE.PaSinkArray_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    netspireSDKPINVOKE.PaSinkArray_reserve(swigCPtr, n);
  }

  public PaSinkArray() : this(netspireSDKPINVOKE.new_PaSinkArray__SWIG_0(), true) {
  }

  public PaSinkArray(PaSinkArray other) : this(netspireSDKPINVOKE.new_PaSinkArray__SWIG_1(PaSinkArray.getCPtr(other)), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public PaSinkArray(int capacity) : this(netspireSDKPINVOKE.new_PaSinkArray__SWIG_2(capacity), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private PaSink getitemcopy(int index) {
    PaSink ret = new PaSink(netspireSDKPINVOKE.PaSinkArray_getitemcopy(swigCPtr, index), true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private PaSink getitem(int index) {
    PaSink ret = new PaSink(netspireSDKPINVOKE.PaSinkArray_getitem(swigCPtr, index), false);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, PaSink val) {
    netspireSDKPINVOKE.PaSinkArray_setitem(swigCPtr, index, PaSink.getCPtr(val));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(PaSinkArray values) {
    netspireSDKPINVOKE.PaSinkArray_AddRange(swigCPtr, PaSinkArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public PaSinkArray GetRange(int index, int count) {
    IntPtr cPtr = netspireSDKPINVOKE.PaSinkArray_GetRange(swigCPtr, index, count);
    PaSinkArray ret = (cPtr == IntPtr.Zero) ? null : new PaSinkArray(cPtr, true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, PaSink x) {
    netspireSDKPINVOKE.PaSinkArray_Insert(swigCPtr, index, PaSink.getCPtr(x));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, PaSinkArray values) {
    netspireSDKPINVOKE.PaSinkArray_InsertRange(swigCPtr, index, PaSinkArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    netspireSDKPINVOKE.PaSinkArray_RemoveAt(swigCPtr, index);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    netspireSDKPINVOKE.PaSinkArray_RemoveRange(swigCPtr, index, count);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public static PaSinkArray Repeat(PaSink value, int count) {
    IntPtr cPtr = netspireSDKPINVOKE.PaSinkArray_Repeat(PaSink.getCPtr(value), count);
    PaSinkArray ret = (cPtr == IntPtr.Zero) ? null : new PaSinkArray(cPtr, true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    netspireSDKPINVOKE.PaSinkArray_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    netspireSDKPINVOKE.PaSinkArray_Reverse__SWIG_1(swigCPtr, index, count);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, PaSinkArray values) {
    netspireSDKPINVOKE.PaSinkArray_SetRange(swigCPtr, index, PaSinkArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
