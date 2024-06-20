using Newtonsoft.Json;
using ServicesExample.Attributes;
using ServicesExample.Helper;
using System.Reflection;

namespace ServicesExample;

public class Host
{
    public Service Services { get; set; }
    private readonly List<Type> Controllers;
    public Host()
    {
        Services = new Service();
        Controllers = new List<Type>();
        var type = typeof(Host);

        string ControllersNameSpace = type.Namespace + ".Controllers";

        foreach (Type item in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (item.Namespace == ControllersNameSpace && item.GetCustomAttribute(typeof(Controller)) != null)
            {
                Controllers.Add(item);
            }
        }
    }

    public object Request(string Params)
    {
        string[] parts = Params.Split('/');
        string ControllerName = parts[0];
        string ActionName = parts[1];
        string[] Parameters = parts.Skip(2).ToArray();


        Type controllerType = Controllers.FirstOrDefault(t => t.Name == ControllerName + "Controller");
        if (controllerType != null)
        {
            object controllerInstance = Activator.CreateInstance(controllerType);

            if (controllerType.GetMethods().Where(m => m.GetCustomAttribute<Method>()?.MethodName == ActionName).ToList().Count > 1)
            {
                throw new Exception("Aynı attribute'e sahip iki method bulunamaz!");
            }

            MethodInfo actionMethod = controllerType.GetMethods().FirstOrDefault(m => m.GetCustomAttribute<Method>()?.MethodName == ActionName);


            if (actionMethod != null)
            {
                object[] methodParameters = new object[Parameters.Length];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    ParameterInfo info = actionMethod.GetParameters()[i];
                    if (info.ParameterType.IsClass)
                    {
                        methodParameters[i] = JsonConvert.DeserializeObject(Parameters[i].ToString(), info.ParameterType);
                    }
                    else
                    {
                        methodParameters[i] = Convert.ChangeType(Parameters[i], actionMethod.GetParameters()[i].ParameterType);
                    }
                }

                object result = actionMethod.Invoke(controllerInstance, methodParameters);
                return JsonConvert.SerializeObject(result);
            }
        }
        else
        {
        }
        return "Invalid request";
    }
}
