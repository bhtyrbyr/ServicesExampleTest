namespace ServicesExample.Helper;

public partial class Service
{
    private readonly Dictionary<Type, Type> _services = new Dictionary<Type, Type>();

    public void AddServices<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _services.Add(typeof(TInterface), typeof(TImplementation));
    }

    public TInterface GetService<TInterface>() where TInterface : class
    {
        var implementationType = _services[typeof(TInterface)];
        var constructorParams = GetConstructorParams(implementationType);
        return (TInterface)Activator.CreateInstance(implementationType, constructorParams);
    }

    private object[] GetConstructorParams(Type type)
    {
        var constructor = type.GetConstructors().First();
        var paramsInfo = constructor.GetParameters();
        var paramsArray = new object[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++)
        {
            var paramType = paramsInfo[i].ParameterType;

            if (paramType.IsPrimitive || paramType.IsValueType || paramType == typeof(string))
            {
                paramsArray[i] = GetDefaultValue(paramType);
            }
            else
            {
                paramsArray[i] = CreateInstance(paramType);
            }
        }

        return paramsArray;
    }

    private object GetDefaultValue(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        else
        {
            return null;
        }
    }

    private object CreateInstance(Type type)
    {
        var constructor = type.GetConstructors().First();
        var paramsInfo = constructor.GetParameters();
        var paramsArray = new object[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++)
        {
            var paramType = paramsInfo[i].ParameterType;
            paramsArray[i] = CreateInstance(paramType);
        }

        return Activator.CreateInstance(type, paramsArray);
    }
}