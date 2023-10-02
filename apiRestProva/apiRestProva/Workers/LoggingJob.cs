using Quartz;


namespace apiRestProva.Workers
{
    [DisallowConcurrentExecution]
    public class LoggingJob : IJob
    {
        private readonly ILogger<LoggingJob> logger;
        private int counter;
        public LoggingJob(ILogger<LoggingJob> logger)
        {
            this.logger = logger;
           
        }
        public Task Execute(IJobExecutionContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                logger.LogInformation("ciao {DateTime}", DateTime.Now);
                Thread.Sleep(11000);
                logger.LogInformation("sono un worker {DateTime} ", DateTime.Now);
                //Thread.Sleep(2000);
            }
            
            return Task.CompletedTask;
        }
    }
}
