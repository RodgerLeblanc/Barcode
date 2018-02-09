using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App2.Helpers
{
    /// <summary>
    /// CommandHelper
    /// </summary>
    public static class CommandHelper
    {
        /// <summary>
        /// Create an async command.
        /// </summary>
        /// <param name="execute"></param>
        public static ICommand CreateAsyncCommand(Func<object, Task> execute)
        {
            return new Command(async (o) => await execute(o));
        }

        /// <summary>
        /// Create an async command.
        /// </summary>
        /// <param name="execute"></param>
        public static ICommand CreateAsyncCommand(Func<Task> execute)
        {
            return new Command(async () => await execute());
        }

        /// <summary>
        /// Create an async command.
        /// </summary>
        /// <param name="execute"></param>
        public static ICommand CreateAsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            return new Command(async (o) => await execute(o), canExecute);
        }

        /// <summary>
        /// Create an async command.
        /// </summary>
        /// <param name="execute"></param>
        public static ICommand CreateAsyncCommand(Func<Task> execute, Func<object, bool> canExecute)
        {
            return new Command(async (o) => await execute(), canExecute);
        }
    }
}
