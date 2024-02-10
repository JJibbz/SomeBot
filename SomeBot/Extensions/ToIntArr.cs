using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeBot.Extensions
{
    public static class ToIntArr
    {
        public static int[] GetIntArray(string message)
        {
            string[] array = message.Split(' ');
            int[] result = new int[array.Length];
            for(int i  = 0; i < array.Length; i++)
            {
                result[i] = Convert.ToInt32(array[i]);
            }
            return result;       
        }
    }
}
