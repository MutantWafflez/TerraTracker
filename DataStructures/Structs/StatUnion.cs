using System.Runtime.InteropServices;

namespace TerraTracker.DataStructures.Structs;

/// <summary>
///     "Union" (see C/C++ unions) type that will be used to hold stat data of any one of the applicable types defined in the
///     struct.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct StatUnion {
    [FieldOffset(0)]
    public uint uintValue;

    [FieldOffset(0)]
    public float floatValue;

    [FieldOffset(0)]
    public double doubleValue;

    [FieldOffset(0)]
    public long longValue;
}