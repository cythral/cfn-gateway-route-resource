using System.Linq;

using NSubstitute;

internal class TestUtils
{
    public static TArg GetArg<TArg>(object target, string methodName, int arg)
    {
        return (from call in target.ReceivedCalls()
                let methodInfo = call.GetMethodInfo()
                where methodInfo.Name == methodName
                select (TArg)call.GetArguments()[arg]).First();
    }
}
