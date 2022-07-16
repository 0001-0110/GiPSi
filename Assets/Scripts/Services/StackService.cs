using System.Collections.Generic;

namespace Services
{
    internal static class StackService
    {
        public static Stack<T> ReverseStack<T>(Stack<T> stack)
        {
            Stack<T> reversed = new Stack<T>();
            foreach (T item in stack)
                reversed.Push(item);
            return reversed;
        }
    }
}
