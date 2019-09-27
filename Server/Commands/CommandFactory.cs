using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ChatServer
{
    public class CommandFactory
    {
        public ICommand Build (ServiceProvider services, string command) 
        {
            List<Type> commands = GetImplementations();
            
            string c = command.Split(' ')[0];

            foreach (Type co in commands) 
            {
                var property = co.GetProperty("command");
                Object o = co.GetProperty("parameter") == null ? 
                    Activator.CreateInstance(co, new object[] { services }) : 
                    Activator.CreateInstance(co, new object[] { services, command }); 

                var value = property.GetValue(o, null);
                
                if ((string)value == c) 
                {
                    return (ICommand)o;
                }
            }

            return new NoCommand(services);
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