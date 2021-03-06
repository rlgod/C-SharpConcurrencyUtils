﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyUtils
{
	/// <summary>
	/// 	Concurrency Light switch utility.
	/// 
	/// 	Author: Daniel Parker 971328X
	/// </summary>
    public class LightSwitch
    {
		private readonly Semaphore lSemaphore;
        private int count;

		/// <summary>
		/// 	Initializes a new instance of the 
		///		<see cref="ConcurrencyUtils.LightSwitch"/> class.
		/// </summary>
		/// <param name="s">S.</param>
        public LightSwitch(Semaphore s)
        {
            lSemaphore = s;
        }

		/// <summary>
		/// 	Acquire the Lightswitch.
		/// </summary>
        public void Acquire()
        {
            lock (this)
            {
                if (count == 0)
                {
					lSemaphore.Acquire();
                }
                count++;
            }
        }

		/// <summary>
		///		Release the Lightswitch.
		/// </summary>
        public void Release()
        {
            count--;
            if (count == 0)
            {
				lSemaphore.Release();
            }
        }
    }
}
