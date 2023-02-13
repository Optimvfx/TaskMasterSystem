namespace UserAuthenticationSystemV2.Systems.Base
{
    public interface ISingleton<T>
    {
        T GetSingletonValue();
    }
}