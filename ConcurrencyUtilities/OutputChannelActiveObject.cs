﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyUtils
{
    /// <summary>
    ///     Channel-based active object that runs some process and places the data on
    ///     its output channel.
	/// 
	/// 	Author: Daniel Parker 971328X
    /// </summary>
    /// <typeparam name="OT">The type of data on the output channel.</typeparam>
    public abstract class OutputChannelActiveObject<OT>: ActiveObject
    {
        /// <summary>
        ///     The channel that this active object will place
        ///     the results of processing onto.
        /// </summary>
        public Channel<OT> outputChannel = new Channel<OT>();

        /// <summary>
        ///     Public constructor.
        /// </summary>
        public OutputChannelActiveObject() : base() { }

        /// <summary>
        ///     Loops forever putting the result of the 'Process' method on 
        ///     this active object's output channel.
        /// </summary>
        protected override void Execute()
        {
			while (true)
			{
				outputChannel.Put(Process());
			}
        }

        /// <summary>
        ///     Run a process and return the result of the process.
        ///     Must be implemented by subclass.
        /// </summary>
        /// <returns></returns>
        protected abstract OT Process();
    }
}
