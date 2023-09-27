using Autofac;

namespace ToDoAppBackend;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<FileSaver>()
            .As<IDataSaver>()
            .SingleInstance();
    }
}