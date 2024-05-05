using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        Global.ACCOUNT_SID = configuration.GetSection("TWILLO_CONFIGURATION")["ACCOUNT_SID"];
        Global.AUTH_TOCKEN = configuration.GetSection("TWILLO_CONFIGURATION")["AUTH_TOCKEN"];
        Global.PHONE_NUMBER = configuration.GetSection("TWILLO_CONFIGURATION")["PHONE_NUMBER"];
        string intervaloEnvio = configuration.GetSection("TWILLO_CONFIGURATION")["INTERVALO_ENVIO"];
        try
        {
            Global.INTERVALO_ENVIO = Convert.ToInt32(intervaloEnvio);
        }
        catch { }

        Global.CONNECTION_STRING = configuration.GetConnectionString("MongoDBConnection");
        Global.DATA_BASE_NAME = configuration.GetConnectionString("databaseName");
        Global.COLLECTION_NAME = configuration.GetConnectionString("collectionName");
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
