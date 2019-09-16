using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChatServer
{
    public class CommandFactory
    {
        public ICommand Build (string command) 
        {
            List<Type> commands = GetImplementations();
            
            string c = command.Split(' ')[0];

            foreach (Type co in commands) 
            {
                var property = co.GetProperty("command");
                Object o = co.GetProperty("_message") == null ? 
                    Activator.CreateInstance(co) : 
                    Activator.CreateInstance(co, new object[] { command }); 

                var value = property.GetValue(o, null);
                
                if ((string)value == c) 
                {
                    return (ICommand)o;
                }
            }

            return new NoCommand();
        }

        private List<Type> GetImplementations () 
        {
            Type type = typeof(ICommand);
            List<Type> types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .ToList();

            return types;
        }
    }
}