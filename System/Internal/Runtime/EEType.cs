using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime;

namespace System.Internal.Runtime
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ObjHeader
    {
        // Contents of the object header
        private IntPtr _objHeaderContents;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct EEInterfaceInfo
    {
        [StructLayout(LayoutKind.Explicit)]
        private unsafe struct InterfaceTypeUnion
        {
            [FieldOffset(0)]
            public EEType* _pInterfaceEEType;
            [FieldOffset(0)]
            public EEType** _ppInterfaceEETypeViaIAT;
        }

        private InterfaceTypeUnion _interfaceType;

        internal EEType* InterfaceType
        {
            get
            {
                if ((unchecked((uint)_interfaceType._pInterfaceEEType) & IndirectionConstants.IndirectionCellPointer) != 0)
                {
#if TARGET_64BIT
                    EEType** ppInterfaceEETypeViaIAT = (EEType**)(((ulong)_interfaceType._ppInterfaceEETypeViaIAT) - IndirectionConstants.IndirectionCellPointer);
#else
                    EEType** ppInterfaceEETypeViaIAT = (EEType**)(((uint)_interfaceType._ppInterfaceEETypeViaIAT) - IndirectionConstants.IndirectionCellPointer);
#endif
                    return *ppInterfaceEETypeViaIAT;
                }

                return _interfaceType._pInterfaceEEType;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _interfaceType._pInterfaceEEType = value;
            }
#endif
        }
    }
    
    
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe partial struct EEType
    {
        
#if TARGET_64BIT
        private const int POINTER_SIZE = 8;
        private const int PADDING = 1; // _numComponents is padded by one Int32 to make the first element pointer-aligned
#else
        private const int POINTER_SIZE = 4;
        private const int PADDING = 0;
#endif
        internal const int SZARRAY_BASE_SIZE = POINTER_SIZE + POINTER_SIZE + (1 + PADDING) * 4;
        
        
        [StructLayout(LayoutKind.Explicit)]
        private unsafe struct RelatedTypeUnion
        {
            // Kinds.CanonicalEEType
            [FieldOffset(0)]
            public EEType* _pBaseType;
            [FieldOffset(0)]
            public EEType** _ppBaseTypeViaIAT;

            // Kinds.ClonedEEType
            [FieldOffset(0)]
            public EEType* _pCanonicalType;
            [FieldOffset(0)]
            public EEType** _ppCanonicalTypeViaIAT;

            // Kinds.ArrayEEType
            [FieldOffset(0)]
            public EEType* _pRelatedParameterType;
            [FieldOffset(0)]
            public EEType** _ppRelatedParameterTypeViaIAT;
        }
        
        /// <summary>
        /// Gets a value indicating whether the statically generated data structures use relative pointers.
        /// </summary>
        internal static bool SupportsRelativePointers
        {
            [Intrinsic]
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether writable data is supported.
        /// </summary>
        internal static bool SupportsWritableData
        {
            get
            {
                // For now just key this off of SupportsRelativePointer to avoid this on both CppCodegen and WASM.
                return SupportsRelativePointers;
            }
        }

        private ushort _usComponentSize;
        private ushort _usFlags;
        private uint _uBaseSize;
        private RelatedTypeUnion _relatedType;
        private ushort _usNumVtableSlots;
        private ushort _usNumInterfaces;
        private uint _uHashCode;
        
        // vtable follows

        // These masks and paddings have been chosen so that the ValueTypePadding field can always fit in a byte of data.
        // if the alignment is 8 bytes or less. If the alignment is higher then there may be a need for more bits to hold
        // the rest of the padding data.
        // If paddings of greater than 7 bytes are necessary, then the high bits of the field represent that padding
        private const uint ValueTypePaddingLowMask = 0x7;
        private const uint ValueTypePaddingHighMask = 0xFFFFFF00;
        private const uint ValueTypePaddingMax = 0x07FFFFFF;
        private const int ValueTypePaddingHighShift = 8;
        private const uint ValueTypePaddingAlignmentMask = 0xF8;
        private const int ValueTypePaddingAlignmentShift = 3;

        internal ushort ComponentSize
        {
            get
            {
                return _usComponentSize;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _usComponentSize = value;
            }
#endif
        }
        
        internal ushort GenericArgumentCount
        {
            get
            {
                Debug.Assert(IsGenericTypeDefinition);
                return _usComponentSize;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                Debug.Assert(IsGenericTypeDefinition);
                _usComponentSize = value;
            }
#endif
        }

        internal ushort Flags
        {
            get
            {
                return _usFlags;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _usFlags = value;
            }
#endif
        }

        internal uint BaseSize
        {
            get
            {
                return _uBaseSize;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _uBaseSize = value;
            }
#endif
        }
        
        internal ushort NumVtableSlots
        {
            get
            {
                return _usNumVtableSlots;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _usNumVtableSlots = value;
            }
#endif
        }

        internal ushort NumInterfaces
        {
            get
            {
                return _usNumInterfaces;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _usNumInterfaces = value;
            }
#endif
        }

        internal uint HashCode
        {
            get
            {
                return _uHashCode;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _uHashCode = value;
            }
#endif
        }

        private EETypeKind Kind
        {
            get
            {
                return (EETypeKind)(_usFlags & (ushort)EETypeFlags.EETypeKindMask);
            }
        }

        internal bool HasOptionalFields
        {
            get
            {
                return ((_usFlags & (ushort)EETypeFlags.OptionalFieldsFlag) != 0);
            }
        }

        // Mark or determine that a type is generic and one or more of it's type parameters is co- or
        // contra-variant. This only applies to interface and delegate types.
        internal bool HasGenericVariance
        {
            get
            {
                return ((_usFlags & (ushort)EETypeFlags.GenericVarianceFlag) != 0);
            }
        }

        internal bool IsFinalizable
        {
            get
            {
                return ((_usFlags & (ushort)EETypeFlags.HasFinalizerFlag) != 0);
            }
        }

        internal bool IsNullable
        {
            get
            {
                return ElementType == EETypeElementType.Nullable;
            }
        }

        internal bool IsCloned
        {
            get
            {
                return Kind == EETypeKind.ClonedEEType;
            }
        }

        internal bool IsCanonical
        {
            get
            {
                return Kind == EETypeKind.CanonicalEEType;
            }
        }

        internal bool IsString
        {
            get
            {
                // String is currently the only non-array type with a non-zero component size.
                return ComponentSize == StringComponentSize.Value && !IsArray && !IsGenericTypeDefinition;
            }
        }

        internal bool IsArray
        {
            get
            {
                EETypeElementType elementType = ElementType;
                return elementType == EETypeElementType.Array || elementType == EETypeElementType.SzArray;
            }
        }


        internal int ArrayRank
        {
            get
            {
                Debug.Assert(this.IsArray);

                int boundsSize = (int)this.ParameterizedTypeShape - SZARRAY_BASE_SIZE;
                if (boundsSize > 0)
                {
                    // Multidim array case: Base size includes space for two Int32s
                    // (upper and lower bound) per each dimension of the array.
                    return boundsSize / (2 * sizeof(int));
                }
                return 1;
            }
        }

        internal bool IsSzArray
        {
            get
            {
                return ElementType == EETypeElementType.SzArray;
            }
        }

        internal bool IsGeneric
        {
            get
            {
                return ((_usFlags & (ushort)EETypeFlags.IsGenericFlag) != 0);
            }
        }

        internal bool IsGenericTypeDefinition
        {
            get
            {
                return Kind == EETypeKind.GenericTypeDefEEType;
            }
        }
        

#if TYPE_LOADER_IMPLEMENTATION
        internal static int GetGenericCompositionSize(int numArguments, bool hasVariance)
        {
            return IntPtr.Size
                + numArguments * IntPtr.Size
                + (hasVariance ? numArguments * sizeof(GenericVariance) : 0);
        }

        internal void SetGenericComposition(IntPtr data)
        {
            Debug.Assert(IsGeneric && IsDynamicType);
            uint cbOffset = GetFieldOffset(EETypeField.ETF_GenericComposition);
            fixed (EEType* pThis = &this)
            {
                *((IntPtr*)((byte*)pThis + cbOffset)) = data;
            }
        }
#endif


        // The parameterized type shape defines the particular form of parameterized type that
        // is being represented.
        // Currently, the meaning is a shape of 0 indicates that this is a Pointer,
        // shape of 1 indicates a ByRef, and >=SZARRAY_BASE_SIZE indicates that this is an array.
        // Two types are not equivalent if their shapes do not exactly match.
        internal uint ParameterizedTypeShape
        {
            get
            {
                return _uBaseSize;
            }
#if TYPE_LOADER_IMPLEMENTATION
            set
            {
                _uBaseSize = value;
            }
#endif
        }
        
        internal EETypeElementType ElementType
        {
            get
            {
                return (EETypeElementType)((_usFlags & (ushort)EETypeFlags.ElementTypeMask) >> (ushort)EETypeFlags.ElementTypeShift);
            }
        }
        
    }
}