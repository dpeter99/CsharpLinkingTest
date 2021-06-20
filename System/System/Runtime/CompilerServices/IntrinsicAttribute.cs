// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace System.Runtime.CompilerServices
{
    // This attribute is only for use in a Class Library
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Field, Inherited = false)]
    internal sealed class IntrinsicAttribute : Attribute
    {
    }

    internal class AttributeUsageAttribute
    {
        public AttributeUsageAttribute(Object constructor)
        {
            throw new Exception();
        }
    }
}