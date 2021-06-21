// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Internal.Runtime
{
    internal static class IndirectionConstants
    {
        /// <summary>
        /// Flag set on pointers to indirection cells to distinguish them
        /// from pointers to the object directly
        /// </summary>
        public const int IndirectionCellPointer = 0x1;

        /// <summary>
        /// Flag set on RVAs to indirection cells to distinguish them
        /// from RVAs to the object directly
        /// </summary>
        public const uint RVAPointsToIndirection = 0x80000000u;
    }
}