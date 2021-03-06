﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DiningPhilosophers
{
	/// <summary>
	/// 	Main Program.
	/// </summary>
    class Program
    {
		/// <summary>
		/// 	The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
			// Permission to eat.
            ConcurrencyUtils.FIFOSemaphore eatPermission = new ConcurrencyUtils.FIFOSemaphore();

			//Initialize the five forks.
            ConcurrencyUtils.Mutex fork1 = new ConcurrencyUtils.Mutex();
            ConcurrencyUtils.Mutex fork2 = new ConcurrencyUtils.Mutex();
            ConcurrencyUtils.Mutex fork3 = new ConcurrencyUtils.Mutex();
            ConcurrencyUtils.Mutex fork4 = new ConcurrencyUtils.Mutex();
            ConcurrencyUtils.Mutex fork5 = new ConcurrencyUtils.Mutex();

			// Initialise the Philosophers with their left and right fork and eat Permission.
            Philosopher p1 = new Philosopher(fork1, fork2, eatPermission);
            Philosopher p2 = new Philosopher(fork2, fork3, eatPermission);
            Philosopher p3 = new Philosopher(fork3, fork4, eatPermission);
            Philosopher p4 = new Philosopher(fork4, fork5, eatPermission);
            Philosopher p5 = new Philosopher(fork5, fork1, eatPermission);

			// Create and start all the Philosopher's threads.
            Thread t1 = new Thread(p1.BeginLifeAmbitions);
            t1.Name = "P1";
            Thread t2 = new Thread(p2.BeginLifeAmbitions);
            t2.Name = "\tP2";
            Thread t3 = new Thread(p3.BeginLifeAmbitions);
            t3.Name = "\t\tP3";
            Thread t4 = new Thread(p4.BeginLifeAmbitions);
            t4.Name = "\t\t\tP4";
            Thread t5 = new Thread(p5.BeginLifeAmbitions);
            t5.Name = "\t\t\t\tP5";

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();

			// Release only 4 tokens into eat permission to ensure no deadlock.
            eatPermission.Release(4);
        }
    }
}
