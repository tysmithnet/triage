// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-19-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="DumpObjectRoot.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Core
{
    /// <summary>
    ///     An object that represents a GC root discovered in the memory dump
    ///     A GC root is a reference like entity that keeps an object alive in memory
    /// </summary>
    public class DumpObjectRoot
    {
        /// <summary>
        ///     Adds the thread.
        /// </summary>
        /// <param name="thread">The thread.</param>
        public void AddThread(DumpThread thread)
        {
            Threads.Add(thread);
        }

        /// <summary>
        ///     Gets or sets the address of this object root
        /// </summary>
        /// <value>The address.</value>
        public ulong Address { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the app domain where this root exists if it is associated with an app domain, null otherwise
        /// </summary>
        /// <value>The application domain.</value>
        public DumpAppDomain AppDomain { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the kind of the gc root.
        /// </summary>
        /// <value>The kind of the gc root.</value>
        public GcRootKind GcRootKind { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this object root points not to the
        ///     object header, but to some location inside the object
        /// </summary>
        /// <value><c>true</c> if this instance is interior pointer; otherwise, <c>false</c>.</value>
        public bool IsInteriorPointer { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the object being kept in memory is
        ///     preventing from being relocated during the compaction phase of GC
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        public bool IsPinned { get; protected internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is possible false positive.
        ///     This comes from the CLRMd engine that uses heuristics to determine if the object
        ///     being kept alive can in fact be garbage collected
        /// </summary>
        /// <value><c>true</c> if this instance is possible false positive; otherwise, <c>false</c>.</value>
        public bool IsPossibleFalsePositive { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the name of this object root
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the managed to which this root points if possible, null otherwise
        /// </summary>
        /// <value>The rooted object.</value>
        public DumpObject RootedObject { get; protected internal set; }

        /// <summary>
        ///     Gets the stack frame that is keeping where this root can be found
        /// </summary>
        /// <value>The stack frame.</value>
        /// <exception cref="NotImplementedException">You still need to implement stack frame in object root</exception>
        public DumpStackFrame StackFrame { get; protected internal set; }

        /// <summary>
        ///     Gets or sets the threads.
        /// </summary>
        /// <value>The threads.</value>
        public IList<DumpThread> Threads { get; set; } = new List<DumpThread>();

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public DumpType Type { get; set; }
    }
}