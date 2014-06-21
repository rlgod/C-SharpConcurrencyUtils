﻿using System;
using System.Collections.Generic;
using ConcurrencyUtils;

namespace ActiveObjectVowelCount
{
	public class VowelCount: InputOutputChannelActiveObject<char, char>
	{
		private Semaphore finishedSemaphore = new Semaphore(0);
		Dictionary<char, int> counts = new Dictionary<char, int>();
		public VowelCount() : base()
		{
			counts.Add('a', 0);
			counts.Add('e', 0);
			counts.Add('i', 0);
			counts.Add('o', 0);
			counts.Add('u', 0);
		}

		protected override char Process(char data)
		{
			if (data != (char)0)
			{
				counts[data] = ++counts[data];
				return data;
			} else
			{
				finishedSemaphore.Release();
				return '\n';
			}
		}

		public void Reset()
		{
			counts['a'] = 0;
			counts['e'] = 0;
			counts['i'] = 0;
			counts['o'] = 0;
			counts['u'] = 0;
		}

		public void PrintCount()
		{
			finishedSemaphore.Acquire();
			foreach (char c in counts.Keys)
			{
				Console.WriteLine("\'" + c + "\' occurred " + counts[c] + " times");
			}
			Reset();
		}
	}
}
