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

public class PaZoneArray : IDisposable, System.Collections.IEnumerable
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerable<PaZone>
#endif
 {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PaZoneArray(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PaZoneArray obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PaZoneArray() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          netspireSDKPINVOKE.delete_PaZoneArray(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public PaZoneArray(System.Collections.ICollection c) : this() {
    if (c == null)
      throw new ArgumentNullException("c");
    foreach (PaZone element in c) {
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

  public PaZone this[int index]  {
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
  public void CopyTo(PaZone[] array)
#endif
  {
    CopyTo(0, array, 0, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array, int arrayIndex)
#else
  public void CopyTo(PaZone[] array, int arrayIndex)
#endif
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(int index, System.Array array, int arrayIndex, int count)
#else
  public void CopyTo(int index, PaZone[] array, int arrayIndex, int count)
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
  System.Collections.Generic.IEnumerator<PaZone> System.Collections.Generic.IEnumerable<PaZone>.GetEnumerator() {
    return new PaZoneArrayEnumerator(this);
  }
#endif

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    return new PaZoneArrayEnumerator(this);
  }

  public PaZoneArrayEnumerator GetEnumerator() {
    return new PaZoneArrayEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class PaZoneArrayEnumerator : System.Collections.IEnumerator
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerator<PaZone>
#endif
  {
    private PaZoneArray collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public PaZoneArrayEnumerator(PaZoneArray collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public PaZone Current {
      get {
        if (currentIndex == -1)
          throw new InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new InvalidOperationException("Collection modified.");
        return (PaZone)currentObject;
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
    netspireSDKPINVOKE.PaZoneArray_Clear(swigCPtr);
  }

  public void Add(PaZone x) {
    netspireSDKPINVOKE.PaZoneArray_Add(swigCPtr, PaZone.getCPtr(x));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = netspireSDKPINVOKE.PaZoneArray_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = netspireSDKPINVOKE.PaZoneArray_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    netspireSDKPINVOKE.PaZoneArray_reserve(swigCPtr, n);
  }

  public PaZoneArray() : this(netspireSDKPINVOKE.new_PaZoneArray__SWIG_0(), true) {
  }

  public PaZoneArray(PaZoneArray other) : this(netspireSDKPINVOKE.new_PaZoneArray__SWIG_1(PaZoneArray.getCPtr(other)), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public PaZoneArray(int capacity) : this(netspireSDKPINVOKE.new_PaZoneArray__SWIG_2(capacity), true) {
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  private PaZone getitemcopy(int index) {
    PaZone ret = new PaZone(netspireSDKPINVOKE.PaZoneArray_getitemcopy(swigCPtr, index), true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private PaZone getitem(int index) {
    PaZone ret = new PaZone(netspireSDKPINVOKE.PaZoneArray_getitem(swigCPtr, index), false);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, PaZone val) {
    netspireSDKPINVOKE.PaZoneArray_setitem(swigCPtr, index, PaZone.getCPtr(val));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(PaZoneArray values) {
    netspireSDKPINVOKE.PaZoneArray_AddRange(swigCPtr, PaZoneArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public PaZoneArray GetRange(int index, int count) {
    IntPtr cPtr = netspireSDKPINVOKE.PaZoneArray_GetRange(swigCPtr, index, count);
    PaZoneArray ret = (cPtr == IntPtr.Zero) ? null : new PaZoneArray(cPtr, true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, PaZone x) {
    netspireSDKPINVOKE.PaZoneArray_Insert(swigCPtr, index, PaZone.getCPtr(x));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, PaZoneArray values) {
    netspireSDKPINVOKE.PaZoneArray_InsertRange(swigCPtr, index, PaZoneArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    netspireSDKPINVOKE.PaZoneArray_RemoveAt(swigCPtr, index);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    netspireSDKPINVOKE.PaZoneArray_RemoveRange(swigCPtr, index, count);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public static PaZoneArray Repeat(PaZone value, int count) {
    IntPtr cPtr = netspireSDKPINVOKE.PaZoneArray_Repeat(PaZone.getCPtr(value), count);
    PaZoneArray ret = (cPtr == IntPtr.Zero) ? null : new PaZoneArray(cPtr, true);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    netspireSDKPINVOKE.PaZoneArray_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    netspireSDKPINVOKE.PaZoneArray_Reverse__SWIG_1(swigCPtr, index, count);
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, PaZoneArray values) {
    netspireSDKPINVOKE.PaZoneArray_SetRange(swigCPtr, index, PaZoneArray.getCPtr(values));
    if (netspireSDKPINVOKE.SWIGPendingException.Pending) throw netspireSDKPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
