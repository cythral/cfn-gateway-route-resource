using System;
using System.Linq;
using System.Reflection;

using AutoFixture.Kernel;

using Microsoft.Extensions.Options;

public class OptionsRelay : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var parameterRequest = request as ParameterInfo;
        var targetAttribute = (OptionsAttribute?)parameterRequest?.GetCustomAttribute(typeof(OptionsAttribute), true);

        if (parameterRequest == null || targetAttribute == null)
        {
            return new NoSpecimen();
        }

        var type = parameterRequest.ParameterType.GenericTypeArguments.ElementAt(0);
        var instance = Activator.CreateInstance(type);

        var createMethod = typeof(Options).GetMethod("Create", BindingFlags.Static | BindingFlags.Public);
        createMethod = createMethod!.MakeGenericMethod(type);
        return createMethod.Invoke(null, new object[] { instance! })!;
    }
}
